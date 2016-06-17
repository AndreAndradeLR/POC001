using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CustomControls_ucFinanceiro : System.Web.UI.UserControl, ICrud
{
    pageBase pb = new pageBase();
    decimal totvl_assegurado = 0;
    decimal totvl_planejado = 0;
    decimal totvl_revisado = 0;
    decimal totvl_liquidado = 0;
    decimal totvl_disponivel = 0;
    decimal conta = 0;
    decimal conta_total = 0;

    public void Page_Load(object sender, EventArgs e)
    {        
        FormBind();
		 GridBind();
        if (!IsPostBack)
        {
			
            ViewState["cod"] = "0";

            //verifica se o usuari é gerente para cadastrar novo
            if (pb.fl_gerente())
            {
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[1].Visible = true;
                if (Session["ScreenResolution"] != null)
                {
                    ScreenResolution scr = new ScreenResolution();
                    scr = (ScreenResolution)Session["ScreenResolution"];
                    if (scr.WidthWeb > 0)
                    {
                        PanelP.Width = Unit.Pixel(scr.WidthWeb);
                    }
                }
            }
            else
            {
                PanelbtnNovo.Visible = false;
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[1].Visible = false;
            }

            t21_fase t21 = new t21_fase();
            t21 = (new t21_faseAction().Retrieve(
                new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));

            if (t21.t21_cd_fase == 2)
            {
                PanelbtnNovo.Visible = false;
                GridView1.Columns[1].Visible = false;
                
            }
        }
    }


    #region ICrud Members

    public void ExibirForm()
    {
        PanelForm.Visible = true;
        PanelGrid.Visible = false;
    }

    public void FormBind()
    {
        if (!IsPostBack){  PopulaDDl();}        
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));
        ucLancFin.inicio = t08.dt_inicio.Year;
        ucLancFin.fim = t08.dt_fim.Year;
        ucLancFin.editar = true;
    }

    public void PopulaDDl()
    {
        t11_financeiro t11 = new t11_financeiro();
        t11_financeiroAction t11a = new t11_financeiroAction();
        /*lista todas as fontes menos as já cadastradas
        if (Convert.ToInt32(ViewState["cod"]) == 0)
        {
            //Response.Write("ENTROU");
            //metodo de tratamento
            ddlt27_cd_fonte.DataSource = t11a.RetrieveFonteCad(Convert.ToInt32(Session["cd_acao"]));
        }
        else {
            //Response.Write("ERRO");
            ddlt27_cd_fonte.DataSource = t11a.RetrieveFonteEdit(Convert.ToInt32(Session["cd_acao"]), Convert.ToInt32(ViewState["cod"]));
         }*/  
              
        ddlt27_cd_fonte.DataSource = new t27_fonteAction().ListTodos();
        ddlt27_cd_fonte.DataTextField = "nm_fonte";
        ddlt27_cd_fonte.DataValueField = "t27_cd_fonte";
        ddlt27_cd_fonte.DataBind();
        pb.AddEmptyItem(ddlt27_cd_fonte, "Selecione");
    }

    public void GridBind()
    {
        GridView1.DataSource = new t11_financeiroAction().ListTodos(Convert.ToInt32(pb.Session("cd_acao")));
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
                        PopulaDDl();
                        ExibirForm();
                        t21_fase t21 = new t21_fase();
                        t21 = (new t21_faseAction().Retrieve(
                            new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));
                        if (t21.t21_cd_fase == 2)
                        {
                            ucLancFin.PrevistoBloqueado = true;
                            ucLancFin.TableClear();
                        }

                        Retrieve();
                        break;
                    case "Deletar":
                        new t11_financeiroAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        //atualiza a data da ação 
                        new t08_acaoAction().UpdateAlteracao(Convert.ToInt32(pb.Session("cd_acao")));
                        break;
                    case "Selecionar":
                        Session["cd_financeiro"] = ViewState["cod"];
                        Retrieve();
                        Response.Redirect("~/FinanceiroDetalhes.aspx");
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
                decimal vl_assegurado = 0;
                decimal vl_planejado = 0;
                decimal vl_revisado = 0;
                decimal vl_empenhado = 0;
                decimal vl_disponivel = 0;

                if (e.Row.Cells[5] != null)
                {
                    char c = char.Parse(",");
                    foreach (t18b_vlfinanceiro t18 in new t11_financeiroAction().Retrieve(
                        Convert.ToInt32(drv["t11_cd_financeiro"])).t18l)
                    {
                        vl_assegurado += t18.vl_assegurado;

                        vl_planejado += (t18.vl_planejado1 + t18.vl_planejado2 + t18.vl_planejado3 + t18.vl_planejado4 + t18.vl_planejado5 +
                    t18.vl_planejado6 + t18.vl_planejado7 + t18.vl_planejado8 + t18.vl_planejado9 +
                    t18.vl_planejado10 + t18.vl_planejado11 + t18.vl_planejado12);

                        vl_revisado += (t18.vl_revisado1 + t18.vl_revisado2 + t18.vl_revisado3 + t18.vl_revisado4 + t18.vl_revisado5 +
                    t18.vl_revisado6 + t18.vl_revisado7 + t18.vl_revisado8 + t18.vl_revisado9 +
                    t18.vl_revisado10 + t18.vl_revisado11 + t18.vl_revisado12);

                        vl_empenhado += (t18.vl_liquidado1 + t18.vl_liquidado2 + t18.vl_liquidado3 + t18.vl_liquidado4 + t18.vl_liquidado5 +
                    t18.vl_liquidado6 + t18.vl_liquidado7 + t18.vl_liquidado8 + t18.vl_liquidado9 + t18.vl_liquidado10 +
                    t18.vl_liquidado11 + t18.vl_liquidado12);


                    }

                    vl_disponivel = vl_planejado-vl_empenhado;
                    e.Row.Cells[4].Text = (vl_assegurado).ToString("N0");
                    e.Row.Cells[5].Text = (vl_planejado).ToString("N0");
                    e.Row.Cells[6].Text = (vl_revisado).ToString("N0");
                    e.Row.Cells[7].Text = (vl_empenhado).ToString("N0");
                    //aplicação da fórmula
                    if (vl_planejado > 0)
                    {
                        conta = (vl_empenhado / vl_planejado) * 100;
                        e.Row.Cells[8].Text = conta.ToString("N2"); 
                    }
                    else {
                        e.Row.Cells[8].Text = "0,00";                                  
                    }

                    totvl_assegurado += vl_assegurado;
                    totvl_disponivel+= vl_disponivel;
                    totvl_planejado+= vl_planejado;
                    totvl_revisado += vl_revisado;
                    totvl_liquidado+=vl_empenhado;
                }

                //Adicionar mensagem de alerta antes da exclusão
                ImageButton btn = (ImageButton)e.Row.Cells[1].Controls[0];
                if (btn != null)
                {
                    btn.Attributes.Add("OnClick", "if (confirm('Tem certeza que deseja excluir?') == false) return false;");
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                
                if (pb.fl_gerente())
                {
                    e.Row.Cells[0].Text = "Total";

                    t21_fase t21 = new t21_fase();
                    t21 = (new t21_faseAction().Retrieve(
                        new t22_faseprojetoAction().Retrieve(Convert.ToInt32(pb.Session("cd_projeto"))).t21_cd_fase));

                    if (t21.t21_cd_fase == 2)
                    {
                        e.Row.Cells[0].ColumnSpan = 3;
                    }
                    else
                    {
                        e.Row.Cells[0].ColumnSpan = 4;
                    }
                    
                    e.Row.Cells[1].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                }
                else
                {
                    e.Row.Cells[2].Text = "Total";
                    e.Row.Cells[2].ColumnSpan = 2;
                    e.Row.Cells[3].Visible = false;
                }

                e.Row.Cells[4].Text = (totvl_assegurado).ToString("N0");
                e.Row.Cells[5].Text = (totvl_planejado).ToString("N0");
                e.Row.Cells[6].Text = (totvl_revisado).ToString("N0");              
                e.Row.Cells[7].Text = (totvl_liquidado).ToString("N0");
                //aplicação da fórmula            

                if (totvl_planejado > 0)
                {
                    conta_total = (totvl_liquidado / totvl_planejado) * 100;
                    e.Row.Cells[8].Text = conta_total.ToString("N2");
                }
                else
                {
                    e.Row.Cells[8].Text = "0,00";                    
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
        t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));
        ucLancFin.TableClear();
        ddlt27_cd_fonte.ClearSelection();
    }

    public void OcultarForm()
    {
        LimparForm();
        PanelForm.Visible = false;
        PanelGrid.Visible = true;
        ViewState["cod"] = 0;

    }

    public void Retrieve()
    {
        LimparForm();
        t11_financeiro t11 = new t11_financeiroAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        ListItem li = ddlt27_cd_fonte.Items.FindByValue(t11.t27_cd_fonte.ToString());
        if (li != null)
            li.Selected = true;


        foreach (t18b_vlfinanceiro t18 in t11.t18l)
        {
            TextBox txtvl_restopagar = (TextBox)ucLancFin.FindControl("txtvl_restopagar" + t18.nu_ano.ToString());
            TextBox txtvl_dotorcado = (TextBox)ucLancFin.FindControl("txtvl_dotorcado" + t18.nu_ano.ToString());
            TextBox txtvl_assegurado = (TextBox)ucLancFin.FindControl("txtvl_assegurado" + t18.nu_ano.ToString());

            TextBox txtvl_planejado1 = (TextBox)ucLancFin.FindControl("txtvl_planejado1" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado1 = (TextBox)ucLancFin.FindControl("txtvl_provisionado1" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado1 = (TextBox)ucLancFin.FindControl("txtvl_empenhado1" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado1 = (TextBox)ucLancFin.FindControl("txtvl_liquidado1" + t18.nu_ano.ToString());
            TextBox txtvl_revisado1 = (TextBox)ucLancFin.FindControl("txtvl_revisado1" + t18.nu_ano.ToString());

            TextBox txtvl_planejado2 = (TextBox)ucLancFin.FindControl("txtvl_planejado2" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado2 = (TextBox)ucLancFin.FindControl("txtvl_provisionado2" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado2 = (TextBox)ucLancFin.FindControl("txtvl_empenhado2" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado2 = (TextBox)ucLancFin.FindControl("txtvl_liquidado2" + t18.nu_ano.ToString());
            TextBox txtvl_revisado2 = (TextBox)ucLancFin.FindControl("txtvl_revisado2" + t18.nu_ano.ToString());

            TextBox txtvl_planejado3 = (TextBox)ucLancFin.FindControl("txtvl_planejado3" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado3 = (TextBox)ucLancFin.FindControl("txtvl_provisionado3" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado3 = (TextBox)ucLancFin.FindControl("txtvl_empenhado3" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado3 = (TextBox)ucLancFin.FindControl("txtvl_liquidado3" + t18.nu_ano.ToString());
            TextBox txtvl_revisado3 = (TextBox)ucLancFin.FindControl("txtvl_revisado3" + t18.nu_ano.ToString());


            TextBox txtvl_planejado4 = (TextBox)ucLancFin.FindControl("txtvl_planejado4" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado4 = (TextBox)ucLancFin.FindControl("txtvl_provisionado4" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado4 = (TextBox)ucLancFin.FindControl("txtvl_empenhado4" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado4 = (TextBox)ucLancFin.FindControl("txtvl_liquidado4" + t18.nu_ano.ToString());
            TextBox txtvl_revisado4 = (TextBox)ucLancFin.FindControl("txtvl_revisado4" + t18.nu_ano.ToString());


            TextBox txtvl_planejado5 = (TextBox)ucLancFin.FindControl("txtvl_planejado5" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado5 = (TextBox)ucLancFin.FindControl("txtvl_provisionado5" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado5 = (TextBox)ucLancFin.FindControl("txtvl_empenhado5" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado5 = (TextBox)ucLancFin.FindControl("txtvl_liquidado5" + t18.nu_ano.ToString());
            TextBox txtvl_revisado5 = (TextBox)ucLancFin.FindControl("txtvl_revisado5" + t18.nu_ano.ToString());


            TextBox txtvl_planejado6 = (TextBox)ucLancFin.FindControl("txtvl_planejado6" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado6 = (TextBox)ucLancFin.FindControl("txtvl_provisionado6" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado6 = (TextBox)ucLancFin.FindControl("txtvl_empenhado6" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado6 = (TextBox)ucLancFin.FindControl("txtvl_liquidado6" + t18.nu_ano.ToString());
            TextBox txtvl_revisado6 = (TextBox)ucLancFin.FindControl("txtvl_revisado6" + t18.nu_ano.ToString());


            TextBox txtvl_planejado7 = (TextBox)ucLancFin.FindControl("txtvl_planejado7" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado7 = (TextBox)ucLancFin.FindControl("txtvl_provisionado7" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado7 = (TextBox)ucLancFin.FindControl("txtvl_empenhado7" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado7 = (TextBox)ucLancFin.FindControl("txtvl_liquidado7" + t18.nu_ano.ToString());
            TextBox txtvl_revisado7 = (TextBox)ucLancFin.FindControl("txtvl_revisado7" + t18.nu_ano.ToString());

            TextBox txtvl_planejado8 = (TextBox)ucLancFin.FindControl("txtvl_planejado8" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado8 = (TextBox)ucLancFin.FindControl("txtvl_provisionado8" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado8 = (TextBox)ucLancFin.FindControl("txtvl_empenhado8" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado8 = (TextBox)ucLancFin.FindControl("txtvl_liquidado8" + t18.nu_ano.ToString());
            TextBox txtvl_revisado8 = (TextBox)ucLancFin.FindControl("txtvl_revisado8" + t18.nu_ano.ToString());


            TextBox txtvl_planejado9 = (TextBox)ucLancFin.FindControl("txtvl_planejado9" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado9 = (TextBox)ucLancFin.FindControl("txtvl_provisionado9" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado9 = (TextBox)ucLancFin.FindControl("txtvl_empenhado9" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado9 = (TextBox)ucLancFin.FindControl("txtvl_liquidado9" + t18.nu_ano.ToString());
            TextBox txtvl_revisado9 = (TextBox)ucLancFin.FindControl("txtvl_revisado9" + t18.nu_ano.ToString());


            TextBox txtvl_planejado10 = (TextBox)ucLancFin.FindControl("txtvl_planejado10" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado10 = (TextBox)ucLancFin.FindControl("txtvl_provisionado10" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado10 = (TextBox)ucLancFin.FindControl("txtvl_empenhado10" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado10 = (TextBox)ucLancFin.FindControl("txtvl_liquidado10" + t18.nu_ano.ToString());
            TextBox txtvl_revisado10 = (TextBox)ucLancFin.FindControl("txtvl_revisado10" + t18.nu_ano.ToString());


            TextBox txtvl_planejado11 = (TextBox)ucLancFin.FindControl("txtvl_planejado11" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado11 = (TextBox)ucLancFin.FindControl("txtvl_provisionado11" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado11 = (TextBox)ucLancFin.FindControl("txtvl_empenhado11" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado11 = (TextBox)ucLancFin.FindControl("txtvl_liquidado11" + t18.nu_ano.ToString());
            TextBox txtvl_revisado11 = (TextBox)ucLancFin.FindControl("txtvl_revisado11" + t18.nu_ano.ToString());


            TextBox txtvl_planejado12 = (TextBox)ucLancFin.FindControl("txtvl_planejado12" + t18.nu_ano.ToString());
            TextBox txtvl_provisionado12 = (TextBox)ucLancFin.FindControl("txtvl_provisionado12" + t18.nu_ano.ToString());
            TextBox txtvl_empenhado12 = (TextBox)ucLancFin.FindControl("txtvl_empenhado12" + t18.nu_ano.ToString());
            TextBox txtvl_liquidado12 = (TextBox)ucLancFin.FindControl("txtvl_liquidado12" + t18.nu_ano.ToString());
            TextBox txtvl_revisado12 = (TextBox)ucLancFin.FindControl("txtvl_revisado12" + t18.nu_ano.ToString());

            

            if (txtvl_planejado1 != null)
            {
                txtvl_dotorcado.Text = t18.vl_dotorcado.ToString("N2");
                txtvl_restopagar.Text = t18.vl_restopagar.ToString("N2");
                txtvl_assegurado.Text = t18.vl_assegurado.ToString("N2");

                txtvl_planejado1.Text = t18.vl_planejado1.ToString("N2");
                txtvl_provisionado1.Text = t18.vl_provisionado1.ToString("N2");
                txtvl_empenhado1.Text = t18.vl_empenhado1.ToString("N2");
                txtvl_liquidado1.Text = t18.vl_liquidado1.ToString("N2");
                txtvl_revisado1.Text = t18.vl_revisado1.ToString("N2");

                txtvl_planejado2.Text = t18.vl_planejado2.ToString("N2");
                txtvl_provisionado2.Text = t18.vl_provisionado2.ToString("N2");
                txtvl_empenhado2.Text = t18.vl_empenhado2.ToString("N2");
                txtvl_liquidado2.Text = t18.vl_liquidado2.ToString("N2");
                txtvl_revisado2.Text = t18.vl_revisado2.ToString("N2");

                txtvl_planejado3.Text = t18.vl_planejado3.ToString("N2");
                txtvl_provisionado3.Text = t18.vl_provisionado3.ToString("N2");
                txtvl_empenhado3.Text = t18.vl_empenhado3.ToString("N2");
                txtvl_liquidado3.Text = t18.vl_liquidado3.ToString("N2");
                txtvl_revisado3.Text = t18.vl_revisado3.ToString("N2");

                txtvl_planejado4.Text = t18.vl_planejado4.ToString("N2");
                txtvl_provisionado4.Text = t18.vl_provisionado4.ToString("N2");
                txtvl_empenhado4.Text = t18.vl_empenhado4.ToString("N2");
                txtvl_liquidado4.Text = t18.vl_liquidado4.ToString("N2");
                txtvl_revisado4.Text = t18.vl_revisado4.ToString("N2");

                txtvl_planejado5.Text = t18.vl_planejado5.ToString("N2");
                txtvl_provisionado5.Text = t18.vl_provisionado5.ToString("N2");
                txtvl_empenhado5.Text = t18.vl_empenhado5.ToString("N2");
                txtvl_liquidado5.Text = t18.vl_liquidado5.ToString("N2");
                txtvl_revisado5.Text = t18.vl_revisado5.ToString("N2");

                txtvl_planejado6.Text = t18.vl_planejado6.ToString("N2");
                txtvl_provisionado6.Text = t18.vl_provisionado6.ToString("N2");
                txtvl_empenhado6.Text = t18.vl_empenhado6.ToString("N2");
                txtvl_liquidado6.Text = t18.vl_liquidado6.ToString("N2");
                txtvl_revisado6.Text = t18.vl_revisado6.ToString("N2");

                txtvl_planejado7.Text = t18.vl_planejado7.ToString("N2");
                txtvl_provisionado7.Text = t18.vl_provisionado7.ToString("N2");
                txtvl_empenhado7.Text = t18.vl_empenhado7.ToString("N2");
                txtvl_liquidado7.Text = t18.vl_liquidado7.ToString("N2");
                txtvl_revisado7.Text = t18.vl_revisado7.ToString("N2");

                txtvl_planejado8.Text = t18.vl_planejado8.ToString("N2");
                txtvl_provisionado8.Text = t18.vl_provisionado8.ToString("N2");
                txtvl_empenhado8.Text = t18.vl_empenhado8.ToString("N2");
                txtvl_liquidado8.Text = t18.vl_liquidado8.ToString("N2");
                txtvl_revisado8.Text = t18.vl_revisado8.ToString("N2");

                txtvl_planejado9.Text = t18.vl_planejado9.ToString("N2");
                txtvl_provisionado9.Text = t18.vl_provisionado9.ToString("N2");
                txtvl_empenhado9.Text = t18.vl_empenhado9.ToString("N2");
                txtvl_liquidado9.Text = t18.vl_liquidado9.ToString("N2");
                txtvl_revisado9.Text = t18.vl_revisado9.ToString("N2");

                txtvl_planejado10.Text = t18.vl_planejado10.ToString("N2");
                txtvl_provisionado10.Text = t18.vl_provisionado10.ToString("N2");
                txtvl_empenhado10.Text = t18.vl_empenhado10.ToString("N2");
                txtvl_liquidado10.Text = t18.vl_liquidado10.ToString("N2");
                txtvl_revisado10.Text = t18.vl_revisado10.ToString("N2");

                txtvl_planejado11.Text = t18.vl_planejado11.ToString("N2");
                txtvl_provisionado11.Text = t18.vl_provisionado11.ToString("N2");
                txtvl_empenhado11.Text = t18.vl_empenhado11.ToString("N2");
                txtvl_liquidado11.Text = t18.vl_liquidado11.ToString("N2");
                txtvl_revisado11.Text = t18.vl_revisado11.ToString("N2");

                txtvl_planejado12.Text = t18.vl_planejado12.ToString("N2");
                txtvl_provisionado12.Text = t18.vl_provisionado12.ToString("N2");
                txtvl_empenhado12.Text = t18.vl_empenhado12.ToString("N2");
                txtvl_liquidado12.Text = t18.vl_liquidado12.ToString("N2");
                txtvl_revisado12.Text = t18.vl_revisado12.ToString("N2");
            }
        }
    }

    public void btnCancelar_Click(object sender, EventArgs e)
    {
        OcultarForm();
    }

    public void btnNovo_Click(object sender, EventArgs e)
    {
        PopulaDDl();
        ucLancFin.PrevistoBloqueado = false;
        ucLancFin.TableClear();


        ViewState["cod"] = "0";
        ExibirForm();
        lblHeader.Text = "Cadastrar Novo";
    }

    private Decimal NullToDecimal(TextBox txt)
    {
        if (txt != null)
        {
            if (txt.Text == "")
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(txt.Text);
            }
        }
        else
        {
            return 0;
        }
    }

    public void btnSalvar_Click(object sender, EventArgs e)
    {
        //script que verifica se conexão caiu antes de executar ação
        if (pb.Session("cd_usuario") != "0")
        {
            int result = 0;
            string msg = "";
            try
            {
                t11_financeiro t11 = new t11_financeiro();
                t11_financeiroAction t11a = new t11_financeiroAction();

                t11.t11_cd_financeiro = Convert.ToInt32(ViewState["cod"]);
                t11.t08_cd_acao = Convert.ToInt32(pb.Session("cd_acao"));
                t11.t27_cd_fonte = Convert.ToInt32(ddlt27_cd_fonte.SelectedValue);


                if (t11.t11_cd_financeiro > 0)
                {
                    result = t11a.UpdateDB(t11);
                }
                else
                {
                    result = t11a.InsertDB(t11);
                    t11.t11_cd_financeiro = new t11_financeiroAction().RetrieveIDENTITY(t11);
                }

                if (result > 0)
                {
                    //atualiza a data da ação 
                    new t08_acaoAction().UpdateAlteracao(Convert.ToInt32(pb.Session("cd_acao")));

                    t08_acao t08 = new t08_acaoAction().Retrieve(Convert.ToInt32(pb.Session("cd_acao")));

                    new t18b_vlfinanceiroAction().DeleteDB(t11.t11_cd_financeiro);

                    for (int ano = t08.dt_inicio.Year; ano <= t08.dt_fim.Year; ano++)
                    {
                        t18b_vlfinanceiro t18 = new t18b_vlfinanceiro();
                        t18.t11_cd_financeiro = t11.t11_cd_financeiro;
                        t18.nu_ano = ano;

                        TextBox txtvl_restopagar = (TextBox)ucLancFin.FindControl("txtvl_restopagar" + ano.ToString());
                        TextBox txtvl_dotorcado = (TextBox)ucLancFin.FindControl("txtvl_dotorcado" + ano.ToString());
                        TextBox txtvl_assegurado = (TextBox)ucLancFin.FindControl("txtvl_assegurado" + ano.ToString());

                        TextBox txtvl_planejado1 = (TextBox)ucLancFin.FindControl("txtvl_planejado1" + ano.ToString());
                        TextBox txtvl_provisionado1 = (TextBox)ucLancFin.FindControl("txtvl_provisionado1" + ano.ToString());
                        TextBox txtvl_empenhado1 = (TextBox)ucLancFin.FindControl("txtvl_empenhado1" + ano.ToString());
                        TextBox txtvl_liquidado1 = (TextBox)ucLancFin.FindControl("txtvl_liquidado1" + ano.ToString());
                        TextBox txtvl_revisado1 = (TextBox)ucLancFin.FindControl("txtvl_revisado1" + ano.ToString());

                        TextBox txtvl_planejado2 = (TextBox)ucLancFin.FindControl("txtvl_planejado2" + ano.ToString());
                        TextBox txtvl_provisionado2 = (TextBox)ucLancFin.FindControl("txtvl_provisionado2" + ano.ToString());
                        TextBox txtvl_empenhado2 = (TextBox)ucLancFin.FindControl("txtvl_empenhado2" + ano.ToString());
                        TextBox txtvl_liquidado2 = (TextBox)ucLancFin.FindControl("txtvl_liquidado2" + ano.ToString());
                        TextBox txtvl_revisado2 = (TextBox)ucLancFin.FindControl("txtvl_revisado2" + ano.ToString());

                        TextBox txtvl_planejado3 = (TextBox)ucLancFin.FindControl("txtvl_planejado3" + ano.ToString());
                        TextBox txtvl_provisionado3 = (TextBox)ucLancFin.FindControl("txtvl_provisionado3" + ano.ToString());
                        TextBox txtvl_empenhado3 = (TextBox)ucLancFin.FindControl("txtvl_empenhado3" + ano.ToString());
                        TextBox txtvl_liquidado3 = (TextBox)ucLancFin.FindControl("txtvl_liquidado3" + ano.ToString());
                        TextBox txtvl_revisado3 = (TextBox)ucLancFin.FindControl("txtvl_revisado3" + ano.ToString());


                        TextBox txtvl_planejado4 = (TextBox)ucLancFin.FindControl("txtvl_planejado4" + ano.ToString());
                        TextBox txtvl_provisionado4 = (TextBox)ucLancFin.FindControl("txtvl_provisionado4" + ano.ToString());
                        TextBox txtvl_empenhado4 = (TextBox)ucLancFin.FindControl("txtvl_empenhado4" + ano.ToString());
                        TextBox txtvl_liquidado4 = (TextBox)ucLancFin.FindControl("txtvl_liquidado4" + ano.ToString());
                        TextBox txtvl_revisado4 = (TextBox)ucLancFin.FindControl("txtvl_revisado4" + ano.ToString());


                        TextBox txtvl_planejado5 = (TextBox)ucLancFin.FindControl("txtvl_planejado5" + ano.ToString());
                        TextBox txtvl_provisionado5 = (TextBox)ucLancFin.FindControl("txtvl_provisionado5" + ano.ToString());
                        TextBox txtvl_empenhado5 = (TextBox)ucLancFin.FindControl("txtvl_empenhado5" + ano.ToString());
                        TextBox txtvl_liquidado5 = (TextBox)ucLancFin.FindControl("txtvl_liquidado5" + ano.ToString());
                        TextBox txtvl_revisado5 = (TextBox)ucLancFin.FindControl("txtvl_revisado5" + ano.ToString());


                        TextBox txtvl_planejado6 = (TextBox)ucLancFin.FindControl("txtvl_planejado6" + ano.ToString());
                        TextBox txtvl_provisionado6 = (TextBox)ucLancFin.FindControl("txtvl_provisionado6" + ano.ToString());
                        TextBox txtvl_empenhado6 = (TextBox)ucLancFin.FindControl("txtvl_empenhado6" + ano.ToString());
                        TextBox txtvl_liquidado6 = (TextBox)ucLancFin.FindControl("txtvl_liquidado6" + ano.ToString());
                        TextBox txtvl_revisado6 = (TextBox)ucLancFin.FindControl("txtvl_revisado6" + ano.ToString());


                        TextBox txtvl_planejado7 = (TextBox)ucLancFin.FindControl("txtvl_planejado7" + ano.ToString());
                        TextBox txtvl_provisionado7 = (TextBox)ucLancFin.FindControl("txtvl_provisionado7" + ano.ToString());
                        TextBox txtvl_empenhado7 = (TextBox)ucLancFin.FindControl("txtvl_empenhado7" + ano.ToString());
                        TextBox txtvl_liquidado7 = (TextBox)ucLancFin.FindControl("txtvl_liquidado7" + ano.ToString());
                        TextBox txtvl_revisado7 = (TextBox)ucLancFin.FindControl("txtvl_revisado7" + ano.ToString());

                        TextBox txtvl_planejado8 = (TextBox)ucLancFin.FindControl("txtvl_planejado8" + ano.ToString());
                        TextBox txtvl_provisionado8 = (TextBox)ucLancFin.FindControl("txtvl_provisionado8" + ano.ToString());
                        TextBox txtvl_empenhado8 = (TextBox)ucLancFin.FindControl("txtvl_empenhado8" + ano.ToString());
                        TextBox txtvl_liquidado8 = (TextBox)ucLancFin.FindControl("txtvl_liquidado8" + ano.ToString());
                        TextBox txtvl_revisado8 = (TextBox)ucLancFin.FindControl("txtvl_revisado8" + ano.ToString());


                        TextBox txtvl_planejado9 = (TextBox)ucLancFin.FindControl("txtvl_planejado9" + ano.ToString());
                        TextBox txtvl_provisionado9 = (TextBox)ucLancFin.FindControl("txtvl_provisionado9" + ano.ToString());
                        TextBox txtvl_empenhado9 = (TextBox)ucLancFin.FindControl("txtvl_empenhado9" + ano.ToString());
                        TextBox txtvl_liquidado9 = (TextBox)ucLancFin.FindControl("txtvl_liquidado9" + ano.ToString());
                        TextBox txtvl_revisado9 = (TextBox)ucLancFin.FindControl("txtvl_revisado9" + ano.ToString());


                        TextBox txtvl_planejado10 = (TextBox)ucLancFin.FindControl("txtvl_planejado10" + ano.ToString());
                        TextBox txtvl_provisionado10 = (TextBox)ucLancFin.FindControl("txtvl_provisionado10" + ano.ToString());
                        TextBox txtvl_empenhado10 = (TextBox)ucLancFin.FindControl("txtvl_empenhado10" + ano.ToString());
                        TextBox txtvl_liquidado10 = (TextBox)ucLancFin.FindControl("txtvl_liquidado10" + ano.ToString());
                        TextBox txtvl_revisado10 = (TextBox)ucLancFin.FindControl("txtvl_revisado10" + ano.ToString());


                        TextBox txtvl_planejado11 = (TextBox)ucLancFin.FindControl("txtvl_planejado11" + ano.ToString());
                        TextBox txtvl_provisionado11 = (TextBox)ucLancFin.FindControl("txtvl_provisionado11" + ano.ToString());
                        TextBox txtvl_empenhado11 = (TextBox)ucLancFin.FindControl("txtvl_empenhado11" + ano.ToString());
                        TextBox txtvl_liquidado11 = (TextBox)ucLancFin.FindControl("txtvl_liquidado11" + ano.ToString());
                        TextBox txtvl_revisado11 = (TextBox)ucLancFin.FindControl("txtvl_revisado11" + ano.ToString());


                        TextBox txtvl_planejado12 = (TextBox)ucLancFin.FindControl("txtvl_planejado12" + ano.ToString());
                        TextBox txtvl_provisionado12 = (TextBox)ucLancFin.FindControl("txtvl_provisionado12" + ano.ToString());
                        TextBox txtvl_empenhado12 = (TextBox)ucLancFin.FindControl("txtvl_empenhado12" + ano.ToString());
                        TextBox txtvl_liquidado12 = (TextBox)ucLancFin.FindControl("txtvl_liquidado12" + ano.ToString());
                        TextBox txtvl_revisado12 = (TextBox)ucLancFin.FindControl("txtvl_revisado12" + ano.ToString());


                        if (txtvl_planejado1 != null)
                        {
                            t18.vl_dotorcado = NullToDecimal(txtvl_dotorcado);
                            t18.vl_restopagar = NullToDecimal(txtvl_restopagar);
                            t18.vl_assegurado = NullToDecimal(txtvl_assegurado);

                            t18.vl_planejado1 = NullToDecimal(txtvl_planejado1);
                            t18.vl_provisionado1 = NullToDecimal(txtvl_provisionado1);
                            t18.vl_empenhado1 = NullToDecimal(txtvl_empenhado1);
                            t18.vl_liquidado1 = NullToDecimal(txtvl_liquidado1);
                            t18.vl_revisado1 = NullToDecimal(txtvl_revisado1);

                            t18.vl_planejado2 = NullToDecimal(txtvl_planejado2);
                            t18.vl_provisionado2 = NullToDecimal(txtvl_provisionado2);
                            t18.vl_empenhado2 = NullToDecimal(txtvl_empenhado2);
                            t18.vl_liquidado2 = NullToDecimal(txtvl_liquidado2);
                            t18.vl_revisado2 = NullToDecimal(txtvl_revisado2);

                            t18.vl_planejado3 = NullToDecimal(txtvl_planejado3);
                            t18.vl_provisionado3 = NullToDecimal(txtvl_provisionado3);
                            t18.vl_empenhado3 = NullToDecimal(txtvl_empenhado3);
                            t18.vl_liquidado3 = NullToDecimal(txtvl_liquidado3);
                            t18.vl_revisado3 = NullToDecimal(txtvl_revisado3);

                            t18.vl_planejado4 = NullToDecimal(txtvl_planejado4);
                            t18.vl_provisionado4 = NullToDecimal(txtvl_provisionado4);
                            t18.vl_empenhado4 = NullToDecimal(txtvl_empenhado4);
                            t18.vl_liquidado4 = NullToDecimal(txtvl_liquidado4);
                            t18.vl_revisado4 = NullToDecimal(txtvl_revisado4);

                            t18.vl_planejado5 = NullToDecimal(txtvl_planejado5);
                            t18.vl_provisionado5 = NullToDecimal(txtvl_provisionado5);
                            t18.vl_empenhado5 = NullToDecimal(txtvl_empenhado5);
                            t18.vl_liquidado5 = NullToDecimal(txtvl_liquidado5);
                            t18.vl_revisado5 = NullToDecimal(txtvl_revisado5);

                            t18.vl_planejado6 = NullToDecimal(txtvl_planejado6);
                            t18.vl_provisionado6 = NullToDecimal(txtvl_provisionado6);
                            t18.vl_empenhado6 = NullToDecimal(txtvl_empenhado6);
                            t18.vl_liquidado6 = NullToDecimal(txtvl_liquidado6);
                            t18.vl_revisado6 = NullToDecimal(txtvl_revisado6);

                            t18.vl_planejado7 = NullToDecimal(txtvl_planejado7);
                            t18.vl_provisionado7 = NullToDecimal(txtvl_provisionado7);
                            t18.vl_empenhado7 = NullToDecimal(txtvl_empenhado7);
                            t18.vl_liquidado7 = NullToDecimal(txtvl_liquidado7);
                            t18.vl_revisado7 = NullToDecimal(txtvl_revisado7);

                            t18.vl_planejado8 = NullToDecimal(txtvl_planejado8);
                            t18.vl_provisionado8 = NullToDecimal(txtvl_provisionado8);
                            t18.vl_empenhado8 = NullToDecimal(txtvl_empenhado8);
                            t18.vl_liquidado8 = NullToDecimal(txtvl_liquidado8);
                            t18.vl_revisado8 = NullToDecimal(txtvl_revisado8);

                            t18.vl_planejado9 = NullToDecimal(txtvl_planejado9);
                            t18.vl_provisionado9 = NullToDecimal(txtvl_provisionado9);
                            t18.vl_empenhado9 = NullToDecimal(txtvl_empenhado9);
                            t18.vl_liquidado9 = NullToDecimal(txtvl_liquidado9);
                            t18.vl_revisado9 = NullToDecimal(txtvl_revisado9);

                            t18.vl_planejado10 = NullToDecimal(txtvl_planejado10);
                            t18.vl_provisionado10 = NullToDecimal(txtvl_provisionado10);
                            t18.vl_empenhado10 = NullToDecimal(txtvl_empenhado10);
                            t18.vl_liquidado10 = NullToDecimal(txtvl_liquidado10);
                            t18.vl_revisado10 = NullToDecimal(txtvl_revisado10);

                            t18.vl_planejado11 = NullToDecimal(txtvl_planejado11);
                            t18.vl_provisionado11 = NullToDecimal(txtvl_provisionado11);
                            t18.vl_empenhado11 = NullToDecimal(txtvl_empenhado11);
                            t18.vl_liquidado11 = NullToDecimal(txtvl_liquidado11);
                            t18.vl_revisado11 = NullToDecimal(txtvl_revisado11);

                            t18.vl_planejado12 = NullToDecimal(txtvl_planejado12);
                            t18.vl_provisionado12 = NullToDecimal(txtvl_provisionado12);
                            t18.vl_empenhado12 = NullToDecimal(txtvl_empenhado12);
                            t18.vl_liquidado12 = NullToDecimal(txtvl_liquidado12);
                            t18.vl_revisado12 = NullToDecimal(txtvl_revisado12);
                        }

                        new t18b_vlfinanceiroAction().InsertDB(t18);
                    }

                    msg = pb.Message("Salvo com sucesso", "ok");
                    OcultarForm();
                    GridBind();
                    ViewState["cod"] = "0";
                }
            }
            catch (Exception ex)
            {
                msg = pb.Message(ex.ToString(), "erro");
            }

            lblMsg.Text = msg;
            lblMsg.Visible = true;
        }
    }

    #endregion
}
