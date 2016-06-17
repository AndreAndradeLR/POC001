using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class MonMarcos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rptMarcos.ItemDataBound += new RepeaterItemEventHandler(rptMarcos_ItemDataBound);
        if (Session["allproject"] != null)
        {
            if ((Session["ValueProjeto"] != null) && (Session["ValueProjeto"] != ""))
            {
                rptMarcos.DataSource = new t03_projetoAction().ListTodos(FiltroSession(), Session["ValueProjeto"].ToString());
            }
            else
            {
                rptMarcos.DataSource = new t03_projetoAction().ListTodos(FiltroSession(), Session["allproject"].ToString());
            }
        }
        else
        {
            if ((Session["ValueProjeto"] != null) && (Session["ValueProjeto"] != ""))
            {
                rptMarcos.DataSource = new t03_projetoAction().ListTodos(FiltroSession(), Session["ValueProjeto"].ToString());
            }
            else
            {
                rptMarcos.DataSource = new t03_projetoAction().ListTodos(FiltroSession());
            }
        }
        rptMarcos.DataBind();
    }

    void rptMarcos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataRowView drv = ((DataRowView)e.Item.DataItem);
        Label lbl = (Label)e.Item.FindControl("lblnm_projeto");
        GridView gv = (GridView)e.Item.FindControl("gvMarcos");
        if (lbl != null)
        {
            lbl.Text = "<h5><span style='font-weight:normal'>Projeto: </span>" + drv["nm_projeto"].ToString() + "</h5>";
        }
            int cd_projeto = Convert.ToInt32(drv["t03_cd_projeto"]);
            DataTable List = new DataTable();
            if (Request["fl_status"] != null)
            {
                switch (Request["fl_status"].ToString())
                {
                    case "R":
                        List = new t09_marcoAction().ListStatusVermelho(cd_projeto);
                        lblHeader.Text = "Marcos Críticos - Com atraso ";
                        break;
                    case "G":
                        List = new t09_marcoAction().ListStatusVerde(cd_projeto);
                        lblHeader.Text = "Marcos Críticos -  Dentro dos prazos previstos ";
                        break;
                    case "B":
                        List = new t09_marcoAction().ListStatusAzul(cd_projeto);
                        lblHeader.Text = "Marcos Críticos - Concluídos ";
                        gv.Columns[3].Visible = true;
                        break;
                    case "Y":
                        List = new t09_marcoAction().ListStatusAmarelo(cd_projeto);
                        lblHeader.Text = "Marcos Críticos - Com restrições ";
                        break;
                }
                
                if (gv != null)
                {
                    if (List.Rows.Count > 0)
                    {
                        gv.DataSource = List;
                        gv.DataBind();
                    }
                    else
                    {
                        gv.Visible = false;
                        lbl.Visible = false;
                    }

                }
            }
            else
            {
                Response.Redirect("MonPainel.aspx");
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
