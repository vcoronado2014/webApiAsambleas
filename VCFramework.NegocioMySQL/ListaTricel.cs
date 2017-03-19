using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class ListaTricel
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static List<VCFramework.Entidad.ListaTricel> ObtenerListaPorNombreInstId(string nombre, int instId)
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

            List<object> lista = fac.Leer<VCFramework.Entidad.ListaTricel>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.ListaTricel> lista2 = new List<VCFramework.Entidad.ListaTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ListaTricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }

        public static List<VCFramework.Entidad.ListaTricel> ObtenerListaPorInstId(int instId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "INST_ID";
            filtro.Valor = instId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;


            List<FiltroGenerico> filtros = new List<FiltroGenerico>();
            filtros.Add(filtro);

            List<object> lista = fac.Leer<VCFramework.Entidad.ListaTricel>(filtros, setCnsWebLun);
            List<VCFramework.Entidad.ListaTricel> lista2 = new List<VCFramework.Entidad.ListaTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ListaTricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static List<VCFramework.EntidadFuniconal.ListaTricelFuncional> ObtenerListaTricelFuncional(int idTricel, int usuResponsable)
        {
            //ESTO ESTÁ TODO MALO!!!!!!! SE DEBEN LISTAR PRIMERO LOS TRICEL DONDE EL USUARIO SEA RESPONSABLE
            //LUEGO SI NO TIENE LISTAS ASOCIADAS HAY QUE GENERAR UN REGISTRO EN EL CUAL SE LE PERMITA CREAR UNA LISTA (ICONO CREAR)
            //SI TIENE LISTAS ASOCIADAS SE DEBE MOSTRAR ICONOS ELIMINAR O MODIFICAR LISTA

            //OBTENEMOS RESPONSABLES
            List<Entidad.ResponsableTricel> responsablesBuscar = NegocioMySQL.ResponsableTricel.ObtenerResponsablesPorUsuId(usuResponsable);
            List<VCFramework.Entidad.Tricel> listaTriceles = new List<Entidad.Tricel>();
            List<VCFramework.EntidadFuniconal.ListaTricelFuncional> listaRetorno = new List<EntidadFuniconal.ListaTricelFuncional>();

            if (responsablesBuscar != null && responsablesBuscar.Count > 0)
            {
                foreach(Entidad.ResponsableTricel resp in responsablesBuscar)
                {
                    //obtenemos los triceles del responsable
                    List<Entidad.Tricel> listaT = NegocioMySQL.Tricel.ObtenerTricelPorId(resp.TriId);
                    if (listaT != null && listaT.Count > 0)
                    {
                        Entidad.Tricel tricel = listaT[0];
                        listaTriceles.Add(tricel);
                    }
                }
                //ahora que tenemos los triceles verificamos si tienen o no tienen listas asociadas
                if (listaTriceles != null && listaTriceles.Count > 0)
                {
                    foreach (Entidad.Tricel tric in listaTriceles)
                    {
                        //VCFramework.EntidadFuniconal.ListaTricelFuncional listaTricelAgregar = new EntidadFuniconal.ListaTricelFuncional();
                        
                        List<Entidad.ListaTricel> listitaTricel = NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(tric.Id);
                        if (listitaTricel != null && listitaTricel.Count > 0)
                        {
                            foreach (Entidad.ListaTricel lt in listitaTricel)
                            {
                                VCFramework.EntidadFuniconal.ListaTricelFuncional listaTricelAgregar = new EntidadFuniconal.ListaTricelFuncional();
                                //si tiene lista asociada entonces se debe mostrar los iconos editar y eliminar
                                //si la lista es del mismo usuario que esta logueado, entonces este puede editar y eliminar
                                if (lt.UsuId == usuResponsable)
                                {
                                    listaTricelAgregar.IconoEditar = true;
                                    //igual debe permitir ver el icono de crear, ya que pueden existir
                                    //más de una lista por TRICEL. CAMBIADO, SOLO PUEDE ESTAR ESTE ICONO
                                    //listaTricelAgregar.IconoCrear = true;
                                    listaTricelAgregar.IconoEliminar = true;
                                }
                                else
                                {
                                    //solo si no tiene ya una lista del mismo tricel, de lo contrario no mostrar
                                    if (!listitaTricel.Exists(p=>p.UsuId == usuResponsable))
                                        listaTricelAgregar.IconoCrear = true;
                                }
                                listaTricelAgregar.Beneficios = lt.Beneficios;
                                listaTricelAgregar.Descripcion = lt.Descripcion;
                                listaTricelAgregar.Id = lt.Id;
                                listaTricelAgregar.InstId = lt.InstId;
                                listaTricelAgregar.Nombre = lt.Nombre;
                                listaTricelAgregar.Objetivo = lt.Objetivo;
                                listaTricelAgregar.Rol = lt.Rol;
                                listaTricelAgregar.RptId = lt.RptId;
                                listaTricelAgregar.TriId = tric.Id;
                                listaTricelAgregar.FechaInicio = lt.FechaInicio;
                                listaTricelAgregar.FechaTermino = lt.FechaTermino;

                                listaRetorno.Add(listaTricelAgregar);
                            }
                        }
                        else
                        {
                            VCFramework.EntidadFuniconal.ListaTricelFuncional listaTricelAgregar = new EntidadFuniconal.ListaTricelFuncional();
                            //no tiene lista asociada, el usuario puede crearla
                            listaTricelAgregar.IconoCrear = true;
                            listaTricelAgregar.Nombre = "Sin Crear (Lista Pendiente)";
                            listaTricelAgregar.TriId = tric.Id;
                            listaRetorno.Add(listaTricelAgregar);

                        }

                        //listaRetorno.Add(listaTricelAgregar);

                    }
                }


            }

            #region comentado

            //VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            //FiltroGenerico filtro = new FiltroGenerico();
            //filtro.Campo = "TRI_ID";
            //filtro.Valor = idTricel.ToString();
            //filtro.TipoDato = TipoDatoGeneral.Entero;

            ////List<object> lista = fac.Leer<VCFramework.Entidad.ListaTricel>(filtro);
            //List<object> lista = fac.Leer<VCFramework.Entidad.ListaTricel>();
            //List<VCFramework.Entidad.ListaTricel> lista2 = new List<VCFramework.Entidad.ListaTricel>();
            //if (lista != null)
            //{
            //    lista2 = lista.Cast<VCFramework.Entidad.ListaTricel>().ToList();
            //}
            //if (lista2 != null)
            //    lista2 = lista2.FindAll(p => p.Eliminado == 0);

            ////AHORA BUSCAMOS DENTRO DE LA LISTA LOS RESPONSABLES, SI EL USUARIO COINCIDE ENTONCES AGREGAMOS LA LISTA
            ////DE LO CONTRARIO NO
            //if (lista2 != null && lista2.Count > 0)
            //{
            //    foreach(Entidad.ListaTricel listaT in lista2)
            //    {
            //        List<Entidad.ResponsableTricel> responsables = NegocioMySQL.ResponsableTricel.ObtenerResponsables(listaT.TriId);
            //        if (responsables != null && responsables.Count > 0)
            //        {
            //            if (responsables.Exists(p=>p.UsuId == usuResponsable))
            //            {
            //                //existe, debe ser agregado a la lista para retornar
            //                EntidadFuniconal.ListaTricelFuncional entidad = new EntidadFuniconal.ListaTricelFuncional();
            //                entidad.Beneficios = listaT.Beneficios;
            //                entidad.Descripcion = listaT.Descripcion;
            //                entidad.Eliminado = listaT.Eliminado;
            //                entidad.Id = listaT.Id;
            //                entidad.InstId = listaT.InstId;
            //                entidad.Nombre = listaT.Nombre;
            //                entidad.Objetivo = listaT.Objetivo;
            //                entidad.Rol = listaT.Rol;
            //                entidad.RptId = listaT.RptId;
            //                entidad.TriId = listaT.TriId;
            //                listaRetorno.Add(entidad);

            //            }
            //        }
            //    }
            //}
            #endregion

            return listaRetorno;
        }

        public static List<VCFramework.EntidadFuniconal.ListaTricelFuncional> ObtenerListaTricelFuncionalProyectos(int instId, int usuResponsable)
        {


            //OBTENEMOS RESPONSABLES
            List<Entidad.ResponsableTricel> responsablesBuscar = NegocioMySQL.ResponsableTricel.ObtenerResponsablesPorUsuId(usuResponsable);
            List<VCFramework.Entidad.Tricel> listaTriceles = new List<Entidad.Tricel>();
            List<VCFramework.EntidadFuniconal.ListaTricelFuncional> listaRetorno = new List<EntidadFuniconal.ListaTricelFuncional>();

            if (responsablesBuscar != null && responsablesBuscar.Count > 0)
            {
                foreach (Entidad.ResponsableTricel resp in responsablesBuscar)
                {
                    //obtenemos los triceles del responsable
                    List<Entidad.Tricel> listaT = NegocioMySQL.Tricel.ObtenerTricelPorId(resp.TriId);
                    if (listaT != null && listaT.Count > 0)
                    {
                        Entidad.Tricel tricel = listaT[0];
                        listaTriceles.Add(tricel);
                    }

                }
                
                //discriminamos por fecha   *************************************************************
                if (listaTriceles != null && listaTriceles.Count > 0)
                    //listaTriceles = listaTriceles.FindAll(p => p.Eliminado == 0 && p.FechaInicio <= DateTime.Now.AddDays(-1));
                    listaTriceles = listaTriceles.FindAll(p => p.Eliminado == 0);
                //***************************************************************************************

                //ahora que tenemos los triceles verificamos si tienen o no tienen listas asociadas
                if (listaTriceles != null && listaTriceles.Count > 0)
                {
                    foreach (Entidad.Tricel tric in listaTriceles)
                    {
                        //VCFramework.EntidadFuniconal.ListaTricelFuncional listaTricelAgregar = new EntidadFuniconal.ListaTricelFuncional();

                        List<Entidad.ListaTricel> listitaTricel = NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(tric.Id);
                        if (listitaTricel != null && listitaTricel.Count > 0)
                        {
                            foreach (Entidad.ListaTricel lt in listitaTricel)
                            {
                                if (lt.FechaInicio <= DateTime.Now.AddHours(-23) && lt.FechaTermino >= DateTime.Now.AddDays(1))
                                {
                                    VCFramework.EntidadFuniconal.ListaTricelFuncional listaTricelAgregar = new EntidadFuniconal.ListaTricelFuncional();
                                    //si tiene lista asociada entonces se debe mostrar los iconos editar y eliminar
                                    listaTricelAgregar.IconoEditar = true;
                                    //igual debe permitir ver el icono de crear, ya que pueden existir
                                    //más de una lista por TRICEL.
                                    listaTricelAgregar.IconoCrear = true;
                                    listaTricelAgregar.IconoEliminar = true;
                                    listaTricelAgregar.Beneficios = lt.Beneficios;
                                    listaTricelAgregar.Descripcion = lt.Descripcion;
                                    listaTricelAgregar.Id = lt.Id;
                                    listaTricelAgregar.InstId = lt.InstId;
                                    listaTricelAgregar.Nombre = lt.Nombre;
                                    listaTricelAgregar.Objetivo = lt.Objetivo;
                                    listaTricelAgregar.Rol = lt.Rol;
                                    listaTricelAgregar.RptId = lt.RptId;
                                    listaTricelAgregar.TriId = tric.Id;
                                    listaTricelAgregar.Tricel = new Entidad.Tricel();
                                    listaTricelAgregar.Tricel = tric;
                                    listaTricelAgregar.FechaInicio = lt.FechaInicio;
                                    listaTricelAgregar.FechaTermino = lt.FechaTermino;

                                    listaRetorno.Add(listaTricelAgregar);
                                }
                            }
                        }
                        else
                        {
                            VCFramework.EntidadFuniconal.ListaTricelFuncional listaTricelAgregar = new EntidadFuniconal.ListaTricelFuncional();
                            //no tiene lista asociada, el usuario puede crearla
                            listaTricelAgregar.IconoCrear = true;
                            listaTricelAgregar.Nombre = "Sin Crear (Lista Pendiente)";
                            listaTricelAgregar.TriId = tric.Id;
                            listaTricelAgregar.Tricel = new Entidad.Tricel();
                            listaTricelAgregar.Tricel = tric;
                            //se comenta porque en la página por defecto no se puede mostrar
                            //listaRetorno.Add(listaTricelAgregar);

                        }

                        //listaRetorno.Add(listaTricelAgregar);

                    }
                }


            }


            return listaRetorno;
        }

        public static VCFramework.EntidadFuniconal.ListaTricelFuncional ObtenerListaTricelFuncionalPorId(int instId, int usuResponsable, int id)
        {
            VCFramework.EntidadFuniconal.ListaTricelFuncional retorno = new EntidadFuniconal.ListaTricelFuncional();

            retorno = ObtenerListaTricelFuncionalProyectos(instId, usuResponsable).FirstOrDefault(p => p.Id == id);


            return retorno;
        }

        public static List<VCFramework.Entidad.ListaTricel> ObtenerListaTricelPorId(int id)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "ID";
            filtro.Valor = id.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ListaTricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ListaTricel> lista2 = new List<VCFramework.Entidad.ListaTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ListaTricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static int Eliminar(Entidad.ListaTricel entidad)
        {
            Factory fac = new Factory();
            entidad.Eliminado = 1;
            return fac.Update<VCFramework.Entidad.ListaTricel>(entidad, setCnsWebLun);
        }
        public static int Modificar(Entidad.ListaTricel entidad)
        {
            Factory fac = new Factory();
            return fac.Update<VCFramework.Entidad.ListaTricel>(entidad, setCnsWebLun);
        }
        public static int Insertar(Entidad.ListaTricel entidad)
        {
            Factory fac = new Factory();
            return fac.Insertar<VCFramework.Entidad.ListaTricel>(entidad, setCnsWebLun);
        }

        public static List<VCFramework.Entidad.ListaTricel> ObtenerListaTricelPorTricelId(int triId)
        {
            VCFramework.Negocio.Factory.Factory fac = new VCFramework.Negocio.Factory.Factory();
            FiltroGenerico filtro = new FiltroGenerico();
            filtro.Campo = "TRI_ID";
            filtro.Valor = triId.ToString();
            filtro.TipoDato = TipoDatoGeneral.Entero;

            List<object> lista = fac.Leer<VCFramework.Entidad.ListaTricel>(filtro, setCnsWebLun);
            List<VCFramework.Entidad.ListaTricel> lista2 = new List<VCFramework.Entidad.ListaTricel>();
            if (lista != null)
            {

                lista2 = lista.Cast<VCFramework.Entidad.ListaTricel>().ToList();
            }
            if (lista2 != null)
                lista2 = lista2.FindAll(p => p.Eliminado == 0);
            return lista2;
        }
        public static int[] ObtenerArregloListasDelTricel(int triId)
        {
            int[] arr = null;
            List<VCFramework.Entidad.ListaTricel> lista2 = ObtenerListaTricelPorTricelId(triId);

            if (lista2 != null && lista2.Count > 0)
            {
                arr = new int[lista2.Count];
                int contador = 0;
                foreach(VCFramework.Entidad.ListaTricel lst in lista2)
                {
                    arr[contador] = lst.Id;
                    contador++;
                }
            }

            return arr;
        }
    }
}
