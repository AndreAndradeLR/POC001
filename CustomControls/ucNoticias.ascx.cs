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

public partial class CustomControls_ucNoticias : System.Web.UI.UserControl, ICrud
{
    bool _projetos;
    public bool projetos
    {
        get { return _projetos; }
        set { _projetos = value; }
    }
    private bool _editar;
    public bool Editar
    {
        get { return _editar; }
        set { _editar = value; }
    }

    pageBase pb = new pageBase();

    public void Page_Load(object sender, EventArgs e)
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
        }
    }


    #region ICrud Members

    public void ExibirForm()
    {
        this.mdlPopup.Show();
    }

    public void FormBind()
    {
        throw new NotImplementedException();
    }

    public void GridBind()
    {
        if (pb.Session("cd_projeto").ToString() == "0")
        {
            GridView1.DataSource = new t15_noticiaAction().ListTodos();
        }
        else
        {
            GridView1.DataSource = new t15_noticiaAction().ListDoProjeto(
                Convert.ToInt32(pb.Session("cd_projeto")));
        }
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
                        trArquivo.Visible = false;
                        trOpcao.Visible = true;
                        lblHeader.Text = "Alteração";
                        ExibirForm();
                        Retrieve();
                        break;
                    case "Deletar":
                        new t15_noticiaAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        break;
                    case "Projeto":
                        t15_noticia t15 = new
                            t15_noticiaAction().Retrieve(
                            Convert.ToInt32(ViewState["cod"]));
                        Session["cd_projeto"] = t15.t03_cd_projeto;
                        Response.Redirect("Arvore.aspx");
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

                //Adicionar mensagem de alerta antes da exclusão
                ImageButton btn = (ImageButton)e.Row.Cells[1].Controls[0];
                if (btn != null)
                {
                    btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja excluir?') == false) return false;");
                }

                if (drv["nm_arquivo"].ToString().Length > 1)
                {
                    e.Row.Cells[4].Text = "<a href='Documentos/" + drv["nm_arquivo"] + "' title='download do arquivo' target='_blank' ><img src='images/ico_download.gif'/></a>";
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
        txtds_noticia.Text = "";
        txtdt_data.Text = "";
        rblArquivo.ClearSelection();
        rblArquivo.SelectedValue = "N";
    }

    public void OcultarForm()
    {
        LimparForm();
        trArquivo.Visible = true;
        trOpcao.Visible = false;
        this.mdlPopup.Hide();

    }

    public void Retrieve()
    {
        LimparForm();
        t15_noticia t15 = new t15_noticiaAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtds_noticia.Text = t15.ds_noticia;
        txtdt_data.Text = t15.dt_data.ToShortDateString();

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
        bool continua = false;
        try
        {
            uploadArquivo up = new uploadArquivo();
            if (funm_arquivo.Visible)
            {
                up.pasta = "Documentos";
                up.nomeinicial = "noticia";
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
                t15_noticia t15 = new t15_noticia();
                t15_noticiaAction t15a = new t15_noticiaAction();

                t15.t15_cd_noticia = Convert.ToInt32(ViewState["cod"]);
                t15.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));
                t15.ds_noticia = txtds_noticia.Text;
                t15.dt_data = DateTime.Parse(txtdt_data.Text);
                t15.nm_arquivo = up.nomearquivo;

                if (t15.t15_cd_noticia > 0)
                {
                    if (funm_arquivo.Visible)
                    {
                        result = t15a.UpdateUploadDB(t15);
                    }
                    else
                    {
                        result = t15a.UpdateDB(t15);
                    }
                }
                else
                {
                    result = t15a.InsertDB(t15);
                }

                if (result > 0)
                {
                    msg = pb.Message("Salvo com sucesso", "ok");
                    OcultarForm();
                    GridBind();
                    ViewState["cod"] = "0";
                }

            }
            else
            {
                ExibirForm();
            }
        }
        catch (Exception ex)
        {
            msg = pb.Message(ex.ToString(), "erro");
        }

        lblMsg.Text = msg;
        lblMsg.Visible = true;
    }
    
    #endregion
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
        ExibirForm();
    }
}