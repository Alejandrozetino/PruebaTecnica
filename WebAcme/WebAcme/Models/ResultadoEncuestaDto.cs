using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ACME.Models;
public class ResultadoEncuestaDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


	[ForeignKey("IdEncuesta")]
	public EncuestaDto? Encuesta { get; set; }
	public int IdEncuesta { get; set; }


	[ForeignKey("IdCamposEncuesta")]
	public CampoEncuestaDto? campoEncuesta { get; set; }
	public int IdCamposEncuesta { get; set; }


    public string? Respuesta { get; set; }
}

public class ResultadosEncuestasDto
{
	public ResultadosEncuestasDto()
	{
		ResultadosEncuestas = new List<ResultadoEncuestaDto>();
	}

	public List<ResultadoEncuestaDto> ResultadosEncuestas { get; set; }
}
