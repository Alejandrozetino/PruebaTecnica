using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ACME.Entities;
public class Encuesta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


	[ForeignKey("IdUsuario")]
	public Usuario? Usuario { get; set; }
	public int IdUsuario { get; set; }


	[StringLength(255)]
    public string Nombre { get; set; } = string.Empty;


	[StringLength(255)]
    public string Descripcion { get; set; } = string.Empty;


	[StringLength(255)]
	public string Link { get; set; } = string.Empty;


	public DateTime FechaCreación { get; set; }
}
