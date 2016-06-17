using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RelAcoes : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulaDDls();
        }
    }

    public void GridBind(List<String> lstFiltros)
    {
        rptProjetos.DataSource = new t03_projetoAction().ListTodosProjetosComAcoes(lstFiltros);
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
        ddl.DataSource = new t02_usuarioAction().ListResponsavelDaAcao(cd_projeto: 0);
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
            Repeater rptAcoes = (Repeater)e.Item.FindControl("rptAcoes");

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

            //acoes
            rptAcoes.DataSource = new t08_acaoAction().ListTodasAcoesComMeta(t03.t03_cd_projeto, filtro());
            rptAcoes.DataBind();

        }
    }

    public void rptAcoes_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
          (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Literal ltrAcao = (Literal)e.Item.FindControl("ltrAcao");
            Literal ltrDataPrevTermino = (Literal)e.Item.FindControl("ltrDataPrevTermino");
            Repeater rptMetas = (Repeater)e.Item.FindControl("rptMetas");

            t08_acao t08 = (t08_acao)e.Item.DataItem;
            ltrAcao.Text = t08.nm_acao;
            ltrDataPrevTermino.Text = t08.nm_acao;
            ltrDataPrevTermino.Text = String.Format("{0:dd/MM/yyyy}", t08.dt_fim);

            //recupera metas fisicas
            rptMetas.DataSource = new t10_produtoAction().ListObjTodosMetas(t08.t08_cd_acao);
            rptMetas.DataBind();

            Panel pnlMetaFisica = (Panel)e.Item.FindControl("pnlMetaFisica");
            pnlMetaFisica.Visible = rptMetas.Items.Count == 0;
        }
    }

    public void rptMetas_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
          (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Literal ltrDescricaoMeta = (Literal)e.Item.FindControl("ltrDescricaoMeta");
            Literal ltrPorcento = (Literal)e.Item.FindControl("ltrPorcento");

            t10_produto t10 = (t10_produto)e.Item.DataItem;

            ltrDescricaoMeta.Text = String.Format("{0:dd/MM/yyyy}", t10.ds_produto);
            //calculo de Realizado / Previsto
            decimal totvl_p = 0;
            decimal totvl_r = 0;
            decimal vlTotalMesAtual = 0;

            foreach (t17_vlproduto t17 in new t10_produtoAction().Retrieve(Convert.ToInt32(t10.t10_cd_produto)).t17)
            {
                totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 +
            t17.vl_p6 + t17.vl_p7 + t17.vl_p8 + t17.vl_p9 +
            t17.vl_p10 + t17.vl_p11 + t17.vl_p12);

                totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 +
            t17.vl_r6 + t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 +
            t17.vl_r11 + t17.vl_r12);
            }

            if ((totvl_p == 0) && (totvl_r != 0))
            {
                vlTotalMesAtual = 100;
            }
            else
            {
                if (totvl_p > 0)
                {
                    vlTotalMesAtual = (totvl_r / totvl_p) * 100;
                }
                else
                {
                    vlTotalMesAtual = 0;
                }
            }
            ltrPorcento.Text = vlTotalMesAtual.ToString("N2");

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
                ddlResponsavel.DataSource = new t02_usuarioAction().ListResponsavelDaAcao(cd_projeto: cd_projeto);
                ddlResponsavel.DataTextField = "nm_nome";
                ddlResponsavel.DataValueField = "t02_cd_usuario";
                ddlResponsavel.DataBind();
                pb.AddEmptyItem(ddlResponsavel, "Todos");
            }
            else
            {
                //---- Responsavel ----//
                ddlResponsavel.DataSource = new t02_usuarioAction().ListResponsavelDaAcao(cd_projeto: cd_projeto);
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
        //passa filtro selecionado
        GridBind(filtro());

    }

    private List<string> filtro()
    {
        //cria uma lista de filtros
        List<String> filtro = new List<String>();
        filtro.Add(ddlArea.SelectedValue);//Filtro Área Resultado
        filtro.Add(ddlProjeto.SelectedValue);//Filtro Projeto                        
        filtro.Add(ddlResponsavel.SelectedValue);//Filtro Responsável
        return filtro;
    }
}
