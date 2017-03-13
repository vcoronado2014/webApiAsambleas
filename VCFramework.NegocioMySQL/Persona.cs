using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Persona
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.Persona> ListarPersonas()
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            List<object> lista = fac.Leer<VCFramework.Entidad.Persona>(setCnsWebLun);
            List<VCFramework.Entidad.Persona> lista2 = new List<VCFramework.Entidad.Persona>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Persona>().ToList();
            }
            return lista2;
        }
        public static List<VCFramework.Entidad.Persona> ObtenerPersonasPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Persona>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Persona> lista2 = new List<VCFramework.Entidad.Persona>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Persona>().ToList();
            }
            return lista2;
        }
        public static VCFramework.Entidad.Persona ObtenerPersonaPorId(int id)
        {
            VCFramework.Entidad.Persona retorno = new Entidad.Persona();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Persona>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Persona> lista2 = new List<VCFramework.Entidad.Persona>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Persona>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                retorno = lista2[0];
            return retorno;
        }
        public static VCFramework.Entidad.Persona ObtenerPersonaPorUsuId(int usuId)
        {
            VCFramework.Entidad.Persona retorno = new Entidad.Persona();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "USU_ID";
            filtro.Valor = usuId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Persona>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Persona> lista2 = new List<VCFramework.Entidad.Persona>();
            if (lista != null)
            {
                lista2 = lista.Cast<VCFramework.Entidad.Persona>().ToList();
            }
            if (lista2 != null && lista2.Count == 1)
                retorno = lista2[0];
            return retorno;
        }
        public static int ModificarUsuario(VCFramework.Entidad.Persona persona)
        {
            bool existe = false;
            persona.Nuevo = false;
            persona.Borrado = false;
            persona.Modificado = false;
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            //buscamos a la persona primero
            if (persona.Id > 0)
            {
                if (ListarPersonas().Find(p => p.Id == persona.Id) != null)
                    existe = true;
            }
            if (!existe)
            {
                persona.Nuevo = true;
                return fac.Insertar<VCFramework.Entidad.Persona>(persona, setCnsWebLun);
            }
            else
            {
                persona.Modificado = true;
                return fac.Update<VCFramework.Entidad.Persona>(persona, setCnsWebLun);
            }
            

            

        }
    }
}
