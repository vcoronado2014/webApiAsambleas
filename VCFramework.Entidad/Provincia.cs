using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Provincia: EntidadBase
    {
        public int RegId { get; set; }
        public string Nombre { get; set; }
        public int NumeroComunas { get; set; }

    }
}
