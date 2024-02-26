using ACME.Helper;
using ACME.Entities;

namespace ACME.Services.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario> GetUserAsync(string nombreUsuario);
    Task<RecordList<Usuario>> ListUsersAsync();
    void AddUser(Usuario usuario);
    void UpdateUser(Usuario usuario);
    void DeleteUser(Usuario usuario);

    Task<bool> SaveChangesAsync();
}
