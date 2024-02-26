using ACME.Helper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ACME.Models;
public class EncuestaDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


	[ForeignKey("IdUsuario")]
	public UsuarioDto? Usuario { get; set; }
	public int IdUsuario { get; set; }


	[Required(ErrorMessage = Constants.CampoRequerido)]
	[StringLength(255)]
    public string Nombre { get; set; } = string.Empty;


	[Required(ErrorMessage = Constants.CampoRequerido)]
	[StringLength(255)]
    public string Descripcion { get; set; } = string.Empty;


	[StringLength(255)]
	public string Link { get; set; } = string.Empty;


	public DateTime FechaCreación { get; set; } = Constants.DateNow();


	[NotMapped]
	public string tipoManto { get; set; } = Constants.Agregar;
}

public class EncuestasDto
{
	public EncuestasDto()
	{
		Encuestas = new List<EncuestaDto>();
	}

	public List<EncuestaDto> Encuestas { get; set; }
}
