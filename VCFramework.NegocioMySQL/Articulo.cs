using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VCFramework.Negocio.Factory;

namespace VCFramework.NegocioMySQL
{
    public class Articulo
    {
        public static System.Configuration.ConnectionStringSettings setCnsWebLun = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["BDColegioSql"];
        public static int Modificar(Entidad.Articulo item)
        {
            int retorno = 0;

            item.Modificado = true;
            item.Nuevo = false;
            item.Borrado = false;

            Factory fac = new Factory();

            retorno = fac.Update<Entidad.Articulo>(item, setCnsWebLun);

            return retorno;
        }
    }
}
