using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.EntidadFuniconal
{
    public class ProyectosFuncional : Entidad.Proyectos
    {
        public string Clase { get; set; }
        public string UrlVotar { get; set; }

        public string CostoStr { get; set; }
        public string ClasePrecio { get; set; }

        public string ClaseTitulo { get; set; }

        public string InfoLista { get; set; }
        public int CantidadVotos { get; set; }
        public string EsMiVoto { get; set; }

    }
}
