using System;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class frmProjeto : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            lblTitle.Text = "Projetos";
            ViewState["cod"] = 0;
            GridBind();
        }
    }
    private void exibirForm()
    {
        PanelForm.Visible = true;
        PanelGrid.Visible = false;
        FormBind();
        limparForm();
    }

    private void ocultarForm()
    {
        PanelForm.Visible = false;
        PanelGrid.Visible = true;
        ViewState["cod"] = 0;
        limparForm();
    }

    private void limparForm()
    {
        txtnm_projeto.Text = "";
        ddlt02_cd_usuario.ClearSelection();
        ddlt02_cd_usuario_monitoramento.ClearSelection();
        ddlt01_cd_entidade.ClearSelection();
        ddlt26_cd_arearesultado.ClearSelection();
    } 

    private void GridBind()
    {
        t03_projetoAction t03a = new t03_projetoAction();
        GridView1.DataSource = t03a.ListTodos();
        GridView1.DataBind();
    }

    public string RemoveCaracter(string word)
    {

        string strcompare = word.ToLower();
        string str = "0123456789abcdefghijklmnopqrstwuvxyz";
        bool passou = false;
        for (int i = 0; i < str.Length; i++)
        {
            if (strcompare.Substring(0, 1) == str.Substring(i, 1))
            {
                passou = true;
                break;

            }

        }
        if (!passou)
        {
            word = word.Remove(0, 1);
        }
        return word;

    }

    private void Retrieve()
    {
        t03_projetoAction t03a = new t03_projetoAction();
        t03_projeto t03 = new t03_projeto();
        t03 = t03a.Retrieve(Convert.ToInt32(ViewState["cod"]));

        ListItem li;
        txtnm_projeto.Text = t03.nm_projeto;
        li = ddlt02_cd_usuario.Items.FindByValue(t03.t02_cd_usuario);
        if (li != null) li.Selected = true;

        li = ddlt02_cd_usuario_monitoramento.Items.FindByValue(t03.t02_cd_usuario_monitoramento);
        if (li != null) li.Selected = true;

        li = ddlt01_cd_entidade.Items.FindByValue(t03.t01_cd_entidade.ToString());
        if (li != null) li.Selected = true;

        li = ddlt26_cd_arearesultado.Items.FindByValue(t03.t26_cd_arearesultado.ToString());
        if (li != null) li.Selected = true;
    }

    protected void btnAcao_Click(object sender, System.EventArgs e)
    {
        t03_projetoAction t03a = new t03_projetoAction();
        t03_projeto t03 = new t03_projeto();
        int result = 0;
        string msg = "";

        t03.t03_cd_projeto = Convert.ToInt32(ViewState["cod"]);
        t03.nm_projeto = txtnm_projeto.Text;
        t03.t01_cd_entidade = Convert.ToInt32(ddlt01_cd_entidade.SelectedValue);
        t03.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
        t03.t02_cd_usuario_monitoramento = ddlt02_cd_usuario_monitoramento.SelectedValue;
        t03.t26_cd_arearesultado = Convert.ToInt32(ddlt26_cd_arearesultado.SelectedValue);

        if (t03.t03_cd_projeto > 0)
        {
            result = t03a.UpdateDB(t03);

        }
        else
        {
            result = t03a.InsertDB(t03);
            
            t22_faseprojeto t22 = new t22_faseprojeto();
            t22.t03_cd_projeto = t03a.RetrieveIDENTITY(t03);
            t22.t21_cd_fase = 1; //Em Estruturação
            t22_faseprojetoAction t22a = new t22_faseprojetoAction();
            t22a.InsertDB(t22);
        }


        if (result > 0)
        {
            ocultarForm();
            GridBind();
            msg = pb.Message("Alteração realizada com sucesso!", "ok");
        }

        lblMsg.Text = msg;
        lblMsg.Visible = true;


    }
    
    protected void link_Click(object sender, System.EventArgs e)
    {
        LinkButton link = (LinkButton)sender;
        Session["cd_projeto"] = link.CommandArgument;
        Response.Redirect("Arvore.aspx");
    }  
   
    protected void btnCadastro_Click(object sender, System.EventArgs e) 
    { 
        this.lblHeader.Text = "Cadastrar Novo"; 
        exibirForm(); 
    } 

    protected void btnCancel_Click(object sender, System.EventArgs e) 
    { 
        ocultarForm(); 
    } 

    protected void FormBind()
    {
        DropDownList ddl = ddlt01_cd_entidade;
        t01_entidadeAction t01a = new t01_entidadeAction();
        {
            ddl.DataSource = t01a.ListTodos();
            ddl.DataTextField = "nm_entidade";
            ddl.DataValueField = "t01_cd_entidade";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }

        t02_usuarioAction t02a = new t02_usuarioAction();
        {
            ddl = ddlt02_cd_usuario;
            ddl.DataSource = t02a.ListTodos();
            ddl.DataTextField = "nm_nome";
            ddl.DataValueField = "t02_cd_usuario";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");

            ddl = ddlt02_cd_usuario_monitoramento;
            ddl.DataSource = t02a.ListTodos();
            ddl.DataTextField = "nm_nome";
            ddl.DataValueField = "t02_cd_usuario";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }

        //--- Área de Resultado ---//
        ddl = ddlt26_cd_arearesultado;
        ddl.DataSource = new t26_arearesultadoAction().ListTodos();
        ddl.DataTextField = "nm_area";
        ddl.DataValueField = "t26_cd_arearesultado";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Selecione");
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
                        exibirForm();
                        lblHeader.Text = "Alteração";
                        Retrieve();
                        break;
                    case "Deletar":
                        t03_projetoAction t03a = new t03_projetoAction();
                        t03a.DeleteDB(Convert.ToInt32(ViewState["cod"]));
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
                //formata caracter do projeto              
                if (e.Row.Cells[2].Text != null)
                {
                    e.Row.Cells[2].Text = RemoveCaracter(e.Row.Cells[2].Text);
                }

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
