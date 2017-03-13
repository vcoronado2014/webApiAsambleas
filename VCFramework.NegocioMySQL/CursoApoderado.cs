using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class CursoApoderado
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.CursoApoderado> ObtenerCursosDelApoderado(int instId, int usuId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;
            
            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "USU_ID";
            filtro1.Valor = usuId.ToString();
            filtro1.TipoDato = TipoDatoGeneral.Entero;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();

            filtros.Add(filtro);
            filtros.Add(filtro1);

            List<object> lista = fac.Leer<VCFramework.Entidad.CursoApoderado>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.CursoApoderado> lista2 = new List<VCFramework.Entidad.CursoApoderado>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.CursoApoderado>().ToList();
            }
            return lista2;
        }

        public static int Insertar(VCFramework.Entidad.CursoApoderado cursoApoderado)
        {
            int retorno = 0;

            cursoApoderado.Nuevo = true;
            cursoApoderado.Modificado = false;
            cursoApoderado.Borrado = false;

            Factory fac = new Factory();

            retorno = fac.Insertar<Entidad.CursoApoderado>(cursoApoderado, setCnsWebLun);

            return retorno;
        }

        public static int Modificar(VCFramework.Entidad.CursoApoderado cursoApoderado)
        {
            int retorno = 0;

            cursoApoderado.Nuevo = false;
            cursoApoderado.Modificado = true;
            cursoApoderado.Borrado = false;

            Factory fac = new Factory();

            retorno = fac.Update<Entidad.CursoApoderado>(cursoApoderado, setCnsWebLun);

            return retorno;
        }

        public static int Eliminar(VCFramework.Entidad.CursoApoderado cursoApoderado)
        {
            int retorno = 0;

            cursoApoderado.Nuevo = false;
            cursoApoderado.Modificado = false;
            cursoApoderado.Borrado = true;

            Factory fac = new Factory();

            retorno = fac.Delete<Entidad.CursoApoderado>(cursoApoderado, setCnsWebLun);

            return retorno;
        }
    }
}
