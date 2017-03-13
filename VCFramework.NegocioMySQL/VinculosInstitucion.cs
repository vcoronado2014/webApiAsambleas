using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class VinculosInstitucion
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.VinculosInstitucion> ObtenerPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.VinculosInstitucion>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.VinculosInstitucion> lista2 = new List<VCFramework.Entidad.VinculosInstitucion>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.VinculosInstitucion>().ToList();
            }

            return lista2;
        }

    }
}
