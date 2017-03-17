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
    public class ArchivoTricelController : ApiController
    {

        [System.Web.Http.AcceptVerbs("GET")]
        public HttpResponseMessage Get([FromUri]string TricelId)
        {

            //validaciones antes de ejecutar la llamada.
            if (TricelId == "0")
                throw new ArgumentNullException("TricelId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string tricelId = TricelId;
                int tricelIdBuscar = int.Parse(tricelId);

                VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
                votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                List<VCFramework.Entidad.ArchivosTricel> archivos = VCFramework.NegocioMySQL.ArchivosTricel.ObtenerArchivosPorTricelId(tricelIdBuscar, null);

                if (archivos != null && archivos.Count > 0)
                {
                    foreach (VCFramework.Entidad.ArchivosTricel tri in archivos)
                    {
                        string urlll = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Repositorio/";

                        VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        us.Id = tri.Id;
                        us.NombreCompleto = tri.RutaArchivo;

                        us.Url = urlll + us.NombreCompleto;
                        //us.UrlEliminar = "CrearModificarVotacion.html?id=" + tri.Id.ToString() + "&ELIMINAR=1";

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
            if (data.TricelId == 0)
                throw new ArgumentNullException("TricelId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string tricelId = data.TricelId;
                int tricelIdBuscar = int.Parse(tricelId);


                List<VCFramework.Entidad.ArchivosTricel> archivos = VCFramework.NegocioMySQL.ArchivosTricel.ObtenerArchivosPorTricelId(tricelIdBuscar, null);

                
                if (buscarNombreUsuario != "")
                {
                    if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
                        archivos = archivos.FindAll(p => p.Id == int.Parse(buscarNombreUsuario));
                }


                if (archivos != null && archivos.Count > 0)
                {
                    foreach (VCFramework.Entidad.ArchivosTricel tri in archivos)
                    {

                        tri.Url = "CrearModificarArchivo.html?id=" + tri.Id.ToString() + "&ELIMINAR=0";
                        tri.UrlEliminar = "CrearModificarVotacion.html?id=" + tri.Id.ToString() + "&ELIMINAR=1";
                    }
                    //establecimientos.Establecimientos = instituciones;
                }

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(archivos);
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