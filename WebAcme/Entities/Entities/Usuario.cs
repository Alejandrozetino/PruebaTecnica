using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ACME.Entities;
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }


    [StringLength(255)]
    public string NombreUsuario { get; set; } = string.Empty;


    [StringLength(255)]
    public string CorreoElectronico { get; set; } = string.Empty;


    [StringLength(255)]
    public string Contraseña { get; set; } = string.Empty;


    public DateTime FechaRegistro { get; set; }
}
