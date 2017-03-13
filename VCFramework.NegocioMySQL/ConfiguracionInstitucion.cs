using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class ConfiguracionInstitucion
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static VCFramework.Entidad.ConfiguracionInstitucion ObtenerConfiguracionPorInstId(int instId)
        {
            VCFramework.Entidad.ConfiguracionInstitucion entidad = null;
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ConfiguracionInstitucion>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ConfiguracionInstitucion> lista2 = new List<VCFramework.Entidad.ConfiguracionInstitucion>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ConfiguracionInstitucion>().ToList();
            }
            if (lista2 != null)
            {
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
                entidad = lista2[0];
            }
            return entidad;
        }
    }
}
