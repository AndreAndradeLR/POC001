using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ProdutoDetalhes : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));
        lblNomeAcao.Text = t08.nm_acao;
        ucPrevisto.inicio = t08.dt_inicio.Year;
        ucPrevisto.fim = t08.dt_fim.Year;
        ucPrevisto.TableClear();

        ucRealizado.inicio = t08.dt_inicio.Year;
        ucRealizado.fim = t08.dt_fim.Year;
        ucRealizado.TableClear();

        if (pb.Session("cd_produto").ToString() != "0")
        {
            Retrieve();

        }

        if (Session["ScreenResolution"] != null)
        {
            ScreenResolution scr = new ScreenResolution();
            scr = (ScreenResolution)Session["ScreenResolution"];
            if (scr.WidthWeb > 0)
            {
                PanelP.Width = Unit.Pixel(scr.WidthWeb);
                PanelR.Width = Unit.Pixel(scr.WidthWeb);
            }
        }

    }

    public void Retrieve()
    {
        t10_produto t10 = new t10_produtoAction().Retrieve(Convert.ToInt32(pb.Session("cd_produto")));
        lblds_produto.Text = t10.ds_produto;
        lblnm_medida.Text = t10.nm_medida;

        foreach (t17_vlproduto t17 in t10.t17)
        {
            TextBox txtvl_p1 = (TextBox)ucPrevisto.FindControl("txtvl_p1" + t17.nu_ano);
            TextBox txtvl_p2 = (TextBox)ucPrevisto.FindControl("txtvl_p2" + t17.nu_ano);
            TextBox txtvl_p3 = (TextBox)ucPrevisto.FindControl("txtvl_p3" + t17.nu_ano);
            TextBox txtvl_p4 = (TextBox)ucPrevisto.FindControl("txtvl_p4" + t17.nu_ano);
            TextBox txtvl_p5 = (TextBox)ucPrevisto.FindControl("txtvl_p5" + t17.nu_ano);
            TextBox txtvl_p6 = (TextBox)ucPrevisto.FindControl("txtvl_p6" + t17.nu_ano);
            TextBox txtvl_p7 = (TextBox)ucPrevisto.FindControl("txtvl_p7" + t17.nu_ano);
            TextBox txtvl_p8 = (TextBox)ucPrevisto.FindControl("txtvl_p8" + t17.nu_ano);
            TextBox txtvl_p9 = (TextBox)ucPrevisto.FindControl("txtvl_p9" + t17.nu_ano);
            TextBox txtvl_p10 = (TextBox)ucPrevisto.FindControl("txtvl_p10" + t17.nu_ano);
            TextBox txtvl_p11 = (TextBox)ucPrevisto.FindControl("txtvl_p11" + t17.nu_ano);
            TextBox txtvl_p12 = (TextBox)ucPrevisto.FindControl("txtvl_p12" + t17.nu_ano);
            TextBox txtvl_ptotal = (TextBox)ucPrevisto.FindControl("txtvl_pTotal" + t17.nu_ano);
            if (txtvl_p1 != null)
            {
                char c = char.Parse(",");
                int casadecimal = 0;
                casadecimal = Int32.Parse(t17.vl_p1.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p1.Text = t17.vl_p1.ToString("N2");
                else
                    txtvl_p1.Text = t17.vl_p1.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p2.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p2.Text = t17.vl_p2.ToString("N2");
                else
                    txtvl_p2.Text = t17.vl_p2.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p3.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p3.Text = t17.vl_p3.ToString("N2");
                else
                    txtvl_p3.Text = t17.vl_p3.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p4.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p4.Text = t17.vl_p4.ToString("N2");
                else
                    txtvl_p4.Text = t17.vl_p4.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p5.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p5.Text = t17.vl_p5.ToString("N2");
                else
                    txtvl_p5.Text = t17.vl_p5.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p6.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p6.Text = t17.vl_p6.ToString("N2");
                else
                    txtvl_p6.Text = t17.vl_p6.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p7.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p7.Text = t17.vl_p7.ToString("N2");
                else
                    txtvl_p7.Text = t17.vl_p7.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p8.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p8.Text = t17.vl_p8.ToString("N2");
                else
                    txtvl_p8.Text = t17.vl_p8.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p9.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p9.Text = t17.vl_p9.ToString("N2");
                else
                    txtvl_p9.Text = t17.vl_p9.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p10.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p10.Text = t17.vl_p10.ToString("N2");
                else
                    txtvl_p10.Text = t17.vl_p10.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p11.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p11.Text = t17.vl_p11.ToString("N2");
                else
                    txtvl_p11.Text = t17.vl_p11.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_p12.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_p12.Text = t17.vl_p12.ToString("N2");
                else
                    txtvl_p12.Text = t17.vl_p12.ToString("N0");

                decimal totvl_p = (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + 
                    t17.vl_p6 + t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + 
                    t17.vl_p10 + t17.vl_p11 + t17.vl_p12);

                if (txtvl_ptotal != null)
                {
                    casadecimal = Int32.Parse(totvl_p.ToString("N2").Split(c)[1].ToString());
                    if (casadecimal > 0)
                        txtvl_ptotal.Text = (totvl_p).ToString("N2");
                    else
                        txtvl_ptotal.Text = (totvl_p).ToString("N0");
                }
            }

            TextBox txtvl_r1 = (TextBox)ucRealizado.FindControl("txtvl_r1" + t17.nu_ano);
            TextBox txtvl_r2 = (TextBox)ucRealizado.FindControl("txtvl_r2" + t17.nu_ano);
            TextBox txtvl_r3 = (TextBox)ucRealizado.FindControl("txtvl_r3" + t17.nu_ano);
            TextBox txtvl_r4 = (TextBox)ucRealizado.FindControl("txtvl_r4" + t17.nu_ano);
            TextBox txtvl_r5 = (TextBox)ucRealizado.FindControl("txtvl_r5" + t17.nu_ano);
            TextBox txtvl_r6 = (TextBox)ucRealizado.FindControl("txtvl_r6" + t17.nu_ano);
            TextBox txtvl_r7 = (TextBox)ucRealizado.FindControl("txtvl_r7" + t17.nu_ano);
            TextBox txtvl_r8 = (TextBox)ucRealizado.FindControl("txtvl_r8" + t17.nu_ano);
            TextBox txtvl_r9 = (TextBox)ucRealizado.FindControl("txtvl_r9" + t17.nu_ano);
            TextBox txtvl_r10 = (TextBox)ucRealizado.FindControl("txtvl_r10" + t17.nu_ano);
            TextBox txtvl_r11 = (TextBox)ucRealizado.FindControl("txtvl_r11" + t17.nu_ano);
            TextBox txtvl_r12 = (TextBox)ucRealizado.FindControl("txtvl_r12" + t17.nu_ano);
            TextBox txtvl_rtotal = (TextBox)ucRealizado.FindControl("txtvl_rTotal" + t17.nu_ano);

            if (txtvl_r1 != null)
            {
                char c = char.Parse(",");
                int casadecimal = 0;
                casadecimal = Int32.Parse(t17.vl_r1.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r1.Text = t17.vl_r1.ToString("N2");
                else
                    txtvl_r1.Text = t17.vl_r1.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r2.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r2.Text = t17.vl_r2.ToString("N2");
                else
                    txtvl_r2.Text = t17.vl_r2.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r3.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r3.Text = t17.vl_r3.ToString("N2");
                else
                    txtvl_r3.Text = t17.vl_r3.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r4.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r4.Text = t17.vl_r4.ToString("N2");
                else
                    txtvl_r4.Text = t17.vl_r4.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r5.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r5.Text = t17.vl_r5.ToString("N2");
                else
                    txtvl_r5.Text = t17.vl_r5.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r6.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r6.Text = t17.vl_r6.ToString("N2");
                else
                    txtvl_r6.Text = t17.vl_r6.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r7.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r7.Text = t17.vl_r7.ToString("N2");
                else
                    txtvl_r7.Text = t17.vl_r7.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r8.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r8.Text = t17.vl_r8.ToString("N2");
                else
                    txtvl_r8.Text = t17.vl_r8.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r9.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r9.Text = t17.vl_r9.ToString("N2");
                else
                    txtvl_r9.Text = t17.vl_r9.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r10.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r10.Text = t17.vl_r10.ToString("N2");
                else
                    txtvl_r10.Text = t17.vl_r10.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r11.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r11.Text = t17.vl_r11.ToString("N2");
                else
                    txtvl_r11.Text = t17.vl_r11.ToString("N0");

                casadecimal = Int32.Parse(t17.vl_r12.ToString("N2").Split(c)[1].ToString());
                if (casadecimal > 0)
                    txtvl_r12.Text = t17.vl_r12.ToString("N2");
                else
                    txtvl_r12.Text = t17.vl_r12.ToString("N0");

                decimal totvl_r = (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + 
                    t17.vl_r6 + t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + 
                    t17.vl_r11 + t17.vl_r12);

                if (txtvl_rtotal != null)
                {
                    casadecimal = Int32.Parse(totvl_r.ToString("N2").Split(c)[1].ToString());
                    if (casadecimal > 0)
                        txtvl_rtotal.Text = (totvl_r).ToString("N2");
                    else
                        txtvl_rtotal.Text = (totvl_r).ToString("N0");
                }
            }
        }
    }
}
