using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class ArchivosProyecto
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.ArchivosProyecto> ObtenerArchivosPorProyectoId(int proId, object listaSesion)
        {

            if (listaSesion != null)
            {
                List<VCFramework.Entidad.ArchivosProyecto> lista = listaSesion as List<VCFramework.Entidad.ArchivosProyecto>;
                return lista;
            }
            else
            {
                VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
                FiltroGenerico filtro = new FiltroGenerico();
                filtro.Campo = "PRO_ID";
                filtro.Valor = proId.ToString();
                filtro.TipoDato = TipoDatoGeneral.Entero;

                List<object> lista = fac.Leer<VCFramework.Entidad.ArchivosProyecto>(filtro, setCnsWebLun);
                List<VCFramework.Entidad.ArchivosProyecto> lista2 = new List<VCFramework.Entidad.ArchivosProyecto>();
                if (lista != null)
                {

                    lista2 = lista.Cast<VCFramework.Entidad.ArchivosProyecto>().ToList();
                }
                if (lista2 != null)
                    lista2 = lista2.FindAll(p => p.Eliminado == 0);

                return lista2;
            }
        }

        public static List<VCFramework.Entidad.ArchivosProyecto> ObtenerArchivoPorId(int id)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ArchivosProyecto>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ArchivosProyecto> lista2 = new List<VCFramework.Entidad.ArchivosProyecto>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ArchivosProyecto>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
    }
}
