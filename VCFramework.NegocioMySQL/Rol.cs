using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Rol
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        static ObjectCache cache = MemoryCache.Default;
        private static List<VCFramework.Entidad.Rol> fileContents = cache["fileContentsRol"] as List<VCFramework.Entidad.Rol>;
        //este esta en días
        private static DateTimeOffset tiempoCache = Cache.Temporal();
        private static string nombreArchivo = "cacheRol.txt";

        public static List<VCFramework.Entidad.Rol> ListarRoles()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<VCFramework.Entidad.Rol> lista2 = new List<VCFramework.Entidad.Rol>();

            if (fileContents == null)
            {

                List<object> lista = fac.Leer<VCFramework.Entidad.Rol>(setCnsWebLun);
                if (lista != null)
                {
                    lista2 = lista.Cast<VCFramework.Entidad.Rol>().ToList();
                }
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = tiempoCache;

                List<string> filePaths = new List<string>();
                string cacheFilePath = AppDomain.CurrentDomain.BaseDirectory + nombreArchivo;

                filePaths.Add(cacheFilePath);

                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                fileContents = lista2;

                cache.Set("fileContentsRol", fileContents, policy);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.Rol>().ToList();
            }
            return lista2;
        }
        public static VCFramework.Entidad.Rol ObtenerRolDelUsuario(int idRol)
        {
            VCFramework.Entidad.Rol retorno = new Entidad.Rol();

            List<VCFramework.Entidad.Rol> lista = ListarRoles();

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Find(p => p.Id == idRol && p.Eliminado == 0);
            }

            return retorno;
        }
        public static List<VCFramework.Entidad.Rol> ListarRoles(string rolUsuariologueado)
        {
            if (rolUsuariologueado != "Super Administrador")
                return ListarRoles().FindAll(p => p.Nombre != "Super Administrador");
            else
                return ListarRoles();
        }

    }

}
