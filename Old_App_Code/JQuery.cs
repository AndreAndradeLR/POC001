using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for JQuery
/// </summary>
public class JQuery
{
    
    
   
    private string _tableID;
    public string tableID
    {
        get { return _tableID; }
        set { _tableID = value; }
    }

   

    private string _sortlist;
    public string SortList
    {
        get { return _sortlist; }
        set
        {
            _sortlist = value;
            if (_sortlist.Length < 1)
                _sortlist = "0, 0";
        }
    }

    private bool _disableSearch;
    public bool DisableSearch
    {
        get { return _disableSearch; }
        set { _disableSearch = value; }
    }

    private bool _disablePaging;
    public bool DisablePaging
    {
        get { return _disablePaging; }
        set { _disablePaging = value; }
    }

    private bool _debug;
    public bool Debug
    {
        get { return _debug; }
        set { _debug = value; }
    }

    private string _headers;
    public string Headers
    {
        get { return _headers; }
        set { _headers = value; }
    }

	public JQuery()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string SearchHtml()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        {
            sb.AppendLine("Pesquisa: <input id=\"filterBox" + tableID + 
                "\" value=\"\" maxlength=\"30\" size=\"30\" type=\"text\" />");
            sb.AppendLine("<img id=\"filterClear" + tableID + 
                "\" src=\"_assets/img/cross.png\" title=\"Limpar o filro\" alt=\"Limpar o filtro\" />");
         return sb.ToString();
        }
    }

    public string PagerHtml()
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        {
            sb.AppendLine("<img src=\"_assets/img/first.png\" class=\"first\"/>");
            sb.AppendLine("<img src=\"_assets/img/prev.png\" class=\"prev\"/>");
            sb.AppendLine("<input type=\"text\" class=\"pagedisplay\"/>");
            sb.AppendLine("<img src=\"_assets/img/next.png\" class=\"next\"/>");
            sb.AppendLine("<img src=\"_assets/img/last.png\" class=\"last\"/>");
            sb.AppendLine("<select class=\"pagesize\">");
            sb.AppendLine("<option selected=\"selected\"  value=\"10\">10</option>");
            sb.AppendLine("<option value=\"20\">20</option>");
            sb.AppendLine("<option value=\"30\">30</option>");
            sb.AppendLine("<option  value=\"40\">40</option>");
            sb.AppendLine("<option  value=\"50\">50</option>");
            sb.AppendLine("</select>");
            return sb.ToString();
        }
    }

    public void TableSorter()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        {
            sb.AppendLine("$(document).ready(function() {");

            sb.AppendLine("$(\"#" + tableID + "\").tablesorter({ debug: " + _debug.ToString().ToLower() + ", widgets: ['zebra'] , headers: {" + _headers + "}, sortList: [[" + SortList + "]]})");
            if (!_disablePaging)
            { 
                sb.AppendLine(".tablesorterPager({ container: $(\"#pager" + tableID + "\"), positionFixed: false })");


                sb.AppendLine(".tablesorterFilter({ filterContainer: $(\"#filterBox" + tableID + "\"),");
                sb.AppendLine("filterClearContainer: $(\"#filterClear" + tableID + "\"),");
                //sb.AppendLine("filterColumns: [0, 1, 2, 3, 4, 5, 6],");
                sb.AppendLine("filterCaseSensitive: false");
                sb.AppendLine("});");

                sb.AppendLine("$(\"#" + tableID + " .header\").click(function() {");
                sb.AppendLine("$(\"#" + tableID + " tfoot .first\").click();");
                sb.AppendLine("}); ");
            }
            

            sb.AppendLine("}); ");

            ((System.Web.UI.Page)HttpContext.Current.Handler).ClientScript.RegisterClientScriptBlock(GetType(), "jquery" + tableID, sb.ToString(), true);

        }
    }

    
}
