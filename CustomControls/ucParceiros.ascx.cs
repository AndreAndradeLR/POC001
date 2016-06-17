using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomControls_ucParceiros : System.Web.UI.UserControl
{
    private bool _editar;
    public bool Editar{
        get {return _editar;}
        set { _editar = value; }
    }

    pageBase pb = new pageBase();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridBind();
            ViewState["cod"] = "0";
            if (pb.fl_gerente())
            {
                _editar = true;
            }

            if (_editar)
            {
                spanbtnNovo.Visible = true;
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[1].Visible = true;
            }
            else
            {
                spanbtnNovo.Visible = false;
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = false;
            }

            t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));
            if (t21.t21_cd_fase == 2)
            {
                spanbtnNovo.Visible = false;
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = false;
            }

        }
    }

    private void GridBind()
    {
        t04_parceiroAction t04a = new t04_parceiroAction();
        GridView1.DataSource = t04a.ListTodos(Convert.ToInt32(pb.Session("cd_projeto")));
        GridView1.DataBind();
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
                        ExibirForm();
                        Retrieve();
                        break;
                    case "Deletar":
                        new t04_parceiroAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
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
        int result = 0;
        string msg = "";
        try
        {
            t04_parceiro t04 = new t04_parceiro();
            t04_parceiroAction t04a = new t04_parceiroAction();

            t04.t04_cd_parceiro = Convert.ToInt32(ViewState["cod"]);
            t04.nm_nome = txtnm_nome.Text;
            t04.ds_atuacao = txtds_atuacao.Text;
            t04.t01_cd_entidade = Convert.ToInt32(ddlt01_cd_entidade.SelectedValue);
            t04.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));

            if (t04.t04_cd_parceiro > 0)
            {
                result = t04a.UpdateDB(t04);
            }
            else
            {
                result = t04a.InsertDB(t04);
            }

            if (result > 0)
            {
                msg = pb.Message("Salvo com sucesso", "ok");
                OcultarForm();
                GridBind();
                ViewState["cod"] = "0";
            }
        }
        catch (Exception ex)
        {
            msg = pb.Message(ex.Message, "erro");
        }

        lblMsg.Text = msg;
        lblMsg.Visible = true;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        OcultarForm();
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        ViewState["cod"] = "0";
        ExibirForm();
        lblHeader.Text = "Cadastrar Novo";
    }

    public void ExibirForm()
    {
        FormBind();
        this.mdlPopup.Show();
    }

    public void OcultarForm()
    {
        LimparForm();
        this.mdlPopup.Hide();

    }

    public void Retrieve()
    {
        LimparForm();
        t04_parceiro t04 = new t04_parceiroAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtds_atuacao.Text = t04.ds_atuacao;
        txtnm_nome.Text = t04.nm_nome;
        ListItem li = ddlt01_cd_entidade.Items.FindByValue(t04.t01_cd_entidade.ToString());
        if (li != null)
        {
            li.Selected = true;
        }

    }

    private void FormBind()
    {
        DropDownList ddl = this.ddlt01_cd_entidade;
        t01_entidadeAction t01a = new t01_entidadeAction();
        {
            ddl.DataSource = t01a.ListTodos();
            ddl.DataTextField = "nm_entidade";
            ddl.DataValueField = "t01_cd_entidade";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
    }

    private void LimparForm()
    {
        txtds_atuacao.Text = "";
        txtnm_nome.Text = "";
        ddlt01_cd_entidade.ClearSelection();
    }
 
    #region JQuery
    public void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            JQuery jquery = new JQuery();
            if (pb.fl_gerente())
            {
                jquery.SortList = "2, 0";
                jquery.Headers = "0: {sorter: false}, 1: {sorter: false}";
            }
            else
            {
                jquery.SortList = "0, 0";
            }
           
            jquery.tableID = GridView1.ClientID;
            jquery.TableSorter();

            gv.UseAccessibleHeader = true;
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    public void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            int g = 0;
            t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));
            if (t21.t21_cd_fase == 2)
            {
                g = 2;
            }            
            if (!pb.fl_gerente())
            {
                g = 2;
            }

            int m = e.Row.Cells.Count;
            for (int i = m - 1; i >= (1 + g); i += -1)
            {
                e.Row.Cells.RemoveAt(i);
            }

            e.Row.Cells[g].ColumnSpan = m;
            e.Row.Attributes.Add("id", "pager" + gv.ClientID);
            JQuery jq = new JQuery();
            e.Row.Cells[g].Text = jq.PagerHtml();
        }
    }
    #endregion
    



    
}
