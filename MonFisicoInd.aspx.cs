using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MonFisicoInd : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    string[] month = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };
    decimal tprev;
    decimal treal;
    decimal tgeral;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblObs.Text = "*Valores até " + month[pb.NomeMes(DateTime.Now.Month - 2)] + " de " + pb.NomeAno(DateTime.Now.Month - 2);
        if (Session["DataTableProduto"] != null)
        {
            GridView1.RowDataBound += new GridViewRowEventHandler(GridView1_RowDataBound);
            GridView1.DataSource = (DataTable)Session["DataTableProduto"];
            GridView1.DataBind();

        }
    }

    void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        System.Data.DataRowView drv = ((System.Data.DataRowView)e.Row.DataItem);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal total, prev, real;
            prev = Convert.ToDecimal(drv["vl_prev"]);
            real = Convert.ToDecimal(drv["vl_real"]);
            tprev +=prev;
            treal += real;
            if (prev > 0)
            {
                total = (real / prev)*100;
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
                e.Row.Cells[1].Text = (tprev / gv.Rows.Count).ToString("N2");
                e.Row.Cells[2].Text = (treal / gv.Rows.Count).ToString("N2");
                //e.Row.Cells[3].Text = (tgeral / gv.Rows.Count).ToString("N2");
                if (tprev > 0) { e.Row.Cells[3].Text = ((treal / tprev) * 100).ToString("N2"); }
                else { e.Row.Cells[3].Text = "0,00"; }
            }
            else
            {
                //e.Row.Cells[1].Text = "0";
            }
        }
    }
}
