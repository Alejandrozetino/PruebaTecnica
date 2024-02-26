using ACME.Entities;
using ACME.Helper;
using ACME.Models;
using ACME.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebAcme.Models;

namespace WebAcme.Controllers;

public class HomeController : Controller
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public HomeController(IUsuarioRepository usuarioRepository, IMapper mapper)
    {
        _usuarioRepository = usuarioRepository ??
            throw new ArgumentNullException(nameof(IUsuarioRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
        ;
    }

    public IActionResult Login()
	{
		return View();
	}

    public IActionResult CreateAcount()
    {
        return View();
    }

	public IActionResult Index()
    {

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	[HttpPost]
	public async Task<ActionResult> IniciarSesion(UsuarioDto usuario)
	{
        // Validación de errores o campos obligatorios.
        if (string.IsNullOrEmpty(usuario.NombreUsuario))
        {
            ModelState.AddModelError("Error", "Debe ingresar su nombre de usuario");
            return View("Login", usuario);
        }

        if (string.IsNullOrEmpty(usuario.Contraseña))
        {
            ModelState.AddModelError("Error", "Debe ingresar su contraseña");
            return View("Login", usuario);
        }

        //Validar si existe un usuario con ese nombre
        var usuarioAutenticado = await _usuarioRepository.GetUserAsync(usuario.NombreUsuario);

        if(usuarioAutenticado == null)
        {
            ModelState.AddModelError("Error", "El Nombre de usuario es incorrecto, por favor ingrese un nombre de usuario válido");
            return View("Login", usuario);
        }

        // Valida la contraseña ingresada contra la contraseña guardada en la BD
        if(!EncryptionPass.VerifyPassword(usuarioAutenticado.Contraseña, usuario.Contraseña))
        {
            ModelState.AddModelError("Error", "La Contraseña es incorrecta, por favor ingrese una contraseña válida");
            return View("Login", usuario);
        }

        //Guarda de forma global el ID del usuario autenticado
        Constants.IdUsuarioAutenticado = usuarioAutenticado.Id;


        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuarioAutenticado.NombreUsuario),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<ActionResult> GrabarUsuario(UsuarioDto usuarioDto)
    {
        if (!ModelState.IsValid) return View("CreateAcount", usuarioDto);

        // Valida si ya existe un usuario con ese nombre
        var existingUser = await _usuarioRepository.GetUserAsync(usuarioDto.NombreUsuario);

        if (existingUser != null)
        {
            ModelState.AddModelError("Error", "El nombre de usuario ya no está disponible, por favor intente con otro nombre");
            return View("CreateAcount", usuarioDto);
        }

        usuarioDto.FechaRegistro = Constants.DateNow();
        
        // Encripta la contraseña ingresada
        string encryptedPassword = EncryptionPass.HashPassword(usuarioDto.Contraseña);
        usuarioDto.Contraseña = encryptedPassword;

        var usuario = _mapper.Map<Usuario>(usuarioDto);

        _usuarioRepository.AddUser(usuario);
        await _usuarioRepository.SaveChangesAsync();

        Constants.IdUsuarioAutenticado = usuario.Id;
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuarioDto.NombreUsuario),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction(nameof(Index));
    }
}