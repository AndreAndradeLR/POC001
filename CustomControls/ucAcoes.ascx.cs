using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class CustomControls_ucAcoes : System.Web.UI.UserControl, ICrud
{
    pageBase pb = new pageBase();
   
    public void Page_Load(object sender, EventArgs e)
    {        

        if (!IsPostBack)
        {
            FormBind();
            GridBind();
            ViewState["cod"] = "0";

            if (pb.fl_gerente())
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

    private string GetXmlValue(t08_acao t08)
    {
        int i;
        decimal fisico = 0;
        decimal financeiro = 0;
        double difdias = 0;
        double difhoje = 0;
        double crono = 0;

        /*
         * TEMPO
         */
        difdias = t08.dt_fim.Subtract(t08.dt_inicio).Days;
        difhoje = t08.dt_fim.Subtract(DateTime.Now).Days;

        if (DateTime.Now.Date > t08.dt_inicio.Date)
        {
            crono = (((difhoje / difdias) * 100) - 100) * -1;
            if (crono < 0)
            {
                crono = 0;
            }
            else if (crono > 100)
            {
                crono = 100;
            }
        }
        else
        {
            crono = 0;
        }

        /*
         * FÍSICO
         */
        i = 0;

        foreach (t10_produto t10 in new t10_produtoAction().ListObjTodos(t08.t08_cd_acao))
        {
            i++;
            decimal tprev = 0;
            decimal treal = 0;
            foreach (t17_vlproduto t17 in t10.t17)
            {
                tprev += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                         t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                treal += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                     t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
            }
            if (tprev > 0)
            {
                if (treal > 0)
                {
                    fisico += ((treal / tprev) * 100);
                }
                else
                {
                    fisico += 0;
                }
            }
            else
            {
                fisico += 0;
            }
        }
        if (i > 0) fisico = fisico / i;

        if (fisico > 100)
        {
            fisico = 100;
        }
        else if (fisico < 0)
        {
            fisico = 0;
        }
        
        foreach (t18b_vlfinanceiro t18 in new t18b_vlfinanceiroAction().ListObjTotal(t08.t08_cd_acao))
        {
            if (t18.vl_liquidado1 > 0)
            {
                financeiro = (t18.vl_liquidado1 / t18.vl_planejado1) * 100;
            }
            break;
        }     
       

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<graph chartRightMargin='30' numberSuffix='%25' " +
            "yAxisMaxValue='100'  showAlternateVGridColor='1' "+
            "alternateVGridAlpha='10' alternateVGridColor='AFD8F8' "+
            " numDivLines='4' decimalPrecision='0' canvasBorderThickness='1' "+
            "canvasBorderColor='114B78' baseFontColor='114B78' "+
            "hoverCapBorderColor='114B78' hoverCapBgColor='E7EFF6'>");
        sb.Append("<set name='Tempo' value='" + crono.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        if (fisico > 0)
        {
            sb.Append("<set name='Físico' value='" + fisico.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
        }
        else
        {
            sb.Append("<set name='Físico' color='AFD8F8' alpha='70'/>");
        }
        if (ShowFinanceiro())
        {
            if (financeiro > 0)
            {
                sb.Append("<set name='Financeiro' value='" + financeiro.ToString().Replace(",", ".") + "' color='AFD8F8' alpha='70'/> ");
            }
            else
            {
                sb.Append("<set name='Financeiro' color='AFD8F8' alpha='70'/>");
            }
        }
        sb.Append("</graph>");
        return sb.ToString();
    }

    #region ICrud Members

    public void ExibirForm()
    {
        this.mdlPopup.Show();
    }

    public void FormBind()
    {
        DropDownList ddl = ddlt02_cd_usuario;
        ddl = ddlt02_cd_usuario;
        ddl.DataSource = new t02_usuarioAction().ListTodos();
        ddl.DataTextField = "nm_nome";
        ddl.DataValueField = "t02_cd_usuario";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Selecione");

    }

    public void GridBind()
    {

        GridView1.DataSource = new t08_acaoAction().ListTodos(
            Convert.ToInt32(pb.Session("cd_projeto")));
        GridView1.DataBind();
    }

    public void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));
            JQuery jquery = new JQuery();
            if (pb.fl_gerente())
            {
                if (t21.t21_cd_fase == 2)
                {
                    jquery.SortList = "3, 0";
                }
                else
                {
                    jquery.SortList = "5, 0";
                    jquery.Headers = "0: {sorter: false}, 1: {sorter: false}";
                }

            }
            else
            {
                jquery.SortList = "3, 0";
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
                g=2;
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
                        break;
                    case "Deletar":
                        new t08_acaoAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        break;
                    case "Selecionar":
                        Session["cd_acao"] = ViewState["cod"];
                        Response.Redirect("AcaoDetalhes.aspx");
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

                t02_usuario t02 = new t02_usuarioAction().Retrieve(drv["t02_cd_usuario"].ToString());
                if (e.Row.Cells[4] != null)
                {
                    e.Row.Cells[4].Text = pb.dadosUsuario(t02, e.Row.RowIndex);
                }

                /*
                if (e.Row.Cells[5] != null)
                {
                    e.Row.Cells[5].Text = "<input id=\"Hidden" + e.Row.RowIndex + "\" type=\"text\" value=\"" + String.Format("{0:dd/MM/yyyy}", drv["dt_inicio"]) + "\" />";
                    e.Row.Cells[5].Text = "Início: " +String.Format("{0:dd/MM/yyyy}", drv["dt_inicio"]) + 
                        " <br /> Fim: "+ String.Format("{0:dd/MM/yyyy}", drv["dt_fim"]);
                }
                */


                if (e.Row.Cells[8] != null)
                {
                    t08_acao t08 = new t08_acao();
                    t08.t08_cd_acao = Convert.ToInt32(drv["t08_cd_acao"]);
                    t08.nm_acao = drv["nm_acao"].ToString();
                    t08.dt_inicio = Convert.ToDateTime(drv["dt_inicio"]);
                    t08.dt_fim= Convert.ToDateTime(drv["dt_fim"]);
                    
                    if (drv["ds_acao"]!=null)
                        t08.ds_acao = drv["ds_acao"].ToString();

                    e.Row.Cells[8].Text = "<div style=\"border:solid 1px #CCC\">"+ 
                        pb.GetFlash(90, 250, "Charts/FC_2_3_Bar2D.swf", GetXmlValue(t08), 0, "acao"+e.Row.RowIndex)+
                        "</div>";
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
        txtnm_acao.Text = "";
        txtds_acao.Text = "";
        txtdt_inicio.Text = "";
        txtdt_fim.Text = "";
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
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtnm_acao.Text = t08.nm_acao;
        txtds_acao.Text = t08.ds_acao;
        txtdt_inicio.Text = t08.dt_inicio.ToShortDateString();
        txtdt_fim.Text = t08.dt_fim.ToShortDateString();
        ListItem li= ddlt02_cd_usuario.Items.FindByValue(t08.t02_cd_usuario);
        if (li != null) li.Selected = true;
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
        StringBuilder sb = new StringBuilder();
        int vl_produto = 0;
        int vl_financeiro = 0;
        decimal total = 0;

        bool erro = false;
        int result = 0;
        string msg = "";
        try
        {
            t03_projeto t03 = new t03_projeto();            
            t03.dt_inicio = Convert.ToDateTime(pb.Session("dt_inicio", DateTime.Now));
            t03.dt_fim = Convert.ToDateTime(pb.Session("dt_fim", DateTime.Now));                      

            t08_acao t08 = new t08_acao();
            t08_acaoAction t08a = new t08_acaoAction();

            t08.t08_cd_acao = Convert.ToInt32(ViewState["cod"]);
            t08.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));
            t08.nm_acao = txtnm_acao.Text;
            t08.ds_acao = txtds_acao.Text;
            t08.dt_inicio = Convert.ToDateTime(txtdt_inicio.Text);
            t08.dt_fim = Convert.ToDateTime(txtdt_fim.Text);
            t08.t02_cd_usuario = ddlt02_cd_usuario.SelectedValue;
            int dt_begin = t08.dt_inicio.Year;
            int dt_end = t08.dt_fim.Year; 

            if ((t08.dt_inicio < t03.dt_inicio) || (t08.dt_fim > t03.dt_fim))
            {
                erro = true;
                sb.Append("As datas de início e término da Ação deve estar entre as " +
                    "datas de início (" + t03.dt_inicio.ToShortDateString() + ") " +
                    "e término (" + t03.dt_fim.ToShortDateString() + ") do Projeto!");
            }
            else if (t08.t08_cd_acao > 0)
            {
                sb.Append("Não foi possível alterar data Início/Fim:<br>");
                    DataTable dt = t08a.ListSomaValoresProduto(dt_begin, dt_end, Convert.ToInt32(ViewState["cod"]));
                    foreach (DataRow dr in dt.Rows)
                    {
                        double vl = 0;
                        if (dr["valor"] != DBNull.Value)
                            vl = Convert.ToDouble(dr["valor"]);
                        if (vl != 0)
                        {
                            erro = true;
                            sb.Append("&nbsp;- Meta física: valores em anos fora do período selecionado, antes de continuar apague os valores.<br>");
                            //sb.Append(pb.Message("Não é possivel atualizar data. Data da Meta física, fora do range da Ação.<br>", "erro"));
                        }
                        break;
                    }

                    dt = t08a.ListSomaValoresFinanceiro(dt_begin, dt_end, Convert.ToInt32(ViewState["cod"]));
                    foreach (DataRow dr in dt.Rows)
                    {
                        double vl = 0;
                        if (dr["vlt_ano"] != DBNull.Value)
                            vl = Convert.ToDouble(dr["vlt_ano"]);
                        if (vl != 0)
                        {
                            erro = true;
                            sb.Append("&nbsp;- Financeiro: valores em anos fora do período selecionado, antes de continuar apague os valores.");
                            //sb.Append(pb.Message("Não é possivel atualizar data. Data do Financeiro fora do range da Ação.<br>", "erro"));
                        }
                        break;
                    }
            }


            if (erro == false)
            {
                if (t08.t08_cd_acao > 0)
                {
                    result = t08a.UpdateDB(t08);                                               
                }
                else
                {                    
                    result = t08a.InsertDB(t08);
                }
            }
            else
            {
                ExibirForm();
                msg = pb.Message(sb.ToString(),"erro");
                lblMsgPopUp.Text = msg;
                lblMsgPopUp.Visible = true;
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

    private bool ShowFinanceiro()
    {
        bool resp = false;
        //chama o metodo q mostra financeiro
        if ((Session["cd_usuario"] != null) && (Session["cd_area"] != null))
        {
            bool result = false;
            result = pb.RestricaoFinanceiro(Session["cd_usuario"].ToString(), Convert.ToInt32(Session["cd_area"]));
            if (result)
            {
                //exibe financeiro
                resp = true;
                linkFin.Visible = true;
            }
            else {
                linkFin.Visible = false;
            }            
        }
        return resp;
    }


    #endregion
}
