using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.EntidadFuniconal
{
    public class ReporteActas
    {
        public string NombreUsuarioCargador { get; set; }
        public string NombreDocumento { get; set; }
        public string FechaSubida { get; set; }
        public string TipoDocumento { get; set; }

    }
    public class ReporteRendiciones
    {
        public string NombreUsuarioCargador { get; set; }
        public string NombreDocumento { get; set; }
        public string FechaSubida { get; set; }
        public string TipoMovimiento { get; set; }
        public string NumeroComprobante { get; set; }
        public string Monto { get; set; }
    }
    public class ReporteEncabezado
    {
        public string NombreReporte { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string FuncionarioSolicitador { get; set; }
        public string Detalle { get; set; }
        public string NombreInstitucion { get; set; }
    }

    public class ReporteVotaciones : Entidad.Proyectos
    {
        public string DocumentosDelProyecto { get; set; }
        public int CantidadApoderadosInscritos { get; set; }
        public int CantidadVotosSi { get; set; }
        public int CantidadVotosNo { get; set; }
    }

}
