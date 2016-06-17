using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MonProjetos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        gvProjetos.RowDataBound += new GridViewRowEventHandler(gvProjetos_RowDataBound);
        gvProjetos.RowCommand += new GridViewCommandEventHandler(gvProjetos_RowCommand);

        if (Session["ValueProjeto"] != null)
        {
            gvProjetos.DataSource = new t03_projetoAction().ListTodos(FiltroSession(), Session["ValueProjeto"].ToString());
        }
        else
        {
            if (Session["allproject"] != null)
            {                
                gvProjetos.DataSource = new t03_projetoAction().ListTodos(FiltroSession(), Session["allproject"].ToString());
            }
            else
            {
                gvProjetos.DataSource = new t03_projetoAction().ListTodos(FiltroSession());
            }
        }

        gvProjetos.DataBind();
    }

    void gvProjetos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                int cod = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName)
                {
                    case "Arvore":
                        Session["cd_projeto"] = cod;
                        Response.Redirect("Arvore.aspx");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
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
    void gvProjetos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataRowView drv = ((System.Data.DataRowView)e.Row.DataItem);
                t03_projeto t03 = (new t03_projetoAction().Retrieve(Convert.ToInt32(drv["t03_cd_projeto"])));
                TableCell tc = null;

                //Fase
                tc = e.Row.Cells[1];
                if (tc != null)
                {
                    t21_fase t21 = (new t21_faseAction().Retrieve(
                        new t22_faseprojetoAction().Retrieve(t03.t03_cd_projeto).t21_cd_fase));
                    tc.Text = t21.nm_fase;
                }

                //Atualizado
                tc = e.Row.Cells[2];
                if (tc != null)
                {
                    if (t03.dt_atualizado.Year > 1)
                    {
                        tc.Text = t03.dt_atualizado.ToShortDateString();
                    }
                    else
                    {
                        tc.Text = "-";
                    }
                }

                //Período
                tc = e.Row.Cells[3];
                if (tc != null)
                {
                    if (t03.dt_inicio.Year > 1)
                    {
                        tc.Text = t03.dt_inicio.Year + "-" + t03.dt_fim.Year;
                    }
                    else
                    {
                        tc.Text = "-";
                    }
                }

                //Restrição
                tc = e.Row.Cells[4];
                if (tc != null)
                {
                    if (new t07_restricaoAction().ListTodos(t03.t03_cd_projeto).Rows.Count > 0)
                    {
                        tc.Text = "<img src=\"images/Restricao.gif\" title=\"Projeto possui uma ou mais restrições\" />";
                    }

                }

                //Status
                tc = e.Row.Cells[5];
                if (tc != null)
                {
                    tc.Text = new t09_marcoAction().Status(t03.t03_cd_projeto);
                }

                //Projeto
                e.Row.Cells[0].ToolTip = "Ir para o detalhamento do projeto";
            }
        }
        catch (Exception ex)
        {

        }
    }
}
