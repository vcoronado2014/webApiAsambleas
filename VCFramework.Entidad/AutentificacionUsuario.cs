﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VCFramework.Entidad
{
    public class AutentificacionUsuario : EntidadBase
    {

        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public int Eliminado { get; set; }
        public string CorreoElectronico { get; set; }

        public int EsVigente { get; set; }

        public int RolId { get; set; }

        public int InstId { get; set; }

    }
}
