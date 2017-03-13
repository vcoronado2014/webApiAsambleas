using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class ResponsableTricel
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.ResponsableTricel> ObtenerResponsablesPorUsuId(int usuId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "USU_ID";
            filtro.Valor = usuId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ResponsableTricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ResponsableTricel> lista2 = new List<VCFramework.Entidad.ResponsableTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ResponsableTricel>().ToList();
            }
            return lista2;
        }
        public static List<VCFramework.Entidad.ResponsableTricel> ObtenerResponsables(int triId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "TRI_ID";
            filtro.Valor = triId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ResponsableTricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ResponsableTricel> lista2 = new List<VCFramework.Entidad.ResponsableTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ResponsableTricel>().ToList();
            }
            return lista2;
        }
        public static int Eliminar(Entidad.ResponsableTricel obj)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            return fac.Delete<Entidad.ResponsableTricel>(obj, setCnsWebLun);
        }
        public static int Insertar(Entidad.ResponsableTricel obj)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            return fac.Insertar<Entidad.ResponsableTricel>(obj, setCnsWebLun);
        }
    }
}
