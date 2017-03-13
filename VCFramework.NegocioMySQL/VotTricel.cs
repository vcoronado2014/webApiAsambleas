using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class VotTricel
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.VotTricel> ObtenerVotacionTricelPorId(int id)
        {

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.VotTricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.VotTricel> lista2 = new List<VCFramework.Entidad.VotTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.VotTricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static List<VCFramework.Entidad.VotTricel> ObtenerVotacionPorUsuario(int usu_id, int ltrId, int triId)
        {

            //int[] arrListas = NegocioMySQL.ListaTricel.ObtenerArregloListasDelTricel(triId);


            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "USU_ID_VOTADOR";
            filtro.Valor = usu_id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "TRI_ID";
            filtro1.Valor = triId.ToString();
            filtro1.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtro1);

            List<object> lista = fac.Leer<VCFramework.Entidad.VotTricel>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.VotTricel> lista2 = new List<VCFramework.Entidad.VotTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.VotTricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);


            return lista2;
        }
        public static List<VCFramework.Entidad.VotTricel> ObtenerVotacionTricelPorListalId(int listaId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "LTR_ID";
            filtro.Valor = listaId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.VotTricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.VotTricel> lista2 = new List<VCFramework.Entidad.VotTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.VotTricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static string SumarVotaciones(int[] arrLIstas)
        {
            StringBuilder retorno = new StringBuilder();
            retorno.Append("<ul>");
            if (arrLIstas != null && arrLIstas.Length > 0)
            {
                foreach (int lst in arrLIstas)
                {
                    
                    List<Entidad.VotTricel> votos = ObtenerVotacionTricelPorListalId(lst);
                    List<Entidad.ListaTricel> listaTricel = NegocioMySQL.ListaTricel.ObtenerListaTricelPorId(lst);
                    retorno.Append("<li>");
                    retorno.AppendFormat("Nombre Lista: {0}, Cantidad Votos: ", listaTricel[0].Nombre);

                    if (votos != null && votos.Count > 0)
                    {
                        retorno.Append(votos.Count.ToString());
                    }
                    else
                    {
                        retorno.Append("0");
                    }
                    retorno.Append("</li>");
                    //retorno.Append("\r\n");
                }
            }
            retorno.Append("</ul>");
            return retorno.ToString();
        }
    }
}
