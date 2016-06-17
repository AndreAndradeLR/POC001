using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rel_Marco : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(IsPostBack))
        {
            PopulaDDls();
        }
    }    

    public void GridBind(List<String> lstFiltros)
    {
        rptProjetos.DataSource = new t03_projetoAction().ListTodosProjetosComMarcos(lstFiltros);
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

            //Recupera MarcosCríticos do projeto
            GridView1.DataSource = new t09_marcoAction().ListRestricoesDoMarco(t03.t03_cd_projeto, ddStatus.SelectedValue);
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

                //Status
                tc = e.Row.Cells[0];
                if (tc != null)
                {
                    tc.Text = "<img src='images/" + drv["fl_status"].ToString() + ".gif'>";
                    tc.Text += "<span style='display:none;'>" + drv["t09_cd_marco"] + "</span>";
                    //tc.Text = pb.RestricaoRelacionadoMarco(new t07_restricaoAction().ListRestricoesDoMarco(Convert.ToInt32(drv["t09_cd_marco"])));
                }

                //Descrição Restrição
                tc = e.Row.Cells[1];
                if (tc != null)
                {
                    tc.Text = drv["Descricao"].ToString();
                    tc.Text += "<span style='display:none;'>" + drv["t09_cd_marco"] + "</span>";
                }

                //Data Inclusão Restrição
                tc = e.Row.Cells[2];
                if (tc != null)
                {
                    tc.Text = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(drv["dt_prevista"]));
                    tc.Text += "<span style='display:none;'>" + drv["t09_cd_marco"] + "</span>";
                }

                //Responsável
                tc = e.Row.Cells[3];
                if (tc != null)
                {
                    //tc.Text = drv["ds_restricao"].ToString();
                    tc.Text = pb.RestricaoRelacionadoMarco(new t07_restricaoAction().ListRestricoesdoMarco(Convert.ToInt32(drv["t03_cd_projeto"]), Convert.ToInt32(drv["t09_cd_marco"])));
                    tc.Text += "<span style='display:none;'>" + drv["t09_cd_marco"] + "</span>";                    
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
                if (row.Cells[i].Text == previousRow.Cells[i].Text)
                {
                    row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                           previousRow.Cells[i].RowSpan + 1;
                    previousRow.Cells[i].Visible = false;
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

    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            if (ddl.SelectedIndex > 0)
            {
                //---- Projeto ----//                                    
                ddlProjeto.ClearSelection();
                ddlProjeto.DataSource = new t03_projetoAction().ListTodosDaProjetosAreaResultado(Convert.ToInt32(ddl.SelectedValue));
                ddlProjeto.DataTextField = "nm_projeto";
                ddlProjeto.DataValueField = "t03_cd_projeto";
                ddlProjeto.DataBind();
                pb.AddEmptyItem(ddlProjeto, "Todos");
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
            DropDownList ddl = (DropDownList)sender;
            //--- Área de Resultado ---//
            if (ddlArea.SelectedValue == "")
            {
                //zera ddl
                ddlArea.DataSource = null;
                ddlArea.DataBind();

                //prepara para popular
                ddlArea.ClearSelection();
                ddlArea.DataSource = new t26_arearesultadoAction().ListTodos();
                ddlArea.DataTextField = "nm_area";
                ddlArea.DataValueField = "t26_cd_arearesultado";
                ddlArea.DataBind();
                pb.AddEmptyItem(ddlArea, "Todas");

                int i = new t26_arearesultadoAction().ListTodosDoProjeto(Convert.ToInt32(ddl.SelectedValue));
                ListItem li = ddlArea.Items.FindByValue(i.ToString());
                if (li != null)
                    li.Selected = true;
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
        lstFiltros.Add(ddStatus.SelectedValue);//Filtro Status        
        Session["lstFiltros"] = lstFiltros;

        //passa filtro selecionado
        GridBind(lstFiltros);

    }

}
