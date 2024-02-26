using ACME.Entities;
using ACME.Helper;
using ACME.Models;
using ACME.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAcme.Controllers;

public class EncuestaController : Controller
{
    private readonly IEncuestaRepository _encuestaRepository;
    private readonly IMapper _mapper;

    public EncuestaController(IEncuestaRepository encuestaRepository, IMapper mapper)
    {
        _encuestaRepository = encuestaRepository ??
            throw new ArgumentNullException(nameof(IEncuestaRepository));
        ;
        _mapper = mapper ??
            throw new ArgumentNullException(nameof(mapper))
        ;
    }

    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }

    #region -Encuesta-
    [Authorize]
    [HttpGet]
    public async Task<ActionResult> IndexEncuesta()
    {
        var encuestas = await _encuestaRepository.ListSurveyAsync();
        var encuestasDto = _mapper.Map<EncuestasDto>(encuestas);

        return View(encuestasDto);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> AgregarEncuesta()
    {
        var encuesta = new EncuestaDto();
        return View("GrabarEncuesta", encuesta);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> EditarEncuesta(int id)
    {
        var encuesta = await _encuestaRepository.GetSurveyAsync(id);

        if (encuesta == null) encuesta = new Encuesta();

        var encuestaDto = _mapper.Map<EncuestaDto>(encuesta);
        encuestaDto.tipoManto = Constants.Modificar;

        return View("GrabarEncuesta", encuestaDto);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> EliminarEncuesta(int id)
    {
        var encuesta = await _encuestaRepository.GetSurveyAsync(id);
        var encuestaDto = _mapper.Map<EncuestaDto>(encuesta);
        encuestaDto.tipoManto = Constants.Eliminar;

        return View("GrabarEncuesta", encuestaDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> GrabarEncuesta(EncuestaDto model, String button)
    {
        if (button == "Salir") return RedirectToAction("IndexEncuesta");

        if (string.IsNullOrEmpty(model.Nombre) && model.tipoManto != Constants.Eliminar)
        {
            return View("GrabarEncuesta", model);
        }
        
        if (string.IsNullOrEmpty(model.Descripcion) && model.tipoManto != Constants.Eliminar)
        {
            return View("GrabarEncuesta", model);
        }

        var encuesta = _mapper.Map<Encuesta>(model);

        switch (model.tipoManto)
        {
            case Constants.Agregar:
                encuesta.IdUsuario = Constants.IdUsuarioAutenticado;
                encuesta.Link = "https://localhost:7083/Encuesta/ResponderEncuesta?Token=";
                encuesta.Link += Guid.NewGuid().ToString();
                encuesta.FechaCreación = Constants.DateNow();
                _encuestaRepository.AddSurvey(encuesta);    
            break;

            case Constants.Modificar:
                encuesta.IdUsuario = Constants.IdUsuarioAutenticado;
                encuesta.FechaCreación = Constants.DateNow();
                _encuestaRepository.UpdateSurvey(encuesta); 
            break;

            case Constants.Eliminar:
                var respuestasEncuesta = await _encuestaRepository.ListSurveyResponseAsync(encuesta.Id);

                if(respuestasEncuesta.Data.Count() > 0)
                {
                    ModelState.AddModelError("Error", "Esta encuesta ya tiene respuestas, por lo tanto no se puede eliminar.");
                    return View("GrabarEncuesta", model);
                }

                var camposEncuesa = await _encuestaRepository.ListSurveyFieldAsync2(encuesta.Id);
                foreach(var campo in camposEncuesa)
                {
                    _encuestaRepository.DeleteSurveyField(campo);
                    await _encuestaRepository.SaveChangesAsync();
                }

                await _encuestaRepository.DeleteSurvey(encuesta); 
            break;
        }

        await _encuestaRepository.SaveChangesAsync();

        return RedirectToAction(nameof(IndexEncuesta));
    }
    #endregion

    #region -CamposEncuesta-
    [Authorize]
    [HttpGet]
    public async Task<ActionResult> IndexCamposEncuesta(int IdEncuesta)
    {
        var camposEncuesta = await _encuestaRepository.ListSurveyFieldAsync(IdEncuesta);
        var camposEncuestaDto = _mapper.Map<CamposEncuestasDto>(camposEncuesta);
        ViewBag.IdEncuesta = IdEncuesta;

        return View(camposEncuestaDto);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> AgregarCamposEncuesta(int IdEncuesta)
    {
        var campoEncuesta = new CampoEncuestaDto();
        campoEncuesta.IdEncuesta = IdEncuesta;
        campoEncuesta.TiposDeDatos = await _encuestaRepository.GetTypeData();
        return View("GrabarCamposEncuesta", campoEncuesta);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> EditarCamposEncuesta(int id)
    {
        var campoEncuesta = await _encuestaRepository.GetSurveyFieldAsync(id);

        if (campoEncuesta == null) campoEncuesta = new CampoEncuesta();

        var campoEncuestaDto = _mapper.Map<CampoEncuestaDto>(campoEncuesta);
        campoEncuestaDto.TiposDeDatos = await _encuestaRepository.GetTypeData();
        campoEncuestaDto.tipoManto = Constants.Modificar;

        return View("GrabarCamposEncuesta", campoEncuestaDto);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> EliminarCamposEncuesta(int id)
    {
        var campoEncuesta = await _encuestaRepository.GetSurveyFieldAsync(id);
        var campoEncuestaDto = _mapper.Map<CampoEncuestaDto>(campoEncuesta);

        campoEncuestaDto.TiposDeDatos = await _encuestaRepository.GetTypeData();
        campoEncuestaDto.tipoManto = Constants.Eliminar;

        return View("GrabarCamposEncuesta", campoEncuestaDto);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult> GrabarCamposEncuesta(CampoEncuestaDto model, String button)
    {
        if (button == "Salir") return RedirectToAction("IndexCamposEncuesta", new { IdEncuesta = model.IdEncuesta });

        if (!ModelState.IsValid && model.tipoManto != Constants.Eliminar)
        {
            model.TiposDeDatos = await _encuestaRepository.GetTypeData();
            return View("GrabarCamposEncuesta", model);
        }

        var camposEncuesta = _mapper.Map<CampoEncuesta>(model);

        switch (model.tipoManto)
        {
            case Constants.Agregar:
                _encuestaRepository.AddSurveyField(camposEncuesta);
                break;

            case Constants.Modificar:
                _encuestaRepository.UpdateSurveyField(camposEncuesta);
                break;

            case Constants.Eliminar: _encuestaRepository.DeleteSurveyField(camposEncuesta); break;
        }

        await _encuestaRepository.SaveChangesAsync();

        return RedirectToAction("IndexCamposEncuesta", new { IdEncuesta = model.IdEncuesta });
    }
    #endregion

    #region -RespuestasEncuestas-
    [Authorize]
    [HttpGet]
    public async Task<ActionResult> ResultadosEncuesta(int IdEncuesta)
    {
        var camposEncuesta = await _encuestaRepository.ListSurveyFieldAsync(IdEncuesta);
        var camposEncuestaDto = _mapper.Map<CamposEncuestasDto>(camposEncuesta);
        ViewBag.IdEncuesta = IdEncuesta;

        return View(camposEncuestaDto);
    }

    [HttpPost]
    public async Task<ActionResult> GrabarRespuestas(string Token)
    {
        var link = $"https://localhost:7083/Encuesta/ResponderEncuesta?Token={Token}";
        var encuesta = await _encuestaRepository.GetSurveyTokenAsync(link);
        var camposEncuesta = await _encuestaRepository.ListSurveyFieldAsync2(encuesta.Id);
        var form = Request.Form;

        foreach (var campo in camposEncuesta)
        {
            var value = form[campo.NombreCampo].ToString();

            ResultadoEncuesta respuesta = new ResultadoEncuesta
            {
                IdEncuesta = encuesta.Id,
                IdCamposEncuesta = campo.Id,
                Respuesta = value,
            };

            _encuestaRepository.AddSurveyResponse(respuesta);
        }

        await _encuestaRepository.SaveChangesAsync();

        return RedirectToAction("ResponderEncuesta", new { Token = Token });
    }

    [HttpGet]
    public async Task<ActionResult> ResponderEncuesta(string Token)
    {
        var link = $"https://localhost:7083/Encuesta/ResponderEncuesta?Token={Token}";
        ViewBag.Token = Token;
        var encuesta = await _encuestaRepository.GetSurveyTokenAsync(link);
        
        if (encuesta != null)
        {
            var camposEncuesta = await _encuestaRepository.ListSurveyFieldAsync(encuesta.Id);
            return View(camposEncuesta);
        }

        return View("Error");
    }
    #endregion

}
