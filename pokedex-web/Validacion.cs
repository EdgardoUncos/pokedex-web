using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace pokedex_web
{
    public static class Validacion
    {
        public static bool validaTextoVacio(object control)
        {
            if (control is TextBox Control)
            {
                if (string.IsNullOrEmpty(Control.Text))
                    return false;
                else
                    return true;

            }

            return false;
        }
    }
}