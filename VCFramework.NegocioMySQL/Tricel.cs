using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Tricel
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.Tricel> ObtenerTricelPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Tricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Tricel> lista2 = new List<VCFramework.Entidad.Tricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Tricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0 && p.FechaInicio >= DateTime.Now.AddDays(-1));
            return lista2;
        }
        public static List<VCFramework.Entidad.Tricel> ObtenerTricelPorInstIdTodos(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Tricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Tricel> lista2 = new List<VCFramework.Entidad.Tricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Tricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static int Modificar(VCFramework.Entidad.Tricel entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Update<Entidad.Tricel>(entidad, setCnsWebLun);
        }

        public static int Insertar(VCFramework.Entidad.Tricel entidad)
        {
            VCFramework.Negocio.Factory.Factory fac = new Factory();
            return fac.Insertar<Entidad.Tricel>(entidad, setCnsWebLun);
        }
        public static List<VCFramework.EntidadFuniconal.TricelFuncional> ObtenerTricelPorInstIdTodosFucnional(int instId, int usuIdLogueado)
        {
            List<VCFramework.Entidad.Tricel> listaTricel = ObtenerTricelPorInstIdTodos(instId);
            List<EntidadFuniconal.TricelFuncional> lista = new List<EntidadFuniconal.TricelFuncional>();

            if (listaTricel != null && listaTricel.Count> 0)
            {
                foreach(Entidad.Tricel tricel in listaTricel)
                {
                    EntidadFuniconal.TricelFuncional tri = new EntidadFuniconal.TricelFuncional();
                    tri.CantidadListas = NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(tricel.Id).Count;
                    tri.Id = tricel.Id;
                    tri.InstId = tricel.InstId;
                    tri.Nombre = tricel.Nombre;
                    tri.Objetivo = tricel.Objetivo;
                    tri.UsuIdCreador = tricel.UsuIdCreador;
                    tri.FechaCreacion = tricel.FechaCreacion;
                    tri.FechaInicio = tricel.FechaInicio;
                    tri.FechaTermino = tricel.FechaTermino;
                    //se debe mostrar u ocultar de acuerdo al responsable
                    List<Entidad.ResponsableTricel> responsables = NegocioMySQL.ResponsableTricel.ObtenerResponsables(tri.Id);
                    if (responsables != null && responsables.Count > 0)
                    {
                        if (responsables.Exists(p => p.UsuId == usuIdLogueado))
                        {
                            tri.MostrarIconoCrear = true;
                            if (tri.CantidadListas > 0)
                                tri.MostrarIconoEliminar = true;
                        }
                    }
                    lista.Add(tri);
                }
            }

            return lista;
        }
        public static List<VCFramework.Entidad.Tricel> ObtenerTricelPorId(int id)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.Tricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.Tricel> lista2 = new List<VCFramework.Entidad.Tricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.Tricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
    }
}
