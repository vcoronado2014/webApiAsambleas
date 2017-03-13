using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class ArchivosTricel : EntidadBase
    {
        public string RutaArchivo { get; set; }
        public int TriId { get; set; }
        public int Eliminado { get; set; }
    }
}
