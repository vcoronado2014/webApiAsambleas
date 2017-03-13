using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Articulo : EntidadBase
    {
        public int Visible { get; set; }
        public int UsaImagen { get; set; }
        public string UrlImagen { get; set; }
        public string Fecha { get; set; }
        public int UsaFecha { get; set; }
        public int UsaTitulo { get; set; }

        public string Titulo { get; set; }

        public string Contenido { get; set; }

        public int Eliminado { get; set; }

        public int InstId { get; set; }

        public int TipoArticulo { get; set; }
    }
}
