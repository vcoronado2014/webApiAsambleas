using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class IngresoEgreso
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.EntidadFuniconal.IngresoEgresoFuncional> ObtenerIngresoEgresoPorInstId(int instId)
        {
            List<VCFramework.EntidadFuniconal.IngresoEgresoFuncional> retorno = new List<EntidadFuniconal.IngresoEgresoFuncional>();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.IngresoEgreso>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.IngresoEgreso> lista2 = new List<VCFramework.Entidad.IngresoEgreso>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.IngresoEgreso>().ToList().FindAll(p => p.Eliminado == 0);
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);

            if (lista2 != null && lista2.Count > 0)
            {
                foreach (Entidad.IngresoEgreso ing in lista)
                {
                    if (ing.Eliminado == 0)
                    {
                        EntidadFuniconal.IngresoEgresoFuncional entidad = new EntidadFuniconal.IngresoEgresoFuncional();
                        entidad.Detalle = ing.Detalle;
                        entidad.Eliminado = ing.Eliminado;
                        entidad.FechaMovimiento = ing.FechaMovimiento;
                        entidad.FechaMovimientoDate = entidad.FechaMovimiento;
                        entidad.Id = ing.Id;
                        entidad.InstId = ing.InstId;
                        entidad.Monto = ing.Monto;
                        entidad.NumeroComprobante = ing.NumeroComprobante;
                        entidad.TipoMovimiento = ing.TipoMovimiento;
                        TipoOperacion tipoMov = TipoOperacion.Ingreso;
                        switch (entidad.TipoMovimiento)
                        {
                            case 1:
                                tipoMov = TipoOperacion.Ingreso;
                                entidad.Icon = "foundicon-left-arrow fg-blue";
                                break;
                            case 2:
                                tipoMov = TipoOperacion.Egreso;
                                entidad.Icon = "foundicon-right-arrow fg-red";
                                break;
                        }
                        entidad.TipoMovimientoString = tipoMov.ToString();
                        entidad.UrlDocumento = ing.UrlDocumento;
                        entidad.UsuId = ing.UsuId;
                        if (entidad.UrlDocumento != null)
                        {
                            string[] nombres = entidad.UrlDocumento.Split('/');
                            string nombre = nombres[nombres.Length - 1].ToString();
                            entidad.NombreDocumento = nombre;
                        }
                        retorno.Add(entidad);
                    }
                }
            }

            return retorno;
        }

        public static VCFramework.EntidadFuniconal.IngresoEgresoFuncional ObtenerIngresoEgresoPorId(int id)
        {
            VCFramework.EntidadFuniconal.IngresoEgresoFuncional retorno = new EntidadFuniconal.IngresoEgresoFuncional();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.IngresoEgreso>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.IngresoEgreso> lista2 = new List<VCFramework.Entidad.IngresoEgreso>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.IngresoEgreso>().ToList().FindAll(p => p.Eliminado == 0);
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);

            if (lista2 != null && lista2.Count > 0)
            {
                foreach (Entidad.IngresoEgreso ing in lista)
                {
                    if (ing.Eliminado == 0)
                    {
                        EntidadFuniconal.IngresoEgresoFuncional entidad = new EntidadFuniconal.IngresoEgresoFuncional();
                        entidad.Detalle = ing.Detalle;
                        entidad.Eliminado = ing.Eliminado;
                        entidad.FechaMovimiento = ing.FechaMovimiento;
                        entidad.FechaMovimientoDate = entidad.FechaMovimiento;
                        entidad.Id = ing.Id;
                        entidad.InstId = ing.InstId;
                        entidad.Monto = ing.Monto;
                        entidad.NumeroComprobante = ing.NumeroComprobante;
                        entidad.TipoMovimiento = ing.TipoMovimiento;
                        TipoOperacion tipoMov = TipoOperacion.Ingreso;
                        switch (entidad.TipoMovimiento)
                        {
                            case 1:
                                tipoMov = TipoOperacion.Ingreso;
                                entidad.Icon = "foundicon-left-arrow fg-blue";
                                break;
                            case 2:
                                tipoMov = TipoOperacion.Egreso;
                                entidad.Icon = "foundicon-right-arrow fg-red";
                                break;
                        }
                        entidad.TipoMovimientoString = tipoMov.ToString();
                        entidad.UrlDocumento = ing.UrlDocumento;
                        entidad.UsuId = ing.UsuId;
                        if (entidad.UrlDocumento != null)
                        {
                            string[] nombres = entidad.UrlDocumento.Split('/');
                            string nombre = nombres[nombres.Length - 1].ToString();
                            entidad.NombreDocumento = nombre;
                        }
                        retorno = entidad;
                    }
                }
            }

            return retorno;
        }

        public static VCFramework.Entidad.IngresoEgreso ObtenerPorId(int id)
        {
            VCFramework.Entidad.IngresoEgreso retorno = new Entidad.IngresoEgreso();
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.IngresoEgreso>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.IngresoEgreso> lista2 = new List<VCFramework.Entidad.IngresoEgreso>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.IngresoEgreso>().ToList().FindAll(p => p.Eliminado == 0);
            }
            if (lista2 != null)
                retorno = lista2[0];

            return retorno;
        }

        public static string SumaEgresosIngresos(int instId)
        {
            string retorno = "0";
            int ingresos = 0;
            int egresos = 0;
            List<VCFramework.EntidadFuniconal.IngresoEgresoFuncional> listaProcesar = ObtenerIngresoEgresoPorInstId(instId);
            if (listaProcesar != null && listaProcesar.Count > 0)
            {
                foreach(EntidadFuniconal.IngresoEgresoFuncional ing in listaProcesar)
                {
                    if (ing.TipoMovimiento == 1)
                        ingresos = ingresos + ing.Monto;
                    else
                        egresos = egresos + ing.Monto;
                }
            }
            retorno = ingresos.ToString() + "|" + egresos.ToString();

            return retorno;
        }

        public static int Modificar(VCFramework.Entidad.IngresoEgreso aus)
        {
            aus.Nuevo = false;
            aus.Borrado = false;
            aus.Modificado = true;

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Update<VCFramework.Entidad.IngresoEgreso>(aus, setCnsWebLun);
        }
        public static int Insertar(VCFramework.Entidad.IngresoEgreso aus)
        {
            aus.Nuevo = true;
            aus.Borrado = false;
            aus.Modificado = false;

            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();

            return fac.Insertar<VCFramework.Entidad.IngresoEgreso>(aus, setCnsWebLun);
        }
    }
    public enum TipoOperacion
    {
        Ingreso = 1,
        Egreso = 2
    }
}
