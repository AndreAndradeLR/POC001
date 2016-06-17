using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
/// <summary>
/// Summary description for ICrud
/// </summary>
 public interface ICrud
{

     void ExibirForm();

     void OcultarForm();

     void LimparForm();

     void GridBind();

     void FormBind();

     void Retrieve();
    
     //void GridView1_RowCommand(object sender, GridViewCommandEventArgs e);

     void GridView1_RowDataBound(object sender, GridViewRowEventArgs e);

     void btnSalvar_Click(object sender, EventArgs e);

     void btnCancelar_Click(object sender, EventArgs e);
    
     void btnNovo_Click(object sender, EventArgs e);
    

    #region JQuery
     void GridView1_PreRender(object sender, EventArgs e);

     void GridView1_RowCreated(object sender, GridViewRowEventArgs e);
    #endregion
}
