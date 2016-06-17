using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CustomControls_ucProvidencia : System.Web.UI.UserControl, ICrud
{
    pageBase pb = new pageBase();

    public void Page_Load(object sender, EventArgs e)
    {
        hdDataAtual.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
        if (!IsPostBack)
        {
            GridBind();
            ViewState["cod"] = "0";            
            
            //GridView1.Columns[0].Visible = false;
            //GridView1.Columns[1].Visible = false;

            if (pb.fl_gerente() || pb.fl_respmonitora())
            {
                spanbtnNovo.Visible = true;
                //GridView1.Columns[0].Visible = true;
                //GridView1.Columns[1].Visible = true;
            }
            else
            {
                spanbtnNovo.Visible = false;
                //GridView1.Columns[0].Visible = false;
                //GridView1.Columns[1].Visible = false;
            }
        }
    }


    #region ICrud Members

    public void ExibirForm()
    {
        LimparForm();
        this.mdlPopup.Show();
    }

    public void FormBind()
    {
        throw new NotImplementedException();
    }

    public void GridBind()
    {
        GridView1.DataSource = new t23_providenciaAction().ListTodos(
            Convert.ToInt32(pb.Session("cd_restricao")));
        GridView1.DataBind();
    }

    public void GridView1_PreRender(object sender, EventArgs e)
    {
        throw new NotImplementedException();
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
                        new t23_providenciaAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
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

    public void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        throw new NotImplementedException();
    }

    public void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = ((DataRowView)e.Row.DataItem);

                if (pb.fl_gerente() || pb.fl_respmonitora())
                {
                    e.Row.Cells[0].Controls[0].Visible = true;
                    e.Row.Cells[1].Controls[0].Visible = true;
                }
                else
                {
                    e.Row.Cells[0].Controls[0].Visible = false;
                    e.Row.Cells[1].Controls[0].Visible = false;
                    
                }

                if (e.Row.Cells[3] != null)
                {
                    e.Row.Cells[3].Text = pb.dadosUsuario(new t02_usuarioAction().Retrieve(
                        drv["t02_cd_usuario"].ToString()), e.Row.RowIndex);

                    //if (e.Row.Cells[4] != null)
                    //{
                    //    if (Convert.ToBoolean(drv["fl_gerente"]))
                    //    {
                    //        e.Row.Cells[4].Text = "Gerente";
                    //    }
                    //    else
                    //    {
                    //        e.Row.Cells[4].Text = "Responsável pelo Monitoramento";
                    //    }

                    //}
                }


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

    public void LimparForm()
    {
        txtds_providencia.Text = "";

        t02_usuarioAction t02a = new t02_usuarioAction();
        {
            DropDownList ddl = ddlt02_cd_usuario;
            ddl.DataSource = t02a.ListTodos();
            ddl.DataTextField = "nm_nome";
            ddl.DataValueField = "t02_cd_usuario";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");
        }
    }

    public void OcultarForm()
    {
        LimparForm();
        this.mdlPopup.Hide();

    }

    public void Retrieve()
    {
        LimparForm();
        t23_providencia t23 = new t23_providenciaAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtds_providencia.Text = t23.ds_providencia;
        txtdt_limiteProv.Text = string.Format("{0:dd/MM/yyyy}", t23.dt_limite);


        t02_usuarioAction t02a = new t02_usuarioAction();
        {
            DropDownList ddl = ddlt02_cd_usuario;
            ddl.ClearSelection();
            ddl.DataSource = t02a.ListTodos();
            ddl.DataTextField = "nm_nome";
            ddl.DataValueField = "t02_cd_usuario";
            ddl.DataBind();
            pb.AddEmptyItem(ddl, "Selecione");

            ListItem litem;
            litem = ddlt02_cd_usuario.Items.FindByValue(t23.t02_cd_usuario);
            if (litem != null) litem.Selected = true;
        }
    }

    public void btnCancelar_Click(object sender, EventArgs e)
    {
        OcultarForm();
    }

    public void btnNovo_Click(object sender, EventArgs e)
    {
        ViewState["cod"] = "0";
        ExibirForm();
        lblHeader.Text = "Cadastrar Novo";
    }

    public void btnSalvar_Click(object sender, EventArgs e)
    {
        int result = 0;
        string msg = "";
        try
        {
            t23_providencia t23 = new t23_providencia();
            t23_providenciaAction t23a = new t23_providenciaAction();

            t23.t23_cd_providencia = Convert.ToInt32(ViewState["cod"]);
            
            t23.t07_cd_restricao = Convert.ToInt32(pb.Session("cd_restricao"));
            t23.ds_providencia = txtds_providencia.Text;
            //t23.t02_cd_usuario = pb.cd_usuario();
            t23.fl_gerente = pb.fl_gerente();
            t23.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
            t23.dt_limite = Convert.ToDateTime(txtdt_limiteProv.Text);

            if (t23.t23_cd_providencia > 0)
            {                
                result = t23a.UpdateDB(t23);
            }
            else
            {
                result = t23a.InsertDB(t23);
            }

            if (result > 0)
            {
                //atualiza data restrição
                AtualizaDataRestricao(t23);

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

    /// <summary>
    /// atualiza data limite da restrição se data limite providencia for maior
    /// </summary>
    /// <param name="t23">Objeto populado</param>
    protected void AtualizaDataRestricao(t23_providencia t23)
    {
        try
        {
            t07_restricao t07 = new t07_restricao();
            int cd_restricao = Convert.ToInt32(pb.Session("cd_restricao"));
            t07 = new t07_restricaoAction().Retrieve(cd_restricao);
            if(t23.dt_limite > t07.dt_limite){
                //atualiza data da restrição
                new t07_restricaoAction().UpdateDataLimite(cd_restricao, t23.dt_limite);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message(ex.Message, "erro");
            lblMsg.Visible = true;
            throw;
        }

    }

    #endregion
}
