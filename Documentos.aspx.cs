using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Documentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        /*
         * Solução para não ser necessário criar
         * mais um link Fotos em ucNavegacao.ascx
         * pois fotos não faz mas parte de Documentos
         * e sim de Comunicação
         */

        if (Request["tipo"] != null)
        {
            if (Request["tipo"].ToString() == "foto")
            {
                HyperLink link = (HyperLink)ucNav.FindControl("linkDocumentos");
                if (link != null)
                {
                    link.Text = "> Fotos";
                }
                lblTitle.Text = "Fotos";
            }
        }
    }
}
