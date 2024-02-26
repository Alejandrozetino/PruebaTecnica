using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ACME.Entities;
public class ResultadoEncuesta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


	[ForeignKey("IdEncuesta")]
	public Encuesta? Encuesta { get; set; }
	public int IdEncuesta { get; set; }


	[ForeignKey("IdCamposEncuesta")]
	public CampoEncuesta? CampoEncuesta { get; set; }
	public int IdCamposEncuesta { get; set; }


    public string? Respuesta { get; set; }
}
