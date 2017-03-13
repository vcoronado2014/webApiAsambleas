using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Institucion
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        static ObjectCache cache = MemoryCache.Default;
        private static List<VCFramework.Entidad.Institucion> fileContents = cache["fileContentsInst"] as List<VCFramework.Entidad.Institucion>;
        private static DateTimeOffset tiempoCache = Cache.Fuerte();
        private static string nombreArchivo = "cacheInstitucion.txt";

        public static List<VCFramework.Entidad.Institucion> ListarInstituciones()
        {
            
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<VCFramework.Entidad.Institucion> lista2 = new List<VCFramework.Entidad.Institucion>();
            if (fileContents == null)
            {
                List<object> lista = fac.Leer<VCFramework.Entidad.Institucion>(setCnsWebLun);

                if (lista != null)
                {
                    lista2 = lista.Cast<VCFramework.Entidad.Institucion>().ToList();
                }

                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = tiempoCache;

                List<string> filePaths = new List<string>();
                string cacheFilePath = AppDomain.CurrentDomain.BaseDirectory + nombreArchivo;

                filePaths.Add(cacheFilePath);

                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                fileContents = lista2;

                cache.Set("fileContentsInst", fileContents, policy);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.Institucion>().ToList();
            }
            return lista2;
        }
        public static VCFramework.Entidad.Institucion ObtenerInstitucionPorId(int id)
        {
            VCFramework.Entidad.Institucion retorno = new Entidad.Institucion();

            List<VCFramework.Entidad.Institucion> lista = ListarInstituciones();

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Find(p => p.Id == id && p.Eliminado == 0);
            }

            return retorno;
        }
        public static VCFramework.Entidad.Institucion ObtenerInstitucionPorIdSinCache(int id)
        {
            VCFramework.Entidad.Institucion retorno = new Entidad.Institucion();

            List<VCFramework.Entidad.Institucion> lista = ListarInstitucionesSinCache();

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Find(p => p.Id == id && p.Eliminado == 0);
            }

            return retorno;
        }
        public static List<VCFramework.Entidad.Institucion> ListarInstitucionesSinCache()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ELIMINADO";
            filtro.TipoDato = TipoDatoGeneral.Entero;
            filtro.Valor = "0";
            List<object> lista = fac.Leer<VCFramework.Entidad.Institucion>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Institucion> lista2 = new List<VCFramework.Entidad.Institucion>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Institucion>().ToList();
            }
            if (lista2 != null && lista2.Count > 0)
            {
                foreach(Entidad.Institucion inti in lista2)
                {
                    inti.Url = "crearModificarInstitucion.html?id=" + inti.Id.ToString() + "&ELIMINADO=0";
                    inti.UrlEliminar = "crearModificarInstitucion.html?id=" + inti.Id.ToString() + "&ELIMINADO=1";
                }
            }
            return lista2;
        }
        public static int Modificar(VCFramework.Entidad.Institucion aus)
        {
            aus.Nuevo = false;
            aus.Borrado = false;
            aus.Modificado = true;

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Update<VCFramework.Entidad.Institucion>(aus, setCnsWebLun);
        }
        public static int Insertar(VCFramework.Entidad.Institucion aus)
        {
            aus.Nuevo = true;
            aus.Borrado = false;
            aus.Modificado = false;

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Insertar<VCFramework.Entidad.Institucion>(aus, setCnsWebLun);
        }

    }
}
