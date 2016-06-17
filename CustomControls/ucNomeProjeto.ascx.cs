using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ucNomeProjeto : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        pageBase pb = new pageBase();
        lblnm_projeto.Text = pb.Session("nm_projeto").ToString();
        //lblnm_area.Text = pb.Session("nm_area").ToString();
        imgArea.ImageUrl = "../Documentos/"+pb.Session("nm_arquivo").ToString().Trim();
    }
}
