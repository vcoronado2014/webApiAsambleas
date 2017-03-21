using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VCFramework.Entidad;
using VCFramework.NegocioMySQL;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Xml;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Text;
using System.Web;
namespace WebApiAsambleas.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ListaTricelController : ApiController
    {
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string buscarNombreUsuario = "";
            if (data.BuscarId != null)
            {
                buscarNombreUsuario = data.BuscarId;
            }

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string triId = data.TriId;
                int triIdBuscar = int.Parse(triId);

                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.ListaTricel> triceles = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(triIdBuscar);

                if (buscarNombreUsuario != "")
                {
                    if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
                        triceles = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorId(int.Parse(buscarNombreUsuario));
                    else
                        triceles = triceles.FindAll(p => p.Nombre == buscarNombreUsuario);
                }


                if (triceles != null && triceles.Count > 0)
                {
                    foreach (VCFramework.Entidad.ListaTricel tri in triceles)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.Objetivo;
                        us.NombreUsuario = tri.Nombre;
                        us.OtroUno = tri.FechaInicio.ToShortDateString();
                        us.OtroDos = tri.FechaTermino.ToShortDateString();
                        us.OtroTres = tri.Descripcion;
                        us.OtroCuatro = tri.Beneficios;
                        //buscamos la informaciòn del tricel
                        List<VCFramework.Entidad.Tricel> padre = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorId(tri.TriId);
                        if (padre != null && padre.Count == 1)
                        {
                            VCFramework.Entidad.Tricel tricelMostrar = padre[0];
                            StringBuilder sb = new StringBuilder();
                            /*
                             * 
                             * 
                             * 
                                <div class='col-xs-12 page-header'><h5>Info <small>del Tricel</small></h5></div>

                            */
                            //sb.Append("<div class='col-xs-12 page-header'><h5>Info <small>del Tricel</small></h5></div>");
                            //sb.Append("\r\n");
                            sb.AppendFormat("Nombre del Tricel {0}", tricelMostrar.Nombre);
                            sb.Append(" --- ");
                            sb.AppendFormat("Objetivo del Tricel: {0}", tricelMostrar.Objetivo);
                            us.OtroCinco = sb.ToString();
                        }
                        //us.UrlDocumento = insti.UrlDocumento;

                        us.Url = "CrearModificarListaTricel.html?id=" + us.Id.ToString() + "&ELIMINAR=0&triId=" + tri.TriId.ToString();
                        us.UrlEliminar = "CrearModificarListaTricel.html?id=" + us.Id.ToString() + "&ELIMINAR=1&triId=" + tri.TriId.ToString();


                        votaciones.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(votaciones);
                httpResponse.Content = new StringContent(JSON);
                httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }
            return httpResponse;


        }


        [System.Web.Http.AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.Id == 0)
                throw new ArgumentNullException("Id");

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string id = data.Id;
                int idBuscar = int.Parse(id);


                VCFramework.Entidad.ListaTricel inst = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorId(idBuscar)[0];

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;


                    VCFramework.NegocioMySQL.ListaTricel.Eliminar(inst);

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(inst);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

                }


            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }
            return httpResponse;

        }

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string id = data.Id;
            if (id == null)
                id = "0";

            string instId = data.InstId;
            string nombre = data.Nombre;
            string objetivo = data.Objetivo;
            string fechaInicio = data.FechaInicio;
            string fechaTermino = data.FechaTermino;
            string usuId = data.IdUsuario;
            string descripcion = data.Descripcion;
            string beneficios = data.Beneficios;
            string triId = data.TriId;
            string rolId = data.RolId;



            HttpResponseMessage httpResponse = new HttpResponseMessage();
            int idNuevo = 0;

            VCFramework.Entidad.Tricel tricelLista = new VCFramework.Entidad.Tricel();

            try
            {
                VCFramework.Entidad.ListaTricel tricel = new VCFramework.Entidad.ListaTricel();
                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");
                IFormatProvider culture1 = new System.Globalization.CultureInfo("es-CL", true);

                List<VCFramework.Entidad.Tricel> tricelesLista = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorId(int.Parse(triId));
                if (tricelesLista != null && tricelesLista.Count == 1)
                    tricelLista = tricelesLista[0];

                if (id != "0")
                {
                    //es modificado
                    List<VCFramework.Entidad.ListaTricel> triceles = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaTricelPorTricelId(int.Parse(id));
                    if (triceles.Count == 1)
                    {
                        tricel = triceles[0];
                        tricel.FechaInicio = tricelLista.FechaInicio;
                        tricel.FechaTermino = tricelLista.FechaTermino;
                        tricel.Nombre = nombre;
                        tricel.Objetivo = objetivo;
                        tricel.Descripcion = descripcion;
                        tricel.Beneficios = beneficios;
                        VCFramework.NegocioMySQL.ListaTricel.Modificar(tricel);
                        
                    }
                }
                else
                {
                    //es nuevo
                    tricel.Eliminado = 0;
                    tricel.FechaInicio = tricelLista.FechaInicio;
                    tricel.FechaTermino = tricelLista.FechaTermino;
                    tricel.Nombre = nombre;
                    tricel.Objetivo = objetivo;
                    tricel.InstId = int.Parse(instId);
                    tricel.Descripcion = descripcion;
                    tricel.Beneficios = beneficios;
                    tricel.Rol = rolId;
                    tricel.TriId = int.Parse(triId);
                    tricel.UsuId = int.Parse(usuId);
                    tricel.Id = VCFramework.NegocioMySQL.ListaTricel.Insertar(tricel);
                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(tricel);
                httpResponse.Content = new StringContent(JSON);
                httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

            }
            catch (Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }

            return httpResponse;
        }

    }
}