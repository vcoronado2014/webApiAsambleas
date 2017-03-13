using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Region: EntidadBase
    {
        public string Nombre { get; set; }
        public string Romano { get; set; }
        public int NumeroProvincias { get; set; }
        public int NumeroComunas { get; set; }
    }
}
