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

namespace AsambleasWeb.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class InstitucionController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string id)
        {
            
            //validaciones antes de ejecutar la llamada.
            if (id == "")
                throw new ArgumentNullException("Id");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                //string instId = data.InstId;
                int idBuscar = int.Parse(id);

                VCFramework.Entidad.Institucion institucion = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorIdSinCache(idBuscar);
                

                if (institucion != null)
                {
                   
                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(institucion);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
                }
                else
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                //Uri uri = new Uri(AgendaWeb.Integracion.Utils.ObtenerUrlLogin());

                //httpResponse = AgendaWeb.Integracion.PostResponse.GetResponse(uri, Input);
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
                string idInstitucion = data.Id;
                int idInstitucionBuscar = int.Parse(idInstitucion);
                VCFramework.Entidad.Institucion inst = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorIdSinCache(idInstitucionBuscar);

                if (inst != null && inst.Id > 0)
                {
                    inst.Eliminado = 1;
                    
                    VCFramework.NegocioMySQL.Institucion.Modificar(inst);

                    List<VCFramework.Entidad.Institucion> instituciones = VCFramework.NegocioMySQL.Institucion.ListarInstitucionesSinCache();


                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(instituciones);
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
            if (data.IdUsuario == 0)
                throw new ArgumentNullException("IdUsuario");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string idUsuario = data.IdUsuario;
                int idUsuarioBuscar = int.Parse(idUsuario);

                VCFramework.EntidadFuncional.proposalss establecimientos = new VCFramework.EntidadFuncional.proposalss();
                establecimientos.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.Institucion> instituciones = VCFramework.NegocioMySQL.Institucion.ListarInstitucionesSinCache();

                if (buscarNombreUsuario != "")
                    instituciones = instituciones.FindAll(p => p.Nombre == buscarNombreUsuario);

                if (instituciones != null && instituciones.Count > 0)
                {
                    foreach(VCFramework.Entidad.Institucion insti in instituciones)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = insti.Id;
                        us.NombreCompleto = insti.Nombre;
                        us.Url = insti.Url;
                        us.UrlEliminar = insti.UrlEliminar;
                        establecimientos.proposals.Add(us);
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(establecimientos);
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

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string idInstitucion = data.Id;
            if (idInstitucion == null)
                idInstitucion = "0";
            int idInstitucionBuscar = int.Parse(idInstitucion);


            //validaciones antes de ejecutar la llamada.
            VCFramework.Entidad.Institucion aus = VCFramework.NegocioMySQL.Institucion.ObtenerInstitucionPorIdSinCache(idInstitucionBuscar);

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string nombre = data.Nombre;
                string idRegion = data.IdRegion;
                string idComuna = data.IdComuna;
                string telefono = data.Telefono;
                string correo = data.CorreoElectronico;
                string direccion = data.Direccion;

                if (aus == null)
                    aus = new VCFramework.Entidad.Institucion();

                if (aus != null)
                {
                    int nuevoId = 0;

                    if (aus.Id == 0)
                    {
                        aus.Nombre = nombre;
                    }
                    aus.Nombre = nombre;
                    aus.RegId = int.Parse(idRegion);
                    aus.ComId = int.Parse(idComuna);
                    aus.Telefono = telefono;
                    aus.CorreoElectronico = correo;
                    aus.Direccion = direccion;
                    if (aus.Id == 0)
                        nuevoId = VCFramework.NegocioMySQL.Institucion.Insertar(aus);
                    else
                    {
                        nuevoId = aus.Id;
                        VCFramework.NegocioMySQL.Institucion.Modificar(aus);
                    }
                    

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(aus);
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

    }
}