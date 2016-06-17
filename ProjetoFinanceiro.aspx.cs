using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ProjetoFinanceiro : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    decimal conta = 0;
    decimal conta2 = 0;
    decimal conta3 = 0;
    decimal conta4 = 0;
    //somar todos valores financeiros
    decimal st_financeiro = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        LiteralGridBind();                
    }

    private void LiteralGridBind()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine("<table width=\"100%\" rules=\"all\" border=\"1\" ");
        sb.AppendLine(" style=\"border: 1px solid #ccc; border-collapse: collapse;\" ");
        sb.AppendLine(" cellpadding=\"5\" cellspacing=\"0\">");
        sb.AppendLine("<tr style=\"background:#eaf6d8;text-align:center;font-weight:bold;\">");
        sb.AppendLine("<td>Ano</td>");
        sb.AppendLine("<td style=\"width:30%\">Fonte</td>");
        sb.AppendLine("<td style=\"width:9%\">Restos a Pagar</td>");
        sb.AppendLine("<td style=\"width:9%\">Dotação Orçamentária</td>");
        sb.AppendLine("<td style=\"width:8%\">Assegurado</td>");
        sb.AppendLine("<td style=\"width:8%\">Planejado</td>");
        sb.AppendLine("<td style=\"width:8%\">Revisado</td>");
        sb.AppendLine("<td style=\"width:8%\">Realizado</td>"); 
        sb.AppendLine("<td style=\"width:8%\">Empenhado</td>");
        sb.AppendLine("<td style=\"width:8%\">Liquidado</td>");        
        sb.AppendLine("<td style=\"width:8%\">Liquidado/<br>Planejado(%)</td>");
        sb.AppendLine("</tr>");

        t18b_vlfinanceiro t18 = new t18b_vlfinanceiro();
        t18b_vlfinanceiro t18ac = new t18b_vlfinanceiro(); //acumulador total ano
        t18b_vlfinanceiro t18t = new t18b_vlfinanceiro(); //total geral
        t18.nu_ano = 0;        
        foreach (DataRow dr in
            new t11_financeiroAction().ListProjetoAnoFonteFin(Convert.ToInt32(pb.Session("cd_projeto"))).Rows)
        {       
            int row_ano = Convert.ToInt32(dr["nu_ano"]);
            t18.vl_dotorcado = Convert.ToDecimal(dr["vl_dotorcado"]);
            t18.vl_restopagar = Convert.ToDecimal(dr["vl_restopagar"]);
            t18.vl_assegurado = Convert.ToDecimal(dr["vl_assegurado"]);                        
            t18.vl_planejado1 = Convert.ToDecimal(dr["vl_planejado"]);
            t18.vl_provisionado1 = Convert.ToDecimal(dr["vl_provisionado"]);
            t18.vl_empenhado1 = Convert.ToDecimal(dr["vl_empenhado"]);
            t18.vl_liquidado1 = Convert.ToDecimal(dr["vl_liquidado"]);
            t18.vl_revisado1 = Convert.ToDecimal(dr["vl_revisado"]);
            //t18.vl_revisado2 += Convert.ToDecimal(dr2["vl_disponivel" + i]);//usando vl_revisado2 para o disponível         

            t18t.vl_dotorcado += t18.vl_dotorcado;
            t18t.vl_restopagar += t18.vl_restopagar;
            t18t.vl_assegurado += t18.vl_assegurado;           
            t18t.vl_planejado1 += t18.vl_planejado1;
            t18t.vl_provisionado1 += t18.vl_provisionado1;
            t18t.vl_empenhado1 += t18.vl_empenhado1;
            t18t.vl_liquidado1 += t18.vl_liquidado1;
            t18t.vl_revisado1 += t18.vl_revisado1;
            //t18t.vl_revisado2 += t18.vl_revisado2;//usando vl_revisado2 para o disponível          

            if ((t18.nu_ano != 0) && (row_ano != t18.nu_ano))
            {
                sb.AppendLine("<tr style=\"background:#ecece9;text-align:right\">");
                sb.AppendLine("<td>" + t18.nu_ano + "</td>");
                sb.AppendLine("<td>Total</td>");
                sb.AppendLine("<td>" + t18ac.vl_restopagar.ToString("N0") + "</td>");
                sb.AppendLine("<td>" + t18ac.vl_dotorcado.ToString("N0") + "</td>");
                sb.AppendLine("<td>" + t18ac.vl_assegurado.ToString("N0") + "</td>");
                sb.AppendLine("<td>" + t18ac.vl_planejado1.ToString("N0") + "</td>");
                sb.AppendLine("<td>" + t18ac.vl_revisado1.ToString("N0") + "</td>");
                sb.AppendLine("<td>" + t18ac.vl_provisionado1.ToString("N0") + "</td>");
                sb.AppendLine("<td>" + t18ac.vl_empenhado1.ToString("N0") + "</td>");
                sb.AppendLine("<td>" + t18ac.vl_liquidado1.ToString("N0") + "</td>");
                

                st_financeiro += t18ac.vl_planejado1;
                //aritimética liquidado / previsto
                if (t18ac.vl_liquidado1 > 0)
                {
                    conta2 = (t18ac.vl_liquidado1 / t18ac.vl_planejado1) * 100;
                    sb.AppendLine("<td>" + conta2.ToString("N2") + "</td>");//somatorio                
                }
                else
                {
                    sb.AppendLine("<td>0,00</td>");//somatorio                    
                }  
                //sb.AppendLine("<td>" + t18ac.vl_revisado2.ToString("N2") + "</td>");//usando vl_revisado2 para o disponível                
                sb.AppendLine("</tr>");
                t18ac = new t18b_vlfinanceiro();
            }
            t18.nu_ano = row_ano;

            sb.AppendLine("<tr style=\"text-align:right\">");
            sb.AppendLine("<td>" + t18.nu_ano + "</td>");
            sb.AppendLine("<td style=\"text-align:left\">" + dr["nm_fonte"] + "</td>");
            sb.AppendLine("<td>" + t18.vl_restopagar.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18.vl_dotorcado.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18.vl_assegurado.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18.vl_planejado1.ToString("N0") + "</td>");            
            sb.AppendLine("<td>" + t18.vl_revisado1.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18.vl_provisionado1.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18.vl_empenhado1.ToString("N0") + "</td>");
            sb.AppendLine("<td>" + t18.vl_liquidado1.ToString("N0") + "</td>");
            

            t18ac.vl_dotorcado += t18.vl_dotorcado;
            t18ac.vl_restopagar += t18.vl_restopagar;
            t18ac.vl_assegurado += t18.vl_assegurado;
            t18ac.vl_planejado1 += t18.vl_planejado1;            
            t18ac.vl_provisionado1 += t18.vl_provisionado1;
            t18ac.vl_empenhado1 += t18.vl_empenhado1;
            t18ac.vl_liquidado1 += t18.vl_liquidado1;
            t18ac.vl_revisado1 += t18.vl_revisado1;
            //t18ac.vl_revisado2 += t18.vl_revisado2;//usando vl_revisado2 para o disponível
            //aritimética liquidado / previsto
            if (t18.vl_planejado1 > 0)
            {
                conta = (t18.vl_liquidado1 / t18.vl_planejado1) * 100;
                sb.AppendLine("<td>" + conta.ToString("N2") + "</td>");//somatorio                
            }
            else
            {
                sb.AppendLine("<td>0,00</td>");//somatorio                    
            }            
            //sb.AppendLine("<td>" + t18.vl_revisado2.ToString("N2") + "</td>");//usando vl_revisado2 para o disponível
            sb.AppendLine("</tr>");
            
        }        

        sb.AppendLine("<tr style=\"background:#ecece9;text-align:right\">");
        sb.AppendLine("<td>" + t18.nu_ano + "</td>");
        sb.AppendLine("<td>Total</td>");
        sb.AppendLine("<td>" + t18ac.vl_restopagar.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18ac.vl_dotorcado.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18ac.vl_assegurado.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18ac.vl_planejado1.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18ac.vl_revisado1.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18ac.vl_provisionado1.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18ac.vl_empenhado1.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18ac.vl_liquidado1.ToString("N0") + "</td>");
        

        st_financeiro += t18ac.vl_planejado1;
        //aritimética liquidado / previsto
        if (t18ac.vl_planejado1 > 0)
        {
            conta3 = (t18ac.vl_liquidado1 / t18ac.vl_planejado1) * 100;
            sb.AppendLine("<td>" + conta3.ToString("N2") + "</td>");//somatorio                
        }
        else
        {
            sb.AppendLine("<td>0,00</td>");//somatorio                    
        }  
        //sb.AppendLine("<td>" + t18ac.vl_revisado2.ToString("N2") + "</td>");//usando vl_revisado2 para o disponível

        sb.AppendLine("</tr>");

        sb.AppendLine("<tr style=\"background:#eaf6d8;font-weight:bold;text-align:right\">");
        sb.AppendLine("<td colspan=\"2\">Total</td>");
        sb.AppendLine("<td>" + t18t.vl_restopagar.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18t.vl_dotorcado.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18t.vl_assegurado.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18t.vl_planejado1.ToString("N0") + "</td>");        
        sb.AppendLine("<td>" + t18t.vl_revisado1.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18t.vl_provisionado1.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18t.vl_empenhado1.ToString("N0") + "</td>");
        sb.AppendLine("<td>" + t18t.vl_liquidado1.ToString("N0") + "</td>");
        

        //aritimética liquidado / previsto
        if (t18t.vl_planejado1 > 0)
        {
            conta4 = (t18t.vl_liquidado1 / t18t.vl_planejado1) * 100;
            sb.AppendLine("<td>" + conta4.ToString("N2") + "</td>");//somatorio                
        }
        else
        {
            sb.AppendLine("<td>0,00</td>");//somatorio                    
        }  
        //sb.AppendLine("<td>" + t18t.vl_revisado2.ToString("N2") + "</td>");//usando vl_revisado2 para o disponível
        sb.AppendLine("</tr>");

        sb.AppendLine("</table>");

        //LiteralGrid.Text = sb.ToString().Replace(",00", "");
        LiteralGrid.Text = sb.ToString();
    }
}
