using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class ArchivosTricel
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.ArchivosTricel> ObtenerArchivosPorTricelId(int triId, object listaSesion)
        {

            if (listaSesion != null)
            {
                List<VCFramework.Entidad.ArchivosTricel> lista = listaSesion as List<VCFramework.Entidad.ArchivosTricel>;
                return lista;
            }
            else
            {
                VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
                FiltroGenerico filtro = new FiltroGenerico();
                filtro.Campo = "TRI_ID";
                filtro.Valor = triId.ToString();
                filtro.TipoDato = TipoDatoGeneral.Entero;

                List<object> lista = fac.Leer<VCFramework.Entidad.ArchivosTricel>(filtro, setCnsWebLun);
                List<VCFramework.Entidad.ArchivosTricel> lista2 = new List<VCFramework.Entidad.ArchivosTricel>();
                if (lista != null)
                {

                    lista2 = lista.Cast<VCFramework.Entidad.ArchivosTricel>().ToList();
                }
                if (lista2 != null)
                    lista2 = lista2.FindAll(p => p.Eliminado == 0);
                return lista2;
            }
        }

        public static List<VCFramework.Entidad.ArchivosTricel> ObtenerArchivoPorId(int id)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ArchivosTricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ArchivosTricel> lista2 = new List<VCFramework.Entidad.ArchivosTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ArchivosTricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static int Insertar(VCFramework.Entidad.ArchivosTricel entidad)
        {
            Factory fac = new Factory();
            return fac.Insertar<VCFramework.Entidad.ArchivosTricel>(entidad, setCnsWebLun);
        }

        public static int Eliminar(VCFramework.Entidad.ArchivosTricel entidad)
        {
            entidad.Eliminado = 1;
            Factory fac = new Factory();
            return fac.Update<VCFramework.Entidad.ArchivosTricel>(entidad, setCnsWebLun);
        }

    }
}
