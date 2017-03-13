using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Encabezado : EntidadBase
    {
        public int UsaImagenSuperior { get; set; }
        public string UrlImagenSuperior { get; set; }
        public string TituloEncabezado { get; set; }
        public string SubtituloEncabezado { get; set; }
        public int Eliminado { get; set; }
        public int InstId { get; set; }
    }
}
