using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class EntidadBase
    {
        public bool Borrado { get; set; }
        public virtual int Id { get; set; }
        public bool Modificado { get; set; }
        public bool Nuevo { get; set; }
        public object TimeStamp { get; set; }

        public string Url { get; set; }

        public string UrlEliminar { get; set; }
    }
}
