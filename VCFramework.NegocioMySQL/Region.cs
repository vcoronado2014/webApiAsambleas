using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Region
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        static ObjectCache cache = MemoryCache.Default;
        private static List<VCFramework.Entidad.Region> fileContents = cache["fileContentsReg"] as List<VCFramework.Entidad.Region>;
        //este esta en días
        private static DateTimeOffset tiempoCache = Cache.Mensual();
        private static string nombreArchivo = "cacheRegion.txt";

        public static List<VCFramework.Entidad.Region> ListarRegiones()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<VCFramework.Entidad.Region> lista2 = new List<VCFramework.Entidad.Region>();

            if (fileContents == null)
            {
                List<object> lista = fac.Leer<VCFramework.Entidad.Region>(setCnsWebLun);
                Entidad.Region regInicio = new Entidad.Region();
                regInicio.Id = 0;
                regInicio.Nombre = "Seleccione";
                if (lista != null)
                {
                    lista.Insert(0, regInicio);

                    lista2 = lista.Cast<VCFramework.Entidad.Region>().ToList();
                }
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = tiempoCache;

                List<string> filePaths = new List<string>();
                string cacheFilePath = AppDomain.CurrentDomain.BaseDirectory + nombreArchivo;

                filePaths.Add(cacheFilePath);

                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                fileContents = lista2;

                cache.Set("fileContentsReg", fileContents, policy);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.Region>().ToList();
            }
            return lista2;
        }

        public static VCFramework.Entidad.Region ObtenerRegionPorId(int idRegion)
        {
            return ListarRegiones().Find(p => p.Id == idRegion);
        }
    }
}
