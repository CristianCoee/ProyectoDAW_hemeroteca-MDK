using ProyectoDAW_hemeroteca_MDK.Models;
using System.Linq;

namespace ProyectoDAW_hemeroteca_MDK.Data
{
    public class DBInitializer
    {
        public static void Initialize(ProyectoDAW_hemeroteca_MDKContext context)
        {
            context.Database.EnsureCreated();

            //Buscar si existen registros de facultad
            if (context.Facultad.Any())
            {
                return;
            }
            var facultad = new Facultad[]
            {
                new Facultad {IdFacultad=1,Nombre="Ciencias empresariales" },
                new Facultad{IdFacultad=2, Nombre="Idiomas"}
            };
            foreach (Facultad c in facultad)
            {
                context.Facultad.Add(c);
            }
            context.SaveChanges();


            //Buscar si existen registros de carrera
            if (context.Carrera.Any())
            {
                return;
            }
            var carreras = new Carrera[]
            {
                new Carrera {IdCarrera=1, Nombre="Licenciatura en administracion de empresas", IdFacultad=1},
                new Carrera{IdCarrera=2, Nombre="Licenciatura en contaduria publica", IdFacultad=1}
            };
            foreach (Carrera c in carreras)
            {
                context.Carrera.Add(c);
            }
            context.SaveChanges();

            //Buscar si existen registros de tipo de proyecto
            if (context.Facultad.Any())
            {
                return;
            }
            var tipoProyecto = new TipoProyecto[]
            {
                new TipoProyecto {IdTipoProyecto=1, Nombre="PASANTIA" },
                new TipoProyecto{ IdTipoProyecto=2,Nombre="PROYECTO"},
                new TipoProyecto{IdTipoProyecto=3, Nombre="TESIS"}
            };
            foreach (Facultad c in facultad)
            {
                context.Facultad.Add(c);
            }
            context.SaveChanges();

            //Buscar si existen registros del autor
            if (context.Autor.Any())
            {
                return;
            }
            var autor = new Autor[]
            {
                new Autor {IdAutor=1, Nombre="Juan", Apellido="Gomez", Carnet="2018-GG-601", IdCarrera=1, Sexo="M"},
                new Autor{ IdAutor=2,Nombre="Xiomara", Apellido="Hernandez", Carnet="2018-HH-601", IdCarrera=1, Sexo="F"},
                new Autor{ IdAutor=3,Nombre="Ximena", Apellido="Godoy", Carnet="2019-GG-601", IdCarrera=1, Sexo="F"}

            };
            foreach (Autor c in autor)
            {
                context.Autor.Add(c);
            }
            context.SaveChanges();

            //Buscar si existen registros de proyecto
            if (context.Proyecto.Any())
            {
                return;
            }
            var proyecto = new Proyecto[]
            {
                new Proyecto {IdProyecto=1, IdTipoProyecto=1},
                new Proyecto{ IdProyecto=2, IdTipoProyecto=3},
                 new Proyecto{ IdProyecto=3, IdTipoProyecto=2}

            };
            foreach (Proyecto c in proyecto)
            {
                context.Proyecto.Add(c);
            }
            context.SaveChanges();

            //Buscar si existen registros de autor por proyecto
            if (context.Autor.Any())
            {
                return;
            }
            var autorPorProyecto = new AutorPorProyecto[]
            {
                new AutorPorProyecto {IdAutorProyecto=1, IdProyecto=1, IdAutor=1},
                new AutorPorProyecto{ IdAutorProyecto=2, IdProyecto=1, IdAutor=2},
                 new AutorPorProyecto{ IdAutorProyecto=3, IdProyecto=2, IdAutor=3}

            };
            foreach (AutorPorProyecto c in autorPorProyecto)
            {
                context.AutorPorProyecto.Add(c);
            }
            context.SaveChanges();
        }
    }
}
