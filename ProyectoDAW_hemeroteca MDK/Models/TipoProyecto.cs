using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProyectoDAW_hemeroteca_MDK.Models
{
    public partial class TipoProyecto
    {
        public TipoProyecto()
        {
            Proyectos = new HashSet<Proyecto>();
        }

        [Key]
        public int IdTipoProyecto { get; set; }
        [Required(ErrorMessage = "EL TIPO DE PROYECTO ES REQUERIDO")]
        public string Nombre { get; set; }

        public virtual ICollection<Proyecto> Proyectos { get; set; }
    }
}
