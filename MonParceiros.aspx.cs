using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MonParceiros : System.Web.UI.Page
{
    double total = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        gvParceiros.RowDataBound += new GridViewRowEventHandler(gvParceiros_RowDataBound);
        gvParceiros.DataSource = new t03_projetoAction().ListMon(FiltroSession());
        gvParceiros.DataBind();
    }

    void gvParceiros_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        System.Data.DataRowView drv = ((System.Data.DataRowView)e.Row.DataItem);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double p = new t04_parceiroAction().ListTodos(
                Convert.ToInt32(drv["t03_cd_projeto"])).Rows.Count;
            e.Row.Cells[1].Text = p.ToString();
            total += p;
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[0].Text = "Quantidade média de parceiros ";
            if (gv.Rows.Count > 0)
            {
                e.Row.Cells[1].Text = (total / gv.Rows.Count).ToString("N2");
            }
            else
            {
                e.Row.Cells[1].Text = "0";
            }
        }
    }
    public FiltroProjeto FiltroSession()
    {
        if (Session["filtroMon"] != null)
        {
            return (FiltroProjeto)Session["filtroMon"];
        }
        else
        {
            return new FiltroProjeto();
        }
    }
}
