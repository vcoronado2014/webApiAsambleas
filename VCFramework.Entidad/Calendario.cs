using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Calendario : EntidadBase
    {
        public string Titulo { get; set; }
        public string Url { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public string Detalle { get; set; }
        public int Eliminado { get; set; }

        public int InstId { get; set; }
        public string Asunto { get; set; }
        public string Ubicacion { get; set; }
        public int Etiqueta { get; set; }

        public string Descripcion { get; set; }

        public int Status { get; set; }

        public int Tipo { get; set; }
    }
}
