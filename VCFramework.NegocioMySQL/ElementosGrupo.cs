using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class ElementosGrupo
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        static ObjectCache cache = MemoryCache.Default;
        private static List<VCFramework.Entidad.ElementosGrupo> fileContents = cache["fileContentsElementosGrupo"] as List<VCFramework.Entidad.ElementosGrupo>;
        //este esta en días
        private static DateTimeOffset tiempoCache = Cache.Temporal();
        private static string nombreArchivo = "cacheElementosGrupo.txt";
        public static List<VCFramework.Entidad.ElementosGrupo> ObtenerElementosGrupo(int idGrupo)
        {
            //VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            //FiltroGenerico filtro = new FiltroGenerico();
            //filtro.Campo = "ID_GRUPO";
            //filtro.Valor = idGrupo.ToString();
            //filtro.TipoDato = TipoDatoGeneral.Entero;

            //List<object> lista = fac.Leer<VCFramework.Entidad.ElementosGrupo>(filtro);
            //List<VCFramework.Entidad.ElementosGrupo> lista2 = new List<VCFramework.Entidad.ElementosGrupo>();
            //if (lista != null)
            //{

            //    lista2 = lista.Cast<VCFramework.Entidad.ElementosGrupo>().ToList();
            //}

            //return lista2;
            List<Entidad.ElementosGrupo> lista = ObtenerElementosGrupoAll();
            List<Entidad.ElementosGrupo> listaDevolver = new List<Entidad.ElementosGrupo>();
            try
            {
                listaDevolver = lista.FindAll(p => p.IdGrupo == idGrupo);
            }
            catch(Exception ex)
            {
                Utiles.Log(ex);
            }
            return listaDevolver;
        }
        public static List<VCFramework.Entidad.ElementosGrupo> ObtenerElementosGrupoAll()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<VCFramework.Entidad.ElementosGrupo> lista2 = new List<VCFramework.Entidad.ElementosGrupo>();

            if (fileContents == null)
            {
                List<object> lista = fac.Leer<VCFramework.Entidad.ElementosGrupo>(setCnsWebLun);

                if (lista != null)
                {

                    lista2 = lista.Cast<VCFramework.Entidad.ElementosGrupo>().ToList();
                }
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = tiempoCache;

                List<string> filePaths = new List<string>();
                string cacheFilePath = AppDomain.CurrentDomain.BaseDirectory + nombreArchivo;

                filePaths.Add(cacheFilePath);

                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                fileContents = lista2;

                cache.Set("fileContentsElementosGrupo", fileContents, policy);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.ElementosGrupo>().ToList();
            }
            return lista2;
        }
    }
}
