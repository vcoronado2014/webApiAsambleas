using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.EntidadFuniconal
{
    public class ListaTricelFuncional : Entidad.ListaTricel
    {
        public int CantidadIntegrantes { get; set; }
        public bool IconoEditar { get; set; }
        public bool IconoEliminar { get; set; }
        public bool IconoCrear { get; set; }

        public Entidad.Tricel Tricel { get; set;  }

    }
}
