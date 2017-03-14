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
using System.Collections.Specialized;
using System.Reflection;

namespace AsambleasWeb.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FileNuevoController : ApiController
    {
        const string UploadDirectory = "~/Repositorio/";
        const string UploadDirectoryImg = "~/img/";
        [HttpPost]
        public HttpResponseMessage CreateContestEntry()
        {
            VCFramework.Entidad.DocumentosUsuario entidad = new VCFramework.Entidad.DocumentosUsuario();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];
                string usuId = HttpContext.Current.Request.Form["UsuId"];
                string instId = HttpContext.Current.Request.Form["InstId"];

                if (httpPostedFile != null)
                {
                    //guardamos el registro
                    #region tratamiento del archivo
                    string resultExtension = Path.GetExtension(httpPostedFile.FileName);
                    string resultFileName = Path.ChangeExtension(httpPostedFile.FileName, resultExtension);
                    string resultFileUrl = UploadDirectory + resultFileName;
                    string fechaSubida = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    string urlExtension = "";
                    switch (resultExtension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            urlExtension = UploadDirectoryImg + "jpeg.png";
                            break;
                        case ".gif":
                            urlExtension = UploadDirectoryImg + "gif.png";
                            break;
                        case ".png":
                            urlExtension = UploadDirectoryImg + "png.png";
                            break;
                        case ".doc":
                        case ".docx":
                            urlExtension = UploadDirectoryImg + "word.png";
                            break;
                        case ".xls":
                        case ".xlsx":
                            urlExtension = UploadDirectoryImg + "excel.png";
                            break;
                        case ".pdf":
                            urlExtension = UploadDirectoryImg + "pdf.png";
                            break;
                    }

                    string name = httpPostedFile.FileName;
                    long sizeInKilobytes = httpPostedFile.ContentLength / 1024;
                    string sizeText = sizeInKilobytes.ToString() + " KB";
                    #endregion

                    //guardamos el registro
                    #region guardado registro
                    entidad.Borrado = false;
                    entidad.Eliminado = 0;
                    entidad.Extension = resultExtension;
                    entidad.FechaSubida = fechaSubida;
                    entidad.Id = 0;
                    entidad.InstId = int.Parse(instId);
                    entidad.Modificado = false;
                    entidad.NombreArchivo = httpPostedFile.FileName;
                    entidad.Nuevo = true;
                    entidad.Tamano = int.Parse(sizeInKilobytes.ToString());
                    entidad.UsuId = int.Parse(usuId);
                    entidad.Url = "";
                    VCFramework.NegocioMySQL.DocumentosUsuario.Insertar(entidad);
                    #endregion

                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Repositorio"), httpPostedFile.FileName);

                    httpPostedFile.SaveAs(fileSavePath);
                }

                #region retorno
                List<VCFramework.Entidad.DocumentosUsuario> documentos = VCFramework.NegocioMySQL.DocumentosUsuario.ObtenerDocumentosPorInstIdNuevo(int.Parse(instId));
                VCFramework.EntidadFuncional.proposalss documentosE = new VCFramework.EntidadFuncional.proposalss();
                documentosE.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

                if (documentos != null && documentos.Count > 0)
                {
                    foreach (VCFramework.Entidad.DocumentosUsuario doc in documentos)
                    {
                        VCFramework.EntidadFuncional.UsuarioEnvoltorio entidadS = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
                        entidadS.Id = doc.Id;
                        entidadS.NombreCompleto = doc.NombreArchivo;
                        entidadS.NombreUsuario = doc.FechaSubida;
                        entidadS.OtroUno = doc.Tamano.ToString();

                        string urlll = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Repositorio/";
                        if (doc.NombreArchivo != null && doc.NombreArchivo != "")
                            entidadS.Url = urlll + doc.NombreArchivo;
                        else
                            entidadS.Url = "#";
                        documentosE.proposals.Add(entidadS);
                    }
                }
                #endregion

                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(documentosE);
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

                VCFramework.Entidad.DocumentosUsuario documento = VCFramework.NegocioMySQL.DocumentosUsuario.ObtenerDocumentoId(idBuscar);


                if (documento != null)
                {
                    //eliminamos el registro
                    documento.Eliminado = 1;
                    VCFramework.NegocioMySQL.DocumentosUsuario.Modificar(documento);

                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Repositorio"), documento.NombreArchivo);

                    if (File.Exists(fileSavePath))
                        File.Delete(fileSavePath);

                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(documento);
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


    }
}