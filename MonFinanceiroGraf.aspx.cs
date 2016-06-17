using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MonFinanceiroGraf : System.Web.UI.Page
{
    string[] mes = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
    decimal tprev;
    decimal treal;
    decimal tplanejado;
    decimal tliquidado;
    decimal tgeral;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //exibo o mes -1 (a conta é -2 pois array inicia de 0)
            //lblObs.Text = "*Valores até " + mes[(DateTime.Now.Month - 2)] + " de " + DateTime.Now.Year;                        

            if (Session["DataTableFinanceiro"] != null)
            {
                GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound);
                GridView1.DataSource = (DataTable)Session["DataTableFinanceiro"];
                GridView1.DataBind();

                Literal1.Text = Session["GraficoFinanceiro"].ToString();
            }
        }
    }   

    void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        decimal total, prev, real, planejado, liquidado;
        GridView gv = (GridView)sender;
        System.Data.DataRowView drv = ((System.Data.DataRowView)e.Row.DataItem);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {            
            prev = Convert.ToDecimal(drv["vl_prev"]);
            real = Convert.ToDecimal(drv["vl_real"]);
            planejado = Convert.ToDecimal(drv["vl_planejado"]);
            liquidado = Convert.ToDecimal(drv["vl_liquidado"]);
            tprev += prev;
            treal += real;
            tliquidado += liquidado;
            tplanejado += planejado;
            if (prev > 0)
            {
                total = (liquidado / planejado) * 100;

            }
            else
            {
                total = 0;
            }            
            e.Row.Cells[5].Text = total.ToString("N2");
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Total/Média";
            if (gv.Rows.Count > 0)
            {
                e.Row.Cells[1].Text = tplanejado.ToString("N0");
                e.Row.Cells[2].Text = tliquidado.ToString("N0");
                e.Row.Cells[3].Text = (tprev / gv.Rows.Count).ToString("N0");
                e.Row.Cells[4].Text = (treal / gv.Rows.Count).ToString("N0");
                if (tplanejado > 0) {
                    tgeral = (tliquidado / tplanejado)*100;
                }
                e.Row.Cells[5].Text = (tgeral).ToString("N2");
                //e.Row.Cells[5].Text = (tgeral / gv.Rows.Count).ToString("N2");
            }
            else
            {
                //e.Row.Cells[1].Text = "0";
            }
        }
    }
}
