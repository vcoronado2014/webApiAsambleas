using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Rol : EntidadBase
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Eliminado { get; set; }

    }
}
