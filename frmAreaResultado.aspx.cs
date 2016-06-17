using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class frmAreaResultado : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            lblTitle.Text = "Área de Resultado";
            ViewState["cod"] = 0;
            GridBind();
        }
    }

    private void exibirForm()
    {
        PanelForm.Visible = true;
        PanelGrid.Visible = false;
    }
    
    private void ocultarForm()
    {
        PanelForm.Visible = false;
        PanelGrid.Visible = true;
        txtnm_area.Text = "";
        ViewState["cod"] = 0;
    }

    private void GridBind()
    {
        t26_arearesultadoAction t26a = new t26_arearesultadoAction();
        GridView1.DataSource = t26a.ListTodos();
        GridView1.DataBind();
    }

    private void Retrieve()
    {
        t26_arearesultadoAction t26a = new t26_arearesultadoAction();
        t26_arearesultado t26 = new t26_arearesultado();
        t26 = t26a.Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtnm_area.Text = t26.nm_area;
        exibirForm();

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                ViewState["cod"] = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName)
                {
                    case "Editar":
                        lblHeader.Text = "Alteração";
                        trArquivo.Visible = false;
                        trOpcao.Visible = true;                        
                        Retrieve();
                        break;
                    case "Deletar":
                        t26_arearesultadoAction t26a = new t26_arearesultadoAction();
                        t26a.DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = 0;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowCommand: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DataRowView drv = ((DataRowView)e.Row.DataItem);

                //Adicionar mensagem de alerta antes da exclusão
                ImageButton btn = (ImageButton)e.Row.Cells[1].Controls[0];
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

    protected void btnSalvar_Click(object sender, EventArgs e)
    {
        bool continua = false;
        int result = 0;
        string msg = "";
        try
        {
            uploadArquivo up = new uploadArquivo();
            if (funm_arquivo.Visible)
            {
                up.pasta = "Documentos";
                up.nomeinicial = "area";
                up.fu = funm_arquivo;
                continua = up.Save();
                msg = pb.Message(up.msg, "erro");
            }
            else
            {
                continua = true;
            }

            if (continua)
            {
                t26_arearesultado t26 = new t26_arearesultado();
                t26.t26_cd_arearesultado = Convert.ToInt32(ViewState["cod"]);
                t26.nm_area = txtnm_area.Text;
                t26.dt_cadastro = DateTime.Now;
                t26.dt_alterado = DateTime.Now;
                t26.nm_arquivo = up.nomearquivo;
                t26_arearesultadoAction t26a = new t26_arearesultadoAction();
                if (Convert.ToInt32(ViewState["cod"]) > 0)
                {
                    if ((t26.nm_arquivo != "")&&(t26.nm_arquivo!=null))
                    {
                        result = t26a.UpdateArquivo(t26);
                    }
                    else
                    {
                        result = t26a.UpdateDB(t26);
                    }
                }
                else
                {
                    result = t26a.InsertDB(t26);
                }

                if (result > 0)
                {
                    msg = pb.Message("Salvo com sucesso", "ok");
                    GridBind();
                    txtnm_area.Text = "";
                    ocultarForm();
                    ViewState["cod"] = 0;
                }
            }
        }
        catch (Exception ex)
        {
            msg = pb.Message(ex.Message, "erro");
        }

        lblMsg.Text = msg;
        lblMsg.Visible = true;

    }

    protected void rblArquivo_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rblArquivo.SelectedValue == "N")
        {
            trArquivo.Visible = false;
        }
        else
        {
            trArquivo.Visible = true;
        }
        exibirForm();
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ocultarForm();
    }
    protected void btnNovo_Click(object sender, EventArgs e)
    {
        exibirForm();
        lblHeader.Text = "Cadastrar Novo";
    }

    #region JQuery
    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            JQuery jquery = new JQuery();
            jquery.SortList = "2, 0";
            jquery.tableID = gv.ClientID;
            jquery.TableSorter();

            Literal lit = (Literal)gv.Parent.FindControl("Literal" + gv.ID);
            if (lit != null)
                lit.Text = jquery.SearchHtml();

            gv.UseAccessibleHeader = true;
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Attributes.Add("class", "{sorter: false}");
            e.Row.Cells[1].Attributes.Add("class", "{sorter: false}");
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
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
    #endregion
  
}
