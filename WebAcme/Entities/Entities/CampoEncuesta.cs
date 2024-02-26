using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ACME.Entities;
public class CampoEncuesta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


	[ForeignKey("IdEncuesta")]
	public Encuesta? Encuesta { get; set; }
	public int IdEncuesta { get; set; }


	[StringLength(255)]
    public string NombreCampo { get; set; } = string.Empty;


	[StringLength(255)]
    public string TituloCampo { get; set; } = string.Empty;


	public bool EsRequerido { get; set; } = true;


	[StringLength(100)]
	public string TipoCampo { get; set; } = string.Empty;
}
