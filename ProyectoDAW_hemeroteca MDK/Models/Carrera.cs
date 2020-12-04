using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProyectoDAW_hemeroteca_MDK.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            Autors = new HashSet<Autor>();
        }
        [Key]
        public int IdCarrera { get; set; }
        [Required(ErrorMessage = "EL NOMBRE DE LA CARRERA ES REQUERIDO")]
        public string Nombre { get; set; }
        public int? IdFacultad { get; set; }

        public virtual Facultad IdFacultadNavigation { get; set; }
        public virtual ICollection<Autor> Autors { get; set; }
    }
}
