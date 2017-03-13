using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class Proyectos: EntidadBase
    {
        public DateTime FechaCreacion { get; set; }
        public int UsuIdCreador { get; set; }
        public int InstId { get; set; }
        public string Nombre { get; set; }
        public string Objetivo { get; set; }
        public string Descripcion { get; set; }
        public string Beneficios { get; set; }
        public int Costo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public int EnviaCorreo { get; set; }
        public int NotificaPopup { get; set; }
        public int EsVigente { get; set; }
        public int Eliminado { get; set; }
        public int FueAprobado { get; set; }
        
    }
}
