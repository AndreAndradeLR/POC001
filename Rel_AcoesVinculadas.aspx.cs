using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class RelAcoesVinculadas : System.Web.UI.Page
{
    pageBase pb = new pageBase();
    string[] month = { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopulaDDls(retornaGerente());
        }

        lblTituloProjeto.Text = "Relatório de Monitoramento";
    }

    public void GridBind(string codArea, string codProj)
    {
        rptProjetos.DataSource = new t03_projetoAction().ListTodosProjetosAcoesVinculadas(codArea, codProj);
        rptProjetos.DataBind();

        if (rptProjetos.Items.Count == 0) {
            lblEmptyData.Visible = true;
            lblEmptyData.Text = "Não Há Registro Encontrado.";
            btnExportar.Visible = false;
        }
        
        
        //lblEmptyData.Text = "Não Há Registro Encontrado.";
    }

    public void PopulaDDls(string idGerente)
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

        if (string.IsNullOrEmpty(idGerente))
            ddl.DataSource = new t30_AcoesVinculadasProjetoAction().ListTodosProjetos();
        else
            ddl.DataSource = new t30_AcoesVinculadasProjetoAction().ListTodosProjetosDoGerente(idGerente);

        ddl.DataTextField = "nm_projeto";
        ddl.DataValueField = "t03_cd_projeto";
        ddl.DataBind();
        pb.AddEmptyItem(ddl, "Selecione . . . ");
    }

    public void rptProjetos_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
          (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Repeater rptProjetosPai = (Repeater)e.Item.FindControl("rptProjetosPai");
            Label lblProjetoTitulo = (Label)e.Item.FindControl("lblProjetoTitulo");
            Literal ltrDataImpressao = (Literal)e.Item.FindControl("ltrDataImpressao");

            //projeto vinculado a acao
            t03_projeto t03 = (t03_projeto)e.Item.DataItem;
            Util.CodProjetoSustentador = t03.t03_cd_projeto.ToString();

            lblProjetoTitulo.Text = "A&ccedil;&otilde;es Vinculadas ao Projeto Sustentador - " + t03.nm_projeto;
            //data impressão
            ltrDataImpressao.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);

            rptProjetosPai.DataSource = new t30_AcoesVinculadasProjetoAction().LisProjPaiAcaoVinculada(t03.t03_cd_projeto, Util.CodAreaResultado);
            rptProjetosPai.DataBind();

            if (rptProjetosPai.Items.Count <= 0)
            {
                rptProjetos.Visible = false;
                lblEmptyData.Text = "Não existem ações vinculadas.";
                lblEmptyData.Visible = true;
                btnExportar.Visible = false;
            }
            else
            {
                btnExportar.Visible = true;
                rptProjetos.Visible = true;
                lblEmptyData.Visible = false;
            }
        }
    }

    public void rptProjetosPai_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) ||
          (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Literal ltrProjPai = (Literal)e.Item.FindControl("ltrProjPai");
            Repeater rptAcoes = (Repeater)e.Item.FindControl("rptAcoes");
            Literal ltrAreaResultado = (Literal)e.Item.FindControl("ltrAreaResultado");
            Literal ltrProjetoSustentador = (Literal)e.Item.FindControl("ltrProjetoSustentador");
            Literal ltrGerente = (Literal)e.Item.FindControl("ltrGerente");
            
            //projeto vinculado a acao
            t03_projeto t03 = (t03_projeto)e.Item.DataItem;
            
            List<t03_projeto> lstProjetoSusten = new t03_projetoAction().listProjetoSustent(t03.t03_cd_projeto.ToString()); //Util.CodProjetoSustentador

            //area de resultado
            t26_arearesultado t26 = new t26_arearesultadoAction().Retrieve(lstProjetoSusten[0].t26_cd_arearesultado);

            Util.CodAreaResultado = t26.t26_cd_arearesultado.ToString();

            ltrAreaResultado.Text =  pb.ReplaceAcentoPorCaracterHTML(lstProjetoSusten[0].t26.nm_area);
            
            //nome do projeto
            ltrProjetoSustentador.Text =  pb.ReplaceAcentoPorCaracterHTML(lstProjetoSusten[0].nm_projeto);
            //gerente do projeto
            ltrGerente.Text =  pb.ReplaceAcentoPorCaracterHTML(new t02_usuarioAction().Retrieve(lstProjetoSusten[0].t02_cd_usuario).nm_nome);
            //codigo do projeto

            rptAcoes.DataSource = new t08_acaoAction().ListTodasAcoesComMetaVinculadas(int.Parse(Util.CodProjetoSustentador), t03.t03_cd_projeto);
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

            ltrAcao.Text = pb.ReplaceAcentoPorCaracterHTML(t08.nm_acao);
            ltrDataPrevTermino.Text = String.Format("{0:dd/MM/yyyy}", t08.dt_fim);

            //recupera metas fisicas
            rptMetas.DataSource = new t10_produtoAction().ListObjTodosMetas(t08.t08_cd_acao);
            rptMetas.DataBind();
        }
    }

    public void rptMetas_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            Literal ltrHeadAteMes = (Literal)e.Item.FindControl("ltrHeadAteMes");
            var mesatual = DateTime.Now.Month;
            var mes = month[pb.NomeMes(mesatual - 2)];
            var ano = pb.NomeAno(mesatual - 2) > 0 ? pb.NomeAno(mesatual - 2) : 0;
            ltrHeadAteMes.Text = "Total At&eacute; " + mes + " de " + ano;
        }

        if ((e.Item.ItemType == ListItemType.Item) ||
          (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Literal ltrDescricaoMeta = (Literal)e.Item.FindControl("ltrDescricaoMeta");
            Literal ltrPorcento = (Literal)e.Item.FindControl("ltrPorcento");
            Literal ltrPorcentoAteMes = (Literal)e.Item.FindControl("ltrPorcentoAteMes");
            
            t10_produto t10 = (t10_produto)e.Item.DataItem;

            //ltrDescricaoMeta.Text = String.Format("{0:dd/MM/yyyy}", t10.ds_produto);
            ltrDescricaoMeta.Text = pb.ReplaceAcentoPorCaracterHTML(t10.ds_produto);

            /* possibilidade de escolha
             * Relatório Mes Atual -1 
             * OU
             * Relatório total */           

            //retorna valor total
            ltrPorcento.Text = RelatorioMetasTotal(new t10_produtoAction().Retrieve(Convert.ToInt32(t10.t10_cd_produto)).t17);

            //retorna valor mes-1
            ltrPorcentoAteMes.Text = RelatorioMetasMesAtualMenosUm(new t10_produtoAction().Retrieve(Convert.ToInt32(t10.t10_cd_produto)).t17);
        }
    }    

    private string RelatorioMetasMesAtualMenosUm(List<t17_vlproduto> lst)
    {
        //calculo de Realizado / Previsto
        decimal totvl_p = 0;
        decimal totvl_r = 0;
        decimal vlTotalMesAtual = 0;
        int AnoAtual = DateTime.Now.Year;
        ViewState["tprev"] = 0;
        ViewState["treal"] = 0;
        try
        {
            foreach (t17_vlproduto t17 in lst)
            {
                if (AnoAtual > t17.nu_ano)
                {
                    totvl_p += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                         t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                    totvl_r += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                         t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
                    ViewState["tprev"] = totvl_p;
                    ViewState["treal"] = totvl_r;
                }
                else if (AnoAtual == t17.nu_ano)
                {
                    switch (DateTime.Now.Month)
                    {
                        case 1:
                            totvl_p = Convert.ToDecimal(ViewState["tprev"]);
                            totvl_r = Convert.ToDecimal(ViewState["treal"]);
                            break;
                        case 2:
                            totvl_p += (t17.vl_p1);

                            totvl_r += (t17.vl_r1);
                            break;
                        case 3:
                            totvl_p += (t17.vl_p1 + t17.vl_p2);

                            totvl_r += (t17.vl_r1 + t17.vl_r2);
                            break;
                        case 4:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3);
                            break;
                        case 5:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4);
                            break;
                        case 6:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5);
                            break;
                        case 7:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 +
                            t17.vl_p6);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 +
                            t17.vl_r6);
                            break;
                        case 8:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 +
                            t17.vl_p6 + t17.vl_p7);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 +
                            t17.vl_r6 + t17.vl_r7);
                            break;
                        case 9:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 +
                            t17.vl_p6 + t17.vl_p7 + t17.vl_p8);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 +
                            t17.vl_r6 + t17.vl_r7 + t17.vl_r8);
                            break;
                        case 10:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 +
                            t17.vl_p6 + t17.vl_p7 + t17.vl_p8 + t17.vl_p9);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 +
                            t17.vl_r6 + t17.vl_r7 + t17.vl_r8 + t17.vl_r9);
                            break;
                        case 11:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 +
                            t17.vl_p6 + t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 +
                            t17.vl_r6 + t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10);
                            break;
                        case 12:
                            totvl_p += (t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 +
                            t17.vl_p6 + t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 +
                            t17.vl_p11);

                            totvl_r += (t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 +
                            t17.vl_r6 + t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 +
                            t17.vl_r11);
                            break;
                    }
                }
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
            return vlTotalMesAtual.ToString("N2");
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RelatorioMetasMesAtualMenosUm: " + ex.Message, "erro");
            lblMsg.Visible = true;
            return "0";
        }
    }

    private string RelatorioMetasTotal(List<t17_vlproduto> lst)
    {
        //calculo de Realizado / Previsto
        decimal totvl_p = 0;
        decimal totvl_r = 0;
        decimal vlTotalMesAtual = 0;

        try
        {
            foreach (t17_vlproduto t17 in lst)
            {
                totvl_p += t17.vl_p1 + t17.vl_p2 + t17.vl_p3 + t17.vl_p4 + t17.vl_p5 + t17.vl_p6 +
                         t17.vl_p7 + t17.vl_p8 + t17.vl_p9 + t17.vl_p10 + t17.vl_p11 + t17.vl_p12;
                totvl_r += t17.vl_r1 + t17.vl_r2 + t17.vl_r3 + t17.vl_r4 + t17.vl_r5 + t17.vl_r6 +
                         t17.vl_r7 + t17.vl_r8 + t17.vl_r9 + t17.vl_r10 + t17.vl_r11 + t17.vl_r12;
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
            return vlTotalMesAtual.ToString("N2");
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("RelatorioMetasTotal: " + ex.Message, "erro");
            lblMsg.Visible = true;
            return "0";
        }
    }
    
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            
            //if (ddl.SelectedValue == "")
            //{
            //    //---- Lista todos os Projeto ----//   
            //    ddlProjeto.ClearSelection();
            //    ddlProjeto.DkataSource = new t30_AcoesVinculadasProjetoAction().ListTodosProjetos();
            //    ddlProjeto.DataTextField = "nm_projeto";
            //    ddlProjeto.DataValueField = "t03_cd_projeto";
            //    ddlProjeto.DataBind();
            //    pb.AddEmptyItem(ddlProjeto, "Selecione . . .");

            //    //mensagem avisando não existem ações vinculadas quando se seleciona uma área de resultado que não possui ações vinculadas 
            //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "alerta", "alert('Não existem ações vinculadas quando se seleciona uma área de resultado que não possui ações vinculadas.')", true);
            //}
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("ddlArea_SelectedIndexChanged: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    //<asp:DropDownList ID="DropDownList1" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_OnSelectedIndexChanged" runat="server">
    //<asp:ListItem Text="Sim"></asp:ListItem>
    //<asp:ListItem Text="Não"></asp:ListItem>                                             
    //</asp:DropDownList>

    //<asp:Label ID="lblTeste" runat="server"></asp:Label> 

    //protected void DropDownList1_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList ddl = (DropDownList)sender;
    //    object numRow = ddl.Parent.Controls[5].UniqueID;

    //    for (int i = 0; i < rptProjetos.Items.Count; i++)
    //    {
    //        //TextBox txt = (TextBox).Items[i].FindControl("txtNome");
    //        Label lblTeste = (Label)rptProjetos.Items[i].FindControl("lblTeste");
    //        var item = lblTeste.UniqueID;
    //        if (item == numRow)
    //        {
    //            lblTeste.Text = "Teste" + i;
    //        }
    //    }
    //}

    protected void ddlProjeto_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //zera ddl
            ddlArea.DataSource = null;
            ddlArea.DataBind();

            DropDownList ddl = (DropDownList)sender;
            //--- Área de Resultado ---//
            if (ddl.SelectedIndex > 0)
            {
                //prepara para popular
                ddlArea.ClearSelection();
                ddlArea.DataSource = new t26_arearesultadoAction().ListAllDoProjeto(Convert.ToInt32(ddl.SelectedValue));
                ddlArea.DataTextField = "nm_area";
                ddlArea.DataValueField = "t26_cd_arearesultado";
                ddlArea.DataBind();
                pb.AddEmptyItem(ddlArea, "Todas");                
            }
            else
            {
                //prepara para popular
                ddlArea.ClearSelection();
                ddlArea.DataSource = new t26_arearesultadoAction().ListTodos();
                ddlArea.DataTextField = "nm_area";
                ddlArea.DataValueField = "t26_cd_arearesultado";
                ddlArea.DataBind();
                pb.AddEmptyItem(ddlArea, "Todas");
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = pb.Message("ddlProjeto_SelectedIndexChanged: " + ex.Message, "erro");
            lblMsg.Visible = true;
        }
    }

    private void ExportarGridExcel()
    {
        //carrega html com formatos
        //lblExcel.Visible = true;
        //CarregarGridExcel();

        //exporta
        Response.Clear();
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Cookies.Clear();
        Response.Cache.SetCacheability(HttpCacheability.Private);
        Response.CacheControl = "private";

        Response.AddHeader("content-disposition", "attachment;filename=" + "Rel_AcoesVinculadas" + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();        
        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        rptProjetos.RenderControl(htmlWrite);        

        Response.Write("");
        Response.Charset = "utf-8";
        Response.Write(stringWrite.ToString());

        //LiteralGrid.Visible = false;
        Response.End();

    }

    protected void btnExportar_Click(object sender, EventArgs e)
    {
        ExportarGridExcel();
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        //passa filtro selecionado por Area Resultado e/ou Projeto ou todos
        GridBind(ddlArea.SelectedValue, ddlProjeto.SelectedValue);
    }

    private string retornaGerente()
    {
        string idGerente = string.Empty;
        if (GerenteProjeto() && !Convert.ToBoolean(pb.Session("fl_admin")))
            idGerente = Session["cd_usuario"].ToString();

        return idGerente;
    }

    //
    //** Metodo verifica se é gerente de algum projeto
    //
    private bool GerenteProjeto()
    {
        bool pass = false;
        if (Session["cd_usuario"] != null)
        {
            int gerente = 0;
            t02_usuarioAction t02a = new t02_usuarioAction();
            gerente = t02a.ListUsuarioGerente(Session["cd_usuario"].ToString()).Rows.Count;
            if (gerente > 0)
            {
                //é gerente do projeto
                pass = true;
            }
            else
            {
                pass = false;
            }
        }
        return pass;
    }
}
