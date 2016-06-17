using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class MonRestricoes : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["fl_status"] != null)
        {
            switch (Request["fl_status"].ToString())
            {
                case "B":
                    lblHeader.Text = "Restrições concluídas";
                    break;
                case "G":
                    lblHeader.Text = "Restrições dentro dos prazos previstos";
                    break;
                case "R":
                    lblHeader.Text = "Restrições com atraso";
                    break;

            }
            GridBind(PanelRel, Request["fl_status"].ToString());
        }
        else
        {
            lblHeader.Text = "Restrições";
            GrafBind(PanelGraf);
        }

    }
    protected void GrafBind(Panel pn)
    {
        PanelGraf.Visible = true;
        PanelOpcao.Visible = true;
        PanelRel.Visible = false;
        linkVoltar.Visible = false;

        double r = 0;
        double g = 0;
        double b = 0;
        foreach (t03_projeto t03 in new t03_projetoAction().ListObjMon(FiltroSession()))
        {

            r += new t07_restricaoAction().ListObjMonVermelho(t03.t03_cd_projeto).Count;
            g += new t07_restricaoAction().ListObjMonVerde(t03.t03_cd_projeto).Count;
            b += new t07_restricaoAction().ListObjMonAzul(t03.t03_cd_projeto).Count;

        }
            
            double total = r + g + b;

            lblAzul.Text = b.ToString();
            lblVerde.Text = g.ToString();
            lblVermelha.Text = r.ToString();

            if (b == 0) linkConcluidos.NavigateUrl = "";
            if (g == 0) linkPrazos.NavigateUrl = "";
            if (r == 0) linkAtraso.NavigateUrl = "";

            if (total > 0)
            {
                lblFatiaAzul.Text = ((b * 100) / total).ToString("N0");
                lblFatiaVerde.Text = ((g * 100) / total).ToString("N0");
                lblFatiaVermelha.Text = ((r * 100) / total).ToString("N0");
            }
            else
            {
                lblFatiaAzul.Text = "0";
                lblFatiaVerde.Text = "0";
                lblFatiaVermelha.Text = "0";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=100% height=20 border=0 cellpadding=0 cellspacing=0><tr>");
            if (b != 0)
            {
                sb.Append("<td style=\"border:none;background:url('images/B.gif');width:" + (b * 100) / total + "%\" title='" + (b * 100) / total + "%'>&nbsp;</td>");
            }
            if (g != 0)
            {
                sb.Append("<td style=\"border:none;background:url('images/G.gif');width:" + (g * 100) / total + "%\" title='" + (g * 100) / total + "%'>&nbsp;</td>");
            }
            if (r != 0)
            {
                sb.Append("<td style=\"border:none;background:url('images/R.gif');width:" + (r * 100) / total + "%\" title='" + (r * 100) / total + "%'>&nbsp;</td>");
            }
            sb.Append("</tr></table>");
            pn.Controls.Add(pb.GetLiteral(sb.ToString()));
        
    }

    protected void GridBind(Panel pn, string fl_status)
    {
        PanelGraf.Visible = false;
        PanelOpcao.Visible = false;
        PanelRel.Visible = true;
        linkVoltar.Visible = true;
        try
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
            sb.Append("<tr style=\"background:#F0EDEB;font-weight:bold\">");
            sb.Append("<td colspan=\"3\">Projetos</td>");
            sb.Append("</tr>");


            foreach (t03_projeto t03 in new t03_projetoAction().ListObjMon(FiltroSession()))
            {
               
                int index = 0;

                List<t07_restricao> t07l = new List<t07_restricao>();
                if (fl_status == "R")
                {
                    t07l = new t07_restricaoAction().ListObjMonVermelho(t03.t03_cd_projeto);
                }
                else if (fl_status == "G")
                {
                    t07l = new t07_restricaoAction().ListObjMonVerde(t03.t03_cd_projeto);
                }
                else if (fl_status == "B")
                {
                    t07l = new t07_restricaoAction().ListObjMonAzul(t03.t03_cd_projeto);
                }
                foreach (t07_restricao t07 in t07l)
                {
                    if (index == 0)
                    {
                        sb.Append("<tr style=\"background:#FAF9F8;font-weight:bold\">");
                        sb.Append("<td colspan=\"3\">" + t03.nm_projeto + "</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr class=\"headerGrid\">");
                        sb.Append("<td>Restrição</td>");
                        sb.Append("<td>Data limite</td>");
                        sb.Append("</tr>");
                        index = 1;
                    }
                    sb.Append("<tr>");
                    sb.Append("<td>" + t07.ds_restricao + "</td>");
                    sb.Append("<td>" + t07.dt_limite.ToShortDateString() + "</td>");
                    sb.Append("</tr>");
                }
            }


            sb.Append("</table>");
            pn.Controls.Add(pb.GetLiteral(sb.ToString()));

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }

    }
    public FiltroProjeto FiltroSession()
    {
        if (Session["filtroMon"] != null)
        {
            return (FiltroProjeto)Session["filtroMon"];
        }
        else
        {
            return new FiltroProjeto();
        }
    }
}
