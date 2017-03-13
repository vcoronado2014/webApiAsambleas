using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Institucion : EntidadBase
    {
        public int Eliminado { get; set; }

        public string Nombre { get; set; }

        public int RegId { get; set; }
        public int ComId { get; set; }

        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Direccion { get; set; }
    }
}
