using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    [Serializable]
    public class ArchivosProyecto :EntidadBase
    {
        public string RutaArchivo { get; set; }
        public int ProId { get; set; }
        public int Eliminado { get; set; }
    }
}
