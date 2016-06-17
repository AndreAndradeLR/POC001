using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Fotos: System.Web.UI.Page
{
    pageBase pb = new pageBase();

    public int cd_projeto
    {
        get
        {
            return Convert.ToInt32(pb.Session("cd_projeto"));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (cd_projeto != 0)
            {
                t03_projeto t03 = new t03_projeto();
                t03_projetoAction t03a = new t03_projetoAction();
                t03 = t03a.Retrieve(cd_projeto);
            }

        }

        GeraGaleriaFotos();

        string Script = "var serverVars = { " +
                                   "adminMode: true " +
                                    "}; ";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowName", Script, true);
    }

    public void GeraGaleriaFotos()
    {
        rptFotos.DataSource = new t14_documentoAction().ListFoto(cd_projeto);
        rptFotos.DataBind();
    }

    protected void rptFotos_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string NM_DOCUMENTO = DataBinder.Eval(e.Item.DataItem, "NM_DOCUMENTO").ToString();
            var NM_ARQUIVO = DataBinder.Eval(e.Item.DataItem, "NM_ARQUIVO").ToString();

            int ID = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "T14_CD_DOCUMENTO").ToString());            

            Label lblNm_DOCUMENTO = (Label)e.Item.FindControl("lblNm_DOCUMENTO");
            if (NM_DOCUMENTO.ToString().Length > 77)
            {
                lblNm_DOCUMENTO.Text = NM_DOCUMENTO.Substring(0, 77);
            }
            else
                lblNm_DOCUMENTO.Text = NM_DOCUMENTO.ToString();

            HtmlAnchor a = (HtmlAnchor)e.Item.FindControl("prettyPhoto");            
            HtmlImage img = (HtmlImage)e.Item.FindControl("idImg");            

            string arquivo = Server.MapPath(".") + @"\Documentos\" + NM_ARQUIVO;
            if (!System.IO.File.Exists(arquivo))
            {
                a.Attributes.Add("href", "Documentos/img.jpg");
                img.Attributes.Add("src", "images/img.jpg");
            }
            else
            {
                a.Attributes.Add("href", "Documentos/" + NM_ARQUIVO);
                img.Attributes.Add("src", "Documentos/" + NM_ARQUIVO);
            }

            img.Attributes.Add("width", "150px");
            img.Attributes.Add("height", "150px");
        }
    }

    protected void lkbEditImage_Click(object sender, EventArgs e)
    {
        lblHeader.Text = "Editar";


        LinkButton lkbEditImage = (LinkButton)sender;

        t14_documento t14 = new t14_documentoAction().Retrieve(Int32.Parse(lkbEditImage.CommandArgument));

        txtNM_DOCUMENTO.Text = t14.nm_documento;

        PanelEdit.Visible = true;
        PanelGaleria.Visible = false;

        Session["Editar"] = true;

        PanelArquivo.Visible = false;

        Session["cd_doc"] = Int32.Parse(lkbEditImage.CommandArgument);

    }

    protected void DelImage_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int result;
        string msg;
        t14_documento t14 = new t14_documentoAction().Retrieve(Int32.Parse(btn.CommandArgument));
        {
            try
            {
                result = new t14_documentoAction().DeleteDB(Int32.Parse(btn.CommandArgument));
                msg = pb.Message("Exclusão realizada com sucesso", "ok");
                GeraGaleriaFotos();
            }
            catch
            {
                msg = pb.Message(pb.msgerro, "erro");
            }
        }
    }

    protected void btnAcao_Click(object sender, EventArgs e)
    {
        t14_documento t14 = new t14_documento();
        {
            //1 megabyte = 1 048 576 bytes
            int maxSize = 10485760;
            int result = 0;
            bool erro = false;
            string extension = "";
            string nomearquivo = "";
            string msg = "";

            if (fuNM_ARQUIVO.HasFile && fuNM_ARQUIVO.PostedFile.ContentLength <= maxSize)
            {
                extension = System.IO.Path.GetExtension(fuNM_ARQUIVO.FileName);
                nomearquivo = "foto" + DateTime.Now.GetHashCode() + extension;
                fuNM_ARQUIVO.PostedFile.SaveAs(Server.MapPath(".") + @"\Documentos\" + nomearquivo);
            }

            t14.t03_cd_projeto = cd_projeto;
            t14.fl_foto = true;
            t14.fl_video = false;
            t14.nm_arquivo = nomearquivo;
            t14.nm_documento = txtNM_DOCUMENTO.Text;
            t14.dt_alterado = DateTime.Now;
            t14.dt_cadastro = DateTime.Now;

            if (fuNM_ARQUIVO.HasFile && fuNM_ARQUIVO.PostedFile.ContentLength > maxSize)
            {
                erro = true;
                msg = pb.Message("Arquivo utrapassou o tamanho máximo de 10 MB!", "erro");
            }            

            if (!(erro))
            {
                if (Convert.ToBoolean(Session["Editar"]) == true)
                {
                    t14.t14_cd_documento = Int32.Parse(Session["cd_doc"].ToString());

                    result = new t14_documentoAction().UpdateDB(t14);
                    msg = pb.Message("Alteração realizado com sucesso!", "ok");
                }
                else
                {
                    result = new t14_documentoAction().InsertFoto(t14);
                    msg = pb.Message("Cadastro realizado com sucesso!", "ok");
                }

                if (result > 0)
                {
                    Ocultar();
                    PanelGaleria.Visible = true;
                    PanelEdit.Visible = false;
                    GeraGaleriaFotos();
                    cod.Value = "0";
                }
                else
                {
                    msg = pb.Message(pb.msgerro, "erro");
                }
            }

            //Label lblMsg = (Label)this.Master.FindControl("lblMsg");
            //lblMsg.Text = msg;
            //lblMsg.Visible = true;
            //lblMsg.Text = msg;
            //lblMsg.Visible = true;
        }
    }

    protected void btnNovo_Click(object sender, EventArgs e)
    {
        Session.Remove("Editar");
        PanelArquivo.Visible = true;
        PanelGaleria.Visible = false;
        PanelEdit.Visible = true;
        lblHeader.Text = "Cadastrar";
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        PanelEdit.Visible = false;
        Ocultar();
        PanelGaleria.Visible = true;
    }

    private void Ocultar()
    {
        // PanelEdit.Visible = false;
        txtNM_DOCUMENTO.Text = "";
        //PanelOpcao.Visible = false;
    }
}