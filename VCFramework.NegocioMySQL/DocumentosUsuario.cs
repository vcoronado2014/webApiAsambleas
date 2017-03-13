using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class DocumentosUsuario
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.DocumentosUsuario> ObtenerDocumentosPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.DocumentosUsuario>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.DocumentosUsuario> lista2 = new List<VCFramework.Entidad.DocumentosUsuario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.DocumentosUsuario>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);

            if (lista2 != null)
            {
                foreach (Entidad.DocumentosUsuario doc in lista2)
                {
                    doc.NombreArchivo = NegocioMySQL.Utiles.EntregaNombreArchivo(doc.NombreArchivo);
                }
            }
            return lista2;
        }

        public static List<VCFramework.Entidad.DocumentosUsuario> ObtenerDocumentosPorInstIdNuevo(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.DocumentosUsuario>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.DocumentosUsuario> lista2 = new List<VCFramework.Entidad.DocumentosUsuario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.DocumentosUsuario>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);


            return lista2;
        }
        public static int Insertar(VCFramework.Entidad.DocumentosUsuario entidad)
        {
            Factory fac = new Factory();
            return fac.Insertar<VCFramework.Entidad.DocumentosUsuario>(entidad, setCnsWebLun);
        }
        public static int Modificar(VCFramework.Entidad.DocumentosUsuario entidad)
        {
            Factory fac = new Factory();
            return fac.Update<VCFramework.Entidad.DocumentosUsuario>(entidad, setCnsWebLun);
        }
        public static VCFramework.Entidad.DocumentosUsuario ObtenerDocumentoId(int id)
        {
            VCFramework.Entidad.DocumentosUsuario retorno = new Entidad.DocumentosUsuario();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.DocumentosUsuario>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.DocumentosUsuario> lista2 = new List<VCFramework.Entidad.DocumentosUsuario>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.DocumentosUsuario>().ToList().FindAll(p => p.Eliminado == 0);
            }
            if (lista2 != null)
                retorno = lista2[0];

            return retorno;
        }
    }
}
