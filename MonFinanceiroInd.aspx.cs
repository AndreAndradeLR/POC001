using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MonFinanceiroInd : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    string[] month = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
    decimal tplanejado;
    decimal tliquidado;
    decimal tgeral;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblObs.Text = "*Valores até " + month[pb.NomeMes(DateTime.Now.Month - 2)] + " de " + pb.NomeAno(DateTime.Now.Month - 2);
        if (Session["DataTableFinanceiro"] != null)
        {
            GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound);
            GridView1.DataSource = (DataTable)Session["DataTableFinanceiro"];
            GridView1.DataBind();

        }
    }

    void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        System.Data.DataRowView drv = ((System.Data.DataRowView)e.Row.DataItem);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal total;
            decimal planejado;
            decimal liquidado;
            planejado = Convert.ToDecimal(drv["vl_fplanejado"]);           
            liquidado = Convert.ToDecimal(drv["vl_fliquidado"]);            
            tplanejado += planejado;
            tliquidado += liquidado;
            if (planejado > 0)
            {
                total = (liquidado / planejado) * 100;
            }
            else
            {
                total = 0;
            }
            tgeral += total;
            e.Row.Cells[3].Text = total.ToString("N2");
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Média";
            if (gv.Rows.Count > 0)
            {
                e.Row.Cells[1].Text = (tplanejado / gv.Rows.Count).ToString("N0");
                e.Row.Cells[2].Text = (tliquidado / gv.Rows.Count).ToString("N0");
                //e.Row.Cells[3].Text = (tgeral / gv.Rows.Count).ToString("N2");
                if (tplanejado > 0) { e.Row.Cells[3].Text = ((tliquidado / tplanejado) * 100).ToString("N2"); }
                else { e.Row.Cells[3].Text = "0,00"; }
            }
            else
            {
                //e.Row.Cells[1].Text = "0";
            }
        }
    }
}
