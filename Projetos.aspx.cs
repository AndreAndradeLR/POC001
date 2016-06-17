using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

public partial class Projetos : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    { 
        ddlt01_cd_entidade_parc.PreRender += new EventHandler(ddl_PreRender);
        ddlt01_cd_entidade_resp.PreRender += new EventHandler(ddl_PreRender);
        ddlt21_cd_fase.PreRender += new EventHandler(ddl_PreRender);
        ddlt26_cd_arearesultado.PreRender += new EventHandler(ddl_PreRender);     

            if (!IsPostBack)
            {                
                FormBind();
                Filtro();                
            }
    }

    public string RemoveCaracter(string word)
    {

        string strcompare = word.ToLower();
        string str = "0123456789abcdefghijklmnopqrstwuvxyz";
        bool passou = false;
        for (int i = 0; i < str.Length; i++)
        {
            if (strcompare.Substring(0, 1) == str.Substring(i, 1))
            {
                passou = true;
                break;

            }

        }
        if (!passou)
        {
            word = word.Remove(0, 1);
        }
        return word;

    }

    void ddl_PreRender(object sender, EventArgs e)
    {
        DropDownList ddl = (DropDownList)sender;
        pb.AddColorLines(ddl, "#f7f9ee");
        
        if (ddl.SelectedValue != "")
        {
            ddl.Attributes.Add("style", "border: solid 1px #fa9600");
        }
        else
        {
            ddl.Attributes.Add("style", "border: solid 1px #999999");
        }

    }

    private void Filtro()
    {
        FiltroProjeto filtro = new FiltroProjeto();
        if (Session["filtro"] != null)
        {
            filtro = (FiltroProjeto)Session["filtro"];
            ListItem li = null;
            if (filtro.T01_cd_entidade_resp != null)
            {
                li = ddlt01_cd_entidade_resp.Items.FindByValue(filtro.T01_cd_entidade_resp.ToString());
                if (li != null)
                    li.Selected = true;
            }
            if (filtro.T01_cd_entidade_parc != null)
            {
                li = ddlt01_cd_entidade_parc.Items.FindByValue(filtro.T01_cd_entidade_parc.ToString());
                if (li != null)
                    li.Selected = true;
            }
            if (filtro.T21_cd_fase != null)
            {
                li = ddlt21_cd_fase.Items.FindByValue(filtro.T21_cd_fase.ToString());
                if (li != null)
                    li.Selected = true;
            }
            if (filtro.T26_cd_arearesultado != null)
            {
                li = ddlt26_cd_arearesultado.Items.FindByValue(filtro.T26_cd_arearesultado.ToString());
                if (li != null)
                    li.Selected = true;
            }
            
        }
        else
        {
            if (ddlt01_cd_entidade_resp.SelectedValue != "")
            {
                filtro.T01_cd_entidade_resp =
                    Convert.ToInt32(ddlt01_cd_entidade_resp.SelectedValue);
                Session["ddlt01_cd_entidade_resp"] = ddlt01_cd_entidade_resp.SelectedItem.Text;
            }
            else {
                Session["ddlt01_cd_entidade_resp"] = null;
            }
            if (ddlt01_cd_entidade_parc.SelectedValue != "")
            {
                filtro.T01_cd_entidade_parc =
                    Convert.ToInt32(ddlt01_cd_entidade_parc.SelectedValue);
                Session["ddlt01_cd_entidade_parc"] = ddlt01_cd_entidade_parc.SelectedItem.Text;
            }
            else {
                Session["ddlt01_cd_entidade_parc"] = null;
            }
            if (ddlt21_cd_fase.SelectedValue != "")
            {
                filtro.T21_cd_fase =
                    Convert.ToInt32(ddlt21_cd_fase.SelectedValue);
                Session["ddlt21_cd_fase"] = ddlt21_cd_fase.SelectedItem.Text;
            }
            else {
                Session["ddlt21_cd_fase"] = null;
            }
            if (ddlt26_cd_arearesultado.SelectedValue != "")
            {
                filtro.T26_cd_arearesultado =
                    Convert.ToInt32(ddlt26_cd_arearesultado.SelectedValue);
                Session["ddlt26_cd_arearesultado"] = ddlt26_cd_arearesultado.SelectedItem.Text;
            }
            else {
                Session["ddlt26_cd_arearesultado"] = null;
            }
            Session["filtro"] = filtro;
        }
        ShowFiltros();
        GridBind(filtro);
    }

    private void GridBind(FiltroProjeto filtro)
    {        
        if (Session["nome"] != null)
        {
            t03_projetoAction t03a = new t03_projetoAction();
            //verifica se ele é Administrador Geral ou Monitor
            if ((Convert.ToBoolean(pb.Session("fl_monitora"))) || 
                (Convert.ToBoolean(pb.Session("fl_admin"))) ||
                (Convert.ToBoolean(pb.Session("fl_visitante"))))
            {                
                GridView1.DataSource = t03a.ListTodos(filtro);
                GridView1.DataBind();
            }
            else
            {        
                //verifica se existe resultado para o filtro
                int n = t03a.ListTodos(filtro).Rows.Count;
                
                //verifica se ele é Gerente ou da mesma area de resultado.                
                DataTable dt = t03a.ListGerenteArea(filtro, Session["cd_usuario"].ToString());
                int m = dt.Rows.Count;

                if (n == 0)
                {
                    mdlPopup.Show();
                    lblMsgPopUp.Text = pb.Message("A seleção efetuada não possui informações. Tente novamente.", "erro");
                    //lblMsgPopUp.Text = pb.Message("Acesso restrito", "erro");
                    lblMsgPopUp.Visible = true;
                }
                else if (n > 0 && m > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();                        
                }
                else
                {
                    mdlPopup.Show();                    
                    lblMsgPopUp.Text = pb.Message("Acesso restrito", "erro");
                    lblMsgPopUp.Visible = true;
                }

                
            }
        }    
        //GridView1.DataSource = new t03_projetoAction().ListTodos(filtro);
        //GridView1.DataBind();
    }

    private void ShowFiltros()
    {
        StringBuilder sb = new StringBuilder();
        if (Session["ddlt26_cd_arearesultado"] != null)
        {
            sb.Append("<b>Área de Resultado:&nbsp;</b>" + Session["ddlt26_cd_arearesultado"]);
            sb.Append("<br>");
        }        

        if (Session["ddlt01_cd_entidade_resp"] != null)
        {
            sb.Append("<b>Entidade Responsável:&nbsp;</b> " + Session["ddlt01_cd_entidade_resp"]);
            sb.Append("<br>");
        }        

        if (Session["ddlt01_cd_entidade_parc"] != null)
        {
            sb.Append("<b>Entidade Parceira:&nbsp;</b>" + Session["ddlt01_cd_entidade_parc"]);
            sb.Append("<br>");
        }        

        if (Session["ddlt21_cd_fase"] != null)
        {
            sb.Append("<b>Fase do Projeto:&nbsp;</b>" + Session["ddlt21_cd_fase"]);
            sb.Append("<br>");
        }
        if ((Session["ddlt26_cd_arearesultado"] == null) &&
            (Session["ddlt01_cd_entidade_resp"] == null) &&
            (Session["ddlt01_cd_entidade_parc"] == null) &&
            (Session["ddlt21_cd_fase"] == null))
        {
            sb.Append("<b>Filtro:</b>&nbsp;Todos");
        }

        litFiltros.Text = sb.ToString();

    }

    private void FormBind()
    {
        DropDownList ddl;

        t21_faseAction t21a = new t21_faseAction();
        ddl = ddlt21_cd_fase;
        ddl.DataSource = t21a.ListTodos();
        ddl.DataTextField = "nm_fase";
        ddl.DataValueField = "t21_cd_fase";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");

        ddl = ddlt01_cd_entidade_parc;
        t01_entidadeAction t01a = new t01_entidadeAction();
        ddl.DataSource = t01a.ListTodos();
        ddl.DataTextField = "nm_entidade";
        ddl.DataValueField = "t01_cd_entidade";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");

        ddl = ddlt01_cd_entidade_resp;
        ddl.DataSource = ddlt01_cd_entidade_parc.DataSource;
        ddl.DataTextField = "nm_entidade";
        ddl.DataValueField = "t01_cd_entidade";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");

        //--- Área de Resultado ---//
        ddl = ddlt26_cd_arearesultado;
        ddl.DataSource = new t26_arearesultadoAction().ListTodos();
        ddl.DataTextField = "nm_area";
        ddl.DataValueField = "t26_cd_arearesultado";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Todas");
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        Session["filtro"] = null;
        Filtro();
        if (GridView1.Rows.Count == 0)
        {
            mdlPopup.Show();
            lblMsgPopUp.Text = pb.Message("A seleção efetuada não possui informações. Tente novamente.", "erro");
            //lblMsgPopUp.Text = pb.Message("Acesso restrito", "erro");
            lblMsgPopUp.Visible = true;
        }
    }

    protected void btnTodos_Click(object sender, EventArgs e)
    {        
        ddlt01_cd_entidade_parc.ClearSelection();
        ddlt01_cd_entidade_resp.ClearSelection();
        ddlt21_cd_fase.ClearSelection();
        ddlt26_cd_arearesultado.ClearSelection();
        Session["ddlt01_cd_entidade_resp"] = null;
        Session["ddlt01_cd_entidade_parc"] = null;
        Session["ddlt21_cd_fase"] = null;
        Session["ddlt26_cd_arearesultado"] = null;
        Session["filtro"] = null;
        Filtro();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {               
                //formata caracter do projeto
                LinkButton lbt = (LinkButton)e.Row.Cells[0].Controls[0];
                if (lbt != null)
                {
                    lbt.Text = RemoveCaracter(lbt.Text);
                }

                System.Data.DataRowView drv = ((System.Data.DataRowView)e.Row.DataItem);
                t03_projeto t03 = (new t03_projetoAction().Retrieve(Convert.ToInt32(drv["t03_cd_projeto"])));
                TableCell tc = null;

                //Fase
                tc = e.Row.Cells[1];
                if (tc != null)
                {
                    t21_fase t21 = (new t21_faseAction().Retrieve(
                        new t22_faseprojetoAction().Retrieve(t03.t03_cd_projeto).t21_cd_fase));
                    tc.Text = t21.nm_fase;
                }

                //Atualizado
                tc = e.Row.Cells[2];
                if (tc != null)
                {
                    if (t03.dt_atualizado.Year > 1)
                    {
                        tc.Text = t03.dt_atualizado.ToShortDateString();
                    }
                    else
                    {
                        tc.Text = "-";
                    }
                }

                //Período
                tc = e.Row.Cells[3];
                if (tc != null)
                {
                    if (t03.dt_inicio.Year > 1)
                    {
                        tc.Text = t03.dt_inicio.Year + "-" + t03.dt_fim.Year;
                    }
                    else
                    {
                        tc.Text = "-";
                    }
                }

                //Restrição
                tc = e.Row.Cells[4];
                if (tc != null)
                {
                    if (new t07_restricaoAction().ListTodos(t03.t03_cd_projeto).Rows.Count > 0)
                    {
                        tc.Controls[0].Visible = true;
                        tc.ToolTip = "Projeto possui uma ou mais restrições";
                        //tc.Text = pb.GetMyFlash(18, 18, "restricao", "restricao", "charts/restricao.swf");
                    }
                    else
                    {
                        tc.Controls[0].Visible = false;
                    }
                }

                //Status
                tc = e.Row.Cells[5];
                if (tc != null)
                {
                    tc.Text = new t09_marcoAction().Status(t03.t03_cd_projeto);
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RowDataBound: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                int cod = Int32.Parse(gv.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());

                switch (e.CommandName)
                {
                    case "Arvore":
                        Session["cd_projeto"] = cod;
                        Response.Redirect("Arvore.aspx");
                        break;
                    case "Restricao":
                        //condição visitante
                        if (!Convert.ToBoolean(pb.Session("fl_visitante")))
                        {
                            Session["cd_projeto"] = cod;
                            if (cod > 0)
                            {
                                t03_projeto t03 = new t03_projeto();
                                t03_projetoAction t03a = new t03_projetoAction();
                                t03 = t03a.Retrieve(Convert.ToInt32(Session["cd_projeto"]));
                                Session["nm_projeto"] = t03.nm_projeto;
                                Session["cd_area"] = t03.t26_cd_arearesultado;
                                Session["nm_area"] = t03.t26.nm_area;
                                Session["nm_arquivo"] = t03.t26.nm_arquivo;
                                Session["dt_inicio"] = t03.dt_inicio;
                                Session["dt_fim"] = t03.dt_fim;
                            }
                            Response.Redirect("Detalhamento.aspx");
                        }
                        else {
                            Response.Redirect("Projetos.aspx");
                        }
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

    protected void GridView1_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            gv.UseAccessibleHeader = true;
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv.FooterRow.TableSection = TableRowSection.TableFooter;

            JQuery jquery = new JQuery();
            jquery.SortList = "0, 0";
            jquery.tableID = gv.ClientID;
            jquery.TableSorter();

            Literal lit = (Literal)gv.Parent.FindControl("Literal" + gv.ID);
            if (lit != null)
                lit.Text = jquery.SearchHtml();

            
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
}
