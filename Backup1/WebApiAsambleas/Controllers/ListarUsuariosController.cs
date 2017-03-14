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
    public class ListarUsuariosController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string instId)
        {
            //string iddd = Request..Content.Headers.GetValues("instId").ToString();
            //string Input = JsonConvert.SerializeObject(DynamicClass);

            //dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (instId == "")
                throw new ArgumentNullException("InstitucionId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                //string instId = data.InstId;
                int idInstitucion = int.Parse(instId);

                List<VCFramework.EntidadFuncional.UsuarioEnvoltorio> usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosEnvoltorio(idInstitucion);
                VCFramework.EntidadFuncional.proposalss proposals = new VCFramework.EntidadFuncional.proposalss();

                if (usuarios != null && usuarios.Count > 0)
                {
                    proposals.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();
                    proposals.proposals = usuarios;

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(proposals);
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
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string buscarNombreUsuario = "";
            if (data.BuscarId !=  null)
            {
                buscarNombreUsuario = data.BuscarId;
            }

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstitucionId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                int idInstitucion = int.Parse(instId);

                List<VCFramework.EntidadFuncional.UsuarioEnvoltorio> usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosEnvoltorio(idInstitucion);
                if (buscarNombreUsuario != "")
                    usuarios = usuarios.FindAll(p => p.NombreUsuario == buscarNombreUsuario);

                if (usuarios != null && usuarios.Count > 0)
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(usuarios);
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

        [System.Web.Http.AcceptVerbs("PUT")]
        public HttpResponseMessage Put()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("PUT: Test message")
            };
        }

    }
}