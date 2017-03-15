using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Net.Http.Headers;


namespace WebApiAsambleas
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration  config)
		{
			// Web API configuration and services
			// Configure Web API to use only bearer token authentication.
			config.SuppressDefaultHostAuthentication();
			config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

			// Web API routes
			config.MapHttpAttributeRoutes();


			#region  ObtenerLogin
			config.Routes.MapHttpRoute(
				name: "Login",
				routeTemplate: "api/Login",
				defaults: new
				{
					controller = "Login"
				}
			);
			#endregion

			#region  ObtenerUsuarios
			config.Routes.MapHttpRoute(
				name: "ObtenerUsuarios",
				routeTemplate: "api/ObtenerUsuarios",
				defaults: new
				{
					controller = "ObtenerUsuarios"
				}
			);
			#endregion

			#region  ListarUsuarios
			config.Routes.MapHttpRoute(
				name: "ListarUsuarios",
				routeTemplate: "api/ListarUsuarios",
				defaults: new
				{
					controller = "ListarUsuarios"
				}
			);
			#endregion

			#region  ObtenerUsuario
			config.Routes.MapHttpRoute(
				name: "ObtenerUsuario",
				routeTemplate: "api/ObtenerUsuario",
				defaults: new
				{
					controller = "ObtenerUsuario"
				}
			);
			#endregion

			#region  ObtenerRegiones
			config.Routes.MapHttpRoute(
				name: "ObtenerRegiones",
				routeTemplate: "api/ObtenerRegiones",
				defaults: new
				{
					controller = "ObtenerRegiones"
				}
			);
			#endregion

			#region  ObtenerComunas
			config.Routes.MapHttpRoute(
				name: "ObtenerComunas",
				routeTemplate: "api/ObtenerComunas",
				defaults: new
				{
					controller = "ObtenerComunas"
				}
			);
			#endregion

			#region  ObtenerRoles
			config.Routes.MapHttpRoute(
				name: "ObtenerRoles",
				routeTemplate: "api/ObtenerRoles",
				defaults: new
				{
					controller = "ObtenerRoles"
				}
			);
			#endregion


			#region  Institucion
			config.Routes.MapHttpRoute(
				name: "Institucion",
				routeTemplate: "api/Institucion",
				defaults: new
				{
					controller = "Institucion"
				}
			);
			#endregion

			#region  Rendicion
			config.Routes.MapHttpRoute(
				name: "Rendicion",
				routeTemplate: "api/Rendicion",
				defaults: new
				{
					controller = "Rendicion"
				}
			);
			#endregion

			#region  Grafico
			config.Routes.MapHttpRoute(
				name: "Grafico",
				routeTemplate: "api/Grafico",
				defaults: new
				{
					controller = "Grafico"
				}
			);
			#endregion

			#region  File
			config.Routes.MapHttpRoute(
				name: "File",
				routeTemplate: "api/File",
				defaults: new
				{
					controller = "File"
				}
			);
			#endregion

			#region  FileDocumento
			config.Routes.MapHttpRoute(
				name: "FileDocumento",
				routeTemplate: "api/FileDocumento",
				defaults: new
				{
					controller = "FileDocumento"
				}
			);
			#endregion

			#region  FileNuevo
			config.Routes.MapHttpRoute(
				name: "FileNuevo",
				routeTemplate: "api/FileNuevo",
				defaults: new
				{
					controller = "FileNuevo"
				}
			);
			#endregion

			#region  Calendario
			config.Routes.MapHttpRoute(
				name: "Calendario",
				routeTemplate: "api/Calendario",
				defaults: new
				{
					controller = "Calendario"
				}
			);
			#endregion


			#region  Votacion
			config.Routes.MapHttpRoute(
				name: "Votacion",
				routeTemplate: "api/Votacion",
				defaults: new
				{
					controller = "Votacion"
				}
			);
            #endregion

            #region  ArchivoTricel
            config.Routes.MapHttpRoute(
                name: "ArchivoTricel",
                routeTemplate: "api/ArchivoTricel",
                defaults: new
                {
                    controller = "ArchivoTricel"
                }
            );
            #endregion

            config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);


		}		
	}
}
