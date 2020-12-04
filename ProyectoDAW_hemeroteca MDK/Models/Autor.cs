using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProyectoDAW_hemeroteca_MDK.Models
{
    public partial class Autor
    {
        public Autor()
        {
            AutorPorProyectos = new HashSet<AutorPorProyecto>();
        }

        [Key]
        public int IdAutor { get; set; }
        [Required(ErrorMessage = "EL NOMBRE ES REQUERIDO")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "EL APELLIDO ES REQUERIDO")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "EL CARNET ES REQUERIDO")]
        public string Carnet { get; set; }
        [Required(ErrorMessage = "LA CARRERA ES REQUERIDO")]
        public int? IdCarrera { get; set; }
        [Required(ErrorMessage = "ELEMENTO ES REQUERIDO")]
        public string Sexo { get; set; }

        public virtual Carrera IdCarreraNavigation { get; set; }
        public virtual ICollection<AutorPorProyecto> AutorPorProyectos { get; set; }
    }
}
