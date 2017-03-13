using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.EntidadFuniconal
{
    public class TricelFuncional : Entidad.Tricel
    {
        public int CantidadListas { get; set; }
        public bool MostrarIconoCrear { get; set; }

        public bool MostrarIconoEliminar { get; set; }
    }
}
