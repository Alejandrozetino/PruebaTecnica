using ACME.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ACME.Models;
public class CampoEncuestaDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


	[ForeignKey("IdEncuesta")]
	public EncuestaDto? Encuesta { get; set; }
	public int IdEncuesta { get; set; }


	[Required(ErrorMessage = Constants.CampoRequerido)]
	[StringLength(255)]
    public string NombreCampo { get; set; } = string.Empty;


	[Required(ErrorMessage = Constants.CampoRequerido)]
	[StringLength(255)]
    public string TituloCampo { get; set; } = string.Empty;


	public bool EsRequerido { get; set; } = true;


	[Required(ErrorMessage = Constants.CampoRequerido)]
	[StringLength(100)]
	public string TipoCampo { get; set; } = string.Empty;


	[NotMapped]
	public string tipoManto { get; set; } = Constants.Agregar;

    public List<SelectListItem>? TiposDeDatos { get; set; }
}

public class CamposEncuestasDto
{
	public CamposEncuestasDto()
	{
		CamposEncuestas = new List<CampoEncuestaDto>();
	}

	public List<CampoEncuestaDto> CamposEncuestas { get; set; }
}
