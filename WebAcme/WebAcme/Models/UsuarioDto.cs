using ACME.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ACME.Models;
public class UsuarioDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


	[Required(ErrorMessage = Constants.CampoRequerido)]
	[StringLength(255)]
    public string NombreUsuario { get; set; } = string.Empty;


	[StringLength(255)]
    [Required(ErrorMessage = Constants.CampoRequerido)]
    public string CorreoElectronico { get; set; } = string.Empty;


	[Required(ErrorMessage = Constants.CampoRequerido)]
	[StringLength(255)]
    public string Contraseña { get; set; } = string.Empty;


    public DateTime FechaRegistro { get; set; }
}

public class UsuariosDto
{
	public UsuariosDto()
	{
		Usuarios = new List<UsuarioDto>();
	}

	public List<UsuarioDto> Usuarios { get; set; }
}
