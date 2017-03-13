using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class IngresoEgreso: EntidadBase
    {
        public int InstId { get; set; }
        public int UsuId { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int TipoMovimiento { get; set; }
        public string NumeroComprobante { get; set; }
        public string Detalle { get; set; }
        public int Monto { get; set; }
        public string UrlDocumento { get; set; }
        public int Eliminado { get; set; }
    }
}
