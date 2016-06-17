using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MonMediaDias : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table cellspacing=\"0\" cellpadding=\"4\" rules=\"all\" border=\"1\" style=\"color:#333333;width:100%;border-collapse:collapse;background:#FFFFFF;\">");
            sb.Append("<tr style=\"background:#F0EDEB;font-weight:bold\">");
            sb.Append("<td colspan=\"2\">Projetos</td>");
            sb.Append("</tr>");
            int contproj = 0;
            int dias = 0;
            int index = 0;
            foreach (DataRow dr in new t03_projetoAction().ListMonAtualizaDuasSemanas(FiltroSession()).Rows)
            {
                if (index <= 0)
                {
                    sb.Append("<tr style=\"background:#E7F3E2;font-weight:bold\">");
                    sb.Append("<td>Até 2 semanas</td>");
                    sb.Append("<td>Dias</td>");
                    sb.Append("</tr>");
                    index = 1;
                }
                dias += (int)dr["dias"];
                contproj++;
                sb.Append("<tr>");
                sb.Append("<td>" + dr["nm_projeto"] + "</td>");
                int dia = (int)dr["dias"];
                if (dia == 0)
                    dia = 1; dias += dia;
                sb.Append("<td>" + dia + "</td>");
                sb.Append("</tr>");
            }
            index = 0; //zerar index
            foreach (DataRow dr in new t03_projetoAction().ListMonAtualizaQuatroSemanas(FiltroSession()).Rows)
            {
                if (index <= 0)
                {
                    sb.Append("<tr style=\"background:#FCF2FF;font-weight:bold\">");
                    sb.Append("<td>De 2 a 4 semanas</td>");
                    sb.Append("<td>Dias</td>");
                    sb.Append("</tr>");
                    index = 1;
                }
                dias += (int)dr["dias"];
                contproj++;
                sb.Append("<tr>");
                sb.Append("<td>" + dr["nm_projeto"] + "</td>");
                sb.Append("<td>" + dr["dias"] + "</td>");
                sb.Append("</tr>");
            }
            index = 0; //zerar index
            foreach (DataRow dr in new t03_projetoAction().ListMonAtualizaMaisQuatroSemanas(FiltroSession()).Rows)
            {
                if (index <= 0)
                {
                    sb.Append("<tr style=\"background:#FFEFEA;font-weight:bold\">");
                    sb.Append("<td>Mais de 4 semanas</td>");
                    sb.Append("<td>Dias</td>");
                    sb.Append("</tr>");
                    index = 1;
                }
                dias += (int)dr["dias"];
                contproj++;
                sb.Append("<tr>");
                sb.Append("<td>" + dr["nm_projeto"] + "</td>");
                sb.Append("<td>" + dr["dias"] + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("<tr style=\"background:#F0EDEB;font-weight:bold\">");
            sb.Append("<td>Média</td>");
            sb.Append("<td>" + (dias / contproj) + "</td>");
            sb.Append("</tr></table>");

            Panel1.Controls.Add(pb.GetLiteral(sb.ToString()));

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
