using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RestricaoDetalhes : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (pb.Session("cd_restricao").ToString() != "0")
        {
            Retrieve();
            if (!pb.fl_gerente() && !pb.fl_respmonitora())
            {
                spanbtnSuperar.Visible = false;
            }
        }
        else
        {
            Response.Redirect("~/Detalhamento.aspx");
        }
    }

    public void Retrieve()
    {
        t07_restricao t07 = new t07_restricaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_restricao")));
        lblds_restricao.Text  = t07.ds_restricao;
        lbldt_limite.Text = t07.dt_limite.ToShortDateString();
        lbldt_cadastro.Text = t07.dt_cadastro.ToShortDateString();
        if (t07.t02_cd_usuario != null)
        {
            Session["Responsavel_Providencia"] = t07.t02_cd_usuario.ToString();
        }
        else
        {
            Session["Responsavel_Providencia"] = "";
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        int mcs = 0;
        foreach (t09_marco t09 in t07.t09)
        {
            mcs++;
            sb.AppendLine("<li>" + t09.ds_marco + "</li>");
        }
        if (mcs > 0)
        {
            LiteralMC.Text = sb.ToString();
            trMC.Visible = true;
        }
        else
        {
            trMC.Visible = false;
        }        
    }
    protected void btnSuperar_Click(object sender, EventArgs e)
    {
        new t07_restricaoAction().UpdateSuperarDB(Convert.ToInt32(pb.Session("cd_restricao")));
        lblMsg.Visible = true;
        lblMsg.Text = pb.Message("Restrição superada com sucesso!", "ok");
        spanbtnSuperar.Visible = false;
        ucProvidencia.FindControl("btnNovo").Visible = false;
        ucProvidencia.FindControl("spanbtnNovo").Visible = false;
    }
}
