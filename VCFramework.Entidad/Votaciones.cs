using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Votaciones:EntidadBase
    {
        public int ProId { get; set; }
        public int InstId { get; set; }
        public int UsuIdVotador { get; set; }
        public DateTime FechaVotacion { get; set; }
        public int Valor { get; set; }
        public int Eliminado { get; set; }
    }
}
