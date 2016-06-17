using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CustomControls_ucRestricoes : System.Web.UI.UserControl
{
    private bool _superadas = false;
    public bool Superadas
    {
        get { return _superadas; }
        set { _superadas = value; }
    }

    pageBase pb = new pageBase();

    public void Page_Load(object sender, EventArgs e)
    {
        hdDataAtual.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
        if (!IsPostBack)
        {
            GridBind();
            ViewState["cod"] = "0";
        }
    }

    #region ICrud Members

    public void ExibirForm()
    {
        FormBind();
        this.mdlPopup.Show();
    }

    public void FormBind()
    {
        CheckBoxList cbl = cblt09_cd_marco;
        cbl.DataSource = new t09_marcoAction().ListTodos(
            Convert.ToInt32(pb.Session("cd_projeto")));               
        cbl.DataTextField = "ds_marco";
        cbl.DataValueField = "t09_cd_marco";
        cbl.DataBind();        

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

    public void GridBind()
    {
        if (_superadas)
        {
            GridView1.DataSource = new t07_restricaoAction().ListSuperadas(
                Convert.ToInt32(pb.Session("cd_projeto")));
            GridView1.Columns[0].Visible = false;
            GridView1.Columns[1].Visible = false;
            GridView1.Columns[2].Visible = false;
            divLinkSup.Visible = false;
            spanbtnNovo.Visible = false;
        }
        else
        {
            GridView1.DataSource = new t07_restricaoAction().ListTodos(
                Convert.ToInt32(pb.Session("cd_projeto")));
            GridView1.Columns[4].Visible = false;
			GridView1.Columns[8].Visible = false;
            if (pb.fl_gerente() || pb.fl_respmonitora())
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
        GridView1.DataBind();
    }

    public void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            JQuery jquery = new JQuery();
            if (pb.fl_gerente() && (_superadas == false))
            {
                jquery.SortList = "5, 0";
                jquery.Headers = "0: {sorter: false}, 1: {sorter: false}, 2: {sorter: false}";
            }
            else if (_superadas == false)
            {
                jquery.SortList = "3, 0";
                jquery.Headers = "0: {sorter: false}";
            }
            else
            {
                jquery.SortList = "4, 0";
            }
            jquery.tableID = GridView1.ClientID;
            jquery.DisablePaging = true;
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
                        OcultaProvidencia();
                        break;
                    case "Deletar":
                        t19_marcorestricaoAction t19a = new t19_marcorestricaoAction();
                        t19a.DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        new t07_restricaoAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        //atualiza barra de marcos
                        new t09_marcoAction().UpdateCorBarra(Convert.ToInt32(pb.Session("cd_projeto")));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        break;
                    case "Selecionar":
                        Session["cd_restricao"] = ViewState["cod"];
                        Response.Redirect("~/RestricaoDetalhes.aspx");
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

                if (e.Row.Cells[5] != null)
                {
                    t23_providencia t23 = new t23_providenciaAction().RetrieveTop1(Convert.ToInt32(drv["t07_cd_restricao"]));
                    e.Row.Cells[5].Text = t23.ds_providencia;
                    e.Row.Cells[6].Text = pb.dadosUsuario(new t02_usuarioAction().Retrieve(t23.t02_cd_usuario), e.Row.RowIndex, "restricoes");
                }

                if (e.Row.Cells[6] != null)
                {
                    t23_providencia t23 = new t23_providenciaAction().RetrieveTop1(Convert.ToInt32(drv["t07_cd_restricao"]));
                    e.Row.Cells[7].Text = String.Format("{0:dd/MM/yyyy}", t23.dt_limite);
                
                }
				
				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				t07_restricao t07 = new t07_restricaoAction().Retrieve(Convert.ToInt32(drv["t07_cd_restricao"]));
				sb.Append("<ul style='margin:0;padding-left:14px'>");
				foreach (t09_marco t09 in t07.t09)
				{
					 sb.Append("<li style='margin-bottom:5px'>"+t09.ds_marco + "</li>");
				}
				sb.Append("</ul>");
				e.Row.Cells[4].Text = sb.ToString();
				
				
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
        txtds_restricao.Text = "";        
        txtds_providencia.Text = "";
        cblt09_cd_marco.ClearSelection();
        ddlt02_cd_usuario.ClearSelection();
    }

    public void OcultarForm()
    {
        LimparForm();
        this.mdlPopup.Hide();

    }

    public void Retrieve()
    {
        LimparForm();        
        t07_restricao t07 = new t07_restricaoAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtds_restricao.Text = t07.ds_restricao;        
        
        foreach (t09_marco t09 in t07.t09)
        {
            ListItem li = cblt09_cd_marco.Items.FindByValue(t09.t09_cd_marco.ToString());
            if (li != null)
                li.Selected = true;
        }

        ListItem litem;      
        litem = ddlt02_cd_usuario.Items.FindByValue(t07.t02_cd_usuario);
        if (litem != null) litem.Selected = true;

    }

    public void OcultaProvidencia()
    {
        phProv1.Visible = false;
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
        phProv1.Visible = true;
    }

    public void btnSalvar_Click(object sender, EventArgs e)
    {
        //script que verifica se conexão caiu antes de executar ação
        if (pb.Session("cd_usuario") != "0")
        {
            int result = 0;
            bool cadastro = false;
            bool selectMc = false;
            string msg = "";
            try
            {
                for (int i = 0; i <= cblt09_cd_marco.Items.Count - 1; i++)
                {
                    ListItem li = cblt09_cd_marco.Items[i];
                    if (li.Selected)
                    {
                        selectMc = true;
                    }

                }

                if (selectMc)
                {
                    t07_restricao t07 = new t07_restricao();
                    t07_restricaoAction t07a = new t07_restricaoAction();

                    t07.t07_cd_restricao = Convert.ToInt32(ViewState["cod"]);
                    t07.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));
                    t07.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
                    t07.ds_restricao = txtds_restricao.Text;

                    if (t07.t07_cd_restricao > 0)
                    {
                        result = t07a.UpdateDB(t07);

                    }
                    else
                    {
                        t07.dt_limite = Convert.ToDateTime(txtdt_limiteProv.Text);
                        result = t07a.InsertDB(t07);
                        t07.t07_cd_restricao = t07a.RetrieveIDENTITY(t07);
                        cadastro = true;
                    }

                    if (result > 0)
                    {

                        t19_marcorestricaoAction t19a = new t19_marcorestricaoAction();
                        t19a.DeleteDB(t07.t07_cd_restricao);
                        for (int i = 0; i <= cblt09_cd_marco.Items.Count - 1; i++)
                        {
                            ListItem li = cblt09_cd_marco.Items[i];
                            if (li.Selected)
                            {
                                t19a = new t19_marcorestricaoAction();
                                t19_marcorestricao t19 = new t19_marcorestricao();
                                t19.t07_cd_restricao = t07.t07_cd_restricao;
                                t19.t09_cd_marco = Convert.ToInt32(li.Value);
                                t19a.InsertDB(t19);
                                new t09_marcoAction().UpdateCorRestricao(t19.t09_cd_marco);
                            }
                        }
                        if (cadastro)
                        {
                            t23_providencia t23 = new t23_providencia();
                            t23.ds_providencia = txtds_providencia.Text;
                            t23.fl_gerente = pb.fl_gerente();
                            t23.t07_cd_restricao = t07.t07_cd_restricao;
                            t23.t02_cd_usuario = pb.Session("cd_usuario").ToString();
                            t23.dt_limite = Convert.ToDateTime(txtdt_limiteProv.Text);
                            if (ddlt02_cd_usuario.SelectedValue != null)
                            {
                                t23.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
                            }
                            else
                            {
                                t23.t02_cd_usuario = pb.Session("cd_usuario").ToString();
                            }
                            new t23_providenciaAction().InsertDB(t23);
                        }

                        new t09_marcoAction().UpdateCorBarra(t07.t03_cd_projeto);

                        msg = pb.Message("Salvo com sucesso", "ok");
                        OcultarForm();
                        GridBind();
                        ViewState["cod"] = "0";
                    }
                }
                else
                {
                    msg = pb.Message("Selecione pelo menos um Marco Crítico", "erro");
                    //cadastro = false;
                    //OcultarForm();                
                    //GridBind();                
                    lblMsgPopUp.Text = msg;
                    lblMsgPopUp.Visible = true;
                    this.mdlPopup.Show();
                }

            }
            catch (Exception ex)
            {
                msg = pb.Message(ex.Message, "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }
    }


    #endregion
}
