using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Rel_UsuarioPerfil : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridBind();
    }

    private void GridBind()
    {
        t02_usuarioAction t02a = new t02_usuarioAction();
        {
            GridView1.DataSource = t02a.ListTodos();
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gv = (GridView)sender;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = ((DataRowView)e.Row.DataItem);
                t02_usuarioAction t02a = new t02_usuarioAction();
                DataTable dt = t02a.RetrievePerfil(drv["t02_cd_usuario"].ToString());
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    sb.AppendLine(" " + dr["nm_perfil"] + "");
                    sb.AppendLine("<br>");
                }
                e.Row.Cells[3].Text = sb.ToString();

                //Gerente
                sb = new StringBuilder();
                foreach (DataRow dr in new 
                    t02_usuarioAction().ListProjetoPerfilGerente(
                    drv["t02_cd_usuario"].ToString()).Rows)
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                    sb.AppendLine(" " + dr["nm_projeto"] + "");
                    sb.AppendLine("<br>");
                }
                e.Row.Cells[4].Text = sb.ToString();


                //Responsável pelo Monitoramento
                sb = new StringBuilder();
                foreach (DataRow dr in new
                    t02_usuarioAction().ListProjetoPerfilMonitora(
                    drv["t02_cd_usuario"].ToString()).Rows)
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                    sb.AppendLine(" " + dr["nm_projeto"] + "");
                    sb.AppendLine("<br>");
                }
                e.Row.Cells[5].Text = sb.ToString();

                //Linha Gerencial
                sb = new StringBuilder();
                foreach (DataRow dr in new
                    t02_usuarioAction().ListLinhaGerencial(
                    drv["t02_cd_usuario"].ToString()).Rows)
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                    sb.AppendLine(" Função: " + dr["nm_funcao"] + "");
                    sb.AppendLine(" (Projeto: " + dr["nm_projeto"] + ") ");
                    sb.AppendLine("<br>");
                }
                e.Row.Cells[6].Text = sb.ToString();

                //Coordenador de ação
                sb = new StringBuilder();
                foreach (DataRow dr in new
                    t02_usuarioAction().ListAcaoPerfilCoordenador(
                    drv["t02_cd_usuario"].ToString()).Rows)
                {
                    e.Row.BackColor = System.Drawing.Color.LightYellow;
                    sb.AppendLine(" Ação:" + dr["nm_acao"] + "");
                    sb.AppendLine(" (Projeto: " + dr["nm_projeto"] + ") ");
                    sb.AppendLine("<br>");
                }
                e.Row.Cells[7].Text = sb.ToString();

            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
    }


}
