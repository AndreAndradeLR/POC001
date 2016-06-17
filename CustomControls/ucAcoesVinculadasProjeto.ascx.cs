using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CustomControls_ucAcoesVinculadasProjeto : System.Web.UI.UserControl, ICrud
{
    pageBase pb1 = new pageBase();

    public void Page_Load(object sender, EventArgs e)
    {        
        FormBind();
            GridBind();
        if (!IsPostBack)
        {
       
            ViewState["cod"] = "0";
        }
    }


    #region ICrud Members

    public void ExibirForm()
    {
        this.mdlPopup.Show();
        PanelGridAcoesVincProj.Visible = true;
        PanelbtnNovo.Visible = true;
        GridView1.Visible = true;
    }

    public void FormBind()
    {
        if (!IsPostBack)
        {  
            PopulaDDl();
        }     
    }

    public void PopulaDDl()
    {
        ddlt03_projeto.DataSource = new t03_projetoAction().ListTodosProjMenosSustent(Session["codigoProjeto"].ToString(), pb1.Session("cd_acao").ToString());
        ddlt03_projeto.DataTextField = "nm_projeto";
        ddlt03_projeto.DataValueField = "t03_cd_projeto";
        ddlt03_projeto.DataBind();
        pb1.AddEmptyItem(ddlt03_projeto, "Selecione");
    }

    public void GridBind()
    {
        GridView1.Visible = true;
        GridView1.DataSource = new t30_AcoesVinculadasProjetoAction().ListTodos(Convert.ToInt32(pb1.Session("cd_acao")));
        GridView1.DataBind();

        //possibilidade de ter 3 projetos vinculadas àquela ação
        int cont = GridView1.Rows.Count;
        if (cont >= 3)
        {
            PanelbtnNovo.Visible = false;      
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                switch (e.CommandName)
                {
                    case "Deletar":
                        new t30_AcoesVinculadasProjetoAction().DeleteDB(Int32.Parse(e.CommandArgument.ToString()));
                        lblMsg.Text = pb1.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        PanelbtnNovo.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb1.Message("RowCommand: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    public void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                //Adicionar mensagem de alerta antes da exclusão
                ImageButton btn = (ImageButton)e.Row.Cells[0].Controls[0];

                if (btn != null)
                {
                    btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja excluir?') == false) return false;");
                    btn.CommandArgument = gv.DataKeys[e.Row.RowIndex].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = pb1.Message("RowDataBound: " + ex.Message, "erro");
                lblMsg.Visible = true;
            }
        }
    }

    public void LimparForm()
    {
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb1.Session("cd_acao")));
        ddlt03_projeto.ClearSelection();
    }

    public void OcultarForm()
    {
        LimparForm();
        PanelGridAcoesVincProj.Visible = true;
        PanelbtnNovo.Visible = true;
        ViewState["cod"] = 0;

    }

    public void Retrieve()
    {
        LimparForm();
        t30_AcoesVinculadasProjeto t30 = new t30_AcoesVinculadasProjetoAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        ListItem li = ddlt03_projeto.Items.FindByValue(t30.t03_cd_projeto.ToString());
        if (li != null)
        {
            li.Selected = true;
        }
    }

    public void btnCancelar_Click(object sender, EventArgs e)
    {
        OcultarForm();
        GridBind();
    }

    public void btnNovo1_Click(object sender, EventArgs e)
    {
        PopulaDDl();
        ViewState["cod"] = "0";
        ExibirForm();
        lblHeader.Text = "Cadastrar Novo";
    }

    public void btnSalvar_Click(object sender, EventArgs e)
    {
        //script que verifica se conexão caiu antes de executar ação
        if (pb1.Session("cd_usuario") != "0")
        {
            int result = 0;
            string msg = "";
            try
            {
                t30_AcoesVinculadasProjeto t30 = new t30_AcoesVinculadasProjeto();
                t30_AcoesVinculadasProjetoAction t30a = new t30_AcoesVinculadasProjetoAction();

                t30.cd_acoes_vinculadas_projeto = Convert.ToInt32(ViewState["cod"]);
                t30.t08_cd_acao = Convert.ToInt32(pb1.Session("cd_acao"));
                t30.t03_cd_projeto = Convert.ToInt32(ddlt03_projeto.SelectedValue);


                if (t30.cd_acoes_vinculadas_projeto == 0)
                {
                    result = t30a.InsertDB(t30);
                }

                if (result > 0)
                {
                    msg = pb1.Message("Salvo com sucesso", "ok");
                    OcultarForm();
                    GridBind();
                    //this.mdlPopup.Show();
                    ViewState["cod"] = "0";
                }
            }
            catch (Exception ex)
            {
                msg = pb1.Message(ex.ToString(), "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }
    }

    #endregion

    #region ICrud Members old


    public void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void GridView1_PreRender(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    public void btnNovo_Click(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    #endregion
}
