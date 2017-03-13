using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Curso : EntidadBase
    {
        public int InstId { get; set; }
        public string Nombre { get; set; }
        public int IdUsuResponsable { get; set; }
        public int Eliminado { get; set; }
        public int Orden { get; set; }
        public int Tipo { get; set; }
        public string Grupo { get; set; }
        public string SubGrupo { get; set; }

    }
}
