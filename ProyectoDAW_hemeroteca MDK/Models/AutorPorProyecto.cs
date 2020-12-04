using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProyectoDAW_hemeroteca_MDK.Models
{
    public partial class AutorPorProyecto
    {
        [Key]
        public int IdAutorProyecto { get; set; }
        public int? IdProyecto { get; set; }
        public int? IdAutor { get; set; }


        public virtual Autor IdAutorNavigation { get; set; }
        public virtual Proyecto IdProyectoNavigation { get; set; }
    }
}
