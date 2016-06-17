using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rel_Restricao : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulaDDls();
        }
    }

    public void GridBind(List<string> lstFiltros)
    {
        //verifica se restrição está superada              
        rptProjetos.DataSource = new t03_projetoAction().ListTodosProjetosRelRestricoes(lstFiltros);
        rptProjetos.DataBind();

        if (rptProjetos.Items.Count == 0)
            lblEmptyData.Visible = true;
        lblEmptyData.Text = "Não Há Registro Encontrado.";
    }

    public void PopulaDDls()
    {

        DropDownList ddl;
        //--- Área de Resultado ---//
        ddl = ddlArea;
        ddl.DataSource = new t26_arearesultadoAction().ListTodos();
        ddl.DataTextField = "nm_area";
        ddl.DataValueField = "t26_cd_arearesultado";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");

        //---- Projeto ----//
        ddl = ddlProjeto;
        ddl.DataSource = new t03_projetoAction().ListTodos();
        ddl.DataTextField = "nm_projeto";
        ddl.DataValueField = "t03_cd_projeto";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todos");

        //---- Responsavel ----//
        ddl = ddlResponsavel;
        ddl.DataSource = new t02_usuarioAction().ListGerenteDoProjeto(cd_projeto: 0);
        ddl.DataTextField = "nm_nome";
        ddl.DataValueField = "t02_cd_usuario";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todos");
    }

    public void rptProjetos_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
          (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Literal ltrAreaResultado = (Literal)e.Item.FindControl("ltrAreaResultado");
            Literal ltrDataImpressao = (Literal)e.Item.FindControl("ltrDataImpressao");
            Literal ltrProjetoSustentador = (Literal)e.Item.FindControl("ltrProjetoSustentador");
            Literal ltrGerente = (Literal)e.Item.FindControl("ltrGerente");
            Repeater rptRestricoes = (Repeater)e.Item.FindControl("rptRestricoes");
            GridView GridView1 = (GridView)e.Item.FindControl("GridView1");

            //t03_projeto t03 = new t03_projetoAction().Retrieve(Convert.ToInt32(hddCod.Value));            
            t03_projeto t03 = (t03_projeto)e.Item.DataItem;
            //area de resultado
            t26_arearesultado t26 = new t26_arearesultadoAction().Retrieve(t03.t26_cd_arearesultado);
            ltrAreaResultado.Text = t26.nm_area;
            //data impressão
            ltrDataImpressao.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
            //nome do projeto
            ltrProjetoSustentador.Text = t03.nm_projeto;
            //gerente do projeto
            ltrGerente.Text = new t02_usuarioAction().Retrieve(t03.t02_cd_usuario).nm_nome;

            //carrega filtros
            List<String> lstFiltros = (List<String>)Session["lstFiltros"];

            //carrega grid
            GridView1.DataSource = new t07_restricaoAction().ListProvidenciasRestricoes(t03.t03_cd_projeto, lstFiltros);
            GridView1.DataBind();

            //retira colunas duplicadas
            MergeRows(GridView1);

        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Data.DataRowView drv = ((System.Data.DataRowView)e.Row.DataItem);
                TableCell tc = null;

                //Descrição Restrição
                tc = e.Row.Cells[0];
                if (tc != null)
                {
                    //adiciona imagem para restrições superadas
                    if (drv["dt_superada"] != DBNull.Value)
                    {
                        //texto da restrição
                        string img = "<img src='images/ok.gif' id='" + drv["t07_cd_restricao"].ToString() + "'/>";
                        tc.Text = img + "&nbsp;<span style='color:green'>Superada</span><br />" + drv["ds_restricao"].ToString();
                    }
                    else
                    {
                        //texto da restrição
                        tc.Text = drv["ds_restricao"].ToString();
                    }
                }

                //Data Inclusão Restrição
                tc = e.Row.Cells[1];
                if (tc != null)
                {
                    if (drv["dt_cadastro1"] != DBNull.Value)
                    {
                        tc.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(drv["dt_cadastro1"]));
                        tc.Text += "<span style='display:none;'>" + drv["t07_cd_restricao"] + "</span>";
                    }
                }

                //Data Limite Restrição
                tc = e.Row.Cells[2];
                if (tc != null)
                {
                    if (drv["dt_limite1"] != DBNull.Value)
                    {
                        tc.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(drv["dt_limite1"]));
                        tc.Text += "<span style='display:none;'>" + drv["t07_cd_restricao"] + "</span>";
                    }
                }

                //Descrção Providência
                tc = e.Row.Cells[3];
                if (tc != null)
                {
                    tc.Text = drv["ds_providencia"].ToString();
                }

                //Responsável
                tc = e.Row.Cells[4];
                if (tc != null)
                {
                    if (drv["t02_cd_usuario"] != DBNull.Value)
                        tc.Text = pb.dadosUsuario(new t02_usuarioAction().Retrieve(drv["t02_cd_usuario"].ToString()), Convert.ToInt32(drv["t23_cd_providencia"]), "Providencia");
                    else
                        tc.Text = "";
                }

                //Data Limite da Providencia
                tc = e.Row.Cells[5];
                if (tc != null)
                {
                    if (drv["dt_limite"] != DBNull.Value)
                        tc.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(drv["dt_limite"]));
                }

                //Responsável
                tc = e.Row.Cells[6];
                if (tc != null)
                {
                    tc.Text = pb.MarcoRelacionadoRestricao(new t09_marcoAction().ListMarcosDaRestricao(Convert.ToInt32(drv["t07_cd_restricao"])));
                    tc.Text += "<span style='display:none;'>" + drv["t07_cd_restricao"] + "</span>";
                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowDataBound: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    public static void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (i == 0 | i == 1 | i == 2 | i == 6)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
            }
        }
    }

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            gv.UseAccessibleHeader = true;
            gv.FooterRow.Visible = false;

        }
    }

    public void rptRestricoes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
          (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Literal ltrRestricao = (Literal)e.Item.FindControl("ltrRestricao");
            Literal ltrDataInclusao = (Literal)e.Item.FindControl("ltrDataInclusao");
            Literal ltrDataLimite = (Literal)e.Item.FindControl("ltrDataLimite");
            Repeater rptProvidencia = (Repeater)e.Item.FindControl("rptProvidencia");
            //Literal ltrMarcosCriticos = (Literal)e.Item.FindControl("ltrMarcosCriticos");            
            //Literal ltrMarcosCriticos = rptProvidencia.Controls[rptProvidencia.Controls.Count - 1].FindControl("ltrMarcosCriticos") as Literal;

            t07_restricao t07 = (t07_restricao)e.Item.DataItem;
            //descrição restrição
            ltrRestricao.Text = t07.ds_restricao;
            ltrDataInclusao.Text = String.Format("{0:dd/MM/yyyy}", t07.dt_cadastro);
            ltrDataLimite.Text = String.Format("{0:dd/MM/yyyy}", t07.dt_limite);
            //ltrMarcosCriticos.Text = pb.MarcoRelacionadoRestricao(new t09_marcoAction().ListMarcosDaRestricao(t07.t07_cd_restricao));            
            //ltrMarcosCriticos.Text = "rwe";            

            //dados da providencia
            rptProvidencia.DataSource = new t23_providenciaAction().ListObjTodos(t07.t07_cd_restricao);
            rptProvidencia.DataBind();

        }
    }

    public void rptProvidencia_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
          (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Literal ltrProvidencia = (Literal)e.Item.FindControl("ltrProvidencia");
            Literal ltrResponsavel = (Literal)e.Item.FindControl("ltrResponsavel");
            Literal ltrDataLimiteProv = (Literal)e.Item.FindControl("ltrDataLimiteProv");

            t23_providencia t23 = (t23_providencia)e.Item.DataItem;

            ltrProvidencia.Text = t23.ds_providencia;
            ltrResponsavel.Text = pb.dadosUsuario(new t02_usuarioAction().Retrieve(t23.t02_cd_usuario), t23.t23_cd_providencia, "Providencia");
            ltrDataLimiteProv.Text = String.Format("{0:dd/MM/yyyy}", t23.dt_limite);

        }
    }

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //zera ddl
            ddlResponsavel.DataSource = null;
            ddlResponsavel.DataBind();
            
            ddlProjeto.DataSource = null;
            ddlProjeto.DataBind();

            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedIndex > 0)
            {
                int cd_area = 0;
                int.TryParse(ddl.SelectedValue, out cd_area);

                //---- Projeto ----//                                    
                ddlProjeto.ClearSelection();
                ddlProjeto.DataSource = new t03_projetoAction().ListTodosDaProjetosAreaResultado(cd_area);
                ddlProjeto.DataTextField = "nm_projeto";
                ddlProjeto.DataValueField = "t03_cd_projeto";
                ddlProjeto.DataBind();
                pb.AddEmptyItem(ddlProjeto, "Todos");

                //---- Responsavel ----//                
                ddlResponsavel.DataSource = new t06_colaboradorAction().ListResponsavel();
                ddlResponsavel.DataTextField = "nm_nome";
                ddlResponsavel.DataValueField = "t02_cd_usuario";
                ddlResponsavel.DataBind();
                pb.AddEmptyItem(ddlResponsavel, "Todos");
            }
            else
            {
                //---- Lista todos os Projeto ----//   
                ddlProjeto.ClearSelection();
                ddlProjeto.DataSource = new t03_projetoAction().ListTodos();
                ddlProjeto.DataTextField = "nm_projeto";
                ddlProjeto.DataValueField = "t03_cd_projeto";
                ddlProjeto.DataBind();
                pb.AddEmptyItem(ddlProjeto, "Todos");

                //---- Responsavel ----//                
                ddlResponsavel.DataSource = new t06_colaboradorAction().ListResponsavel();
                ddlResponsavel.DataTextField = "nm_nome";
                ddlResponsavel.DataValueField = "t02_cd_usuario";
                ddlResponsavel.DataBind();
                pb.AddEmptyItem(ddlResponsavel, "Todos");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("ddlArea_SelectedIndexChanged: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    protected void ddlProjeto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int cd_projeto = 0;

            //zera ddl
            ddlResponsavel.DataSource = null;
            ddlResponsavel.DataBind();

            DropDownList ddl = (DropDownList)sender;
            //--- Área de Resultado ---//
            if (ddl.SelectedIndex > 0)
            {                
                int.TryParse(ddl.SelectedValue, out cd_projeto);

                //---- Responsavel ----//
                ddlResponsavel.ClearSelection();
                ddlResponsavel.DataSource = new t02_usuarioAction().ListGerenteDoProjeto(cd_projeto: cd_projeto);
                ddlResponsavel.DataTextField = "nm_nome";
                ddlResponsavel.DataValueField = "t02_cd_usuario";
                ddlResponsavel.DataBind();
                pb.AddEmptyItem(ddlResponsavel, "Todos");
            }
            else
            {
                //---- Responsavel ----//
                ddlResponsavel.DataSource = new t02_usuarioAction().ListGerenteDoProjeto(cd_projeto: cd_projeto);
                ddlResponsavel.DataTextField = "nm_nome";
                ddlResponsavel.DataValueField = "t02_cd_usuario";
                ddlResponsavel.DataBind();
                pb.AddEmptyItem(ddlResponsavel, "Todos");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("ddlProjeto_SelectedIndexChanged: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    protected void ddlResponsavel_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedValue != "")
            {
                //zera ddl
                ddlArea.DataSource = null;
                ddlArea.DataBind();

                //--- Área de Resultado ---//
                ddlArea.DataSource = new t26_arearesultadoAction().ListTodosDoResponsavel(ddl.SelectedValue);
                ddlArea.DataTextField = "nm_area";
                ddlArea.DataValueField = "t26_cd_arearesultado";
                ddlArea.DataBind();
                pb.AddEmptyItem(ddlArea, "Todas");


                //zera ddl
                ddlProjeto.DataSource = null;
                ddlProjeto.DataBind();

                //---- Projeto ----//
                ddlProjeto.DataSource = new t03_projetoAction().ListTodosDoResponsavel(ddl.SelectedValue);
                ddlProjeto.DataTextField = "nm_projeto";
                ddlProjeto.DataValueField = "t03_cd_projeto";
                ddlProjeto.DataBind();
                pb.AddEmptyItem(ddlProjeto, "Todos");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("ddlProjeto_SelectedIndexChanged: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        //cria uma lista de filtros
        List<String> lstFiltros = new List<String>();
        lstFiltros.Add(ddlArea.SelectedValue);//Filtro Área Resultado
        lstFiltros.Add(ddlProjeto.SelectedValue);//Filtro Projeto
        lstFiltros.Add(ddlData.SelectedValue);//Filtro Data
        lstFiltros.Add(ddlResponsavel.SelectedValue);//Filtro Responsável
        lstFiltros.Add(rblEstado.SelectedValue);//Filtro Estado
        Session["lstFiltros"] = lstFiltros;

        //passa filtro selecionado
        GridBind(lstFiltros);

    }
}