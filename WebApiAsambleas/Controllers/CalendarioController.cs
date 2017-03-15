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
    public class CalendarioController : ApiController
    {
        [System.Web.Http.AcceptVerbs("POST")]
        public HttpResponseMessage Post(dynamic DynamicClass)
        {

            string Input = JsonConvert.SerializeObject(DynamicClass);

            dynamic data = JObject.Parse(Input);

            //validaciones antes de ejecutar la llamada.
            if (data.InstId == 0)
                throw new ArgumentNullException("InstId");


            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                string instId = data.InstId;
                int instIdBuscar = int.Parse(instId);

                List<VCFramework.Entidad.Calendario> eventos = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstId(instIdBuscar);
                List<AsambleasWeb.Controllers.evento> lista = new List<AsambleasWeb.Controllers.evento>();
                if (eventos != null && eventos.Count > 0)
                {


                    foreach (VCFramework.Entidad.Calendario cal in eventos)
                    {
                        AsambleasWeb.Controllers.evento entidad = new AsambleasWeb.Controllers.evento();
                        entidad.allDay = false;
                        entidad.content = cal.Titulo;

                        entidad.annoIni = cal.FechaInicio.Year.ToString();
                        entidad.mesIni = (cal.FechaInicio.Month - 1).ToString();
                        entidad.diaIni = cal.FechaInicio.Day.ToString();
                        entidad.horaIni = cal.FechaInicio.Hour.ToString();
                        entidad.minutosIni = cal.FechaInicio.Minute.ToString();

                        entidad.annoTer = cal.FechaTermino.Year.ToString();
                        entidad.mesTer = (cal.FechaTermino.Month - 1).ToString();
                        entidad.diaTer = cal.FechaTermino.Day.ToString();
                        entidad.horaTer = cal.FechaTermino.Hour.ToString();
                        entidad.minutosTer = cal.FechaTermino.Minute.ToString();

                        entidad.id = cal.Id;
                        entidad.clientId = cal.Id;
                        //entidad.startDate = "Sun Mar 20 2017 12:00:00 GMT-0400 (Hora est. Sudamérica Pacífico)";
                        //entidad.endDate = "Sun Mar 20 2017 13:00:00 GMT-0400 (Hora est. Sudamérica Pacífico)";
                        //entidad.endDate = new DateTime(cal.FechaTermino.Year, cal.FechaTermino.Month, cal.FechaTermino.Day, cal.FechaTermino.Hour, cal.FechaTermino.Minute, cal.FechaTermino.Second, cal.FechaTermino.Millisecond);
                        //entidad.startDate = new DateTime(cal.FechaInicio.Year, cal.FechaInicio.Month, cal.FechaInicio.Day, cal.FechaInicio.Hour, cal.FechaInicio.Minute, cal.FechaInicio.Second, cal.FechaInicio.Millisecond);
                        //entidad.start = cal.FechaInicio.ToShortDateString() + " " + cal.FechaInicio.ToShortTimeString();
                        //entidad.end = cal.FechaTermino.ToShortDateString() + " " + cal.FechaTermino.ToShortTimeString();
                        lista.Add(entidad);
                    }


                    httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                    String JSON = JsonConvert.SerializeObject(lista);
                    httpResponse.Content = new StringContent(JSON);
                    httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(VCFramework.NegocioMySQL.Utiles.JSON_DOCTYPE);
                }
                else
                {
                    httpResponse = new HttpResponseMessage(HttpStatusCode.NoContent);
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

            string idUsuario = data.IdUsuario;
            if (idUsuario == null)
                idUsuario = "0";

            string instId = data.InstId;
            string titulo = data.Titulo;
            string fechaInicio = data.FechaInicio;
            string fechaTermino = data.FechaTermino;
            string esNuevo = data.EsNuevo;


            HttpResponseMessage httpResponse = new HttpResponseMessage();

            //buscamos el evento con todos los parametros para ver si es nuevo o antiguo
            bool nuevoRegistro = Convert.ToBoolean(esNuevo);
            int idNuevo = 0;



            try
            {
                VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();
				System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-CL");

                //si es nuevo registro se inserta y no se realiza busqueda
                if (nuevoRegistro)
                {
                    calendario.Asunto = titulo;
                    calendario.Descripcion = titulo;
                    calendario.Detalle = titulo;
                    calendario.Eliminado = 0;
                    calendario.Etiqueta = 1;
					calendario.FechaInicio = DateTime.Parse(fechaInicio, culture);
					calendario.FechaTermino = DateTime.Parse(fechaTermino, culture);
                    //calendario.FechaTermino = Convert.ToDateTime(fechaTermino);
                    calendario.InstId = int.Parse(instId);
                    calendario.Status = 1;
                    calendario.Tipo = 1;
                    calendario.Titulo = titulo;
                    calendario.Ubicacion = "";
                    calendario.Url = "";
                    //insertar
                    idNuevo = VCFramework.NegocioMySQL.Calendario.Insertar(calendario);
                    calendario.Id = idNuevo;
                }
                else
                {
                    List<VCFramework.Entidad.Calendario> calendarios = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstId(int.Parse(instId));
                    calendario = calendarios.Find(p => p.FechaInicio == DateTime.Parse(fechaInicio, culture) && p.FechaTermino == DateTime.Parse(fechaTermino, culture));
                    if (calendario != null && calendario.Id > 0)
                    {
                        calendario.Asunto = titulo;
                        calendario.Descripcion = titulo;
                        calendario.Detalle = titulo;
                        calendario.Titulo = titulo;
                        VCFramework.NegocioMySQL.Calendario.Modificar(calendario);

                    }
                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(calendario);
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

            string idUsuario = data.IdUsuario;
            if (idUsuario == null)
                idUsuario = "0";

            string instId = data.InstId;
            string titulo = data.Titulo;
            string fechaInicio = data.FechaInicio;
            string fechaTermino = data.FechaTermino;

            HttpResponseMessage httpResponse = new HttpResponseMessage();


            try
            {
                VCFramework.Entidad.Calendario calendario = new VCFramework.Entidad.Calendario();

                List<VCFramework.Entidad.Calendario> calendarios = VCFramework.NegocioMySQL.Calendario.ObtenerCalendarioPorInstId(int.Parse(instId));
                calendario = calendarios.Find(p => p.Titulo == titulo && p.FechaInicio == Convert.ToDateTime(fechaInicio) && p.FechaTermino == Convert.ToDateTime(fechaTermino));
                if (calendario != null && calendario.Id > 0)
                {
                    calendario.Eliminado = 1;
                    VCFramework.NegocioMySQL.Calendario.Modificar(calendario);

                }


                httpResponse = new HttpResponseMessage(HttpStatusCode.OK);
                String JSON = JsonConvert.SerializeObject(calendario);
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

    public class evento
    {

        public bool allDay { get; set; }

        public string content { get; set; }

        public string annoIni { get; set; }
        public string mesIni { get; set; }
        public string diaIni { get; set; }
        public string horaIni { get; set; }

        public string minutosIni { get; set; }

        public string annoTer { get; set; }
        public string mesTer { get; set; }
        public string diaTer { get; set; }
        public string horaTer { get; set; }
        public string minutosTer { get; set; }

        public int clientId { get; set; }
        public int id { get; set; }

    }

}