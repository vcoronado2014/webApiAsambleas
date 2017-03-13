using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Security.Cryptography;

namespace VCFramework.NegocioMySQL
{
    public class Utiles
    {
        public const string HTML_DOCTYPE = "text/html";
        public const string JSON_DOCTYPE = "application/json";
        public const string XML_DOCTYPE = "application/xml";

        public static bool IsNumeric(object Expression)
        {
            double retNum;

            bool isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        //public static byte[] IV = Encoding.ASCII.GetBytes("Devjoker7.37hAES");

        //public static string Encripta(string Cadena, string Password)
        //{
        //    byte[] Clave = Encoding.ASCII.GetBytes(Password);
        //    byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
        //    byte[] encripted;
        //    RijndaelManaged cripto = new RijndaelManaged();
        //    using (MemoryStream ms = new MemoryStream(inputBytes.Length))
        //    {
        //        using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(Clave, IV), CryptoStreamMode.Write))
        //        {
        //            objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
        //            objCryptoStream.FlushFinalBlock();
        //            objCryptoStream.Close();
        //        }
        //        encripted = ms.ToArray();
        //    }
        //    return Convert.ToBase64String(encripted);
        //}



        //public static string Desencripta(string Cadena, string Password)
        //{
        //    byte[] Clave = Encoding.ASCII.GetBytes(Password);
        //    byte[] inputBytes = Convert.FromBase64String(Cadena);
        //    byte[] resultBytes = new byte[inputBytes.Length];
        //    string textoLimpio = String.Empty;
        //    RijndaelManaged cripto = new RijndaelManaged();
        //    using (MemoryStream ms = new MemoryStream(inputBytes))
        //    {
        //        using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(Clave, IV), CryptoStreamMode.Read))
        //        {
        //            using (StreamReader sr = new StreamReader(objCryptoStream, true))
        //            {
        //                textoLimpio = sr.ReadToEnd();
        //            }
        //        }
        //    }
        //    return textoLimpio;
        //}

        // Connection String
        //public const string ConnStr =
        //   "Driver={MySQL ODBC 3.51 Driver};Server=localhost;Database=bdcolegios_mysql;uid=root;pwd=co2008;option=3";
        //public const string ConnStr =
        //   "Driver={MySQL ODBC 5.1 Driver};Server=MYSQL5011.Smarterasp.net;Database=db_9dac90_cole;User=9dac90_cole;Password=antonia2006;Option=3;";

        public static string ConnStr()
        {
            string cns = "Driver={MySQL ODBC 5.1 Driver};Server=MYSQL5011.Smarterasp.net;Database=db_9dac90_cole;User=9dac90_cole;Password=antonia2006;Option=3;";
            if (System.Configuration.ConfigurationManager.ConnectionStrings["BDColegioSql"].ConnectionString != null)
                cns = System.Configuration.ConfigurationManager.ConnectionStrings["BDColegioSql"].ConnectionString;
            return cns;
        }

        public const string CNS = "BDColegioSql";

        public static string NombreBaseDatos()
        {
            string retorno = "'db_9dac90_cole'";

            if (System.Configuration.ConfigurationManager.AppSettings["NOMBRE_BD"] != null )
            {
                retorno = "'" + System.Configuration.ConfigurationManager.AppSettings["NOMBRE_BD"].ToString() + "'";
            }

            return retorno;
        }
        public static string SMTP()
        {
            string retorno = "smtp.gmail.com";
            if (System.Configuration.ConfigurationManager.AppSettings["SMTP"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["SMTP"].ToString();
            }

            return retorno;
        }
        public static string PUERTO_SMTP()
        {
            string retorno = "587";
            if (System.Configuration.ConfigurationManager.AppSettings["PUERTO_SMTP"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["PUERTO_SMTP"].ToString();
            }

            return retorno;
        }
        public static string NOMBRE_SERVIDOR_CORREO()
        {
            string retorno = "vcoronado.alarcon@gmail.com";
            if (System.Configuration.ConfigurationManager.AppSettings["NOMBRE_SERVIDOR_CORREO"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["NOMBRE_SERVIDOR_CORREO"].ToString();
            }

            return retorno;
        }
        public static string CLAVE_SERVIDOR_CORREO()
        {
            string retorno = "antonia2005";
            if (System.Configuration.ConfigurationManager.AppSettings["CLAVE_SERVIDOR_CORREO"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["CLAVE_SERVIDOR_CORREO"].ToString();
            }

            return retorno;
        }
        public static string HABILITA_COPIA_ADMIN1()
        {
            string retorno = "0";
            if (System.Configuration.ConfigurationManager.AppSettings["HABILITA_COPIA_ADMIN1"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["HABILITA_COPIA_ADMIN1"].ToString();
            }

            return retorno;
        }
        public static string ENVIA_CORREO_EVENTO(int instId)
        {
            string retorno = "0";
            //primero validamos la base de datos 
            Entidad.ConfiguracionInstitucion config = NegocioMySQL.ConfiguracionInstitucion.ObtenerConfiguracionPorInstId(instId);
            if (config != null)
            {
                retorno = config.EnviaDocumentos.ToString();
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ENVIA_CORREO_EVENTO"] != null)
                {
                    retorno = System.Configuration.ConfigurationManager.AppSettings["ENVIA_CORREO_EVENTO"].ToString();
                }
            }
            return retorno;
        }
        public static string HABILITA_COPIA_ADMIN2()
        {
            string retorno = "0";
            if (System.Configuration.ConfigurationManager.AppSettings["HABILITA_COPIA_ADMIN2"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["HABILITA_COPIA_ADMIN2"].ToString();
            }

            return retorno;
        }
        public static string COPIA_ADMIN_1()
        {
            string retorno = "turk182@gmail.com";
            if (System.Configuration.ConfigurationManager.AppSettings["COPIA_ADMIN_1"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["COPIA_ADMIN_1"].ToString();
            }

            return retorno;
        }
        public static string COPIA_ADMIN_2()
        {
            string retorno = "vcoronado.alarcon@gmail.com";
            if (System.Configuration.ConfigurationManager.AppSettings["COPIA_ADMIN_2"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["COPIA_ADMIN_2"].ToString();
            }

            return retorno;
        }
        public static string ENABLE_SSL()
        {
            string retorno = "0";
            if (System.Configuration.ConfigurationManager.AppSettings["ENABLE_SSL"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["ENABLE_SSL"].ToString();
            }

            return retorno;
        }
        public static System.Net.Mail.MailMessage ConstruyeMensajeContacto(string nombre, string telefono, string email, string motivo)
        {

            System.Net.Mail.MailMessage sms = new System.Net.Mail.MailMessage();
            sms.Subject = "Formulario Contacto CPAS";
            sms.To.Add(Utiles.NOMBRE_SERVIDOR_CORREO());
            string habilitaCopiaAdmin1 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin1 == "1")
            {
                sms.To.Add(COPIA_ADMIN_1());
            }
            string habilitaCopiaAdmin2 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin2 == "1")
            {
                sms.To.Add(COPIA_ADMIN_2());
            }
            sms.From = new System.Net.Mail.MailAddress("contacto@cpas.cl", "CPAS");
            sms.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");

            string htmlMensaje = ObtenerMensajeXML("Contacto", true);
            if (htmlMensaje != null)
            {
                htmlMensaje = htmlMensaje.Replace("{Nombre}", nombre).Replace("{Telefono}", telefono).Replace("{Email}", email).Replace("{Motivo}", motivo);
                sb.Append(htmlMensaje);
            }
            else
            {
                sb.AppendFormat("{0}, Teléfono Contacto {1}, correo electónico {2}:<br />", nombre, telefono, email);
                sb.AppendFormat("Desea contactarse mediante el formulario de contacto, donde ha ingresado el siguiente motivo: {0}<br />", motivo);
                sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ******");

            }
            sb.Append("</html>");
            sms.Body = sb.ToString();
            return sms;
        }

        public static System.Net.Mail.MailMessage ConstruyeMensajeCrearProyecto(string nombreInstitucion, string nombreProyecto, List<string> correos, bool esNuevo)
        {
            System.Net.Mail.MailMessage sms = new System.Net.Mail.MailMessage();
            sms.Subject = "Creación de Proyecto en CPAS";
            string habilitaCopiaAdmin1 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin1 == "1")
            {
                sms.To.Add(COPIA_ADMIN_1());
            }
            string habilitaCopiaAdmin2 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin2 == "1")
            {
                sms.To.Add(COPIA_ADMIN_2());
            }
            //recorremos la lista de usuarios de la institución
            if (correos != null && correos.Count > 0)
            {
                foreach(string s in correos)
                {
                    sms.To.Add(s);
                }
            }


            sms.From = new System.Net.Mail.MailAddress("contacto@cpas.cl", "CPAS");
            sms.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            if (esNuevo)
            {
                string htmlMensaje = ObtenerMensajeXML("Proyecto", true);
                if (htmlMensaje != null)
                {
                    htmlMensaje = htmlMensaje.Replace("{NombreItem}", nombreProyecto).Replace("{NombreInstitucion}", nombreInstitucion).Replace("{Url}", ObtenerUrl());
                    sb.Append(htmlMensaje);
                }
                else
                {
                    sb.AppendFormat("Se ha creado el Proyecto <strong>{0}</strong>, para el Establecimiento {1}:<br />", nombreProyecto, nombreInstitucion);
                    sb.Append("Estimado Usuario CPAS, ha sido creado un Proyecto para su establecimiento, ingrese a www.cpas.cl para poder verlo.");
                    sb.Append("<br />");
                    sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ****** <br />");
                }
            }
            else
            {
                string htmlMensaje = ObtenerMensajeXML("Proyecto", false);
                if (htmlMensaje != null)
                {
                    htmlMensaje = htmlMensaje.Replace("{NombreItem}", nombreProyecto).Replace("{NombreInstitucion}", nombreInstitucion).Replace("{Url}", ObtenerUrl());
                    sb.Append(htmlMensaje);
                }
                else
                {
                    sb.AppendFormat("Ha sido modificado el Proyecto <strong>{0}</strong>, para el Establecimiento {1}:<br />", nombreProyecto, nombreInstitucion);
                    sb.Append("Estimado Usuario CPAS, ha sido Modificado un Proyecto para su establecimiento, ingrese a www.cpas.cl para poder verlo.");
                    sb.Append("<br />");
                    sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ****** <br />");
                }
            }
            
            sb.Append("</html>");
            sms.Body = sb.ToString();
            return sms;
        }

        public static System.Net.Mail.MailMessage ConstruyeMensajeAgregarDocumento(string nombreInstitucion, string nombreDocumento, List<string> correos, bool esNuevo)
        {
            System.Net.Mail.MailMessage sms = new System.Net.Mail.MailMessage();
            sms.Subject = "Documento en CPAS";
            string habilitaCopiaAdmin1 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin1 == "1")
            {
                sms.To.Add(COPIA_ADMIN_1());
            }
            string habilitaCopiaAdmin2 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin2 == "1")
            {
                sms.To.Add(COPIA_ADMIN_2());
            }
            //recorremos la lista de usuarios de la institución
            if (correos != null && correos.Count > 0)
            {
                foreach (string s in correos)
                {
                    sms.To.Add(s);
                }
            }


            sms.From = new System.Net.Mail.MailAddress("contacto@cpas.cl", "CPAS");
            sms.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            if (esNuevo)
            {
                string htmlMensaje = ObtenerMensajeXML("Documento", true);
                if (htmlMensaje != null)
                {
                    htmlMensaje =  htmlMensaje.Replace("{NombreItem}", EntregaNombreArchivo(nombreDocumento)).Replace("{NombreInstitucion}", nombreInstitucion).Replace("{Url}", ObtenerUrl());
                    sb.Append(htmlMensaje);
                }
                else
                {
                    sb.AppendFormat("Se ha subido el documento <strong>{0}</strong>, para el Establecimiento {1}:<br />", nombreDocumento, nombreInstitucion);
                    sb.Append("Estimado Usuario CPAS, ha sido subido un documento para su establecimiento, ingrese a www.cpas.cl para poder verlo.");
                }
            }
            else
            {
                string htmlMensaje = ObtenerMensajeXML("Documento", false);
                if (htmlMensaje != null)
                {
                    htmlMensaje = htmlMensaje.Replace("{NombreItem}", EntregaNombreArchivo(nombreDocumento)).Replace("{NombreInstitucion}", nombreInstitucion).Replace("{Url}", ObtenerUrl());
                    sb.Append(htmlMensaje);
                }
                else
                {
                    sb.AppendFormat("Ha sido modificado el documento <strong>{0}</strong>, para el Establecimiento {1}:<br />", nombreDocumento, nombreInstitucion);
                    sb.Append("Estimado Usuario CPAS, ha sido Modificado un documento para su establecimiento, ingrese a www.cpas.cl para poder verlo.");
                    sb.Append("<br />");
                    sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ****** <br />");
                }
            }
            
            sb.Append("</html>");
            sms.Body = sb.ToString();
            return sms;
        }

        public static System.Net.Mail.MailMessage ConstruyeMensajeCrearRendicion(string nombreInstitucion, string nombreDocumento, List<string> correos, bool esNuevo)
        {
            System.Net.Mail.MailMessage sms = new System.Net.Mail.MailMessage();
            sms.Subject = "Rendición en CPAS";
            string habilitaCopiaAdmin1 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin1 == "1")
            {
                sms.To.Add(COPIA_ADMIN_1());
            }
            string habilitaCopiaAdmin2 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin2 == "1")
            {
                sms.To.Add(COPIA_ADMIN_2());
            }
            //recorremos la lista de usuarios de la institución
            if (correos != null && correos.Count > 0)
            {
                foreach (string s in correos)
                {
                    sms.To.Add(s);
                }
            }


            sms.From = new System.Net.Mail.MailAddress("contacto@cpas.cl", "CPAS");
            sms.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            if (esNuevo)
            {
                string htmlMensaje = ObtenerMensajeXML("Rendicion", true);
                if (htmlMensaje != null)
                {
                    htmlMensaje = htmlMensaje.Replace("{NombreItem}", EntregaNombreArchivo(nombreDocumento)).Replace("{NombreInstitucion}", nombreInstitucion).Replace("{Url}", ObtenerUrl());
                    sb.Append(htmlMensaje);
                }
                else
                {
                    sb.AppendFormat("Se ha agregado una rendición  <strong>{0}</strong>, para el Establecimiento {1}:<br />", nombreDocumento, nombreInstitucion);
                    sb.Append("Estimado Usuario CPAS, ha sido agrgada una rendición para su establecimiento, ingrese a www.cpas.cl para poder verlo.");
                    sb.Append("<br />");
                    sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ****** <br />");
                }
            }
            else
            {
                string htmlMensaje = ObtenerMensajeXML("Rendicion", false);
                if (htmlMensaje != null)
                {
                    htmlMensaje = htmlMensaje.Replace("{NombreItem}", EntregaNombreArchivo(nombreDocumento)).Replace("{NombreInstitucion}", nombreInstitucion).Replace("{Url}", ObtenerUrl());
                    sb.Append(htmlMensaje);
                }
                else
                {
                    sb.AppendFormat("Ha sido modificado una rendición <strong>{0}</strong>, para el Establecimiento {1}:<br />", nombreDocumento, nombreInstitucion);
                    sb.Append("Estimado Usuario CPAS, ha sido Modificada una rendición para su establecimiento, ingrese a www.cpas.cl para poder verlo.");
                    sb.Append("<br />");
                    sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ****** <br />");
                }
            }
            
            sb.Append("</html>");
            sms.Body = sb.ToString();
            return sms;
        }
        public static System.Net.Mail.MailMessage ConstruyeMensajeRecuperarClave(string nombre, string clave, string email)
        {

            System.Net.Mail.MailMessage sms = new System.Net.Mail.MailMessage();
            sms.Subject = "Recuperación Clave CPAS";
            sms.To.Add(email);
            
            sms.From = new System.Net.Mail.MailAddress("contacto@cpas.cl", "CPAS");
            sms.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");

            string htmlMensaje = ObtenerMensajeXML("RecuperarClave", true);
            if (htmlMensaje != null)
            {
                htmlMensaje = htmlMensaje.Replace("{NombreItem}", nombre).Replace("{Clave}", clave).Replace("{Url}", ObtenerUrl());
                sb.Append(htmlMensaje);
            }
            else
            {
                sb.AppendFormat("Estimado Usuario {0}:<br />", nombre);
                sb.AppendFormat("Su password fué recuperada con exito: {0}<br />", clave);
                sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ******");
            }

            sb.Append("</html>");
            sms.Body = sb.ToString();
            return sms;
        }
        public static System.Net.Mail.MailMessage ConstruyeMensajeCambiarClave(string nombre, string clave, string email)
        {

            System.Net.Mail.MailMessage sms = new System.Net.Mail.MailMessage();
            sms.Subject = "Cambio Clave CPAS";
            sms.To.Add(email);

            sms.From = new System.Net.Mail.MailAddress("contacto@cpas.cl", "CPAS");
            sms.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");


            string htmlMensaje = ObtenerMensajeXML("RecuperarClave", false);
            if (htmlMensaje != null)
            {
                htmlMensaje = htmlMensaje.Replace("{NombreItem}", nombre).Replace("{Clave}", clave).Replace("{Url}", ObtenerUrl());
                sb.Append(htmlMensaje);
            }
            else
            {
                sb.AppendFormat("Estimado Usuario {0}:<br />", nombre);
                sb.AppendFormat("Su password fué cambiada con exito: {0}<br />", clave);
                sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ******");
            }



            sb.Append("</html>");
            sms.Body = sb.ToString();
            return sms;
        }
        public static System.Net.Mail.MailMessage ConstruyeMensajeCreacionMasiva(string nombre, string clave, string email)
        {

            System.Net.Mail.MailMessage sms = new System.Net.Mail.MailMessage();
            sms.Subject = "Creación Usuario CPAS";
            sms.To.Add(email);

            sms.From = new System.Net.Mail.MailAddress("contacto@cpas.cl", "CPAS");
            sms.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            string htmlMensaje = ObtenerMensajeXML("CrearUsuario", true);
            if (htmlMensaje != null)
            {
                htmlMensaje = htmlMensaje.Replace("{NombreUsuario}", nombre).Replace("{Clave}", clave).Replace("{Url}", ObtenerUrl());
                sb.Append(htmlMensaje);
            }
            else
            {

                sb.AppendFormat("Estimado Usuario {0} se ha creado una cuenta de acceso a CPAS,<br />", nombre);
                sb.AppendFormat("Su nueva password fué es: {0}<br />", clave);
                sb.Append("***** Puede acceder al Sistema y cambiarla cuando lo estime conveniente ******");
                sb.Append("<br />");
                sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ******");
            }

            sb.Append("</html>");
            sms.Body = sb.ToString();
            return sms;
        }

        public static string ENVIA_DOCUMENTOS(int instId)
        {
            string retorno = "0";
            //primero validamos la base de datos 
            Entidad.ConfiguracionInstitucion config = NegocioMySQL.ConfiguracionInstitucion.ObtenerConfiguracionPorInstId(instId);
            if (config != null)
            {
                retorno = config.EnviaDocumentos.ToString();
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ENVIA_DOCUMENTOS"] != null)
                {
                    retorno = System.Configuration.ConfigurationManager.AppSettings["ENVIA_DOCUMENTOS"].ToString();
                }
            }

            return retorno;
        }
        public static string ENVIA_PROYECTOS(int instId)
        {
            string retorno = "0";
            //primero validamos la base de datos 
            Entidad.ConfiguracionInstitucion config = NegocioMySQL.ConfiguracionInstitucion.ObtenerConfiguracionPorInstId(instId);
            if (config != null)
            {
                retorno = config.EnviaProyectos.ToString();
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ENVIA_PROYECTOS"] != null)
                {
                    retorno = System.Configuration.ConfigurationManager.AppSettings["ENVIA_PROYECTOS"].ToString();
                }
            }

            return retorno;
        }
        public static string ENVIA_RENDICIONES(int instId)
        {
            string retorno = "0";
            //primero validamos la base de datos 
            Entidad.ConfiguracionInstitucion config = NegocioMySQL.ConfiguracionInstitucion.ObtenerConfiguracionPorInstId(instId);
            if (config != null)
            {
                retorno = config.EnviaRendiciones.ToString();
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings["ENVIA_RENDICIONES"] != null)
                {
                    retorno = System.Configuration.ConfigurationManager.AppSettings["ENVIA_RENDICIONES"].ToString();
                }
            }

            return retorno;
        }
        public static string ENVIA_CORREO_CREACION_MASIVA()
        {
            string retorno = "0";
            if (System.Configuration.ConfigurationManager.AppSettings["ENVIA_CORREO_CREACION_MASIVA"] != null)
            {
                retorno = System.Configuration.ConfigurationManager.AppSettings["ENVIA_CORREO_CREACION_MASIVA"].ToString();
            }

            return retorno;
        }
        public static string ConstruyeFecha(DateTime fecha)
        {
            string retorno = "";
            string dia = "";
            string mes = "";
            string anno = "";
            string hora = "";
            string minutos = "";
            string segundos = "";


            if (fecha.Day < 10)
                dia = "0" + fecha.Day.ToString();
            else
                dia = fecha.Day.ToString();

            if (fecha.Month < 10)
                mes = "0" + fecha.Month.ToString();
            else
                mes = fecha.Month.ToString();

            if (fecha.Hour < 10)
                hora = "0" + fecha.Hour.ToString();
            else
                hora = fecha.Hour.ToString();

            if (fecha.Minute < 10)
                minutos = "0" + fecha.Minute.ToString();
            else
                minutos = fecha.Minute.ToString();

            if (fecha.Second < 10)
                segundos = "0" + fecha.Second.ToString();
            else
                segundos = fecha.Second.ToString();

            anno = fecha.Year.ToString();

            retorno = anno + "-" + mes + "-" + dia + " " + hora + ":" + segundos;
            return retorno;
        }
        public static System.Net.Mail.MailMessage ConstruyeMensajeCrearEvento(string nombreInstitucion, string nombreEvento, string fechaInicioYTermino, string ubicacion, List<string> correos, bool esNuevo)
        {
            System.Net.Mail.MailMessage sms = new System.Net.Mail.MailMessage();
            sms.Subject = "Evento en CPAS";
            string habilitaCopiaAdmin1 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin1 == "1")
            {
                sms.To.Add(COPIA_ADMIN_1());
            }
            string habilitaCopiaAdmin2 = Utiles.HABILITA_COPIA_ADMIN1();
            if (habilitaCopiaAdmin2 == "1")
            {
                sms.To.Add(COPIA_ADMIN_2());
            }
            //recorremos la lista de usuarios de la institución
            if (correos != null && correos.Count > 0)
            {
                foreach (string s in correos)
                {
                    sms.To.Add(s);
                }
            }


            sms.From = new System.Net.Mail.MailAddress("contacto@cpas.cl", "CPAS");
            sms.IsBodyHtml = true;

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            if (esNuevo)
            {
                string htmlMensaje = ObtenerMensajeXML("Evento", true);
                if (htmlMensaje != null)
                {
                    htmlMensaje = htmlMensaje.Replace("{NombreItem}", nombreEvento).Replace("{NombreInstitucion}", nombreInstitucion).Replace("{FechaInicioTermino}", fechaInicioYTermino).Replace("{Url}", ObtenerUrl());
                    sb.Append(htmlMensaje);
                }
                else
                {
                    sb.AppendFormat("Se ha agregado el evento  <strong>{0}</strong>, para el Establecimiento {1}:<br />", nombreEvento, nombreInstitucion);
                    sb.AppendFormat("Estimado Usuario CPAS, ha sido agregado un evento para su establecimiento, este tendrá lugar en {0} entre {1}, ingrese a www.cpas.cl para poder verlo.", ubicacion, fechaInicioYTermino);
                    sb.Append("<br />");
                    sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ****** <br />");
                    sb.Append("</html>");
                }
            }
            else
            {
                string htmlMensaje = ObtenerMensajeXML("Evento", false);
                if (htmlMensaje != null)
                {
                    htmlMensaje = htmlMensaje.Replace("{NombreItem}", nombreEvento).Replace("{NombreInstitucion}", nombreInstitucion).Replace("{FechaInicioTermino}", fechaInicioYTermino).Replace("{Url}", ObtenerUrl());
                    sb.Append(htmlMensaje);
                }
                else
                {
                    sb.AppendFormat("Ha sido modificado el evento  <strong>{0}</strong>, para el Establecimiento {1}:<br />", nombreEvento, nombreInstitucion);
                    sb.AppendFormat("Estimado Usuario CPAS, ha sido agregado un evento para su establecimiento, este tendrá lugar en {0} entre {1}, ingrese a www.cpas.cl para poder verlo.", ubicacion, fechaInicioYTermino);
                    sb.Append("<br />");
                    sb.Append("***** Mensaje enviado desde el sistema automático de envio de correos de CPAS ****** <br />");
                    sb.Append("</html>");
                }
            }
            
            sms.Body = sb.ToString();
            return sms;
        }
        public static void Log(string mensaje)
        {
            string carpetaArchivo = @"Logs\log.txt";
            string rutaFinal = AppDomain.CurrentDomain.BaseDirectory + carpetaArchivo;

            object Locker = new object();
            XmlDocument _doc = new XmlDocument();

            try
            {
                if (!File.Exists(rutaFinal))
                {
                    File.Create(rutaFinal);
                }

                _doc.Load(rutaFinal);

                lock (Locker)
                {
                    //var id = (XmlElement)_doc.DocumentElement.LastChild;
                    //id.GetElementsByTagName("Id");
                    int cantidad = _doc.ChildNodes.Count;
                    int indice = 1;
                    if (cantidad > 0)
                    {
                        //obtener el ultimo elemento id
                        if ((XmlElement)_doc.DocumentElement.LastChild != null)
                        {
                            var ultimo = (XmlElement)_doc.DocumentElement.LastChild;
                            indice = int.Parse(ultimo.LastChild.InnerText);
                            indice = indice + 1;
                        }
                    }

                    var el = (XmlElement)_doc.DocumentElement.AppendChild(_doc.CreateElement("error"));
                    //el.SetAttribute("Fecha", ConstruyeFecha(DateTime.Now));
                    
                    el.AppendChild(_doc.CreateElement("Fecha")).InnerText = ConstruyeFecha(DateTime.Now);
                    el.AppendChild(_doc.CreateElement("Detalle")).InnerText = mensaje;
                    el.AppendChild(_doc.CreateElement("Id")).InnerText = indice.ToString();
                    _doc.Save(rutaFinal);
                }

            }
            catch (Exception ex)
            {

            }

        }
        public static string EntregaNombreArchivo(string nombreArchivo)
        {
            StringBuilder sb = new StringBuilder();
            string[] texto = nombreArchivo.ToString().Split(' ');
            string nuevoNombre = string.Empty;
            if (texto.Length > 0)
            {
                for (int i = 0; i < texto.Length; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(texto[i]);
                        sb.Append(" ");
                    }
                }
            }
            return sb.ToString();
        }
        public static string ObtenerMensajeXML(string nombre, bool esNuevo)
        {
            string retorno = "";
            string carpetaArchivo = @"Mensajes.xml";
            string rutaFinal = AppDomain.CurrentDomain.BaseDirectory + carpetaArchivo;
            XmlDocument doc = new XmlDocument();
            doc.Load(rutaFinal);

            try
            {
                XmlNodeList mensaje = doc.GetElementsByTagName("Mensaje");
                XmlNodeList lista = ((XmlElement)mensaje[0]).GetElementsByTagName("item");
                if (lista != null && lista.Count > 0)
                {
                    foreach (XmlElement nodo in lista)
                    {
                        if (nodo != null)
                        {
                            if (nodo.Attributes[0] != null)
                            {
                                if (nodo.Attributes[0].InnerText.ToString().ToUpper() == nombre.ToUpper())
                                {
                                    string otraBusqueda = "nuevo";
                                    if (!esNuevo)
                                        otraBusqueda = "modificado";

                                    if (nodo.ChildNodes != null && nodo.ChildNodes.Count > 0)
                                    {
                                        foreach(XmlElement nodito in nodo.ChildNodes)
                                        {
                                            if (nodito.Name.ToUpper() == otraBusqueda.ToUpper())
                                            {
                                                retorno = nodito.InnerXml;
                                                break;
                                            }
                                        }
                                    }


                                }
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                NegocioMySQL.Utiles.Log(ex);
            }

            return retorno;
        }

        public static string ObtenerUrl()
        {
            string retorno = "http://www.cpas.cl";
            try {
                retorno = System.Web.HttpContext.Current.Request.Url.Host;
            }
            catch(Exception ex)
            {
                Log(ex);
            }
            return retorno;
        }
        public static void Log(Exception mensaje)
        {
            string carpetaArchivo = @"Logs\log.txt";
            string rutaFinal = AppDomain.CurrentDomain.BaseDirectory + carpetaArchivo;

            object Locker = new object();
            XmlDocument _doc = new XmlDocument();

            try
            {
                if (!File.Exists(rutaFinal))
                {
                    File.Create(rutaFinal);
                }

                _doc.Load(rutaFinal);

                lock (Locker)
                {
                    //var id = (XmlElement)_doc.DocumentElement.LastChild;
                    //id.GetElementsByTagName("Id");
                    int cantidad = _doc.ChildNodes.Count;
                    int indice = 1;
                    if (cantidad > 0)
                    {
                        //obtener el ultimo elemento id
                        if ((XmlElement)_doc.DocumentElement.LastChild != null)
                        {
                            var ultimo = (XmlElement)_doc.DocumentElement.LastChild;
                            indice = int.Parse(ultimo.LastChild.InnerText);
                            indice = indice + 1;
                        }
                    }

                    var el = (XmlElement)_doc.DocumentElement.AppendChild(_doc.CreateElement("error"));
                    //el.SetAttribute("Fecha", ConstruyeFecha(DateTime.Now));

                    el.AppendChild(_doc.CreateElement("Fecha")).InnerText = ConstruyeFecha(DateTime.Now);
                    el.AppendChild(_doc.CreateElement("Detalle")).InnerText = mensaje.Message;
                    el.AppendChild(_doc.CreateElement("Id")).InnerText = indice.ToString();
                    _doc.Save(rutaFinal);
                }

            }
            catch (Exception ex)
            {

            }


        }
        /// <summary>
        /// Validar Rut en el formato 12.333.66-K
        /// </summary>
        /// <param name="rut">Rut Formateado</param>
        /// <returns></returns>
        public static bool validarRut(string rut)
        {
            
            bool validacion = false;
            try
            {
                rut = rut.ToUpper();
                rut = rut.Replace(".", "");
                rut = rut.Replace("-", "");
                int rutAux = int.Parse(rut.Substring(0, rut.Length - 1));

                char dv = char.Parse(rut.Substring(rut.Length - 1, 1));

                int m = 0, s = 1;
                for (; rutAux != 0; rutAux /= 10)
                {
                    s = (s + rutAux % 10 * (9 - m++ % 6)) % 11;
                }
                if (dv == (char)(s != 0 ? s + 47 : 75))
                {
                    validacion = true;
                }
            }
            catch (Exception)
            {
            }
            return validacion;
        }
    }
}
