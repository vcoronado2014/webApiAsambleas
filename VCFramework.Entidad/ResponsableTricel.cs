using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class ResponsableTricel : EntidadBase
    {
        public int TriId { get; set; }
        public int UsuId { get; set; }
        public int Eliminado { get; set; }

    }
}
