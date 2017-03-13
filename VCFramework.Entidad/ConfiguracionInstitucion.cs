using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class ConfiguracionInstitucion: EntidadBase
    {
        public int InstId { get; set; }
        public int EnviaDocumentos { get; set; }
        public int EnviaProyectos { get; set; }
        public int EnviaRendiciones { get; set; }
        public int EnviaCorreoEventos { get; set; }
        public int Eliminado { get; set; }
    }
}
