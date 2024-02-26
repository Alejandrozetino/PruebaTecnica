using Microsoft.EntityFrameworkCore;
using ACME.Entities;

namespace ACME.DBContext;
public class AcmeDbContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Encuesta> Encuestas { get; set; }
    public DbSet<CampoEncuesta> CamposEncuestas { get; set; }
	public DbSet<ResultadoEncuesta> ResultadosEncuestas { get; set; }

	public AcmeDbContext(DbContextOptions<AcmeDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         base.OnModelCreating(modelBuilder);
    }
}
