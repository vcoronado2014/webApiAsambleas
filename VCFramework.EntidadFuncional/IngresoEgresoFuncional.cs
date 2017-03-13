using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.EntidadFuniconal
{
    public class IngresoEgresoFuncional: Entidad.IngresoEgreso
    {
        public string TipoMovimientoString { get; set; }

        public DateTime FechaMovimientoDate { get; set; }

        public string Icon { get; set; }

        public string NombreDocumento { get; set; }

    }
}
