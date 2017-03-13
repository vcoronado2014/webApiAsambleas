using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Proyectos
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.Proyectos> ObtenerProyectosPorInstIdN(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Proyectos>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Proyectos> lista2 = new List<VCFramework.Entidad.Proyectos>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Proyectos>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static List<VCFramework.Entidad.Proyectos> ObtenerProyectosPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            DateTime fechaSistemaInicio = DateTime.Now.AddDays(-1);
            DateTime fechaSistemaTermino = DateTime.Now.AddDays(1);

            List<object> lista = fac.Leer<VCFramework.Entidad.Proyectos>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Proyectos> lista2 = new List<VCFramework.Entidad.Proyectos>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Proyectos>().ToList();
            }
            if (lista2 != null)
                //lista2 = lista2.FindAll(p => (p.Eliminado == 0) && (p.FechaInicio >= fechaSistemaInicio &&   fechaSistemaTermino <= p.FechaTermino));
                lista2 = lista2.FindAll(p => (p.Eliminado == 0) && (p.FechaInicio <= fechaSistemaInicio && fechaSistemaTermino <= p.FechaTermino));

            return lista2;
        }

        public static List<VCFramework.Entidad.Proyectos> ObtenerProyectosPorNombreInstId(string nombre, int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            FiltroGenerico filtro1 = new FiltroGenerico();
            filtro1.Campo = "NOMBRE";
            filtro1.Valor = nombre.ToString();
            filtro1.TipoDato = TipoDatoGeneral.Varchar;

            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);
            filtros.Add(filtro1);

            DateTime fechaSistemaInicio = DateTime.Now.AddDays(-1);
            DateTime fechaSistemaTermino = DateTime.Now.AddDays(1);

            List<object> lista = fac.Leer<VCFramework.Entidad.Proyectos>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.Proyectos> lista2 = new List<VCFramework.Entidad.Proyectos>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Proyectos>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => (p.Eliminado == 0) && (p.FechaInicio >= fechaSistemaInicio && fechaSistemaTermino <= p.FechaTermino));
            return lista2;
        }

        public static List<VCFramework.Entidad.Proyectos> ObtenerProyectosListasPorInstId(int instId, int usuId)
        {
            //los proyectos
            List<VCFramework.Entidad.Proyectos> proyectos = ObtenerProyectosPorInstId(instId);
            //las listas
            List<VCFramework.EntidadFuniconal.ListaTricelFuncional> listas = NegocioMySQL.ListaTricel.ObtenerListaTricelFuncionalProyectos(instId, usuId);
            if (listas != null && listas.Count > 0)
            {
                //ahora convertimos a un proyecto para asociar los resultados.
                foreach(VCFramework.EntidadFuniconal.ListaTricelFuncional li in listas)
                {
                    VCFramework.Entidad.Proyectos pro = new Entidad.Proyectos();
                    pro.Beneficios = li.Beneficios;
                    pro.Costo = 0;
                    pro.Descripcion = li.Descripcion;
                    pro.EsVigente = 1;
                    if (li.Tricel != null)
                    {
                        pro.FechaCreacion = li.Tricel.FechaCreacion;
                        //pro.FechaInicio = li.Tricel.FechaInicio;
                        //pro.FechaTermino = li.Tricel.FechaTermino;
                        pro.Id = li.Id;
                        pro.Nombre = "Tricel " + li.Tricel.Nombre;
                    }
                    pro.FechaInicio = li.FechaInicio;
                    pro.FechaTermino = li.FechaTermino;
                    pro.InstId = instId;
                    pro.Nombre = pro.Nombre + " Lista: " + li.Nombre;
                    pro.Objetivo = li.Objetivo;
                    pro.UsuIdCreador = usuId;
                    proyectos.Add(pro);
                }
            }

            return proyectos;
        }
        public static List<VCFramework.Entidad.Proyectos> ObtenerProyectosPorInstIdTodos(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Proyectos>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Proyectos> lista2 = new List<VCFramework.Entidad.Proyectos>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Proyectos>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static List<VCFramework.Entidad.Proyectos> ObtenerProyectosPorId(int id)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Proyectos>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Proyectos> lista2 = new List<VCFramework.Entidad.Proyectos>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Proyectos>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
    }
}
