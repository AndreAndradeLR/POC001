using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CustomControls_ucSituacoes : System.Web.UI.UserControl
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["id"] != null && Request["id"] != "")
            {
                ViewState["ddlValue"] = Convert.ToInt32(Request["id"]);
                ddlt03_cd_projeto.Items.Add(new ListItem(Session["nm_projeto"].ToString(), Request["id"].ToString()));
                ddlt03_cd_projeto.DataBind();
                GridBind(Convert.ToInt32(Request["id"]));
            }
            else
            {
                FormBind();
            }
        }
    }

    private void GridBind(int cd_projeto)
    {
        int mes = 0, ano = 0;
        int.TryParse(ddlMes.SelectedValue, out mes);
        int.TryParse(ddlAno.SelectedValue, out ano);
        GridView1.DataSource = new t05_situacaoAction().ListProjeto(cd_projeto, mes, ano);
        GridView1.DataBind();
    }

    private void FormBind()
    {
        if ((Convert.ToBoolean(pb.Session("fl_monitora"))) || (Convert.ToBoolean(pb.Session("fl_admin"))))
        {
            ddlt03_cd_projeto.DataSource = new t03_projetoAction().ListTodos();
            ddlt03_cd_projeto.DataTextField = "nm_projeto";
            ddlt03_cd_projeto.DataValueField = "t03_cd_projeto";
            ddlt03_cd_projeto.DataBind();
            pb.AddEmptyItem(ddlt03_cd_projeto, "Selecione");

            ddlAno.DataSource = new t05_situacaoAction().ListAnoSituacaoProjeto();
            ddlAno.DataTextField = "ANO";
            ddlAno.DataValueField = "ANO";
            ddlAno.DataBind();
            pb.AddEmptyItem(ddlAno, "Todos");
        }
        else
        {
            RestricaoRelatorioSituacoes();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int result = 0;
                //apenas o gerente do projeto pode excluir
                result = new t02_usuarioAction().ListProjetoGerente(Convert.ToInt32(ViewState["ddlValue"]), Session["cd_usuario"].ToString()).Rows.Count;
                if (result > 0) { e.Row.Cells[0].Controls[0].Visible = true; }
                else { e.Row.Cells[0].Controls[0].Visible = false; }

                //Adicionar mensagem de alerta antes da exclusão                
                ImageButton btn = (ImageButton)e.Row.Cells[0].Controls[0];
                if (btn != null)
                {
                    btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja excluir?') == false) return false;");
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowDataBound: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                ViewState["cod"] = gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString();

                if (e.CommandName == "Deletar")
                {
                    new t05_situacaoAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                    GridBind(Convert.ToInt32(ViewState["ddlValue"]));
                    GridView1.DataBind();
                    lblMsg.Text = pb.Message("Situação deletada com sucesso!", "ok");
                    lblMsg.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowCommand: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }

    }

    public void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            int m = e.Row.Cells.Count;
            for (int i = m - 1; i >= 1; i += -1)
            {
                e.Row.Cells.RemoveAt(i);
            }

            e.Row.Cells[0].ColumnSpan = m;
            e.Row.Attributes.Add("id", "pager" + gv.ClientID);
            JQuery jq = new JQuery();
            e.Row.Cells[0].Text = jq.PagerHtml();
        }
    }

    public void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            JQuery jquery = new JQuery();
            jquery.SortList = "1, 0";
            //jquery.Headers = "0: {sorter: false}";

            jquery.tableID = gv.ClientID;
            jquery.TableSorter();

            gv.UseAccessibleHeader = true;
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void ddlt03_cd_projeto_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList ddl = (DropDownList)sender;
        DropDownList ddl = ddlt03_cd_projeto;
        ViewState["ddlValue"] = 0;
        if (ddl.SelectedValue != "")
        {
            ViewState["ddlValue"] = Convert.ToInt32(ddl.SelectedValue);
            GridBind(Convert.ToInt32(ddl.SelectedValue));
        }
        //else
        //{
        //    GridBind(0);
        //}
    }

    public void RestricaoRelatorioSituacoes()
    {
        ddlt03_cd_projeto.ClearSelection();
        t02_usuarioAction t02a = new t02_usuarioAction();
        DataTable dt = t02a.ListRelatorioSituacoes(Session["cd_usuario"].ToString());
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["t03_cd_projeto"] != DBNull.Value)
                ddlt03_cd_projeto.Items.Add(new ListItem(dr["nm_projeto"].ToString(), dr["t03_cd_projeto"].ToString()));
        }
        ddlt03_cd_projeto.DataBind();
        pb.AddEmptyItem(ddlt03_cd_projeto, "Selecione");

    }

}
