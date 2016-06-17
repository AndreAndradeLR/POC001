using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class AcaoFinanceiro : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    string ExibirMes = "none";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FormBind();
            PanelFin.Visible = false;
        }
    }

    private void FormBind()
    {
        ddlt27_cd_fonte.DataSource = new t27_fonteAction().ListTodos(Convert.ToInt32(pb.Session("cd_projeto")));
        ddlt27_cd_fonte.DataTextField = "nm_fonte";
        ddlt27_cd_fonte.DataValueField = "t27_cd_fonte";
        ddlt27_cd_fonte.DataBind();
        pb.AddEmptyItem(ddlt27_cd_fonte, "Todas");
        
        DateTime dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));
        DateTime dt_fim = Convert.ToDateTime(pb.Session("dt_fim", DateTime.Now));

        for (int ano = dt_inicio.Year; ano <= dt_fim.Year; ano++)
        {
            ListItem li = new ListItem();
            li.Value = ano.ToString();
            if (ano == DateTime.Now.Year)
            {
                li.Selected = true;
            }
            ddlAno.Items.Add(li);
        }
    }

    private void LiteralGridBind(int cd_fonte, int ano)
    {
        //Response.Write(ExibirMes + "  teste");
        string display_meses = ExibirMes;
        DataTable dtfin = new t11_financeiroAction().ListAcaoFonteFinanceiro(cd_fonte, ano, Convert.ToInt32(pb.Session("cd_projeto")));
        if (dtfin.Rows.Count > 0)
        {
            string[] mes = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("<table width=\"100%\" rules=\"all\" border=\"1\" ");
            sb.AppendLine(" style=\"border: 1px solid #ccc; border-collapse: collapse;\" ");
            sb.AppendLine(" cellpadding=\"5\" cellspacing=\"0\">");
            sb.AppendLine("<tr>");
            sb.AppendLine("<td colspan=\"4\">&nbsp;</td>");
            for (int i = 0; i < 12; i++)
            {
                sb.AppendLine("<td colspan=\"5\" style=\"background:#daf0b8; display:" + display_meses + "\"><b>" + mes[i] + "</b></td>");
            }

            sb.AppendLine("<td colspan=\"10\" style=\"background:#d9dad7\"><b>Total</b></td>");
            sb.AppendLine("</tr>");
            sb.AppendLine("<tr style=\"background:#eaf6d8\">");
            sb.AppendLine("<td>Ação</td>");
            sb.AppendLine("<td>Restos a Pagar </td>");
            sb.AppendLine("<td>Dotação Orçamentária</td>");
            sb.AppendLine("<td>Assegurado </td>");
            for (int i = 0; i < 12; i++)
            {
                sb.AppendLine("<td style=\"display:" + display_meses + "\">Planejado </td>");
                sb.AppendLine("<td style=\"display:" + display_meses + "\">Revisado </td>");
                sb.AppendLine("<td style=\"display:" + display_meses + "\">Realizado </td>");
                sb.AppendLine("<td style=\"display:" + display_meses + "\">Empenhado </td>");
                sb.AppendLine("<td style=\"display:" + display_meses + "\">Liquidado </td>");                
            }


            sb.AppendLine("<td style=\"background:#ecece9\">Planejado </td>");
            sb.AppendLine("<td style=\"background:#ecece9\">Revisado </td>");
            sb.AppendLine("<td style=\"background:#ecece9\">Realizado </td>");
            sb.AppendLine("<td style=\"background:#ecece9\">Empenhado </td>");
            sb.AppendLine("<td style=\"background:#ecece9\">Liquidado </td>");            

            sb.AppendLine("</tr>");
            t18b_vlfinanceiro t18 = new t18b_vlfinanceiro();
            t18b_vlfinanceiro t18ac = new t18b_vlfinanceiro(); //acumulador total ano
            t18b_vlfinanceiro t18t = new t18b_vlfinanceiro(); //total geral
            t18.nu_ano = 0;


            foreach (DataRow dr in dtfin.Rows)
            {
                if (dr["vl_dotorcado"] != DBNull.Value)
                {
                    t18.vl_dotorcado = Convert.ToDecimal(dr["vl_dotorcado"]);
                    t18.vl_restopagar = Convert.ToDecimal(dr["vl_restopagar"]);
                    t18.vl_assegurado = Convert.ToDecimal(dr["vl_assegurado"]);
                    sb.AppendLine("<tr style=\"text-align:right\">");
                    sb.AppendLine("<td style=\"text-align:left\"><b>" + dr["nm_acao"] + "</b><img src=\"images/blank.gif\" /></td>");
                    sb.AppendLine("<td>");
                    sb.AppendLine(t18.vl_restopagar.ToString("N0"));
                    sb.AppendLine("</td>");
                    sb.AppendLine("<td>");
                    sb.AppendLine(t18.vl_dotorcado.ToString("N0"));
                    sb.AppendLine("</td>");
                    sb.AppendLine("<td>");
                    sb.AppendLine(t18.vl_assegurado.ToString("N0"));
                    sb.AppendLine("</td>");

                    for (int i = 1; i < 13; i++)
                    {
                        t18.vl_planejado1 = Convert.ToDecimal(dr["vl_planejado" + i]);
                        t18.vl_revisado1 = Convert.ToDecimal(dr["vl_revisado" + i]);
                        t18.vl_provisionado1 = Convert.ToDecimal(dr["vl_provisionado" + i]);
                        t18.vl_empenhado1 = Convert.ToDecimal(dr["vl_empenhado" + i]);
                        t18.vl_liquidado1 = Convert.ToDecimal(dr["vl_liquidado" + i]);                        

                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_planejado1.ToString("N0") + "</td>");
                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_revisado1.ToString("N0") + "</td>");
                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_provisionado1.ToString("N0") + "</td>");
                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_empenhado1.ToString("N0") + "</td>");
                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_liquidado1.ToString("N0") + "</td>");                        

                        t18ac.vl_dotorcado += t18.vl_dotorcado;
                        t18ac.vl_restopagar += t18.vl_restopagar;
                        t18ac.vl_assegurado += t18.vl_assegurado;
                        t18ac.vl_planejado1 += t18.vl_planejado1;
                        t18ac.vl_provisionado1 += t18.vl_provisionado1;
                        t18ac.vl_empenhado1 += t18.vl_empenhado1;
                        t18ac.vl_liquidado1 += t18.vl_liquidado1;
                        t18ac.vl_revisado1 += t18.vl_revisado1;

                    }

                    sb.AppendLine("<td>" + t18ac.vl_planejado1.ToString("N0") + "</td>");
                    sb.AppendLine("<td>" + t18ac.vl_revisado1.ToString("N0") + "</td>");
                    sb.AppendLine("<td>" + t18ac.vl_provisionado1.ToString("N0") + "</td>");
                    sb.AppendLine("<td>" + t18ac.vl_empenhado1.ToString("N0") + "</td>");
                    sb.AppendLine("<td>" + t18ac.vl_liquidado1.ToString("N0") + "</td>");
                    

                    t18t.vl_planejado1 += t18ac.vl_planejado1;
                    t18t.vl_provisionado1 += t18ac.vl_provisionado1;
                    t18t.vl_empenhado1 += t18ac.vl_empenhado1;
                    t18t.vl_liquidado1 += t18ac.vl_liquidado1;
                    t18t.vl_revisado1 += t18ac.vl_revisado1;


                    sb.AppendLine("</tr>");
                }

                t18ac.vl_dotorcado = 0;
                t18ac.vl_restopagar = 0;
                t18ac.vl_assegurado = 0;
                t18ac.vl_planejado1 = 0;
                t18ac.vl_provisionado1 = 0;
                t18ac.vl_empenhado1 = 0;
                t18ac.vl_liquidado1 = 0;
                t18ac.vl_revisado1 = 0;

            }

            sb.AppendLine("<tr style=\"background:#ecece9;text-align:right\">");
            sb.AppendLine("<td ><b>Total</b></td>");

            foreach (DataRow dr in
                new t11_financeiroAction().ListAcaoFinanceiroTotal(cd_fonte, ano, Convert.ToInt32(pb.Session("cd_projeto"))).Rows)
            {
                if (dr["vl_dotorcado"] != DBNull.Value)
                {
                    t18.vl_dotorcado = Convert.ToDecimal(dr["vl_dotorcado"]);
                    t18.vl_restopagar = Convert.ToDecimal(dr["vl_restopagar"]);
                    t18.vl_assegurado = Convert.ToDecimal(dr["vl_assegurado"]);
                    sb.AppendLine("<td>" + t18.vl_restopagar.ToString("N0") + "</td>");
                    sb.AppendLine("<td>" + t18.vl_dotorcado.ToString("N0") + "</td>");
                    sb.AppendLine("<td>" + t18.vl_assegurado.ToString("N0") + "</td>");

                    for (int i = 1; i < 13; i++)
                    {
                        t18.vl_planejado1 = Convert.ToDecimal(dr["vl_planejado" + i]);
                        t18.vl_revisado1 = Convert.ToDecimal(dr["vl_revisado" + i]);
                        t18.vl_provisionado1 = Convert.ToDecimal(dr["vl_provisionado" + i]);
                        t18.vl_empenhado1 = Convert.ToDecimal(dr["vl_empenhado" + i]);
                        t18.vl_liquidado1 = Convert.ToDecimal(dr["vl_liquidado" + i]);

                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_planejado1.ToString("N0") + "</td>");
                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_revisado1.ToString("N0") + "</td>");
                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_provisionado1.ToString("N0") + "</td>");
                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_empenhado1.ToString("N0") + "</td>");
                        sb.AppendLine("<td style=\"display:" + display_meses + "\">" + t18.vl_liquidado1.ToString("N0") + "</td>");                        
                        
                    }
                }
            }

            sb.AppendLine("<td>" + t18t.vl_planejado1.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18t.vl_revisado1.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18t.vl_provisionado1.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18t.vl_empenhado1.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18t.vl_liquidado1.ToString("N0") + "</td>");           

            sb.AppendLine("</tr>");

            sb.AppendLine("</table>");
            LiteralGrid.Text = sb.ToString().Replace(",00", "");
        }
        else
        {
            PanelFin.Visible = false;
            lblMsg.Text = pb.Message("A seleção efetuada não possui informações. Tente novamente.", "erro");
            lblMsg.Visible = true;
        }
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        LinkExibir.Visible = true;
        PanelFin.Visible = true;
        if (Session["ScreenResolution"] != null)
        {
            ScreenResolution scr = new ScreenResolution();
            scr = (ScreenResolution)Session["ScreenResolution"];
            if (scr.WidthWeb > 0)
            {
                PanelFin.Width = Unit.Pixel(scr.WidthWeb);
            }
        }

        int cd_fonte = 0;
        if (ddlt27_cd_fonte.SelectedValue != "")
        {
            cd_fonte = Convert.ToInt32(ddlt27_cd_fonte.SelectedValue);
        }
        LiteralGridBind(cd_fonte, Convert.ToInt32(ddlAno.SelectedValue));
    }

    protected void linkFiltroExibe_Click(object sender, System.EventArgs e)
    {
        int cd_fonte = 0;
        if (ddlt27_cd_fonte.SelectedValue != "")
        {
            cd_fonte = Convert.ToInt32(ddlt27_cd_fonte.SelectedValue);
        }
        LiteralGridBind(cd_fonte, Convert.ToInt32(ddlAno.SelectedValue));
        if (this.LinkExibir.Text == "Exibir Meses")
        {
            this.LinkExibir.Text = "Ocultar Meses";
            ExibirMes = "";
            LiteralGridBind(cd_fonte, Convert.ToInt32(ddlAno.SelectedValue));
        }
        else
        {
            this.LinkExibir.Text = "Exibir Meses";
            ExibirMes = "none";
            LiteralGridBind(cd_fonte, Convert.ToInt32(ddlAno.SelectedValue));
        }
    }
}
