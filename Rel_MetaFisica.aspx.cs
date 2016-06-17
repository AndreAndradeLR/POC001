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

public partial class Rel_MetaFisica : System.Web.UI.Page
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
        PanelGrid.Visible = true;
        StringBuilder sb = new StringBuilder();

        StringBuilder sbTotalDados = new StringBuilder();
        int gd_ano = ViewState["AnoRel"] != null ? Convert.ToInt32(ViewState["AnoRel"]) : DateTime.Now.Year;
        int gd_mes = ViewState["MesRel"] != null ? Convert.ToInt32(ViewState["MesRel"]) : DateTime.Now.Month;

        int empty = 0;
        int count = 0;
        sb.AppendLine("<table width='100%' class='tbRestricao' cellpadding='2' cellspacing='0' bgcolor='#FFFFFF'>");
        sb.AppendLine("<thead><tr style='color:#ffffff;text-align:center;'>");
        sb.AppendLine("<th colspan='3' style='color:#666666;text-align:center;'>Data da Impressão:&nbsp;<span style='color:#70A2F3;'>" + string.Format("{0:dd/MM/yyyy}", DateTime.Now) + "</span></th>");

        sb.AppendLine("<th colspan='3' bgcolor='#5D7B9D' height='30'>Total em " + gd_ano + " até " + month[pb.NomeMes(gd_mes - 1)] + "</th>");

        sb.AppendLine("<th colspan='3' bgcolor='#5D7B9D' height='30'>Total Acumulado até " + month[pb.NomeMes(gd_mes - 1)] + " de " + gd_ano + "</th>");
        sb.AppendLine("<th colspan='3' bgcolor='#5D7B9D' height='30'>Total Geral</th>");
        sb.AppendLine("</tr>");
        sb.AppendLine("<tr style='font-weight:bold;color:#333333'>");
        sb.AppendLine("<th bgcolor='#87A6C9' width='30%' height='30'>Ação</td><td bgcolor='#87A6C9' width='15%'>Meta Física</td><td bgcolor='#87A6C9' width='15%'>Unidade de Medida</th>");

        sb.AppendLine("<td bgcolor='#87A6C9' width='5%' style='text-align:center;'>Previsto</td><td bgcolor='#87A6C9' width='5%' style='text-align:center;'>Realizado</td><td bgcolor='#87A6C9' width='10%' style='text-align:center;'>Realizado / Previsto (%)</th>");

        sb.AppendLine("<td bgcolor='#87A6C9' width='5%' style='text-align:center;'>Previsto</td><td bgcolor='#87A6C9' width='5%' style='text-align:center;'>Realizado</td><td bgcolor='#87A6C9' width='10%' style='text-align:center;'>Realizado / Previsto (%)</th>");
        sb.AppendLine("<td bgcolor='#87A6C9' width='5%' style='text-align:center;'>Previsto</td><td bgcolor='#87A6C9' width='5%' style='text-align:center;'>Realizado</td><td bgcolor='#87A6C9' width='10%' style='text-align:center;'>Realizado / Previsto (%)</th>");
        sb.AppendLine("</tr></thead><tbody>");

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
            empty = new t10_produtoAction().ListTodosAcoes(t08.t08_cd_acao).Rows.Count;
            if (empty == 0)
            {
                btnExportar.Visible = false;
                btnPrint.Visible = false;
                sb.AppendLine("<td width='30%'><b>" + t08.nm_acao + "</b></td>");
                for (int i = 1; i < 9; i++)
                {
                    sb.AppendLine("<td style='text-align:center;'> - </td>");
                }
            }
            else
            {
                btnExportar.Visible = true;
                btnPrint.Visible = true;
                sb.AppendLine("<td width='30%' rowspan='" + (empty + 1) + "'><b>" + t08.nm_acao + "</b></td>");
            }
            sb.AppendLine("</tr>");
            //verifica se existe meta para ação            
            foreach (t10_produto t10 in new t10_produtoAction().ListObjTodos(t08.t08_cd_acao))
            {
                var t = t10.nm_medida;
                int ano = gd_ano;
                int mes = gd_mes;
                decimal tprev = 0;
                decimal treal = 0;
                decimal vlPrevMesAtual = 0;
                decimal vlRealMesAtual = 0;
                decimal vlTotalMesAtual = 0;
                decimal prevtotal = 0;
                decimal realtotal = 0;
                decimal vlTotal_acumulado = 0;
                decimal vlTotal = 0;
                ViewState["tprev"] = 0;
                ViewState["treal"] = 0;
                ViewState["tprevMesAtual"] = 0;
                ViewState["trealMesAtual"] = 0;
                foreach (t17_vlproduto t17 in t10.t17)
                {
                    if (ano > t17.nu_ano)
                    {
                        tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                             t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                        treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                             t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
                        ViewState["tprev"] = tprev;
                        ViewState["treal"] = treal;

                        ViewState["tprevMesAtual"] = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                             t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                        ViewState["trealMesAtual"] = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                             t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
                        //quando for janeiro pega total do ano anterior até dezembro
                        if (mes == 1)
                        {
                            if (ano == (t17.nu_ano + 1))
                            {
                                vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                             t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;

                                vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                     t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
                            }
                        }
                    }
                    else if (ano == t17.nu_ano)
                    {
                        //if (mes == 1)
                        //{
                        //    //tprev += t17.vl_p1;
                        //    //treal += t17.vl_r1;
                        //    tprev = Convert.ToDecimal(ViewState["tprev"]);
                        //    treal = Convert.ToDecimal(ViewState["treal"]);

                        //    vlPrevMesAtual = Convert.ToDecimal(ViewState["tprevMesAtual"]);
                        //    vlRealMesAtual = Convert.ToDecimal(ViewState["trealMesAtual"]);
                        //}else
                        if (mes == 1)
                        {
                            tprev += t17.vl_p1;
                            treal += t17.vl_r1;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1;
                            vlRealMesAtual = t17.vl_r1;
                        }
                        else if (mes == 2)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2;
                            treal += t17.vl_r1 + t17.vl_r2;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2;
                        }
                        else if (mes == 3)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3;
                        }
                        else if (mes == 4)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4;
                        }
                        else if (mes == 5)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5;
                        }
                        else if (mes == 6)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6;
                        }
                        else if (mes == 7)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                 t17.vl_r7;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                t17.vl_r7;
                        }
                        else if (mes == 8)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                 t17.vl_r7 + t17.vl_r8;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                t17.vl_r7 + t17.vl_r8;
                        }
                        else if (mes == 9)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8 + t17.vl_p9;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                 t17.vl_r7 + t17.vl_r8 + t17.vl_r9;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8 + t17.vl_p9;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                t17.vl_r7 + t17.vl_r8 + t17.vl_r9;
                        }
                        else if (mes == 10)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                 t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10;
                        }
                        else if (mes == 11)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                 t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11;
                        }
                        else if (mes == 12)
                        {
                            tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                            treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                 t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;

                            //valor do ano até o mês anterior
                            vlPrevMesAtual = t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                                t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                            vlRealMesAtual = t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                                t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
                        }
                    }
                    prevtotal += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                             t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;

                    realtotal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                             t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
                }

                if (count % 2 != 0)
                {
                    sb.AppendLine("<tr bgcolor='#F1F5F5'>");
                }
                else
                {
                    sb.AppendLine("<tr bgcolor='#EBEBEB'>");
                }

                sb.AppendLine("<td width='134' height='30'>" + t10.ds_produto + "</td>");
                sb.AppendLine("<td width='136'>" + t10.nm_medida + "</td>");

                //valor do ano até o mês anterior INI
                sb.AppendLine("<td width='48' style='text-align:center;'>" + vlPrevMesAtual.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + vlRealMesAtual.ToString("N0") + "</td>");

                //calcula o valor acumulado totatl
                if ((vlPrevMesAtual == 0) && (vlRealMesAtual != 0))
                {
                    vlTotalMesAtual = 100;
                }
                else
                {
                    if (vlPrevMesAtual > 0)
                    {
                        vlTotalMesAtual = (vlRealMesAtual / vlPrevMesAtual) * 100;
                    }
                    else
                    {
                        vlTotalMesAtual = 0;
                    }
                }
                sb.AppendLine("<td width='92' style='text-align:center;'>" + vlTotalMesAtual.ToString("N2").Replace(",00", "") + "</td>");
                //valor do ano até o mês anterior FIM

                sb.AppendLine("<td width='48' style='text-align:center;'>" + tprev.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + treal.ToString("N0") + "</td>");
                //calcula o valor acumulado totatl
                if ((tprev == 0) && (treal != 0))
                {
                    vlTotal_acumulado = 100;
                }
                else
                {
                    if (tprev > 0)
                    {
                        vlTotal_acumulado = (treal / tprev) * 100;
                    }
                    else
                    {
                        vlTotal_acumulado = 0;
                    }
                }
                sb.AppendLine("<td width='92' style='text-align:center;'>" + vlTotal_acumulado.ToString("N2").Replace(",00", "") + "</td>");

                sb.AppendLine("<td width='48' style='text-align:center;'>" + prevtotal.ToString("N0") + "</td>");
                sb.AppendLine("<td width='55' style='text-align:center;'>" + realtotal.ToString("N0") + "</td>");
                //calcula o valor totatl geral
                if ((prevtotal == 0) && (realtotal != 0))
                {
                    vlTotal = 100;
                }
                else
                {
                    if (prevtotal > 0)
                    {
                        vlTotal = (realtotal / prevtotal) * 100;
                    }
                    else
                    {
                        vlTotal = 0;
                    }
                }
                sb.AppendLine("<td width='90' style='text-align:center;'>" + vlTotal.ToString("N2").Replace(",00", "") + "</td>");
                sb.AppendLine("</tr>");
                vlTotal_acumulado = 0;
                vlTotal = 0;
            }
            count++;
        }
        sb.AppendLine("<tbody></table>");
        LiteralGrid.Text = sb.ToString();
    }

    private void ExportarGridExcel()
    {
        //carrega html com formatos
        //btnExportar.Visible = true;
        //CarregarGridExcel();

        //exporta
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Cookies.Clear();
        Response.Cache.SetCacheability(HttpCacheability.Private);
        Response.CacheControl = "private";

        Response.AddHeader("content-disposition", "attachment;filename=" + "Rel_MetaFisica" + ".xls");
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
