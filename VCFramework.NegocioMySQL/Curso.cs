using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Curso
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        static ObjectCache cache = MemoryCache.Default;
        private static List<VCFramework.Entidad.Curso> fileContents = cache["fileContentsCurso"] as List<VCFramework.Entidad.Curso>;
        //este esta en días
        private static DateTimeOffset tiempoCache = Cache.Temporal();
        private static string nombreArchivo = "cacheCurso.txt";

        public static List<VCFramework.Entidad.Curso> ListarCursos()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<VCFramework.Entidad.Curso> lista2 = new List<VCFramework.Entidad.Curso>();

            if (fileContents == null)
            {
                List<object> lista = fac.Leer<VCFramework.Entidad.Curso>(setCnsWebLun);

                if (lista != null)
                {
                    lista2 = lista.Cast<VCFramework.Entidad.Curso>().ToList();
                }
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = tiempoCache;

                List<string> filePaths = new List<string>();
                string cacheFilePath = AppDomain.CurrentDomain.BaseDirectory + nombreArchivo;

                filePaths.Add(cacheFilePath);

                policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePaths));

                fileContents = lista2;

                cache.Set("fileContentsCurso", fileContents, policy);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.Curso>().ToList();
            }
            return lista2;
        }

        public static List<VCFramework.Entidad.Curso> ListarCursosPorInstId(int instId)
        {
            List<VCFramework.Entidad.Curso> lista2 = new List<VCFramework.Entidad.Curso>();
            if (fileContents == null)
            {
                VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
                FiltroGenerico filtro = new FiltroGenerico();
                filtro.Campo = "INST_ID";
                filtro.Valor = instId.ToString();
                filtro.TipoDato = TipoDatoGeneral.Entero;

                List<object> lista = fac.Leer<VCFramework.Entidad.Curso>(filtro, setCnsWebLun);

                if (lista != null)
                {

                    lista2 = lista.Cast<VCFramework.Entidad.Curso>().ToList().OrderBy(p=>p.Orden).ToList();
                }
                if (lista2 != null)
                    lista2 = lista2.FindAll(p => p.Eliminado == 0);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.Curso>().ToList().FindAll(p=>p.InstId == instId).OrderBy(p=>p.Orden).ToList();
            }
            return lista2;
        }
        public static VCFramework.Entidad.Curso ObtenerCursoPorNombre(int instId, string nombre)
        {
            Entidad.Curso retorno = new Entidad.Curso();
            List<Entidad.Curso> lista = ListarCursosPorInstId(instId);

            if (lista != null && lista.Count > 0)
            {
                retorno = lista.Find(p => p.Nombre.ToUpper() == nombre.ToUpper());
            }

            return retorno;
        }
        public static List<VCFramework.Entidad.Curso> ListarCursosPorInstIdTipo(int instId, int tipo)
        {
            List<VCFramework.Entidad.Curso> lista2 = new List<VCFramework.Entidad.Curso>();
            if (fileContents == null)
            {
                VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

                List<FiltroGenerico> listaFiltros = new List<FiltroGenerico>();

                FiltroGenerico filtro = new FiltroGenerico();
                filtro.Campo = "INST_ID";
                filtro.Valor = instId.ToString();
                filtro.TipoDato = TipoDatoGeneral.Entero;

                FiltroGenerico filtro1 = new FiltroGenerico();
                filtro1.Campo = "TIPO";
                filtro1.Valor = tipo.ToString();
                filtro1.TipoDato = TipoDatoGeneral.Entero;

                listaFiltros.Add(filtro);
                listaFiltros.Add(filtro1);

                List<object> lista = fac.Leer<VCFramework.Entidad.Curso>(listaFiltros, setCnsWebLun);

                if (lista != null)
                {

                    lista2 = lista.Cast<VCFramework.Entidad.Curso>().ToList().OrderBy(p => p.Orden).ToList();
                }
                if (lista2 != null)
                    lista2 = lista2.FindAll(p => p.Eliminado == 0);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.Curso>().ToList().FindAll(p => p.InstId == instId).OrderBy(p => p.Orden).ToList();
            }
            return lista2;
        }

        public static List<VCFramework.Entidad.Curso> ListarCursosPorInstId(int instId, int idUsu)
        {
            List<VCFramework.Entidad.Curso> lista2 = new List<VCFramework.Entidad.Curso>();
            if (fileContents == null)
            {
                VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
                FiltroGenerico filtro = new FiltroGenerico();
                filtro.Campo = "INST_ID";
                filtro.Valor = instId.ToString();
                filtro.TipoDato = TipoDatoGeneral.Entero;

                List<object> lista = fac.Leer<VCFramework.Entidad.Curso>(filtro, setCnsWebLun);

                if (lista != null)
                {

                    lista2 = lista.Cast<VCFramework.Entidad.Curso>().ToList().OrderBy(p=>p.Orden).ToList();
                }
                if (lista2 != null)
                    lista2 = lista2.FindAll(p => p.Eliminado == 0);
            }
            else
            {
                lista2 = fileContents.Cast<VCFramework.Entidad.Curso>().ToList().FindAll(p => p.InstId == instId).OrderBy(p => p.Orden).ToList();
            }
            return lista2;
        }

        public static List<VCFramework.Entidad.Curso> ObtenerCursosPorArr(int instId, string [] arr)
        {
            List<VCFramework.Entidad.Curso> lista = new List<Entidad.Curso>();
            List<VCFramework.Entidad.Curso> listaR = new List<Entidad.Curso>();

            lista = ListarCursosPorInstId(instId);

            if (lista != null && lista.Count > 0)
            {
                if (arr != null && arr.Length > 0)
                {
                    foreach(string r in arr)
                    {
                        if (r!= "")
                        {
                            VCFramework.Entidad.Curso item = lista.Find(p => p.Id == int.Parse(r));
                            if (item != null && item.Id > 0)
                                listaR.Add(item);
                        }
                    }
                }
            }

            return listaR;
        }

    }
}
