using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class UsuarioLista : EntidadBase
    {
        public int UsuId { get; set; }
        public int Eliminado { get; set; }
        public int LtrId { get; set; }

        public string Rol { get; set; }

    }
}
