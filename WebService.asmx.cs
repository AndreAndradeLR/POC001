using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace SigeObras.FrontEnd
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        public WebService()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        #region Reorder
        [WebMethod(EnableSession = true)]
        public string Reorder(object objOrdem)
        {
            string[] s = objOrdem.ToString().Split(',');
            int i = 1;
            foreach (string item in s)
            {
                AtualizarOrdem(Convert.ToInt32(item), i);
                i++;
            }
            return string.Empty;
        }
        #endregion

        #region AtualizarOrdem
        private void AtualizarOrdem(int iID, int iOrder)
        {
            t14_documento t14 = new t14_documentoAction().Retrieve(iID);

            t14.nu_ordem = iOrder;

            int result = new t14_documentoAction().UpdateDB(t14);
        }
        #endregion
    }
}
