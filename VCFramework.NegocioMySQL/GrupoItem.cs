using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class GrupoItem
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        static ObjectCache cache = MemoryCache.Default;
        private static List<VCFramework.Entidad.GrupoItem> fileContents = cache["fileContentsGrupoItem"] as List<VCFramework.Entidad.GrupoItem>;
        //este esta en días
        private static DateTimeOffset tiempoCache = Cache.Temporal();
        private static string nombreArchivo = "cacheGrupoItems.txt";

        public static List<VCFramework.Entidad.GrupoItem> ListarGrupos()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<VCFramework.Entidad.GrupoItem> lista2 = new List<VCFramework.Entidad.GrupoItem>();

            if (fileContents == null)
            {
                List<object> lista = fac.Leer<VCFramework.Entidad.GrupoItem>(setCnsWebLun);

                if (lista != null)
                {
                    lista2 = lista.Cast<VCFramework.Entidad.GrupoItem>().ToList();
                }
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = tiempoCache;

                List<string> filePaths = new List<string>();
                string cacheFilePath = AppDomain.CurrentDomain.BaseDirectory + nombreArchivo;

                filePaths.Add(cacheFilePath);

                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                fileContents = lista2;

                cache.Set("fileContentsGrupoItem", fileContents, policy);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.GrupoItem>().ToList();
            }
            return lista2;
        }
    }
}
