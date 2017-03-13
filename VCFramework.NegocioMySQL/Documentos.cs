using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Documentos
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.Documentos> ObtenerDocumentosPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Documentos>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Documentos> lista2 = new List<VCFramework.Entidad.Documentos>();
            if (lista != null)
            {
                
                lista2 = lista.Cast<VCFramework.Entidad.Documentos>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);

            if(lista2 != null)
            {
                foreach(Entidad.Documentos doc in lista2)
                {
                    doc.NombreArchivo = NegocioMySQL.Utiles.EntregaNombreArchivo(doc.NombreArchivo);
                }
            }

            return lista2;
        }
        public static VCFramework.Entidad.Documentos ObtenerDocumentoId(int id)
        {
            VCFramework.Entidad.Documentos retorno = new Entidad.Documentos();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Documentos>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Documentos> lista2 = new List<VCFramework.Entidad.Documentos>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Documentos>().ToList().FindAll(p=>p.Eliminado == 0);
            }
            if (lista2 != null)
                retorno = lista2[0];

            return retorno;
        }
    }
}
