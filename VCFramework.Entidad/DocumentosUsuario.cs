using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class DocumentosUsuario : EntidadBase
    {
        public int UsuId { get; set; }
        public int InstId { get; set; }
        public string NombreArchivo { get; set; }
        public string Url { get; set; }
        public int Tamano { get; set; }
        public string FechaSubida { get; set; }
        public string Extension { get; set; }
        public int Eliminado { get; set; }
    }
}
