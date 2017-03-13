using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class VinculosInstitucion : EntidadBase
    {
        public int InstId { get; set; }
        public string ImagenVinculo1 { get; set; }
        public string UrlVinculo1 { get; set; }
        public string TextoVinculo1 { get; set; }
        public int VisibleVinculo1 { get; set; }
        public string ImagenVinculo2 { get; set; }
        public string UrlVinculo2 { get; set; }
        public string TextoVinculo2 { get; set; }
        public int VisibleVinculo2 { get; set; }
        public string ImagenVinculo3 { get; set; }
        public string UrlVinculo3 { get; set; }
        public string TextoVinculo3 { get; set; }
        public int VisibleVinculo3 { get; set; }
    }
}
