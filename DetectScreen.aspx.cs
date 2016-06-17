using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DetectScreen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["action"] != null)
        {
            try
            {
                ScreenResolution scr = new ScreenResolution(Request.QueryString["b"].ToString(),
                    Request.QueryString["bv"].ToString(), Convert.ToInt32(Request.QueryString["h"]),
                    Convert.ToInt32(Request.QueryString["w"]), Request.QueryString["c"].ToString());
                Session["ScreenResolution"] = scr;
            }
            catch
            {
                Session["ScreenResolution"] = new ScreenResolution();
            }

            Response.Redirect("Home.aspx");
        }
    }
}
