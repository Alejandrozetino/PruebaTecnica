using ACME.DBContext;
using ACME.Helper;
using ACME.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ACME.Entities;

namespace ACME.Services;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AcmeDbContext _context;
    public UsuarioRepository(AcmeDbContext context)
    {
        _context = context;
    }

	public async Task<Usuario> GetUserAsync(string nombreUsuario)
	{
		return await _context.Usuarios.Where(x => x.NombreUsuario == nombreUsuario)
			.FirstOrDefaultAsync();
	}

	public async Task<RecordList<Usuario>> ListUsersAsync()
	{
		var recordList = new RecordList<Usuario>();

		var usuarios = await _context.Usuarios
			.OrderBy(order => order.NombreUsuario)
			.ToListAsync();

		recordList.Data.AddRange(usuarios);

		return recordList;
	}

	public void AddUser(Usuario usuario)
	{
		_context.Usuarios.Add(usuario);
	}

	public void UpdateUser(Usuario usuario)
	{
		_context.Usuarios.Update(usuario);
	}

	public void DeleteUser(Usuario usuario)
	{
		_context.Usuarios.Remove(usuario);
	}

	public async Task<bool> SaveChangesAsync()
	{
		return (await _context.SaveChangesAsync() >= 0);
	}
}
