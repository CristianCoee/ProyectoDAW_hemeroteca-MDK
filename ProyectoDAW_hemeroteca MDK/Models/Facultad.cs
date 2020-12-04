using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProyectoDAW_hemeroteca_MDK.Models
{
    public partial class Facultad
    {
        public Facultad()
        {
            Carreras = new HashSet<Carrera>();
        }

        [Key]
        public int IdFacultad { get; set; }
        [Required(ErrorMessage = "EL NOMBRE DE LA FACULTAD ES REQUERIDO")]
        public string Nombre { get; set; }

        public virtual ICollection<Carrera> Carreras { get; set; }
    }
}
