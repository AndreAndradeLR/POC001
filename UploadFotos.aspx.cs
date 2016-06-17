using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

public partial class UploadFotos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var Filename = Request["Filename"];

        var cd_projeto = int.Parse(Request["cd_projeto"]);

        t14_documento t14 = new t14_documento();
        {
            string extension = "";
            string nomearquivo = "";

            extension = System.IO.Path.GetExtension(Filename);
            nomearquivo = "foto" + DateTime.Now.GetHashCode() + extension;

            var folder = "Documentos//";
            var activeDir = Server.MapPath(string.Format("{0}", folder));
            var local = activeDir + nomearquivo;

            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                hpf.SaveAs(local);
            }

            t14.ds_descricao = "";
            t14.dt_alterado = DateTime.Now;
            t14.dt_cadastro = DateTime.Now;
            t14.fl_cronograma = false;
            t14.fl_deletedo = false;
            t14.fl_foto = true;
            t14.fl_outros = false;
            t14.fl_video = false;
            t14.nm_arquivo = nomearquivo;
            t14.nm_documento = "";
            t14.t03_cd_projeto = cd_projeto;

            int result = new t14_documentoAction().InsertFoto(t14);

            #region
            ////HttpPostedFileBase

            //var guid = Filename;
            //var workingImageExtension = Path.GetExtension(Filename).ToLower();
            //string[] allowedExtensions = { ".png", ".jpeg", ".jpg", ".gif" }; // Make sure it is an image that can be processed
            //if (allowedExtensions.Contains(workingImageExtension))
            //{
            //    var folder = "Documentos";
            //    var activeDir = context.Server.MapPath(string.Format("../{0}", folder));
            //    var nomeDaImagemSemExt = Filename.Replace(Path.GetExtension(Filename), "");
            //    var workingImage = new Bitmap(uploadedFileMeta.fileData.InputStream);

            //    var imageFolderOriginal = string.Format("{0}\\{1}",
            //        activeDir,
            //        Filename);

            //    //salva imagem original no tamanho da grande
            //    //salva documento na pasta
            //    uploadedFileMeta.fileData.SaveAs(imageFolderOriginal);
            //}
            #endregion
        }
    }
}