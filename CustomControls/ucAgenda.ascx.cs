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

public partial class CustomControls_ucAgenda : System.Web.UI.UserControl, ICrud
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
            GridView1.DataSource = new t16_agendaAction().ListTodos();
        }
        else
        {
            GridView1.DataSource = new t16_agendaAction().ListDoProjeto(
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
                        lblHeader.Text = "Alteração";
                        ExibirForm();
                        Retrieve();
                        break;
                    case "Deletar":
                        new t16_agendaAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        break;
                    case "Projeto":
                        t16_agenda t16 = new
                            t16_agendaAction().Retrieve(
                            Convert.ToInt32(ViewState["cod"]));
                        Session["cd_projeto"] = t16.t03_cd_projeto;
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

    public void LimparForm()
    {
        txtds_agenda.Text = "";
        txtdt_data.Text = "";
    }

    public void OcultarForm()
    {
        LimparForm();
        this.mdlPopup.Hide();

    }

    public void Retrieve()
    {
        LimparForm();
        t16_agenda t16 = new t16_agendaAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtds_agenda.Text = t16.ds_agenda;
        txtdt_data.Text = t16.dt_data.ToShortDateString();

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
            t16_agenda t16 = new t16_agenda();
            t16_agendaAction t16a = new t16_agendaAction();

            t16.t16_cd_agenda = Convert.ToInt32(ViewState["cod"]);
            t16.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));
            t16.ds_agenda = txtds_agenda.Text;
            t16.dt_data = DateTime.Parse(txtdt_data.Text);

            if (t16.t16_cd_agenda > 0)
            {
                result = t16a.UpdateDB(t16);
            }
            else
            {
                result = t16a.InsertDB(t16);
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

    #endregion
}
