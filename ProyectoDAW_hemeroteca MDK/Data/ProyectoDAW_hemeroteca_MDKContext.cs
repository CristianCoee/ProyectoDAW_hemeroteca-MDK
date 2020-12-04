using Microsoft.EntityFrameworkCore;

namespace ProyectoDAW_hemeroteca_MDK.Data
{
    public class ProyectoDAW_hemeroteca_MDKContext : DbContext
    {
        public ProyectoDAW_hemeroteca_MDKContext(DbContextOptions<ProyectoDAW_hemeroteca_MDKContext> options)
            : base(options)
        {
        }

        public DbSet<ProyectoDAW_hemeroteca_MDK.Models.Carrera> Carrera { get; set; }

        public DbSet<ProyectoDAW_hemeroteca_MDK.Models.Facultad> Facultad { get; set; }

        public DbSet<ProyectoDAW_hemeroteca_MDK.Models.Autor> Autor { get; set; }

        public DbSet<ProyectoDAW_hemeroteca_MDK.Models.AutorPorProyecto> AutorPorProyecto { get; set; }

        public DbSet<ProyectoDAW_hemeroteca_MDK.Models.TipoProyecto> TipoProyecto { get; set; }

        public DbSet<ProyectoDAW_hemeroteca_MDK.Models.Proyecto> Proyecto { get; set; }
    }
}
