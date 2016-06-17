using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class frmUsuario : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, System.EventArgs e) 
    {
        if (!IsPostBack)
        {
            ViewState["cod"] = "";
            lblTitle.Text = "Usuários";
            GridBind();
            FormBind();
            if (Request["altersenha"] != null) { 
                lblMsg.Text = pb.Message("Alteração realizada com sucesso!", "ok"); 
                lblMsg.Visible = true; 
            }            
        } 
    }
    private void limparForm()
    {
        this.txtnm_nome.Text = "";
        this.txtnm_email.Text = "";
        this.txtt02_cd_usuario.Text = "";
        this.txtnm_dddc.Text = "";
        this.txtnm_celular.Text = "";
        this.txtnm_dddt.Text = "";
        this.txtnm_telefone.Text = "";
        ddlt01_cd_entidade.ClearSelection();

        rblt24_cd_perfil.ClearSelection();

        txtnm_cargo.Text = "";
    }

    private void exibirForm() 
    {
        this.PanelForm.Visible = true; 
        this.PanelGrid.Visible = false;
    } 

    private void ocultarForm() 
    { 
        this.PanelForm.Visible = false; 
        this.PanelGrid.Visible = true;
        GridBind();
        ViewState["cod"] = "";
        limparForm();
    } 

    private void GridBind() 
    {
        t02_usuarioAction t02a = new t02_usuarioAction(); 
        {
            GridView1.DataSource = t02a.ListTodos();
            GridView1.DataBind(); 
        } 
    }

    private void Retrieve()
    {
        limparForm();
        t02_usuarioAction t02a = new t02_usuarioAction();
        t02_usuario t02 = new t02_usuario();
        t02 = t02a.Retrieve(ViewState["cod"].ToString());
        this.txtnm_nome.Text = t02.nm_nome;
        this.txtnm_email.Text = t02.nm_email;
        this.txtnm_cargo.Text = t02.nm_cargo;
        if (t02.nm_telefone.Length == 10)
        {
            this.txtnm_dddt.Text = t02.nm_telefone.Substring(0, 2);
            this.txtnm_telefone.Text = t02.nm_telefone.Substring(2, 8);
        }
        

        if (t02.nm_celular.ToString().Length == 10)
        {
            this.txtnm_celular.Text = t02.nm_celular.Substring(2, 8);
            this.txtnm_dddc.Text = t02.nm_celular.Substring(0, 2);
        }

        this.ddlt01_cd_entidade.ClearSelection();
        ListItem li = this.ddlt01_cd_entidade.Items.FindByValue(t02.t01_cd_entidade.ToString());
        if (li != null)
            li.Selected = true;

        li = null;
        foreach (t24_perfil t24 in t02.t24l)
        {
            li = rblt24_cd_perfil.Items.FindByValue(t24.t24_cd_perfil.ToString());
            if (li != null)
                li.Selected = true;
        }
     

    }

    protected void btnAcao_Click(object sender, System.EventArgs e)
    {        
        t02_usuarioAction t02a = new t02_usuarioAction();
        t02_usuario t02 = new t02_usuario();
        string pass = "";        
        int result = 0;
        bool erro = false;
        string msg = "";

         pass = t02a.DuplicidadeUsuario(txtt02_cd_usuario.Text);
        if (pass != "")
        {
            msg = pb.Message("Usuario já cadastrado (<span style='font-size:9px;'>modifique o usuário</span>) ", "erro");
            erro = true;
        }
        t02.nm_nome = this.txtnm_nome.Text;
        t02.nm_email = this.txtnm_email.Text;
        t02.nm_cargo = txtnm_cargo.Text;
        t02.pw_senha = this.txtpw_senha.Text;
        if (txtnm_dddt.Text != "" && txtnm_telefone.Text != "")
        {
            t02.nm_telefone = txtnm_dddt.Text + txtnm_telefone.Text;
        }
        else
        {
            t02.nm_telefone = "";
        }

        if (txtnm_dddc.Text != "" && txtnm_celular.Text != "")
        {
            t02.nm_celular = txtnm_dddc.Text + txtnm_celular.Text;
        }
        else
        {
            t02.nm_celular = "";
        }

        if (ddlt01_cd_entidade.SelectedValue != "")
            t02.t01_cd_entidade = Int32.Parse(ddlt01_cd_entidade.SelectedValue);


        if ((txtnm_dddt.Text + txtnm_telefone.Text).Length != 10 && (txtnm_dddt.Text + txtnm_telefone.Text).Length > 0)
        {
            msg = pb.Message("Formato de telefone inválido! ", "erro");
            erro = true;
        }
        if ((txtnm_dddc.Text + txtnm_celular.Text).Length != 10 && (txtnm_dddc.Text + txtnm_celular.Text).Length > 0)
        {
            msg += pb.Message("Formato de celular inválido! ", "erro");
            erro = true;
        }
        if (!(erro))
        {
            //Response.Write(ViewState["cod"].ToString().Length);
            if (ViewState["cod"].ToString().Length > 0)
            {
                t02.t02_cd_usuario = ViewState["cod"].ToString();
                result = t02a.UpdateDB(t02);
            }
            else
            {
                t02.t02_cd_usuario = txtt02_cd_usuario.Text;
                result = t02a.InsertDB(t02);
            }

            if (result > 0)
            {
                t25_usuarioperfilAction t25a = new t25_usuarioperfilAction();
                t25a.DeleteDB(t02.t02_cd_usuario);
                foreach (ListItem li in rblt24_cd_perfil.Items)
                {
                    if (li.Selected)
                    {
                        t25a.InsertDB(new t25_usuarioperfil(t02.t02_cd_usuario, Convert.ToInt32(li.Value)));
                    }
                }

                msg = pb.Message("Salvo com sucesso", "ok");
                ocultarForm();
                GridBind();
                ViewState["cod"] = "";
            }
        }
        lblMsg.Text = msg;
        lblMsg.Visible = true;


    } 

    protected void GridView1_SelectedIndexChanged(object sender, System.EventArgs e) 
    { 
        exibirForm();
        this.lblHeader.Text = "Alteração (Usuário: <b>" + GridView1.SelectedValue.ToString() + "</b>)"; 
        this.btnAcao.Text = "Alterar"; 
        Retrieve(); 
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                ViewState["cod"] = gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString();

                switch (e.CommandName)
                {
                    case "Editar":
                        clean.Visible = true;
                        exibirForm();
                        trLogin.Visible = false; 
                        trSenha.Visible = false; 
                        trSenhaConf.Visible = false;
                        lblHeader.Text = "Alteração";
                        FormBind();
                        Retrieve();
                        break;
                    case "Senha":
                        Response.Redirect("frmSenha.aspx?cd_usuario=" + ViewState["cod"]); 
                        break;
                    case "Deletar":
                        t02_usuarioAction t02a = new t02_usuarioAction();
                        t02a.DeleteDB(ViewState["cod"].ToString());
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "";
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
                DataRowView drv = ((DataRowView)e.Row.DataItem);
                t02_usuarioAction t02a = new t02_usuarioAction();
                DataTable dt = t02a.RetrievePerfil(drv["t02_cd_usuario"].ToString());
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {                   
                   sb.AppendLine(" " + dr["nm_perfil"] + "");
                   sb.AppendLine("<br>");
                }
                e.Row.Cells[9].Text = sb.ToString();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowDataBound: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }
    

    protected void btnCadastro_Click(object sender, System.EventArgs e)
    {
        exibirForm();
        clean.Visible = false;
        trLogin.Visible = true;
        trSenha.Visible = true;
        trSenhaConf.Visible = true;
        lblHeader.Text = "Cadastrar Novo";
    } 

    protected void btnCancel_Click(object sender, System.EventArgs e) 
    { 
        ocultarForm(); 
    }

    protected void FormBind()
    {
        DropDownList ddl = this.ddlt01_cd_entidade;
        ddl.DataSource = new t01_entidadeAction().ListTodos();
        ddl.DataTextField = "nm_entidade";
        ddl.DataValueField = "t01_cd_entidade";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Selecione");

        //CheckBoxList cbl = this.cblt24_cd_perfil;
        //cbl.DataSource = new t24_perfilAction().ListTodos();
        //cbl.DataTextField = "nm_perfil";
        //cbl.DataValueField = "t24_cd_perfil";
        //cbl.DataBind();

        RadioButtonList rbl = this.rblt24_cd_perfil;
        rbl.DataSource = new t24_perfilAction().ListTodos();
        rbl.DataTextField = "nm_perfil";
        rbl.DataValueField = "t24_cd_perfil";
        rbl.DataBind();

    }

    protected void btnLimparRbl_Click(object sender, EventArgs e)
    {
        rblt24_cd_perfil.ClearSelection();
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
    #endregion

   
}
