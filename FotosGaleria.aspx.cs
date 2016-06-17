using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class FotosGaleria: System.Web.UI.Page
{
    pageBase pb = new pageBase();

    public int cd_projeto
    {
        get
        {
            return Convert.ToInt32(pb.Session("cd_projeto"));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (cd_projeto != 0)
            {
                t03_projeto t03 = new t03_projeto();
                t03_projetoAction t03a = new t03_projetoAction();
                t03 = t03a.Retrieve(cd_projeto);
            }

        }

        GeraGaleriaFotos();

        string Script = "var serverVars = { " +
                                   "adminMode: true " +
                                    "}; ";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowName", Script, true);
    }

    public void GeraGaleriaFotos()
    {
        rptFotos.DataSource = new t14_documentoAction().ListFoto(cd_projeto);
        rptFotos.DataBind();
    }

    protected void rptFotos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string NM_DOCUMENTO = DataBinder.Eval(e.Item.DataItem, "NM_DOCUMENTO").ToString();
            var NM_ARQUIVO = DataBinder.Eval(e.Item.DataItem, "NM_ARQUIVO").ToString();

            int ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "T14_CD_DOCUMENTO").ToString());            

            Label lblNm_DOCUMENTO = (Label)e.Item.FindControl("lblNm_DOCUMENTO");
            if (NM_DOCUMENTO.ToString().Length > 77)
            {
                lblNm_DOCUMENTO.Text = NM_DOCUMENTO.Substring(0, 77);
            }
            else
                lblNm_DOCUMENTO.Text = NM_DOCUMENTO.ToString();

            HtmlAnchor a = (HtmlAnchor)e.Item.FindControl("prettyPhoto");            
            HtmlImage img = (HtmlImage)e.Item.FindControl("idImg");

            string arquivo = Server.MapPath(".") + @"\Documentos\" + NM_ARQUIVO;
            if (!System.IO.File.Exists(arquivo))
            {
                a.Attributes.Add("href", "Documentos/img.jpg");
                img.Attributes.Add("src", "images/img.jpg");
            }
            else
            {
                a.Attributes.Add("href", "Documentos/" + NM_ARQUIVO);
                img.Attributes.Add("src", "Documentos/" + NM_ARQUIVO);
            }

            img.Attributes.Add("width", "150px");
            img.Attributes.Add("height", "150px");
        }
    }
}