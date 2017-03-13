using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class ElementosGrupo: EntidadBase
    {
        public int IdGrupo { get; set; }
        public string HrefItem { get; set; }
        public string ClassItem { get; set; }
        public string Nombre { get; set; }
        public string RolesId { get; set; }
    }
}
