using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class LoginUsuario
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static int Insertar(Entidad.LoginUsuario login)
        {
            Factory fac = new Factory();
            return fac.Insertar<Entidad.LoginUsuario>(login, setCnsWebLun);
        }
        public static List<VCFramework.Entidad.LoginUsuario> ObtenerLogin()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();


            List<object> lista = fac.Leer<VCFramework.Entidad.LoginUsuario>(setCnsWebLun);
            List<VCFramework.Entidad.LoginUsuario> lista2 = new List<VCFramework.Entidad.LoginUsuario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.LoginUsuario>().ToList();
            }

            return lista2;
        }
        public static List<Entidad.LoginUsuario> ObtenerPorUsuId(int usuId)
        {
            return ObtenerLogin().FindAll(p => p.UsuId == usuId);
        }

        public static List<EnvoltorioLGN> ObtenerTodo()
        {
            List<EnvoltorioLGN> lista = new List<EnvoltorioLGN>();

            //recorremos todos los usuarios
            List<Entidad.AutentificacionUsuario> usuarios = NegocioMySQL.AutentificacionUsuario.ListarUsuarios();
            if (usuarios != null && usuarios.Count > 0)
            {
                foreach(Entidad.AutentificacionUsuario us in usuarios)
                {
                    //obtenemos los datos
                    Entidad.Institucion institucion =  NegocioMySQL.Institucion.ObtenerInstitucionPorId(us.InstId);
                    Entidad.Persona persona = NegocioMySQL.Persona.ObtenerPersonaPorUsuId(us.Id);
                    Entidad.Rol rol = NegocioMySQL.Rol.ObtenerRolDelUsuario(us.RolId);
                    List<Entidad.LoginUsuario> logins = ObtenerPorUsuId(us.Id);

                    EnvoltorioLGN envoltorio = new EnvoltorioLGN();
                    if (institucion != null && institucion.Id > 0)
                        envoltorio.NombreInstitucion = institucion.Nombre;
                    else
                        envoltorio.NombreInstitucion = "No Ingresada";

                    if (persona != null && persona.Id > 0)
                    {
                        envoltorio.NombreCompleto = persona.Nombres + " " + persona.ApellidoPaterno + " " + persona.ApellidoMaterno;
                    }
                    else
                        envoltorio.NombreCompleto = "No registrado";

                    envoltorio.NombreUsuario = us.NombreUsuario;

                    int cantidad = logins.Count;
                    DateTime ultimoLogin = DateTime.MinValue;
                    if (cantidad > 0)
                        ultimoLogin = logins.Max(p => p.FechaMovimiento);

                    if (ultimoLogin != DateTime.MinValue)
                        envoltorio.UltimaFechaLogin = ultimoLogin.ToShortDateString() + " " + ultimoLogin.ToShortTimeString();
                    else
                        envoltorio.UltimaFechaLogin = "No ha ingresado";

                    if (rol != null && rol.Id > 0)
                        envoltorio.Rol = rol.Nombre;
                    else
                        envoltorio.Rol = "Sin Rol";

                    envoltorio.Cantidad = cantidad;

                    lista.Add(envoltorio);

                }
            }

            if (lista != null && lista.Count > 0)
                lista = lista.OrderByDescending(p => p.NombreInstitucion).OrderByDescending(p=>p.Cantidad).ToList();

            return lista;
        }

    }
    public class EnvoltorioLGN
    {
        public string NombreInstitucion { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public int Cantidad { get; set; }
        public string UltimaFechaLogin { get; set; }
        public string Rol { get; set; }
    }
}
