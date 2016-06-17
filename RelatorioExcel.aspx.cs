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
using System.IO;

public partial class RelatorioExcel : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    decimal planejado = 0;
    decimal revisado = 0;
    decimal provisionado = 0;
    decimal empenhado = 0;
    decimal liquidado = 0;
    decimal restopagar = 0;
    decimal assegurado = 0;
    decimal dotaorcado = 0;
    string mes_selecao = "";
    int ano = 0;
    string[] month = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
    string[] year = { "2008", "2009", "2010", "2011", "2012", "2013", "2014" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulaDDls();
        }
    }

    private void GridBind(int ano)
    {
        t18b_vlfinanceiroAction t18a = new t18b_vlfinanceiroAction();
        if (ddlMes.SelectedValue != "")
        {
            GridView1.Visible = true;
            GridView2.Visible = false;

            GridView1.DataSource = t18a.RelatorioExcel(ano);
            GridView1.DataBind();
        }
        else
        {
            GridView1.Visible = false;
            GridView2.Visible = true;

            GridView2.DataSource = t18a.RelatorioExcel(ano);
            GridView2.DataBind();
        }

    }

    private void PopulaDDls()
    {
        //popula mes
        ddlMes.DataSource = month;
        ddlMes.DataBind();
        pb.AddEmptyItem(ddlMes, "Todos");


        //popula ano
        ddlAno.DataSource = year;
        ddlAno.DataBind();
        pb.AddEmptyItem(ddlAno, "Todos");
    }

    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    GridViewExportUtil.Export("Relatorio_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xls", this.GridView1);
    //}
    //<asp:Button ID="btnExcel" runat="server" Text="Exportar Excel"  onclick="btnExcel_Click/>     
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataRowView drv = ((DataRowView)e.Row.DataItem);
                planejado = 0;
                revisado = 0;
                provisionado = 0;
                empenhado = 0;
                liquidado = 0;
                restopagar = 0;
                dotaorcado = 0;
                assegurado = 0;

                StringBuilder sb = new StringBuilder();
                DataRowView drv = ((DataRowView)e.Row.DataItem);

                for (int i = RetornaNumMes(mes_selecao); i <= RetornaNumMes(mes_selecao); i++)
                {
                    if (drv["vl_planejado" + i + ""] != DBNull.Value)
                    {
                        planejado += Convert.ToDecimal(drv["vl_planejado" + i + ""]);
                    }
                    if (drv["vl_revisado" + i + ""] != DBNull.Value)
                    {
                        revisado += Convert.ToDecimal(drv["vl_revisado" + i + ""]);
                    }
                    if (drv["vl_provisionado" + i + ""] != DBNull.Value)
                    {
                        provisionado += Convert.ToDecimal(drv["vl_provisionado" + i + ""]);
                    }
                    if (drv["vl_empenhado" + i + ""] != DBNull.Value)
                    {
                        empenhado += Convert.ToDecimal(drv["vl_empenhado" + i + ""]);
                    }
                    if (drv["vl_liquidado" + i + ""] != DBNull.Value)
                    {
                        liquidado += Convert.ToDecimal(drv["vl_liquidado" + i + ""]);
                    }
                }

                e.Row.Cells[4].Text = ddlMes.SelectedValue.Trim();


                if (drv["vl_assegurado"] != DBNull.Value)
                {
                    assegurado = Convert.ToDecimal(drv["vl_assegurado"]);
                }

                if (drv["vl_restopagar"] != DBNull.Value)
                {
                    restopagar = Convert.ToDecimal(drv["vl_restopagar"]);
                }

                if (drv["vl_dotorcado"] != DBNull.Value)
                {
                    dotaorcado = Convert.ToDecimal(drv["vl_dotorcado"]);
                }

                e.Row.Cells[6].Text = dotaorcado.ToString("N0");
                e.Row.Cells[7].Text = assegurado.ToString("N0");
                e.Row.Cells[8].Text = restopagar.ToString("N0").Replace("0,00", "0");
                e.Row.Cells[9].Text = planejado.ToString("N0");
                e.Row.Cells[10].Text = revisado.ToString("N0");
                e.Row.Cells[11].Text = provisionado.ToString("N0");
                e.Row.Cells[12].Text = empenhado.ToString("N0");
                e.Row.Cells[13].Text = liquidado.ToString("N0");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowDataBound: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }


    /** GridView2 **/
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataRowView drv = ((DataRowView)e.Row.DataItem);
                planejado = 0;
                revisado = 0;
                provisionado = 0;
                empenhado = 0;
                liquidado = 0;
                restopagar = 0;
                dotaorcado = 0;
                assegurado = 0;

                StringBuilder sb = new StringBuilder();
                DataRowView drv = ((DataRowView)e.Row.DataItem);

                for (int i = 1; i < 13; i++)
                {

                    if (drv["vl_planejado" + i + ""] != DBNull.Value)
                    {
                        planejado += Convert.ToDecimal(drv["vl_planejado" + i + ""]);
                    }
                    if (drv["vl_revisado" + i + ""] != DBNull.Value)
                    {
                        revisado += Convert.ToDecimal(drv["vl_revisado" + i + ""]);
                    }
                    if (drv["vl_provisionado" + i + ""] != DBNull.Value)
                    {
                        provisionado += Convert.ToDecimal(drv["vl_provisionado" + i + ""]);
                    }
                    if (drv["vl_empenhado" + i + ""] != DBNull.Value)
                    {
                        empenhado += Convert.ToDecimal(drv["vl_empenhado" + i + ""]);
                    }
                    if (drv["vl_liquidado" + i + ""] != DBNull.Value)
                    {
                        liquidado += Convert.ToDecimal(drv["vl_liquidado" + i + ""]);
                    }
                }
                if (drv["vl_assegurado"] != DBNull.Value)
                {
                    assegurado = Convert.ToDecimal(drv["vl_assegurado"]);
                }

                if (drv["vl_restopagar"] != DBNull.Value)
                {
                    restopagar = Convert.ToDecimal(drv["vl_restopagar"]);
                }

                if (drv["vl_dotorcado"] != DBNull.Value)
                {
                    dotaorcado = Convert.ToDecimal(drv["vl_dotorcado"]);
                }


                e.Row.Cells[5].Text = dotaorcado.ToString("N0");
                e.Row.Cells[6].Text = assegurado.ToString("N0");
                e.Row.Cells[7].Text = restopagar.ToString("N0").Replace("0,00", "0");
                e.Row.Cells[8].Text = planejado.ToString("N0");
                e.Row.Cells[9].Text = revisado.ToString("N0");
                e.Row.Cells[10].Text = provisionado.ToString("N0");
                e.Row.Cells[11].Text = empenhado.ToString("N0");
                e.Row.Cells[12].Text = liquidado.ToString("N0");

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowDataBound: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    protected int RetornaNumMes(string mes)
    {
        int num_mes = 0;
        switch (mes)
        {
            case "Janeiro":
                num_mes = 1;
                break;
            case "Fevereiro":
                num_mes = 2;
                break;
            case "Março":
                num_mes = 3;
                break;
            case "Abril":
                num_mes = 4;
                break;
            case "Maio":
                num_mes = 5;
                break;
            case "Junho":
                num_mes = 6;
                break;
            case "Julho":
                num_mes = 7;
                break;
            case "Agosto":
                num_mes = 8;
                break;
            case "Setembro":
                num_mes = 9;
                break;
            case "Outubro":
                num_mes = 10;
                break;
            case "Novembro":
                num_mes = 11;
                break;
            case "Dezembro":
                num_mes = 12;
                break;
        }

        return num_mes;
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {       
        mes_selecao = ddlMes.SelectedValue;
        if (ddlAno.SelectedValue != "")
        {
            GridBind(Convert.ToInt32(ddlAno.SelectedValue));
        }
        else {
            GridBind(0);
        }
        if (GridView1.Rows.Count > 0 || GridView2.Rows.Count > 0)
        {
            btnExcel.Visible = true;
        }
        else
        {
            btnExcel.Visible = false;
        }
        
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        if (ddlMes.SelectedValue != "")
        {
            GridViewExportUtil.Export("Relatorio_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls", GridView1);
        }
        else
        {
            GridViewExportUtil.Export("Relatorio_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".xls", GridView2);
        }
    }

}
