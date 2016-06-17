using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CustomControls_ucResponsaveis : System.Web.UI.UserControl, ICrud
{
    private bool _editar;
    public bool Editar
    {
        get { return _editar; }
        set { _editar = value; }
    }
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridBind();
            ViewState["cod"] = "0";
            PerfilBind();

            t03_projeto t03 = new t03_projetoAction().Retrieve(
            Convert.ToInt32(pb.Session("cd_projeto")));

            t02_usuario t02 = new t02_usuarioAction().Retrieve(t03.t02_cd_usuario);
            lbldados_gerente.Text = pb.dadosUsuario(t02, 0);

            t02 = new t02_usuarioAction().Retrieve(t03.t02_cd_usuario_monitoramento);
            lbldados_monitoramento.Text = pb.dadosUsuario(t02, 1);

        }
    }

    public void PerfilBind()
    {
        if (pb.fl_gerente())
        {
            _editar = true;
        }

        if (_editar)
        {
            spanbtnNovo.Visible = true;
            spanbtnOrdem.Visible = true;
            GridView1.Columns[0].Visible = true;
            GridView1.Columns[1].Visible = true;
            GridView1.Columns[2].Visible = true;
        }
        else
        {
            spanbtnNovo.Visible = false;
            spanbtnOrdem.Visible = false;
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
            GridView1.Columns[2].Visible = false;
        }
    }

    #region ICrud Members

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

    public void LimparForm()
    {        
        ddlt02_cd_usuario.ClearSelection();
    }

    public void GridBind()
    {
        GridView1.DataSource = new 
            t06_colaboradorAction().ListTodos(
            Convert.ToInt32(pb.Session("cd_projeto")));
        GridView1.DataBind();

        
    }

    public void FormBind()
    {
        DropDownList ddl = this.ddlt02_cd_usuario;
        ddl.DataSource = new t02_usuarioAction().ListTodos();
        ddl.DataTextField = "nm_nome";
        ddl.DataValueField = "t02_cd_usuario";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Selecione");
    }

    public void Retrieve()
    {
        t06_colaborador t06 = new 
            t06_colaboradorAction().Retrieve(Convert.ToInt32(ViewState["cod"]));        
        ListItem li = ddlt02_cd_usuario.Items.FindByValue(t06.t02_cd_usuario);
        if (li != null)
            li.Selected = true;
        
    }

    public void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
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
                        new t06_colaboradorAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
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

    public void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = ((DataRowView)e.Row.DataItem);

                //Adicionar mensagem de alerta antes da exclusão
                ImageButton btn = (ImageButton)e.Row.Cells[1].Controls[0];
                if (btn != null)
                {
                    btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja excluir?') == false) return false;");
                }

                TextBox txtOrdem = (TextBox)e.Row.FindControl("txtnu_ordem");
                if (txtOrdem != null)
                {
                    txtOrdem.Text = (e.Row.RowIndex + 1).ToString();
                }

                t02_usuario t02 = new t02_usuarioAction().Retrieve(drv["t02_cd_usuario"].ToString());
                if (e.Row.Cells[5] != null)
                {
                    e.Row.Cells[5].Text = pb.dadosUsuario(t02, e.Row.RowIndex + 2);
                }

                if (e.Row.Cells[3] != null)
                {
                    e.Row.Cells[3].Text = t02.nm_cargo;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowDataBound: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    public void btnSalvar_Click(object sender, EventArgs e)
    {
        int result = 0;
        string msg = "";
        try
        {
            t06_colaborador t06 = new t06_colaborador();
            t06_colaboradorAction t06a = new t06_colaboradorAction();

            t06.t06_cd_colaborador = Convert.ToInt32(ViewState["cod"]);           
            t06.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
            t06.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));

            if (t06.t06_cd_colaborador > 0)
            {
                result = t06a.UpdateDB(t06);
            }
            else
            {
                result = t06a.InsertDB(t06);
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

    public void btnCancelar_Click(object sender, EventArgs e)
    {
        OcultarForm();
    }

    public void btnNovo_Click(object sender, EventArgs e)
    {
        if (GridView1.Rows.Count > 4)
        {
            lblMsg.Text = pb.Message("Não é possível \"Cadastrar Novo\", limite de 5 responsáveis foi atingido. ", "erro");
            lblMsg.Visible = true;
        }
        else
        {
            ViewState["cod"] = "0";
            ExibirForm();
            lblHeader.Text = "Cadastrar Novo";
        }
    }

    public void GridView1_PreRender(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        throw new NotImplementedException();
    }

    #endregion
    protected void btnOrdem_Click(object sender, EventArgs e)
    {
        string msg = "";
        try
        {
            int i = 0;
            foreach (DataRow dr in new
                t06_colaboradorAction().ListTodos(
                Convert.ToInt32(pb.Session("cd_projeto"))).Rows)
            {
                TextBox txtOrdem = (TextBox)GridView1.Rows[i].Cells[2].FindControl("txtnu_ordem");
                if (txtOrdem != null)
                {
                    try
                    {
                        new t06_colaboradorAction().UpdateOrdem(
                            (int)dr[0], txtOrdem.Text);
                    }
                    catch
                    {

                    }
                }
                i++;
            }
            GridBind();
            msg = pb.Message("Salvo com sucesso", "ok");
        }
        catch (Exception ex)
        {
            msg = pb.Message(ex.Message, "erro");
        }
        lblMsg.Text = msg;
        lblMsg.Visible = true;
    }
}
