using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CustomControls_ucSenha : System.Web.UI.UserControl
{
    private string _usuario;
    private string _origem;
    private string _destino;
    private bool _senhaAtual;

    public string Destino
    {
        get { return _destino; }
        set { _destino = value; }
    }
    public string Origem
    {
        get { return _origem; }
        set { _origem = value; }
    }
    public bool SenhaAtual
    {
        get { return _senhaAtual; }
        set { _senhaAtual = value; }
    }
    public string Usuario
    {
        get { return _usuario; }
        set { _usuario = value; }
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        this.lblHeader.Text = "Alterar Senha (Usuário: " + _usuario + " )";
        //Me.btnAlterar.Attributes.Add("onclick", "javascript:alert('Alteração realizada com sucesso!')") 

        if (SenhaAtual)
        {
            this.PanelSenhaAtual.Visible = true;
        }
    }

    protected void btnCancelar_Click(object sender, System.EventArgs e)
    {
        if (this._destino.Length > 0)
        {
            Response.Redirect(this._destino);
        }
    }

    protected void btnAlterar_Click(object sender, System.EventArgs e)
    {

        t02_usuarioAction t02a = new t02_usuarioAction();
        t02_usuario t02 = new t02_usuario();
        t02.t02_cd_usuario = _usuario;

        if (this._senhaAtual == true)
        {
            t02.pw_senha = this.txtSenhaAtual.Text;
            if (t02a.ValidaSenha(t02) > 0)
            {
                t02.pw_senha = this.txtNovaSenha2.Text;
                t02a.UpdateSenhaDB(t02);
                if (this._destino.Length > 0)
                {
                    Response.Redirect(this._destino + "?altersenha=1");
                }
                pageBase pb = new pageBase();
                this.lblMsg.Text = pb.Message("Senha alterada com sucesso!", "ok");
                this.lblMsg.Visible = true;
            }
            else
            {
                pageBase pb = new pageBase();
                this.lblMsg.Text = pb.Message("Senha atual incorreta, tente novamente!", "erro");
                this.lblMsg.Visible = true;
            }
        }
        else
        {
            t02.pw_senha = this.txtNovaSenha2.Text;
            t02a.UpdateSenhaDB(t02);
            if (this._destino.Length > 0)
            {
                Response.Redirect(this._destino + "?altersenha=1");
            }
            
        }

    } 

}
