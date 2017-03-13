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
    public class ObtenerUsuarioController : ApiController
    {
        [AcceptVerbs("OPTIONS")]
        public void Options()
        { }

        [AgendaExceptionFilter]
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return null;
        }

        [System.Web.Http.AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.IdUsuario == 0)
                throw new ArgumentNullException("Id");


            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string idUsuario = data.IdUsuario;
                int idUsuarioBuscar = int.Parse(idUsuario);
                VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(idUsuarioBuscar);

                if (aus != null && aus.Id > 0)
                {
                    aus.Eliminado = 1;
                    VCFramework.NegocioMySQL.AutentificacionUsuario.ModificarAus(aus);

                    VCFramework.Entidad.Persona persona = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(idUsuarioBuscar);

                    if (persona != null && persona.Id > 0)
                    {
                        persona.Eliminado = 1;
                        VCFramework.NegocioMySQL.Persona.ModificarUsuario(persona);

                        List<VCFramework.EntidadFuncional.UsuarioEnvoltorio> usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ListarUsuariosEnvoltorio(aus.InstId);
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
                        
                    }
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

            //validaciones antes de ejecutar la llamada.
            if (data.IdUsuario == 0)
                throw new ArgumentNullException("IdUsuario");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string idUsuario = data.IdUsuario;
                int idUsuarioBuscar = int.Parse(idUsuario);

                VCFramework.EntidadFuncional.UsuarioFuncional usuarios = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuarioFuncional(idUsuarioBuscar);
                if (usuarios != null && usuarios.AutentificacionUsuario != null)
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
        public HttpResponseMessage Put(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            string idUsuario = data.Id;
            if (idUsuario == null)
                idUsuario = "0";
            int idUsuarioBuscar = int.Parse(idUsuario);


            //validaciones antes de ejecutar la llamada.
            VCFramework.Entidad.AutentificacionUsuario aus = VCFramework.NegocioMySQL.AutentificacionUsuario.ObtenerUsuario(idUsuarioBuscar);

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                string correo = data.Correo;
                string nombres = data.Nombres;
                string primerApellido = data.PrimerApellido;
                string segundoApellido = data.SegundoApellido;
                string rut = data.Rut;
                string telefono = data.Telefono;
                string idRegion = data.IdRegion;
                string idComuna = data.IdComuna;
                string direccion = data.Direccion;
                string idRol = data.IdRol;
                string nombreUsuario = data.NombreUsuario;
                string password = data.Password;
                string instId = data.InstId;
                if (aus == null)
                    aus = new VCFramework.Entidad.AutentificacionUsuario();
                
                if (aus != null)
                {
                    int nuevoId = 0;

                    if (aus.Id == 0)
                    {
                        aus.NombreUsuario = nombreUsuario;
                    }
                    aus.CorreoElectronico = correo;
                    aus.RolId = int.Parse(idRol);
                    aus.InstId = int.Parse(instId);
                    aus.EsVigente = 1;
                    if (password != "")
                        aus.Password = VCFramework.NegocioMySQL.Utiles.Encriptar(password);
                    if (aus.Id == 0)
                        nuevoId = VCFramework.NegocioMySQL.AutentificacionUsuario.InsertarAus(aus);
                    else
                    {
                        nuevoId = aus.Id;
                        VCFramework.NegocioMySQL.AutentificacionUsuario.ModificarAus(aus);
                    }
                    VCFramework.Entidad.Persona persona = VCFramework.NegocioMySQL.Persona.ObtenerPersonaPorUsuId(idUsuarioBuscar);

                    if (persona == null)
                        persona = new VCFramework.Entidad.Persona();

                    if (persona != null)
                    {
                        persona.ApellidoMaterno = segundoApellido;
                        persona.ApellidoPaterno = primerApellido;
                        persona.ComId = int.Parse( idComuna);
                        persona.DireccionCompleta = direccion;
                        persona.Nombres = nombres;
                        persona.RegId = int.Parse(idRegion);
                        persona.Rut = rut;
                        persona.Telefonos = telefono;
                        persona.InstId = int.Parse(instId);
                        persona.UsuId = nuevoId;
                        VCFramework.NegocioMySQL.Persona.ModificarUsuario(persona);

                        httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                        String JSON = JsonConvert.SerializeObject(persona);
                        httpResponse.Content = new StringContent(JSON);
                        httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);

                    }


                }
            }
            catch(Exception ex)
            {
                httpResponse = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                throw ex;
            }

            return httpResponse;
        }
    }
}