using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CustomControls_ucDocs : System.Web.UI.UserControl, ICrud
{
    private bool _editar;
    public bool Editar
    {
        get { return _editar; }
        set { _editar = value; }
    }
    pageBase pb = new pageBase();
    bool fl_foto;
    bool fl_cronograma;
    bool fl_outros;
    bool fl_gerente;
    string tipo;

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Buffer = true;
        Response.AddHeader("pragma", "no-cache");
        Response.Expires = 0;

        if (Request["tipo"] != null)
        {
            fl_foto = false;
            fl_cronograma = false;
            fl_outros = false;
            tipo = Request["tipo"].ToString();

            switch (Request["tipo"].ToString())
            {
                case "foto":
                    fl_foto = true;
                    imgTipo.ImageUrl = "~/images/ico_foto.gif";
                    lblTipo.Text = "Fotos";
                          

                    break;
                case "cronograma":
                    fl_cronograma = true;
                    imgTipo.ImageUrl = "~/images/ico_cronograma.gif";
                    lblTipo.Text = "Cronogramas";
                    break;
                case "outros":
                    fl_outros = true;
                    imgTipo.ImageUrl = "~/images/ico_outros.gif";
                    lblTipo.Text = "Outros";
                    break;
            }
            GridView1.Columns[2].HeaderText = lblTipo.Text;

        }
        else
        {
            Response.Redirect("~/Arvore.aspx");
        }
        PerfilBind();

        if (!IsPostBack)
        {
            
            GridBind();
            ViewState["cod"] = "0";
        }
        
    }

    public void PerfilBind()
    {

        if (pb.fl_gerente())
        {
            _editar = true;
            fl_gerente = true;
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

    protected void perfil_Load(object sender, EventArgs e)
    {
        Control obj = (Control)sender;
        if (!pb.fl_gerente()) //se não for gerente
        {
            obj.Visible = false;
        }
    }

    protected void dlFotos_ItemCommand(object sender, DataListCommandEventArgs e)
    {
        try
        {
            DataList dl = (DataList)sender;

            ViewState["cod"] = Int32.Parse(dl.DataKeys[e.Item.ItemIndex].ToString());

            switch (e.CommandName)
            {
                case "Editar":
                    lblHeader.Text = "Alteração";
                    trArquivo.Visible = false;
                    trOpcao.Visible = true;
                    ExibirForm();
                    Retrieve();
                    break;
                case "Excluir":
                    new t14_documentoAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                    lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                    lblMsg.Visible = true;
                    GridBind();
                    ViewState["cod"] = "0";
                    break;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowCommand: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    #region ICrud Members

    public void ExibirForm()
    {
        this.mdlPopup.Show();
    }

    public void OcultarForm()
    {
        LimparForm();
        trArquivo.Visible = true;
        trOpcao.Visible = false;
        this.mdlPopup.Hide();
    }

    public void LimparForm()
    {
        txtnm_documento.Text = "";
        rblArquivo.ClearSelection();
        rblArquivo.SelectedValue = "N";
    }

    public void GridBind()
    {
        System.Data.DataTable dt = null;
        if (fl_cronograma)
        {
            dt = new t14_documentoAction().ListCrono(
                Convert.ToInt32(pb.Session("cd_projeto")));
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else if (fl_foto)
        {
            dt = new t14_documentoAction().ListFoto(
                Convert.ToInt32(pb.Session("cd_projeto")));
            dlFotos.DataSource = dt;
            dlFotos.DataBind();
        }
        else if (fl_outros)
        {
            dt = new t14_documentoAction().ListDoc(
                Convert.ToInt32(pb.Session("cd_projeto")));
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        
    }

    public void FormBind()
    {
        throw new NotImplementedException();
    }

    public void Retrieve()
    {
        LimparForm();
        t14_documento t14 = new
            t14_documentoAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtnm_documento.Text = t14.nm_documento;

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
                        trArquivo.Visible = false;
                        trOpcao.Visible = true;
                        ExibirForm();
                        Retrieve();
                        break;
                    case "Deletar":
                        new t14_documentoAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
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
                up.nomeinicial = tipo;
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
                t14_documento t14 = new t14_documento();
                t14_documentoAction t14a = new t14_documentoAction();

                t14.t14_cd_documento = Convert.ToInt32(ViewState["cod"]);
                t14.nm_documento = txtnm_documento.Text;
                t14.nm_arquivo = up.nomearquivo;
                t14.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));

                if (t14.t14_cd_documento > 0)
                {
                    if (funm_arquivo.Visible)
                    {
                        result = t14a.UpdateUploadDB(t14);
                    }
                    else
                    {
                        result = t14a.UpdateDB(t14);
                    }
                }
                else
                {
                    
                    if (fl_cronograma)
                    {
                        result = t14a.InsertCrono(t14);
                    }
                    else if (fl_foto)
                    {
                        result = t14a.InsertFoto(t14);
                    }
                    else if (fl_outros)
                    {
                        result = t14a.InsertDoc(t14);
                    }
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
        ViewState["cod"] = "0";
        ExibirForm();
        lblHeader.Text = "Cadastrar Novo";
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
        ExibirForm();
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
}
