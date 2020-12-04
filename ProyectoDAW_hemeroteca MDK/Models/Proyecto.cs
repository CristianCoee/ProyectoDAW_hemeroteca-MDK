using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProyectoDAW_hemeroteca_MDK.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            AutorPorProyectos = new HashSet<AutorPorProyecto>();
        }

        [Key]
        public int IdProyecto { get; set; }
        [Required(ErrorMessage = "EL TIPO DE PROYECTO ES REQUERIDO")]
        public int? IdTipoProyecto { get; set; }
        //[Required(ErrorMessage = "EL NOMBRE DEL AUTOR ES REQUERIDO")]
        public int? IdAutor { get; set; }
        [Required(ErrorMessage = "EL AÑO ES REQUERIDO")]

        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? Año { get; set; }
        [Required(ErrorMessage = "EL TITULO ES REQUERIDO")]
        public string Titulo { get; set; }

        public virtual TipoProyecto IdTipoProyectoNavigation { get; set; }
        public virtual ICollection<AutorPorProyecto> AutorPorProyectos { get; set; }
    }
}
