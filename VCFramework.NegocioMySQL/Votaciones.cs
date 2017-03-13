using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Votaciones
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.Votaciones> ObtenerVotaciones(int proId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<FiltroGenerico> filtro = new List<FiltroGenerico>();
            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "PRO_ID";
            filtro1.TipoDato = TipoDatoGeneral.Entero;
            filtro1.Valor = proId.ToString();


            filtro.Add(filtro1);


            List<object> lista = fac.Leer<VCFramework.Entidad.Votaciones>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Votaciones> lista2 = new List<VCFramework.Entidad.Votaciones>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Votaciones>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static List<VCFramework.Entidad.Votaciones> ObtenerVotaciones(int proId, int usuId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<FiltroGenerico> filtro = new List<FiltroGenerico>();
            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "PRO_ID";
            filtro1.TipoDato = TipoDatoGeneral.Entero;
            filtro1.Valor = proId.ToString();

            FiltroGenerico filtro2 = new FiltroGenerico();
            filtro2.Campo = "USU_ID_VOTADOR";
            filtro2.TipoDato = TipoDatoGeneral.Entero;
            filtro2.Valor = usuId.ToString();

            filtro.Add(filtro1);
            filtro.Add(filtro2);

            List<object> lista = fac.Leer<VCFramework.Entidad.Votaciones>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Votaciones> lista2 = new List<VCFramework.Entidad.Votaciones>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Votaciones>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static string SumaVotaciones(int proId)
        {
            string retorno = "0";
            int si = 0;
            int no = 0;
            List<Entidad.Votaciones> listaProcesar = ObtenerVotaciones(proId);
            if (listaProcesar != null && listaProcesar.Count > 0)
            {
                foreach (Entidad.Votaciones ing in listaProcesar)
                {
                    if (ing.Valor == 1)
                        si++;
                    else
                        no++;
                }
            }
            retorno = si.ToString() + "|" + no.ToString();

            return retorno;
        }
    }
}
