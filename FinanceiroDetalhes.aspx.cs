using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FinanceiroDetalhes : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));
        lblNomeAcao.Text = t08.nm_acao;
        ucLancFin.inicio = t08.dt_inicio.Year;
        ucLancFin.fim = t08.dt_fim.Year;
        

        if (Session["ScreenResolution"] != null)
        {
            ScreenResolution scr = new ScreenResolution();
            scr = (ScreenResolution)Session["ScreenResolution"];
            if (scr.WidthWeb > 0)
            {
                PanelP.Width = Unit.Pixel(scr.WidthWeb);
            }
        }

    }
    protected void LancamentoFinanceiro_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (pb.Session("cd_financeiro").ToString() != "0")
            {
                Retrieve();
                
                //Remover casas decimais caso seja igual a zero
                Panel pn = (Panel)ucLancFin.FindControl("Panel1");
                if (pn != null)
                {
                    foreach (Control ctrl in pn.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            ((TextBox)ctrl).Text = ((TextBox)ctrl).Text.Replace(",00", "");
                        }
                        else if (ctrl is Label)
                        {
                            ((Label)ctrl).Text = ((Label)ctrl).Text.Replace(",00", "");
                        }
                    }
                }
                
            }
        }
    }
    
    public void Retrieve()
    {
        t11_financeiro t11 = new t11_financeiroAction().Retrieve(Convert.ToInt32(pb.Session("cd_financeiro")));
        lblnm_fonte.Text = new t27_fonteAction().Retrieve(t11.t27_cd_fonte).nm_fonte;

        t18b_vlfinanceiro t18ac = new t18b_vlfinanceiro();
        decimal totvl_planejado = 0;
        decimal totvl_provisionado = 0;
        decimal totvl_empenhado = 0;
        decimal totvl_liquidado = 0;
        decimal totvl_revisado = 0;

        foreach (t18b_vlfinanceiro t18 in t11.t18l)
        {
            TextBox txtvl_restopagar = (TextBox)ucLancFin.FindControl("txtvl_restopagar" + t18.nu_ano.ToString());
            TextBox txtvl_dotorcado = (TextBox)ucLancFin.FindControl("txtvl_dotorcado" + t18.nu_ano.ToString());
            TextBox txtvl_assegurado = (TextBox)ucLancFin.FindControl("txtvl_assegurado" + t18.nu_ano.ToString());

            TextBox txtvl_planejado1 = (TextBox)ucLancFin.FindControl("txtvl_planejado1" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado1 = (TextBox)ucLancFin.FindControl("txtvl_provisionado1" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado1 = (TextBox)ucLancFin.FindControl("txtvl_empenhado1" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado1 = (TextBox)ucLancFin.FindControl("txtvl_liquidado1" + t18.nu_ano.ToString());
            TextBox txtvl_revisado1 = (TextBox)ucLancFin.FindControl("txtvl_revisado1" + t18.nu_ano.ToString());

            TextBox txtvl_planejado2 = (TextBox)ucLancFin.FindControl("txtvl_planejado2" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado2 = (TextBox)ucLancFin.FindControl("txtvl_provisionado2" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado2 = (TextBox)ucLancFin.FindControl("txtvl_empenhado2" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado2 = (TextBox)ucLancFin.FindControl("txtvl_liquidado2" + t18.nu_ano.ToString());
            TextBox txtvl_revisado2 = (TextBox)ucLancFin.FindControl("txtvl_revisado2" + t18.nu_ano.ToString());

            TextBox txtvl_planejado3 = (TextBox)ucLancFin.FindControl("txtvl_planejado3" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado3 = (TextBox)ucLancFin.FindControl("txtvl_provisionado3" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado3 = (TextBox)ucLancFin.FindControl("txtvl_empenhado3" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado3 = (TextBox)ucLancFin.FindControl("txtvl_liquidado3" + t18.nu_ano.ToString());
            TextBox txtvl_revisado3 = (TextBox)ucLancFin.FindControl("txtvl_revisado3" + t18.nu_ano.ToString());


            TextBox txtvl_planejado4 = (TextBox)ucLancFin.FindControl("txtvl_planejado4" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado4 = (TextBox)ucLancFin.FindControl("txtvl_provisionado4" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado4 = (TextBox)ucLancFin.FindControl("txtvl_empenhado4" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado4 = (TextBox)ucLancFin.FindControl("txtvl_liquidado4" + t18.nu_ano.ToString());
            TextBox txtvl_revisado4 = (TextBox)ucLancFin.FindControl("txtvl_revisado4" + t18.nu_ano.ToString());


            TextBox txtvl_planejado5 = (TextBox)ucLancFin.FindControl("txtvl_planejado5" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado5 = (TextBox)ucLancFin.FindControl("txtvl_provisionado5" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado5 = (TextBox)ucLancFin.FindControl("txtvl_empenhado5" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado5 = (TextBox)ucLancFin.FindControl("txtvl_liquidado5" + t18.nu_ano.ToString());
            TextBox txtvl_revisado5 = (TextBox)ucLancFin.FindControl("txtvl_revisado5" + t18.nu_ano.ToString());


            TextBox txtvl_planejado6 = (TextBox)ucLancFin.FindControl("txtvl_planejado6" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado6 = (TextBox)ucLancFin.FindControl("txtvl_provisionado6" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado6 = (TextBox)ucLancFin.FindControl("txtvl_empenhado6" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado6 = (TextBox)ucLancFin.FindControl("txtvl_liquidado6" + t18.nu_ano.ToString());
            TextBox txtvl_revisado6 = (TextBox)ucLancFin.FindControl("txtvl_revisado6" + t18.nu_ano.ToString());


            TextBox txtvl_planejado7 = (TextBox)ucLancFin.FindControl("txtvl_planejado7" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado7 = (TextBox)ucLancFin.FindControl("txtvl_provisionado7" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado7 = (TextBox)ucLancFin.FindControl("txtvl_empenhado7" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado7 = (TextBox)ucLancFin.FindControl("txtvl_liquidado7" + t18.nu_ano.ToString());
            TextBox txtvl_revisado7 = (TextBox)ucLancFin.FindControl("txtvl_revisado7" + t18.nu_ano.ToString());

            TextBox txtvl_planejado8 = (TextBox)ucLancFin.FindControl("txtvl_planejado8" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado8 = (TextBox)ucLancFin.FindControl("txtvl_provisionado8" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado8 = (TextBox)ucLancFin.FindControl("txtvl_empenhado8" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado8 = (TextBox)ucLancFin.FindControl("txtvl_liquidado8" + t18.nu_ano.ToString());
            TextBox txtvl_revisado8 = (TextBox)ucLancFin.FindControl("txtvl_revisado8" + t18.nu_ano.ToString());


            TextBox txtvl_planejado9 = (TextBox)ucLancFin.FindControl("txtvl_planejado9" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado9 = (TextBox)ucLancFin.FindControl("txtvl_provisionado9" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado9 = (TextBox)ucLancFin.FindControl("txtvl_empenhado9" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado9 = (TextBox)ucLancFin.FindControl("txtvl_liquidado9" + t18.nu_ano.ToString());
            TextBox txtvl_revisado9 = (TextBox)ucLancFin.FindControl("txtvl_revisado9" + t18.nu_ano.ToString());


            TextBox txtvl_planejado10 = (TextBox)ucLancFin.FindControl("txtvl_planejado10" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado10 = (TextBox)ucLancFin.FindControl("txtvl_provisionado10" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado10 = (TextBox)ucLancFin.FindControl("txtvl_empenhado10" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado10 = (TextBox)ucLancFin.FindControl("txtvl_liquidado10" + t18.nu_ano.ToString());
            TextBox txtvl_revisado10 = (TextBox)ucLancFin.FindControl("txtvl_revisado10" + t18.nu_ano.ToString());


            TextBox txtvl_planejado11 = (TextBox)ucLancFin.FindControl("txtvl_planejado11" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado11 = (TextBox)ucLancFin.FindControl("txtvl_provisionado11" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado11 = (TextBox)ucLancFin.FindControl("txtvl_empenhado11" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado11 = (TextBox)ucLancFin.FindControl("txtvl_liquidado11" + t18.nu_ano.ToString());
            TextBox txtvl_revisado11 = (TextBox)ucLancFin.FindControl("txtvl_revisado11" + t18.nu_ano.ToString());


            TextBox txtvl_planejado12 = (TextBox)ucLancFin.FindControl("txtvl_planejado12" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado12 = (TextBox)ucLancFin.FindControl("txtvl_provisionado12" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado12 = (TextBox)ucLancFin.FindControl("txtvl_empenhado12" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado12 = (TextBox)ucLancFin.FindControl("txtvl_liquidado12" + t18.nu_ano.ToString());
            TextBox txtvl_revisado12 = (TextBox)ucLancFin.FindControl("txtvl_revisado12" + t18.nu_ano.ToString());


            /*
             * TOTAIS DA LINHA
             */
            Label lblvl_planejado = (Label)ucLancFin.FindControl("lblvl_planejado" + t18.nu_ano.ToString());
            Label lblvl_provisionado = (Label)ucLancFin.FindControl("lblvl_provisionado" + t18.nu_ano.ToString());
            Label lblvl_empenhado = (Label)ucLancFin.FindControl("lblvl_empenhado" + t18.nu_ano.ToString());
            Label lblvl_liquidado = (Label)ucLancFin.FindControl("lblvl_liquidado" + t18.nu_ano.ToString());
            Label lblvl_revisado = (Label)ucLancFin.FindControl("lblvl_revisado" + t18.nu_ano.ToString());
            


            if (txtvl_planejado1 != null)
            {
                txtvl_dotorcado.Text = t18.vl_dotorcado.ToString("N0");
                txtvl_restopagar.Text = t18.vl_restopagar.ToString("N0");
                txtvl_assegurado.Text = t18.vl_assegurado.ToString("N0");

                txtvl_planejado1.Text = t18.vl_planejado1.ToString("N0");
                txtvl_provisionado1.Text = t18.vl_provisionado1.ToString("N0");
                txtvl_empenhado1.Text = t18.vl_empenhado1.ToString("N0");
                txtvl_liquidado1.Text = t18.vl_liquidado1.ToString("N0");
                txtvl_revisado1.Text = t18.vl_revisado1.ToString("N0");

                txtvl_planejado2.Text = t18.vl_planejado2.ToString("N0");
                txtvl_provisionado2.Text = t18.vl_provisionado2.ToString("N0");
                txtvl_empenhado2.Text = t18.vl_empenhado2.ToString("N0");
                txtvl_liquidado2.Text = t18.vl_liquidado2.ToString("N0");
                txtvl_revisado2.Text = t18.vl_revisado2.ToString("N0");

                txtvl_planejado3.Text = t18.vl_planejado3.ToString("N0");
                txtvl_provisionado3.Text = t18.vl_provisionado3.ToString("N0");
                txtvl_empenhado3.Text = t18.vl_empenhado3.ToString("N0");
                txtvl_liquidado3.Text = t18.vl_liquidado3.ToString("N0");
                txtvl_revisado3.Text = t18.vl_revisado3.ToString("N0");

                txtvl_planejado4.Text = t18.vl_planejado4.ToString("N0");
                txtvl_provisionado4.Text = t18.vl_provisionado4.ToString("N0");
                txtvl_empenhado4.Text = t18.vl_empenhado4.ToString("N0");
                txtvl_liquidado4.Text = t18.vl_liquidado4.ToString("N0");
                txtvl_revisado4.Text = t18.vl_revisado4.ToString("N0");

                txtvl_planejado5.Text = t18.vl_planejado5.ToString("N0");
                txtvl_provisionado5.Text = t18.vl_provisionado5.ToString("N0");
                txtvl_empenhado5.Text = t18.vl_empenhado5.ToString("N0");
                txtvl_liquidado5.Text = t18.vl_liquidado5.ToString("N0");
                txtvl_revisado5.Text = t18.vl_revisado5.ToString("N0");

                txtvl_planejado6.Text = t18.vl_planejado6.ToString("N0");
                txtvl_provisionado6.Text = t18.vl_provisionado6.ToString("N0");
                txtvl_empenhado6.Text = t18.vl_empenhado6.ToString("N0");
                txtvl_liquidado6.Text = t18.vl_liquidado6.ToString("N0");
                txtvl_revisado6.Text = t18.vl_revisado6.ToString("N0");

                txtvl_planejado7.Text = t18.vl_planejado7.ToString("N0");
                txtvl_provisionado7.Text = t18.vl_provisionado7.ToString("N0");
                txtvl_empenhado7.Text = t18.vl_empenhado7.ToString("N0");
                txtvl_liquidado7.Text = t18.vl_liquidado7.ToString("N0");
                txtvl_revisado7.Text = t18.vl_revisado7.ToString("N0");

                txtvl_planejado8.Text = t18.vl_planejado8.ToString("N0");
                txtvl_provisionado8.Text = t18.vl_provisionado8.ToString("N0");
                txtvl_empenhado8.Text = t18.vl_empenhado8.ToString("N0");
                txtvl_liquidado8.Text = t18.vl_liquidado8.ToString("N0");
                txtvl_revisado8.Text = t18.vl_revisado8.ToString("N0");

                txtvl_planejado9.Text = t18.vl_planejado9.ToString("N0");
                txtvl_provisionado9.Text = t18.vl_provisionado9.ToString("N0");
                txtvl_empenhado9.Text = t18.vl_empenhado9.ToString("N0");
                txtvl_liquidado9.Text = t18.vl_liquidado9.ToString("N0");
                txtvl_revisado9.Text = t18.vl_revisado9.ToString("N0");

                txtvl_planejado10.Text = t18.vl_planejado10.ToString("N0");
                txtvl_provisionado10.Text = t18.vl_provisionado10.ToString("N0");
                txtvl_empenhado10.Text = t18.vl_empenhado10.ToString("N0");
                txtvl_liquidado10.Text = t18.vl_liquidado10.ToString("N0");
                txtvl_revisado10.Text = t18.vl_revisado10.ToString("N0");

                txtvl_planejado11.Text = t18.vl_planejado11.ToString("N0");
                txtvl_provisionado11.Text = t18.vl_provisionado11.ToString("N0");
                txtvl_empenhado11.Text = t18.vl_empenhado11.ToString("N0");
                txtvl_liquidado11.Text = t18.vl_liquidado11.ToString("N0");
                txtvl_revisado11.Text = t18.vl_revisado11.ToString("N0");

                txtvl_planejado12.Text = t18.vl_planejado12.ToString("N0");
                txtvl_provisionado12.Text = t18.vl_provisionado12.ToString("N0");
                txtvl_empenhado12.Text = t18.vl_empenhado12.ToString("N0");
                txtvl_liquidado12.Text = t18.vl_liquidado12.ToString("N0");
                txtvl_revisado12.Text = t18.vl_revisado12.ToString("N0");


                /*
                 * TOTAIS DA LINHA
                 */
                decimal vl_planejado = (t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                   t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 +
                   t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12);
                lblvl_planejado.Text = vl_planejado.ToString("N0");
                totvl_planejado += vl_planejado;

                decimal vl_provisionado = (t18.vl_provisionado1 + t18.vl_provisionado2 + t18.vl_provisionado3 + t18.vl_provisionado4 + t18.vl_provisionado5 +
                    t18.vl_provisionado6 + t18.vl_provisionado7 + t18.vl_provisionado8 + t18.vl_provisionado9 +
                    t18.vl_provisionado10 + t18.vl_provisionado11 + t18.vl_provisionado12);
                lblvl_provisionado.Text = vl_provisionado.ToString("N0");
                totvl_provisionado += vl_provisionado;

                decimal vl_empenhado = (t18.vl_empenhado1 + t18.vl_empenhado2 + t18.vl_empenhado3 + t18.vl_empenhado4 + t18.vl_empenhado5 +
                    t18.vl_empenhado6 + t18.vl_empenhado7 + t18.vl_empenhado8 + t18.vl_empenhado9 +
                    t18.vl_empenhado10 + t18.vl_empenhado11 + t18.vl_empenhado12);
                lblvl_empenhado.Text = vl_empenhado.ToString("N0");
                totvl_empenhado += vl_empenhado;

                decimal vl_liquidado = (t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                    t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 +
                    t18.vl_liquidado10 + t18.vl_liquidado11 + t18.vl_liquidado12);
                lblvl_liquidado.Text = vl_liquidado.ToString("N0");
                totvl_liquidado += vl_liquidado;

                decimal vl_revisado = (t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 +
                    t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 +
                    t18.vl_revisado10 + t18.vl_revisado11 + t18.vl_revisado12);
                lblvl_revisado.Text = vl_revisado.ToString("N0");
                totvl_revisado += vl_revisado;

                /*
                 * TOTAIS DAS COLUNAS ACUMULADOR
                 */
                t18ac.vl_restopagar += t18.vl_restopagar;
                t18ac.vl_dotorcado += t18.vl_dotorcado;
                t18ac.vl_assegurado += t18.vl_assegurado;

                t18ac.vl_planejado1 += t18.vl_planejado1;
                t18ac.vl_provisionado1 += t18.vl_provisionado1;
                t18ac.vl_empenhado1 += t18.vl_empenhado1;
                t18ac.vl_liquidado1 += t18.vl_liquidado1;
                t18ac.vl_revisado1 += t18.vl_revisado1;

                t18ac.vl_planejado2 += t18.vl_planejado2;
                t18ac.vl_provisionado2 += t18.vl_provisionado2;
                t18ac.vl_empenhado2 += t18.vl_empenhado2;
                t18ac.vl_liquidado2 += t18.vl_liquidado2;
                t18ac.vl_revisado2 += t18.vl_revisado2;

                t18ac.vl_planejado3 += t18.vl_planejado3;
                t18ac.vl_provisionado3 += t18.vl_provisionado3;
                t18ac.vl_empenhado3 += t18.vl_empenhado3;
                t18ac.vl_liquidado3 += t18.vl_liquidado3;
                t18ac.vl_revisado3 += t18.vl_revisado3;

                t18ac.vl_planejado4 += t18.vl_planejado4;
                t18ac.vl_provisionado4 += t18.vl_provisionado4;
                t18ac.vl_empenhado4 += t18.vl_empenhado4;
                t18ac.vl_liquidado4 += t18.vl_liquidado4;
                t18ac.vl_revisado4 += t18.vl_revisado4;

                t18ac.vl_planejado5 += t18.vl_planejado5;
                t18ac.vl_provisionado5 += t18.vl_provisionado5;
                t18ac.vl_empenhado5 += t18.vl_empenhado5;
                t18ac.vl_liquidado5 += t18.vl_liquidado5;
                t18ac.vl_revisado5 += t18.vl_revisado5;

                t18ac.vl_planejado6 += t18.vl_planejado6;
                t18ac.vl_provisionado6 += t18.vl_provisionado6;
                t18ac.vl_empenhado6 += t18.vl_empenhado6;
                t18ac.vl_liquidado6 += t18.vl_liquidado6;
                t18ac.vl_revisado6 += t18.vl_revisado6;

                t18ac.vl_planejado7 += t18.vl_planejado7;
                t18ac.vl_provisionado7 += t18.vl_provisionado7;
                t18ac.vl_empenhado7 += t18.vl_empenhado7;
                t18ac.vl_liquidado7 += t18.vl_liquidado7;
                t18ac.vl_revisado7 += t18.vl_revisado7;

                t18ac.vl_planejado8 += t18.vl_planejado8;
                t18ac.vl_provisionado8 += t18.vl_provisionado8;
                t18ac.vl_empenhado8 += t18.vl_empenhado8;
                t18ac.vl_liquidado8 += t18.vl_liquidado8;
                t18ac.vl_revisado8 += t18.vl_revisado8;

                t18ac.vl_planejado9 += t18.vl_planejado9;
                t18ac.vl_provisionado9 += t18.vl_provisionado9;
                t18ac.vl_empenhado9 += t18.vl_empenhado9;
                t18ac.vl_liquidado9 += t18.vl_liquidado9;
                t18ac.vl_revisado9 += t18.vl_revisado9;

                t18ac.vl_planejado10 += t18.vl_planejado10;
                t18ac.vl_provisionado10 += t18.vl_provisionado10;
                t18ac.vl_empenhado10 += t18.vl_empenhado10;
                t18ac.vl_liquidado10 += t18.vl_liquidado10;
                t18ac.vl_revisado10 += t18.vl_revisado10;

                t18ac.vl_planejado11 += t18.vl_planejado11;
                t18ac.vl_provisionado11 += t18.vl_provisionado11;
                t18ac.vl_empenhado11 += t18.vl_empenhado11;
                t18ac.vl_liquidado11 += t18.vl_liquidado11;
                t18ac.vl_revisado11 += t18.vl_revisado11;

                t18ac.vl_planejado12 += t18.vl_planejado12;
                t18ac.vl_provisionado12 += t18.vl_provisionado12;
                t18ac.vl_empenhado12 += t18.vl_empenhado12;
                t18ac.vl_liquidado12 += t18.vl_liquidado12;
                t18ac.vl_revisado12 += t18.vl_revisado12;
            }
        }

        /*
         * TOTAIS DAS COLUNAS 
         */

        Label lblvl_restopagar = (Label)ucLancFin.FindControl("lblvl_restopagar");
        Label lblvl_dotorcado = (Label)ucLancFin.FindControl("lblvl_dotorcado");
        Label lblvl_assegurado = (Label)ucLancFin.FindControl("lblvl_assegurado");
               
        Label lblvl_planejado1 = (Label)ucLancFin.FindControl("lblvl_planejado1");
        Label lblvl_provisionado1 = (Label)ucLancFin.FindControl("lblvl_provisionado1");
        Label lblvl_empenhado1 = (Label)ucLancFin.FindControl("lblvl_empenhado1");
        Label lblvl_liquidado1 = (Label)ucLancFin.FindControl("lblvl_liquidado1");
        Label lblvl_revisado1 = (Label)ucLancFin.FindControl("lblvl_revisado1");

        Label lblvl_planejado2 = (Label)ucLancFin.FindControl("lblvl_planejado2");
        Label lblvl_provisionado2 = (Label)ucLancFin.FindControl("lblvl_provisionado2");
        Label lblvl_empenhado2 = (Label)ucLancFin.FindControl("lblvl_empenhado2");
        Label lblvl_liquidado2 = (Label)ucLancFin.FindControl("lblvl_liquidado2");
        Label lblvl_revisado2 = (Label)ucLancFin.FindControl("lblvl_revisado2");

        Label lblvl_planejado3 = (Label)ucLancFin.FindControl("lblvl_planejado3");
        Label lblvl_provisionado3 = (Label)ucLancFin.FindControl("lblvl_provisionado3");
        Label lblvl_empenhado3 = (Label)ucLancFin.FindControl("lblvl_empenhado3");
        Label lblvl_liquidado3 = (Label)ucLancFin.FindControl("lblvl_liquidado3");
        Label lblvl_revisado3 = (Label)ucLancFin.FindControl("lblvl_revisado3");


        Label lblvl_planejado4 = (Label)ucLancFin.FindControl("lblvl_planejado4");
        Label lblvl_provisionado4 = (Label)ucLancFin.FindControl("lblvl_provisionado4");
        Label lblvl_empenhado4 = (Label)ucLancFin.FindControl("lblvl_empenhado4");
        Label lblvl_liquidado4 = (Label)ucLancFin.FindControl("lblvl_liquidado4");
        Label lblvl_revisado4 = (Label)ucLancFin.FindControl("lblvl_revisado4");


        Label lblvl_planejado5 = (Label)ucLancFin.FindControl("lblvl_planejado5");
        Label lblvl_provisionado5 = (Label)ucLancFin.FindControl("lblvl_provisionado5");
        Label lblvl_empenhado5 = (Label)ucLancFin.FindControl("lblvl_empenhado5");
        Label lblvl_liquidado5 = (Label)ucLancFin.FindControl("lblvl_liquidado5");
        Label lblvl_revisado5 = (Label)ucLancFin.FindControl("lblvl_revisado5");


        Label lblvl_planejado6 = (Label)ucLancFin.FindControl("lblvl_planejado6");
        Label lblvl_provisionado6 = (Label)ucLancFin.FindControl("lblvl_provisionado6");
        Label lblvl_empenhado6 = (Label)ucLancFin.FindControl("lblvl_empenhado6");
        Label lblvl_liquidado6 = (Label)ucLancFin.FindControl("lblvl_liquidado6");
        Label lblvl_revisado6 = (Label)ucLancFin.FindControl("lblvl_revisado6");


        Label lblvl_planejado7 = (Label)ucLancFin.FindControl("lblvl_planejado7");
        Label lblvl_provisionado7 = (Label)ucLancFin.FindControl("lblvl_provisionado7");
        Label lblvl_empenhado7 = (Label)ucLancFin.FindControl("lblvl_empenhado7");
        Label lblvl_liquidado7 = (Label)ucLancFin.FindControl("lblvl_liquidado7");
        Label lblvl_revisado7 = (Label)ucLancFin.FindControl("lblvl_revisado7");

        Label lblvl_planejado8 = (Label)ucLancFin.FindControl("lblvl_planejado8");
        Label lblvl_provisionado8 = (Label)ucLancFin.FindControl("lblvl_provisionado8");
        Label lblvl_empenhado8 = (Label)ucLancFin.FindControl("lblvl_empenhado8");
        Label lblvl_liquidado8 = (Label)ucLancFin.FindControl("lblvl_liquidado8");
        Label lblvl_revisado8 = (Label)ucLancFin.FindControl("lblvl_revisado8");


        Label lblvl_planejado9 = (Label)ucLancFin.FindControl("lblvl_planejado9");
        Label lblvl_provisionado9 = (Label)ucLancFin.FindControl("lblvl_provisionado9");
        Label lblvl_empenhado9 = (Label)ucLancFin.FindControl("lblvl_empenhado9");
        Label lblvl_liquidado9 = (Label)ucLancFin.FindControl("lblvl_liquidado9");
        Label lblvl_revisado9 = (Label)ucLancFin.FindControl("lblvl_revisado9");


        Label lblvl_planejado10 = (Label)ucLancFin.FindControl("lblvl_planejado10");
        Label lblvl_provisionado10 = (Label)ucLancFin.FindControl("lblvl_provisionado10");
        Label lblvl_empenhado10 = (Label)ucLancFin.FindControl("lblvl_empenhado10");
        Label lblvl_liquidado10 = (Label)ucLancFin.FindControl("lblvl_liquidado10");
        Label lblvl_revisado10 = (Label)ucLancFin.FindControl("lblvl_revisado10");


        Label lblvl_planejado11 = (Label)ucLancFin.FindControl("lblvl_planejado11");
        Label lblvl_provisionado11 = (Label)ucLancFin.FindControl("lblvl_provisionado11");
        Label lblvl_empenhado11 = (Label)ucLancFin.FindControl("lblvl_empenhado11");
        Label lblvl_liquidado11 = (Label)ucLancFin.FindControl("lblvl_liquidado11");
        Label lblvl_revisado11 = (Label)ucLancFin.FindControl("lblvl_revisado11");


        Label lblvl_planejado12 = (Label)ucLancFin.FindControl("lblvl_planejado12");
        Label lblvl_provisionado12 = (Label)ucLancFin.FindControl("lblvl_provisionado12");
        Label lblvl_empenhado12 = (Label)ucLancFin.FindControl("lblvl_empenhado12");
        Label lblvl_liquidado12 = (Label)ucLancFin.FindControl("lblvl_liquidado12");
        Label lblvl_revisado12 = (Label)ucLancFin.FindControl("lblvl_revisado12");

        Label lbltotvl_planejado = (Label)ucLancFin.FindControl("lblvl_planejado");
        Label lbltotvl_provisionado = (Label)ucLancFin.FindControl("lblvl_provisionado");
        Label lbltotvl_empenhado = (Label)ucLancFin.FindControl("lblvl_empenhado");
        Label lbltotvl_liquidado = (Label)ucLancFin.FindControl("lblvl_liquidado");
        Label lbltotvl_revisado = (Label)ucLancFin.FindControl("lblvl_revisado");


        if (lblvl_restopagar != null)
        {
            lblvl_restopagar.Text = t18ac.vl_restopagar.ToString("N0");
            lblvl_dotorcado.Text = t18ac.vl_dotorcado.ToString("N0");
            lblvl_assegurado.Text = t18ac.vl_assegurado.ToString("N0");

            lblvl_planejado1.Text = t18ac.vl_planejado1.ToString("N0");
            lblvl_provisionado1.Text = t18ac.vl_provisionado1.ToString("N0");
            lblvl_empenhado1.Text = t18ac.vl_empenhado1.ToString("N0");
            lblvl_liquidado1.Text = t18ac.vl_liquidado1.ToString("N0");
            lblvl_revisado1.Text = t18ac.vl_revisado1.ToString("N0");

            lblvl_planejado2.Text = t18ac.vl_planejado2.ToString("N0");
            lblvl_provisionado2.Text = t18ac.vl_provisionado2.ToString("N0");
            lblvl_empenhado2.Text = t18ac.vl_empenhado2.ToString("N0");
            lblvl_liquidado2.Text = t18ac.vl_liquidado2.ToString("N0");
            lblvl_revisado2.Text = t18ac.vl_revisado2.ToString("N0");

            lblvl_planejado3.Text = t18ac.vl_planejado3.ToString("N0");
            lblvl_provisionado3.Text = t18ac.vl_provisionado3.ToString("N0");
            lblvl_empenhado3.Text = t18ac.vl_empenhado3.ToString("N0");
            lblvl_liquidado3.Text = t18ac.vl_liquidado3.ToString("N0");
            lblvl_revisado3.Text = t18ac.vl_revisado3.ToString("N0");

            lblvl_planejado4.Text = t18ac.vl_planejado4.ToString("N0");
            lblvl_provisionado4.Text = t18ac.vl_provisionado4.ToString("N0");
            lblvl_empenhado4.Text = t18ac.vl_empenhado4.ToString("N0");
            lblvl_liquidado4.Text = t18ac.vl_liquidado4.ToString("N0");
            lblvl_revisado4.Text = t18ac.vl_revisado4.ToString("N0");

            lblvl_planejado5.Text = t18ac.vl_planejado5.ToString("N0");
            lblvl_provisionado5.Text = t18ac.vl_provisionado5.ToString("N0");
            lblvl_empenhado5.Text = t18ac.vl_empenhado5.ToString("N0");
            lblvl_liquidado5.Text = t18ac.vl_liquidado5.ToString("N0");
            lblvl_revisado5.Text = t18ac.vl_revisado5.ToString("N0");

            lblvl_planejado6.Text = t18ac.vl_planejado6.ToString("N0");
            lblvl_provisionado6.Text = t18ac.vl_provisionado6.ToString("N0");
            lblvl_empenhado6.Text = t18ac.vl_empenhado6.ToString("N0");
            lblvl_liquidado6.Text = t18ac.vl_liquidado6.ToString("N0");
            lblvl_revisado6.Text = t18ac.vl_revisado6.ToString("N0");

            lblvl_planejado7.Text = t18ac.vl_planejado7.ToString("N0");
            lblvl_provisionado7.Text = t18ac.vl_provisionado7.ToString("N0");
            lblvl_empenhado7.Text = t18ac.vl_empenhado7.ToString("N0");
            lblvl_liquidado7.Text = t18ac.vl_liquidado7.ToString("N0");
            lblvl_revisado7.Text = t18ac.vl_revisado7.ToString("N0");

            lblvl_planejado8.Text = t18ac.vl_planejado8.ToString("N0");
            lblvl_provisionado8.Text = t18ac.vl_provisionado8.ToString("N0");
            lblvl_empenhado8.Text = t18ac.vl_empenhado8.ToString("N0");
            lblvl_liquidado8.Text = t18ac.vl_liquidado8.ToString("N0");
            lblvl_revisado8.Text = t18ac.vl_revisado8.ToString("N0");

            lblvl_planejado9.Text = t18ac.vl_planejado9.ToString("N0");
            lblvl_provisionado9.Text = t18ac.vl_provisionado9.ToString("N0");
            lblvl_empenhado9.Text = t18ac.vl_empenhado9.ToString("N0");
            lblvl_liquidado9.Text = t18ac.vl_liquidado9.ToString("N0");
            lblvl_revisado9.Text = t18ac.vl_revisado9.ToString("N0");

            lblvl_planejado10.Text = t18ac.vl_planejado10.ToString("N0");
            lblvl_provisionado10.Text = t18ac.vl_provisionado10.ToString("N0");
            lblvl_empenhado10.Text = t18ac.vl_empenhado10.ToString("N0");
            lblvl_liquidado10.Text = t18ac.vl_liquidado10.ToString("N0");
            lblvl_revisado10.Text = t18ac.vl_revisado10.ToString("N0");

            lblvl_planejado11.Text = t18ac.vl_planejado11.ToString("N0");
            lblvl_provisionado11.Text = t18ac.vl_provisionado11.ToString("N0");
            lblvl_empenhado11.Text = t18ac.vl_empenhado11.ToString("N0");
            lblvl_liquidado11.Text = t18ac.vl_liquidado11.ToString("N0");
            lblvl_revisado11.Text = t18ac.vl_revisado11.ToString("N0");

            lblvl_planejado12.Text = t18ac.vl_planejado12.ToString("N0");
            lblvl_provisionado12.Text = t18ac.vl_provisionado12.ToString("N0");
            lblvl_empenhado12.Text = t18ac.vl_empenhado12.ToString("N0");
            lblvl_liquidado12.Text = t18ac.vl_liquidado12.ToString("N0");
            lblvl_revisado12.Text = t18ac.vl_revisado12.ToString("N0");

            lbltotvl_planejado.Text = totvl_planejado.ToString("N0");
            lbltotvl_provisionado.Text = totvl_provisionado.ToString("N0");
            lbltotvl_empenhado.Text = totvl_empenhado.ToString("N0");
            lbltotvl_liquidado.Text = totvl_liquidado.ToString("N0");
            lbltotvl_revisado.Text = totvl_revisado.ToString("N0");
        }
        
    }

    protected void linkFiltroExibe_Click(object sender, System.EventArgs e)
    {
        if (this.LinkExibir.Text == "Exibir Meses")
        {
            this.LinkExibir.Text = "Ocultar Meses";
            ucLancFin.ExibirMeses = "";
        }
        else
        {
            this.LinkExibir.Text = "Exibir Meses";
            ucLancFin.ExibirMeses = "none";
        }
        ucLancFin.TableClear();
        Retrieve();
    }
}
