using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VCFramework.EntidadFuncional
{
    public class UsuarioFuncional
    {
        public VCFramework.Entidad.AutentificacionUsuario AutentificacionUsuario { get; set; }
        public VCFramework.Entidad.Rol Rol { get; set; }

        public VCFramework.Entidad.Institucion Institucion { get; set; }

        public VCFramework.Entidad.Persona Persona { get; set; }

        public VCFramework.Entidad.Region Region { get; set; }

        public VCFramework.Entidad.Comuna Comuna { get; set; }

    }
    public class UsuarioEnvoltorio
    {
        public int Id { get; set; }

        public string NombreUsuario { get; set; }

        public string NombreCompleto { get; set; }
        public string Rol { get; set; }

        public string Url { get; set; }
        public string UrlEliminar { get; set; }

        public String OtroUno { get; set; }

        public String OtroDos { get; set; }

        public String OtroTres { get; set; }

        public String OtroCuatro { get; set; }

        public String OtroCinco { get; set; }

        public String UrlDocumento { get; set; }
    }
    public class NotificacionRetorno
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public string Nombre { get; set; }
        public string Fecha { get; set; }

        public string Detalle { get; set; }

        public string Url { get; set; }
    }


    public class proposalss
    {
        public List<UsuarioEnvoltorio> proposals { get; set; }
    }
}
