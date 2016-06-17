using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

public partial class Rel_Financeiro : System.Web.UI.Page
{
    string[] month = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
    pageBase pb = new pageBase();
    DateTime dt_inicio;
    DateTime dt_fim;

    protected void Page_Load(object sender, EventArgs e)
    {
        dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));
        dt_fim = Convert.ToDateTime(pb.Session("dt_fim", DateTime.Now));
        if (!IsPostBack)
        {
            //GridBind();
            FormBind();
        }
    }

    public void AddColorLines(DropDownList ddl, string hexcolor)
    {
        for (int i = 0; i <= ddl.Items.Count - 1; i++)
        {
            if (i % 2 != 0)
            {
                ddl.Items[i].Attributes.Add("Style", "Background-Color:" + hexcolor);
            }
        }

    }

    private void FormBind()
    {
        ddlMes.ClearSelection();
        ddlMes.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            ListItem li = new ListItem();
            li.Text = month[i - 1];
            li.Value = i.ToString();
            if (i == DateTime.Now.Month - 1)
            {
                li.Selected = true;
            }
            ddlMes.Items.Add(li);
        }
        pb.AddEmptyItem(ddlMes, "Selecione");        

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
        pb.AddEmptyItem(ddlAno, "Selecione");
    }

    protected void btnFiltrar_Click(object sender, System.EventArgs e)
    {
        ViewState["AnoRel"] = ddlAno.SelectedValue.ToString();
        ViewState["MesRel"] = ddlMes.SelectedValue.ToString();
        GridBind();
    }

    private void GridBind() 
    {
        Session["CodRelatorio"] = DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        PanelGrid.Visible = true;
        //btnExportar.Visible = true;
        StringBuilder sb = new StringBuilder();
        StringBuilder sbTotalDados = new StringBuilder();
        int ano = ViewState["AnoRel"] != null ? Convert.ToInt32(ViewState["AnoRel"]) : DateTime.Now.Year;
        int mes = ViewState["MesRel"] != null ? Convert.ToInt32(ViewState["MesRel"]) : DateTime.Now.Month;

        int empty = 0;
        int count = 0;
        sb.AppendLine("<table width='100%' class='tbRestricao' cellpadding='2' cellspacing='0' bgcolor='#FFFFFF'>");
        sb.AppendLine("<thead><tr style='color:#ffffff;text-align:center;'>");
        sb.AppendLine("<th colspan='3' style='color:#666666;text-align:center;'>Data da Impress&atilde;o:&nbsp;<span style='color:#70A2F3;'>" + string.Format("{0:dd/MM/yyyy}", DateTime.Now) + "</span></th>");

        sb.AppendLine("<th colspan='5' bgcolor='#9FCA60' height='30'>Total em " + ano + " at&eacute; " + month[pb.NomeMes(mes - 1)] + "</th>");//#5D7B9D

        sb.AppendLine("<th colspan='5' bgcolor='#9FCA60' height='30'>Total Acumulado at&eacute; " + month[pb.NomeMes(mes - 1)] + " de " + ano + "</th>");
        sb.AppendLine("<th colspan='5' bgcolor='#9FCA60' height='30'>Total Geral</th>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr style='font-weight:bold;color:#333333'>");
        sb.AppendLine("<th bgcolor='#EAF6D8' width='30%' height='30'>A&ccedil;&atilde;o</td><td bgcolor='#EAF6D8' width='15%'>Fonte</td><td bgcolor='#EAF6D8' width='15%'>Unidade de Medida</th>");

        for (int num = 1; num <= 3; num++)
        {
            sb.AppendLine("<td bgcolor='#EAF6D8' width='5%' style='text-align:center;'>Planejado</td>");
            sb.AppendLine("<td bgcolor='#EAF6D8' width='5%' style='text-align:center;'>Revisado</td>");
            sb.AppendLine("<td bgcolor='#EAF6D8' width='5%' style='text-align:center;'>Realizado</td>");
            sb.AppendLine("<td bgcolor='#EAF6D8' width='5%' style='text-align:center;'>Empenhado</td>");
            sb.AppendLine("<td bgcolor='#EAF6D8' width='5%' style='text-align:center;'>Liquidado</td>");//#87A6C9
        }
        sb.AppendLine("</tr></thead><tbody>");

        string NomeFonte = "";
        string NomeFonte2 = "";
        //int CountAcoes = new t08_acaoAction().ListObjTodosMeta(Convert.ToInt32(Session["cd_projeto"])).Count();
        foreach (t08_acao t08 in new t08_acaoAction().ListObjTodosMeta(Convert.ToInt32(Session["cd_projeto"])))
        {            
            if (count % 2 != 0)
            {
                sb.AppendLine("<tr style='background-color:#F1F5F5'>");
            }
            else
            {
                sb.AppendLine("<tr style='background-color:#EBEBEB'>");
            }
            empty = new t11_financeiroAction().ListTodosAcoes(t08.t08_cd_acao).Rows.Count;
            if (empty == 0)
            {
                //btnExportar.Visible = false;
                sb.AppendLine("<td width='30%'><b>" + pb.ReplaceAcentoPorCaracterHTML(t08.nm_acao) + "</b></td>");
                for (int i = 1; i < 18; i++)
                {
                    sb.AppendLine("<td style='text-align:center;'> - </td>");
                }
            }
            else
            {
                btnExportar.Visible = true;
                btnPrint.Visible = true;
                //somo + 1 para linha de total de cada ação
                sb.AppendLine("<td width='30%' rowspan='" + ((empty + 1) + 1) + "'><b>" + pb.ReplaceAcentoPorCaracterHTML(t08.nm_acao) + "</b></td>");
            }
            sb.AppendLine("</tr>");

            //linha de totais para cada ação
            decimal prevLinhaTotal = 0;
            decimal revLinhaTotal = 0;
            decimal realLinhaTotal = 0;
            decimal empLinhaTotal = 0;
            decimal liqLinhaTotal = 0;

            decimal prevLinhaMesAtualTotal = 0;
            decimal revLinhaMesAtualTotal = 0;
            decimal realLinhaMesAtualTotal = 0;
            decimal empLinhaMesAtualTotal = 0;
            decimal liqLinhaMesAtualTotal = 0;

            decimal prevLinhaTotalGeral = 0;
            decimal revLinhaTotalGeral = 0;
            decimal realLinhaTotalGeral = 0;
            decimal empLinhaTotalGeral = 0;
            decimal liqLinhaTotalGeral = 0;
            
            foreach (t11_financeiro t11 in new t11_financeiroAction().ListObjTodos(t08.t08_cd_acao))
            {
                NomeFonte = pb.ReplaceAcentoPorCaracterHTML(new t27_fonteAction().Retrieve(t11.t27_cd_fonte).nm_fonte);
                //mes acumulado
                decimal tprev = 0;
                decimal trev = 0;
                decimal treal = 0;
                decimal temp = 0;
                decimal tliq = 0;

                //ano até o mês anterior
                decimal vlPrevMesAtual = 0;
                decimal vlRevMesAtual = 0;
                decimal vlRealMesAtual = 0;
                decimal vlEmpMesAtual = 0;
                decimal vlLiqMesAtual = 0;

                //totalgeral
                decimal prevtotal = 0;
                decimal revtotal = 0;
                decimal realtotal = 0;
                decimal emptotal = 0;
                decimal liqtotal = 0;
                //-------------------               

                ViewState["tprev"] = 0;
                ViewState["trev"] = 0;
                ViewState["treal"] = 0;
                ViewState["temp"] = 0;
                ViewState["tliq"] = 0;

                foreach (t18b_vlfinanceiro t18 in t11.t18l)
                {
                    if (ano > t18.nu_ano)
                    {
                        tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 +
                             t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 + t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12;

                        trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 +
                             t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 + t18.vl_revisado10 + t18.vl_revisado11 + t18.vl_revisado12;

                        treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 +
                             t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 + t18.vl_provisionado10 + t18.vl_provisionado11 + t18.vl_provisionado12;

                        temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 +
                             t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 + t18.vl_empenhado10 + t18.vl_empenhado11 + t18.vl_empenhado12;

                        tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 +
                             t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 + t18.vl_liquidado11 + t18.vl_liquidado12;

                        ViewState["tprev"] = tprev;
                        ViewState["trev"] = trev;
                        ViewState["treal"] = treal;
                        ViewState["temp"] = temp;
                        ViewState["tliq"] = tliq;
                    }
                    else if (ano == t18.nu_ano)
                    {
                        if (mes == 1)
                        {
                            //tprev += t18.vl_planejado1;
                            //treal += t18.vl_provisionado1;
                            //tprev = Convert.ToDecimal(ViewState["tprev"]);
                            //trev = Convert.ToDecimal(ViewState["trev"]);
                            //treal = Convert.ToDecimal(ViewState["treal"]);
                            //temp = Convert.ToDecimal(ViewState["temp"]);
                            //tliq = Convert.ToDecimal(ViewState["tliq"]);
                            tprev += t18.vl_planejado1;
                            trev += t18.vl_revisado1;
                            treal += t18.vl_provisionado1;
                            temp += t18.vl_empenhado1;
                            tliq += t18.vl_liquidado1;

                            vlPrevMesAtual = t18.vl_planejado1;
                            vlRevMesAtual = t18.vl_revisado1;
                            vlRealMesAtual = t18.vl_provisionado1;
                            vlEmpMesAtual = t18.vl_empenhado1;
                            vlLiqMesAtual = t18.vl_liquidado1;
                        }
                        else if (mes == 2)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2;
                            trev += t18.vl_revisado1 + t18.vl_revisado2;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2;
                        }
                        else if (mes == 3)
                        {
                            tprev += t18.vl_planejado1 + +t18.vl_planejado2 + t18.vl_planejado3;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3;
                        }
                        else if (mes == 4)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4;
                        }
                        else if (mes == 5)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5;
                        }
                        else if (mes == 6)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6;
                        }
                        else if (mes == 7)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6+ t18.vl_liquidado7;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7;
                        }
                        else if (mes == 8)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8;
                        }
                        else if (mes == 9)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9;
                        }
                        else if (mes == 10)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 + t18.vl_planejado10;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 + t18.vl_revisado10;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 + t18.vl_provisionado10;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 + t18.vl_empenhado10;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 + t18.vl_planejado10;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 + t18.vl_revisado10;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 + t18.vl_provisionado10;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 + t18.vl_empenhado10;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10;
                        }
                        else if (mes == 11)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 + t18.vl_planejado10 + t18.vl_planejado11;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 + t18.vl_revisado10 + t18.vl_revisado11;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 + t18.vl_provisionado10 + t18.vl_provisionado11;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 + t18.vl_empenhado10 + t18.vl_empenhado11;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 + t18.vl_liquidado11;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 + t18.vl_planejado10 + t18.vl_planejado11;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 + t18.vl_revisado10 + t18.vl_revisado11;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 + t18.vl_provisionado10 + t18.vl_provisionado11;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 + t18.vl_empenhado10 + t18.vl_empenhado11;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 + t18.vl_liquidado11;
                        }
                        else if (mes == 12)
                        {
                            tprev += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 + t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12;
                            trev += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 + t18.vl_revisado10 + t18.vl_revisado11 + t18.vl_revisado12;
                            treal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 + t18.vl_provisionado10 + t18.vl_provisionado11 + t18.vl_provisionado12;
                            temp += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 + t18.vl_empenhado10 + t18.vl_empenhado11 + t18.vl_empenhado12;
                            tliq += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 + t18.vl_liquidado11 + t18.vl_liquidado12;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 + t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12;
                            vlRevMesAtual = t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 + t18.vl_revisado10 + t18.vl_revisado11 + t18.vl_revisado12;
                            vlRealMesAtual = t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 + t18.vl_provisionado10 + t18.vl_provisionado11 + t18.vl_provisionado12;
                            vlEmpMesAtual = t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 + t18.vl_empenhado10 + t18.vl_empenhado11 + t18.vl_empenhado12;
                            vlLiqMesAtual = t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 + t18.vl_liquidado11 + t18.vl_liquidado12;
                        }
                    }

                    prevtotal += t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 + t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 + t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12;
                    revtotal += t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 + t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 + t18.vl_revisado10 + t18.vl_revisado11 + t18.vl_revisado12;
                    realtotal += t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 + t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 + t18.vl_provisionado10 + t18.vl_provisionado11 + t18.vl_provisionado12;
                    emptotal += t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 + t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 + t18.vl_empenhado10 + t18.vl_empenhado11 + t18.vl_empenhado12;
                    liqtotal += t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 + t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 + t18.vl_liquidado11 + t18.vl_liquidado12;
                }

                if (count % 2 != 0)
                {
                    sb.AppendLine("<tr bgcolor='#F1F5F5'>");
                }
                else
                {
                    sb.AppendLine("<tr bgcolor='#EBEBEB'>");
                }

                sb.AppendLine("<td width='134' height='30'>" + NomeFonte + "</td>");
                sb.AppendLine("<td width='136'>R$</td>");

                //valor do ano até o mês anterior INI
                sb.AppendLine("<td width='48' style='text-align:center;'>" + vlPrevMesAtual.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + vlRevMesAtual.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + vlRealMesAtual.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + vlEmpMesAtual.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + vlLiqMesAtual.ToString("N0") + "</td>");
                //sb.AppendLine("<td width='92' style='text-align:center;'>" + vlTotalMesAtual.ToString("N2").Replace(",00", "") + "</td>");
                //valor do ano até o mês anterior FIM

                //calcula o valor acumulado totatl
                sb.AppendLine("<td width='48' style='text-align:center;'>" + tprev.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + trev.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + treal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + temp.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + tliq.ToString("N0") + "</td>");
                //sb.AppendLine("<td width='92' style='text-align:center;'>" + vlTotal_acumulado.ToString("N2").Replace(",00", "") + "</td>");

                //calcula o valor totatl geral
                sb.AppendLine("<td width='48' style='text-align:center;'>" + prevtotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + revtotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + realtotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + emptotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + liqtotal.ToString("N0") + "</td>");
                //sb.AppendLine("<td width='90' style='text-align:center;'>" + vlTotal.ToString("N2").Replace(",00", "") + "</td>");

                sb.AppendLine("</tr>");

                //BLOCO - LINHA TOTAL -
                prevLinhaTotal += tprev;
                revLinhaTotal += trev;
                realLinhaTotal += treal;
                empLinhaTotal += temp;
                liqLinhaTotal += tliq;

                prevLinhaMesAtualTotal += vlPrevMesAtual;
                revLinhaMesAtualTotal += vlRevMesAtual;
                realLinhaMesAtualTotal += vlRealMesAtual;
                empLinhaMesAtualTotal += vlEmpMesAtual;
                liqLinhaMesAtualTotal += vlLiqMesAtual;

                prevLinhaTotalGeral += prevtotal;
                revLinhaTotalGeral += revtotal;
                realLinhaTotalGeral += realtotal;
                empLinhaTotalGeral += emptotal;
                liqLinhaTotalGeral += liqtotal;

                if (empty != 0)
                {
                    btnExportar.Visible = true;
                    btnPrint.Visible = true;
                    //ALGORITIMO COM CONSULTA A BASE PARA GERAR O TOTAL GERAL DE DADOS
                    if (NomeFonte != NomeFonte2 & new t29_temp_vlFinanceiroAction().ExisteFonte(Convert.ToInt32(Session["cd_projeto"]), NomeFonte, Convert.ToInt32(Session["CodRelatorio"])) == false)
                    {
                        //script cadastro
                        t29_temp_vlFinanceiro t29_temp = new t29_temp_vlFinanceiro();

                        t29_temp.t03_cd_projeto = Convert.ToInt32(Session["cd_projeto"]);
                        t29_temp.CodRelatorio = Convert.ToInt32(Session["CodRelatorio"]);
                        t29_temp.nm_fonte = NomeFonte;
                        t29_temp.nm_unidade = "R$";

                        t29_temp.PlanejadoMes += vlPrevMesAtual;
                        t29_temp.RevisadoMes += vlRevMesAtual;
                        t29_temp.RealizadoMes += vlRealMesAtual;
                        t29_temp.EmpenhadoMes += vlEmpMesAtual;
                        t29_temp.LiquidadoMes += vlLiqMesAtual;

                        t29_temp.PlanejadoAcu += tprev;
                        t29_temp.RevisadoAcu += trev;
                        t29_temp.RealizadoAcu += treal;
                        t29_temp.EmpenhadoAcu += temp;
                        t29_temp.LiquidadoAcu += tliq;

                        t29_temp.PlanejadoTot += prevtotal;
                        t29_temp.RevisadoTot += revtotal;
                        t29_temp.RealizadoTot += realtotal;
                        t29_temp.EmpenhadoTot += emptotal;
                        t29_temp.LiquidadoTot += liqtotal;
                        try
                        {
                            new t29_temp_vlFinanceiroAction().InsertDB(t29_temp);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                            throw;
                        }

                        t29_temp = null;
                        NomeFonte2 = NomeFonte;
                    }
                    else
                    {
                        
                        //script update
                        //recupera dados para somar a fonte variavel=NomeFonte
                        t29_temp_vlFinanceiro t29_temp = new t29_temp_vlFinanceiro();
                        foreach (var item in new t29_temp_vlFinanceiroAction().ListObjTodos(Convert.ToInt32(Session["cd_projeto"]), NomeFonte, Convert.ToInt32(Session["CodRelatorio"])))
                        {
                            t29_temp.t03_cd_projeto = item.t03_cd_projeto;
                            t29_temp.CodRelatorio = item.CodRelatorio;
                            t29_temp.nm_fonte = item.nm_fonte;
                            t29_temp.nm_unidade = item.nm_unidade;

                            t29_temp.PlanejadoMes = item.PlanejadoMes + vlPrevMesAtual;
                            t29_temp.RevisadoMes = item.RevisadoMes + vlRevMesAtual;
                            t29_temp.RealizadoMes = item.RealizadoMes + vlRealMesAtual;
                            t29_temp.EmpenhadoMes = item.EmpenhadoMes + vlEmpMesAtual;
                            t29_temp.LiquidadoMes = item.LiquidadoMes + vlLiqMesAtual;

                            t29_temp.PlanejadoAcu = item.PlanejadoAcu + tprev;
                            t29_temp.RevisadoAcu = item.RevisadoAcu + trev;
                            t29_temp.RealizadoAcu = item.RealizadoAcu + treal;
                            t29_temp.EmpenhadoAcu = item.EmpenhadoAcu + temp;
                            t29_temp.LiquidadoAcu = item.LiquidadoAcu + tliq;

                            t29_temp.PlanejadoTot = item.PlanejadoTot + prevtotal;
                            t29_temp.RevisadoTot = item.RevisadoTot + revtotal;
                            t29_temp.RealizadoTot = item.RealizadoTot + realtotal;
                            t29_temp.EmpenhadoTot = item.EmpenhadoTot + emptotal;
                            t29_temp.LiquidadoTot = item.LiquidadoTot + liqtotal;
                        }
                        try
                        {
                            new t29_temp_vlFinanceiroAction().UpdateDB(t29_temp);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                            throw;
                        }
                        t29_temp = null;
                    }
                    //---FIM ALGORITIMO --
                }

            }//fim laço finaceiro t11

            // LINHA DE TOTAL DE FONTES POR AÇÂO
            if (empty != 0)
            {
                btnExportar.Visible = true;
                btnPrint.Visible = true;
                if (count % 2 != 0)
                {
                    sb.AppendLine("<tr bgcolor='#F1F5F5'>");
                }
                else
                {
                    sb.AppendLine("<tr bgcolor='#EBEBEB'>");
                }

                sb.AppendLine("<td width='134' height='30'><b>Total</b></td>");
                sb.AppendLine("<td width='136'>R$</td>");

                //valor do ano até o mês anterior INI
                sb.AppendLine("<td width='48' style='text-align:center;'>" + prevLinhaMesAtualTotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + revLinhaMesAtualTotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + realLinhaMesAtualTotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + empLinhaMesAtualTotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + liqLinhaMesAtualTotal.ToString("N0") + "</td>");
                //valor do ano até o mês anterior FIM

                //calcula o valor acumulado totatl
                sb.AppendLine("<td width='48' style='text-align:center;'>" + prevLinhaTotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + revLinhaTotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + realLinhaTotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + empLinhaTotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + liqLinhaTotal.ToString("N0") + "</td>");

                //calcula o valor totatl geral
                sb.AppendLine("<td width='48' style='text-align:center;'>" + prevLinhaTotalGeral.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + revLinhaTotalGeral.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + realLinhaTotalGeral.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + empLinhaTotalGeral.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + liqLinhaTotalGeral.ToString("N0") + "</td>");

                sb.AppendLine("</tr>");
            }

            count++;
        }


        //ULTIMA LINHA QUE APRESENTA O SOMATORIO DE TODOS OS DADOS        
        {
            //----HEAD ADICIONA TOTAL GERAL DE DADOS
            if (count % 2 != 0)
                sbTotalDados.AppendLine("<tr style='background-color:#F1F5F5'>");
            else
                sbTotalDados.AppendLine("<tr style='background-color:#EBEBEB'>");

            sbTotalDados.AppendLine("<td width='30%' rowspan='" + (new t29_temp_vlFinanceiroAction().ListObjTodos(Convert.ToInt32(Session["cd_projeto"]), Convert.ToInt32(Session["CodRelatorio"])).Count() + 2) + "'><b>Total</b></td>");
            sbTotalDados.AppendLine("</tr>");

            //LAÇO DO TOTAL
            t29_temp_vlFinanceiro t29 = new t29_temp_vlFinanceiro();
            decimal vlTotalPlanMes = 0;
            decimal vlTotalRevMes = 0;
            decimal vlTotalRealMes = 0;
            decimal vlTotalEmpMes = 0;
            decimal vlTotalLiqMes = 0;

            decimal vlTotalPlanAcu = 0;
            decimal vlTotalRevAcu = 0;
            decimal vlTotalRealAcu = 0;
            decimal vlTotalEmpAcu = 0;
            decimal vlTotalLiqAcu = 0;

            decimal vlTotalPlanTot = 0;
            decimal vlTotalRevTot = 0;
            decimal vlTotalRealTot = 0;
            decimal vlTotalEmpTot = 0;
            decimal vlTotalLiqTot = 0;
            foreach (var item in new t29_temp_vlFinanceiroAction().ListObjTodos(Convert.ToInt32(Session["cd_projeto"]), Convert.ToInt32(Session["CodRelatorio"])))
            {
                if (count % 2 != 0)
                {
                    sbTotalDados.AppendLine("<tr bgcolor='#F1F5F5'>");
                }
                else
                {
                    sbTotalDados.AppendLine("<tr bgcolor='#EBEBEB'>");
                }
                sbTotalDados.AppendLine("<td width='134' height='30'><b>" + item.nm_fonte + "</b></td>");
                sbTotalDados.AppendLine("<td width='136'>R$</td>");

                //valor do ano até o mês anterior INI
                sbTotalDados.AppendLine("<td width='48' style='text-align:center;'>" + item.PlanejadoMes.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.RevisadoMes.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.RealizadoMes.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.EmpenhadoMes.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.LiquidadoMes.ToString("N0") + "</td>");
                //atribui valor da linha total
                vlTotalPlanMes += item.PlanejadoMes;
                vlTotalRevMes += item.RevisadoMes;
                vlTotalRealMes += item.RealizadoMes;
                vlTotalEmpMes += item.EmpenhadoMes;
                vlTotalLiqMes += item.LiquidadoMes;
                //valor do ano até o mês anterior FIM

                //calcula o valor acumulado totatl
                sbTotalDados.AppendLine("<td width='48' style='text-align:center;'>" + item.PlanejadoAcu.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.RevisadoAcu.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.RealizadoAcu.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.EmpenhadoAcu.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.LiquidadoAcu.ToString("N0") + "</td>");
                //atribui valor da linha total
                vlTotalPlanAcu += item.PlanejadoAcu;
                vlTotalRevAcu += item.RevisadoAcu;
                vlTotalRealAcu += item.RealizadoAcu;
                vlTotalEmpAcu += item.EmpenhadoAcu;
                vlTotalLiqAcu += item.LiquidadoAcu;
                //calcula o valor acumulado totatl FIM

                //calcula o valor totatl geral
                sbTotalDados.AppendLine("<td width='48' style='text-align:center;'>" + item.PlanejadoTot.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.RevisadoTot.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.RealizadoTot.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.EmpenhadoTot.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + item.LiquidadoTot.ToString("N0") + "</td>");
                sbTotalDados.AppendLine("</tr>");
                //atribui valor da linha total
                vlTotalPlanTot += item.PlanejadoTot;
                vlTotalRevTot += item.RevisadoTot;
                vlTotalRealTot += item.RealizadoTot;
                vlTotalEmpTot += item.EmpenhadoTot;
                vlTotalLiqTot += item.LiquidadoTot;
                //calcula o valor totatl geral FIM

            }//fim laço

            //ultima linha total
            if (count % 2 != 0)
            {
                sbTotalDados.AppendLine("<tr bgcolor='#F1F5F5'>");
            }
            else
            {
                sbTotalDados.AppendLine("<tr bgcolor='#EBEBEB'>");
            }
            sbTotalDados.AppendLine("<td width='134' height='30'><b> Total </b></td>");
            sbTotalDados.AppendLine("<td width='136'>R$</td>");

            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalPlanMes.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalRevMes.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalRealMes.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalEmpMes.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalLiqMes.ToString("N0") + "</td>");

            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalPlanAcu.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalRevAcu.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalRealAcu.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalEmpAcu.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalLiqAcu.ToString("N0") + "</td>");

            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalPlanTot.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalRevTot.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalRealTot.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalEmpTot.ToString("N0") + "</td>");
            sbTotalDados.AppendLine("<td width='55' style='text-align:center;'>" + vlTotalLiqTot.ToString("N0") + "</td>");

            sbTotalDados.AppendLine("</tr>");

            //-------------------------------------------------------------
        }

        //APAGA REGISTRO DA TABELA TEMPORARIA
        new t29_temp_vlFinanceiroAction().DeleteDB(Convert.ToInt32(Session["cd_projeto"]), Convert.ToInt32(Session["CodRelatorio"]));
        
        sb.AppendLine(sbTotalDados.ToString());        
        sb.AppendLine("<tbody></table>");
        LiteralGrid.Text = sb.ToString();
    }

    private void ExportarGridExcel()
    {
        //carrega html com formatos
        //lblExcel.Visible = true;
        //CarregarGridExcel();
        //btnExportar.Visible = true;
        //exporta
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Cookies.Clear();
        Response.Cache.SetCacheability(HttpCacheability.Private);
        Response.CacheControl = "private";

        Response.AddHeader("content-disposition", "attachment;filename=" + "Rel_Financeiro" + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        LiteralGrid.RenderControl(htmlWrite);

        Response.Write("");
        Response.Charset = "utf-8";
        Response.Write(stringWrite.ToString());

        //LiteralGrid.Visible = false;
        Response.End();

    }

    protected void cmdImprimir_Click(object sender, EventArgs e)
    {
        Response.Write("<script>window.print();</script>");
    }

    protected void cmdExportar_Click(object sender, EventArgs e)
    {
        ExportarGridExcel();
    }
}
