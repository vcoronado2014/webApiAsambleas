﻿using System;
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

namespace AsambleasWeb.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class VotacionController : ApiController
	{
		[AcceptVerbs("OPTIONS")]
		public void Options()
		{ }

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
				string instId = data.InstId;
				int instIdBuscar = int.Parse(instId);

				VCFramework.EntidadFuncional.proposalss votaciones = new VCFramework.EntidadFuncional.proposalss();
				votaciones.proposals = new List<VCFramework.EntidadFuncional.UsuarioEnvoltorio>();

				List<VCFramework.Entidad.Tricel> triceles = VCFramework.NegocioMySQL.Tricel.ObtenerTricelPorInstIdTodos(instIdBuscar);

				if (buscarNombreUsuario != "")
				{
					if (VCFramework.NegocioMySQL.Utiles.IsNumeric(buscarNombreUsuario))
						triceles = triceles.FindAll(p => p.Id == int.Parse(buscarNombreUsuario));
					else
						triceles = triceles.FindAll(p => p.Nombre == buscarNombreUsuario);
				}


				if (triceles != null && triceles.Count > 0)
				{
					foreach (VCFramework.Entidad.Tricel tri in triceles)
					{
						VCFramework.EntidadFuncional.UsuarioEnvoltorio us = new VCFramework.EntidadFuncional.UsuarioEnvoltorio();
						us.Id = tri.Id;
						us.NombreCompleto = tri.Objetivo;
						us.NombreUsuario = tri.Nombre;
						us.OtroUno = tri.FechaInicio.ToShortDateString();
						us.OtroDos = tri.FechaTermino.ToShortDateString();
						us.OtroTres = tri.FechaCreacion.ToShortDateString();
						//cantidad de listas asociadas al tricel
						List<VCFramework.Entidad.ListaTricel> listas = VCFramework.NegocioMySQL.ListaTricel.ObtenerListaPorInstId(instIdBuscar);
						us.OtroCuatro = listas.Count.ToString();
						//us.UrlDocumento = insti.UrlDocumento;

						us.Url = "CrearModificarVotacion.aspx?id=" + us.Id.ToString() + "&ELIMINAR=0";
						us.UrlEliminar = "CrearModificarVotacion.aspx?id=" + us.Id.ToString() + "&ELIMINAR=1";
						//string urlll = Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/Repositorio/";
						//if (us.UrlDocumento != null && us.UrlDocumento != "" && us.UrlDocumento != "#")
						//    us.Rol = urlll + us.UrlDocumento;
						//else
						//    us.Rol = "#";


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

	}
}