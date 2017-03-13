using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class ListaTricel : EntidadBase
    {
        public int TriId { get; set; }
        public int UsuId { get; set; }
        public int InstId { get; set; }
        public int RptId { get; set; }
        public string Rol { get; set; }
        public int Eliminado { get; set; }
        public string Nombre { get; set; }
        public string Objetivo { get; set; }
        public string Descripcion { get; set; }
        public string Beneficios {get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
    }
}
