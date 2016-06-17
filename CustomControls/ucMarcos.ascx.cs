using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class CustomControls_ucMarcos : System.Web.UI.UserControl, ICrud
{
    pageBase pb = new pageBase();

    public void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                GridView1.Columns[1].Visible = false;
                txtds_marco.Enabled = false;
                txtnu_esforco.Enabled = false;
                txtdt_prevista.Enabled = false;
            }
        }
    }

    #region ICrud Members


    public void DatasProjeto() {
        t03_projeto t03 = new t03_projetoAction().RetriveDatas(Convert.ToInt32(pb.Session("cd_projeto")));        
        lblDatas.Visible = true;
        lblDatas.Text = "<b>Datas do Projeto</b>&nbsp;&nbsp;&nbsp; inicio:&nbsp;" + t03.dt_inicio.ToShortDateString() + "&nbsp;&nbsp;&nbsp;fim:&nbsp;" + t03.dt_fim.ToShortDateString() + "";
        dt_fimProjeto.Text = String.Format("{0:dd/MM/yyyy}", t03.dt_fim);                
        dtHoje.Text = String.Format("{0:dd/MM/yyyy}", DateTime.Now);
        //<asp:HiddenField ID="dt_fimProjeto" runat="server" />
    }

    public void ExibirForm()
    {
        DatasProjeto();
        this.mdlPopup.Show();
    }

    public void FormBind()
    {
        throw new NotImplementedException();
    }

    public void GridBind()
    {
        ViewState["nu_esforco"] = 0;
        DataTable dtb = new t09_marcoAction().ListTodos(
            Convert.ToInt32(pb.Session("cd_projeto")));
        GridView1.DataSource = dtb;
        GridView1.DataBind();
        if (dtb.Rows.Count > 0)
        {
            if ((Convert.ToInt32(ViewState["nu_esforco"]) != 100) && (pb.fl_gerente()))
            {
                lblMsgEsforco.Text = "Atenção: Percentual de esforço do(s) Marco(s) Crítico(s) é de: " + ViewState["nu_esforco"] + "% e deveria ser 100%.";
            }
            else
            {
                lblMsgEsforco.Text = "";
            }
        }
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
                    jquery.SortList = "4, 0";
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
            //jquery.SorterDateFormat();

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
                        trReal.Visible = true;
                        lblHeader.Text = "Alteração";
                        ExibirForm();
                        Retrieve();
                        break;
                    case "Deletar":
                        new t09_marcoAction().DeleteDB(Convert.ToInt32(ViewState["cod"]));
                        lblMsg.Text = pb.Message("Exclusão realizada com sucesso!", "ok");
                        lblMsg.Visible = true;
                        GridBind();
                        ViewState["cod"] = "0";
                        break;
                    case "Projeto":
                        t09_marco t09 = new
                            t09_marcoAction().Retrieve(
                            Convert.ToInt32(ViewState["cod"]));
                        Session["cd_projeto"] = t09.t03_cd_projeto;
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
   
    public void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = ((DataRowView)e.Row.DataItem);
                if (drv["nu_esforco"] != DBNull.Value)
                {
                    int ac = Convert.ToInt32(ViewState["nu_esforco"]);
                    ac += Convert.ToInt32(drv["nu_esforco"]);
                    ViewState["nu_esforco"] = ac;
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
        txtds_marco.Text = "";
        txtds_comentario.Text = "";
        txtdt_prevista.Text = "";
        txtdt_realizada.Text = "";
        txtnu_esforco.Text = "";

    }

    public void OcultarForm()
    {
        LimparForm();
        this.mdlPopup.Hide();

    }

    public void Retrieve()
    {
        LimparForm();
        t09_marco t09 = new t09_marcoAction().Retrieve(Convert.ToInt32(ViewState["cod"]));
        txtds_marco.Text = t09.ds_marco;
        txtds_comentario.Text = t09.ds_comentario;
        txtnu_esforco.Text = t09.nu_esforco.ToString();
        txtdt_prevista.Text = t09.dt_prevista.ToShortDateString();
        if (t09.dt_realizada.Year > 1)
        {
            txtdt_realizada.Text = t09.dt_realizada.ToShortDateString();
        }
    }

    public void btnCancelar_Click(object sender, EventArgs e)
    {
        OcultarForm();
    }

    public void btnNovo_Click(object sender, EventArgs e)
    {
        trReal.Visible = false;
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
            t09_marco t09 = new t09_marco();
            t09_marcoAction t09a = new t09_marcoAction();

            t09.t09_cd_marco = Convert.ToInt32(ViewState["cod"]);
            t09.t03_cd_projeto = Convert.ToInt32(pb.Session("cd_projeto"));
            t09.ds_marco = txtds_marco.Text;
            t09.ds_comentario = txtds_comentario.Text;
            t09.nu_esforco = Convert.ToInt32(txtnu_esforco.Text);
            t09.dt_prevista = Convert.ToDateTime(txtdt_prevista.Text);
            t09.fl_status = "G";
            

            if (t09.t09_cd_marco > 0)
            {
                if (txtdt_realizada.Text.Length > 0)
                {
                    t09.dt_realizada = Convert.ToDateTime(txtdt_realizada.Text);
                    result = t09a.UpdateDB(t09);
                }
                else
                {
                    result = t09a.UpdateDtRealizadaNullDB(t09);
                }
            }
            else
            {
                result = t09a.InsertDB(t09);
            }

            if (result > 0)
            {
                msg = pb.Message("Salvo com sucesso", "ok");
                OcultarForm();
                ViewState["cod"] = "0";
                t09a.UpdateCorBarra(t09.t03_cd_projeto);
                GridBind();
            }

        }
        catch (Exception ex)
        {
            msg = pb.Message(ex.Message, "erro");
        }

        lblMsg.Text = msg;
        lblMsg.Visible = true;
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        //Response.Write("entrou");
    }



    #endregion
}
