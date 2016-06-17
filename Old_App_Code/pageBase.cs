using System;
using System.Collections;
using System.Configuration;
using System.Text;
using System.IO;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Net.Mail;
using System.Collections.Generic;


/// <summary>
/// Summary description for pageBase
/// </summary>
public class pageBase
{
    
    public string msgerro = "Não foi possível efetuar a operação, caso a dificuldade persista, contate o administrador.";

    public string cd_usuario()
    {
        return Convert.ToString(Session("cd_usuario"));
    }

    public bool fl_gerente()
    {
        if (Session("fl_gerente").ToString() == "0")
        {
            return false;
        }
        return Convert.ToBoolean(Session("fl_gerente"));
    }
    public bool fl_respacao()
    {
        if (Session("fl_respacao").ToString() == "0")
        {
            return false;
        }
        return Convert.ToBoolean(Session("fl_respacao"));
    }
    public bool fl_respmonitora()
    {
        if (Session("fl_respmonitora").ToString() == "0")
        {
            return false;
        }
        return Convert.ToBoolean(Session("fl_respmonitora"));
    }

    public object Session(string s)
    {

        if (HttpContext.Current.Session[s] != null)
        {
            return HttpContext.Current.Session[s];
        }
        else
        {
            return "0";
        } 
    }

    public object Session(string s, object defaultValue) {

        if (HttpContext.Current.Session[s] != null)
        {
            return HttpContext.Current.Session[s];
        }
        else
        {
            return defaultValue;
        }
    }

    
    public string Message(string str, string img)
    {
        //img=ok or erro
        return "<span class=msg><img src=\"images/" + img + ".gif\" />&nbsp;" + str + "</span>";
    }
    public void AddEmptyItem(DropDownList ddl, string str)
    {
        ListItem li = new ListItem(str, "");
        ddl.Items.Insert(0, li);
    }
    public void AddColorLines(DropDownList ddl, string hexcolor)
    {
        for (int i = 0; i <= ddl.Items.Count - 1; i++)
        {
            if (i % 2 != 0)
            {
                ddl.Items[i].Attributes.Add("Style", "Background-Color:" + hexcolor);
            }
        }

    }
    public string ReplaceNewLines(string text)
    {
        if (text == null) return null;

        int length;
        StringReader reader;
        StringWriter writer;
        StringBuilder builder;
        string line;

        length = (int)((double)text.Length * 1.2); //apply some padding to avoid array resizing, you probably want to
        //tweak this value for the size of the strings you're using
        reader = new StringReader(text);
        builder = new StringBuilder(length);
        writer = new StringWriter(builder);

        line = reader.ReadLine();
        if (line != null)
        {
            /*this if then while loop avoids adding an extra blank line at the end of the conversion
            * as opposed to just using:
            * while (line != null) {
            * writer.Write(line);
            * writer.WriteLine("<br/>");
            */

            writer.Write(line);
            line = reader.ReadLine();

            while (line != null)
            {
                writer.WriteLine("<br/>");
                writer.Write(line);
                line = reader.ReadLine();
            }
        }

        return writer.ToString();

    }

    public Literal GetLiteral(string text)
    {
        Literal rv;
        rv = new Literal();
        rv.Text = text;
        return rv;
    }

    public string ReplaceAspas(object input)
    {
        string functionReturnValue = null;
        functionReturnValue = "";
        if ((!object.ReferenceEquals(input, DBNull.Value)))
        {
            functionReturnValue = (string)input.ToString().Replace("'", "");
        }
        return functionReturnValue;
    }
    public void MakeAccessible(GridView grid)
    {
        if (grid.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute 
            grid.UseAccessibleHeader = true;

            //This will add the <thead> and <tbody> elements 
            grid.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. Remove if you don't have a footer row 
            grid.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    public void MaxLength(TextBox txt)
    {
        txt.Attributes.Add("onkeypress", "doKeypress(this);");
        txt.Attributes.Add("onbeforepaste", "doBeforePaste(this);");
        txt.Attributes.Add("onpaste", "doPaste(this);");
        txt.Attributes.Add("maxLength", txt.MaxLength.ToString());
    }
    public string dadosUsuario(t02_usuario t02, int index)
    {
        StringBuilder sb = new StringBuilder();
        {
            if (t02.t02_cd_usuario != null)
            {
                sb.Append("<div class='userInfo'>");
                sb.Append("<div class='userTitle'>" + t02.t01.nm_entidade + "</div>");
                sb.Append("<div class='userName'><a href=\"javascript:switchMenu('userID" + index +
                    "');\" title='Detalhes de " + t02.nm_nome +
                    "' class=link >" + t02.nm_nome + "</a></div>");

                sb.Append("<div id='userID" + index + "' style='display:none'>");
                sb.Append("<div class='userMail'><a href=mailto:" + t02.nm_email + ">" +
                    t02.nm_email + "</a></div>");
                if ((t02.nm_telefone.Length > 0) || (t02.nm_celular.Length > 0))
                {
                    sb.Append("<div class='userFone'>");

                    if (t02.nm_telefone.Length > 0)
                    {
                        //sb.Append(" (" + NM_DDDT + ") " + NM_TELEFONE);

                        sb.Append(String.Format("{0:(##) ####-####}", Convert.ToInt64(t02.nm_telefone)) + " ");
                    }
                    if (t02.nm_celular.Length > 0)
                    {
                        //sb.Append(" (" + NM_DDDT + ") " + NM_TELEFONE);
                        sb.Append(String.Format("{0:(##) ####-####}", Convert.ToInt64(t02.nm_celular)) + " ");
                    }
                    sb.Append("</div>"); //userFone
                }
                sb.Append("</div>"); //userID
                sb.Append("</div>"); //userInfo
            }
        }
        return sb.ToString();
    }


    public string dadosUsuario(t02_usuario t02, int index, string page)
    {
        StringBuilder sb = new StringBuilder();
        {
            if (t02.t02_cd_usuario != null)
            {
                sb.Append("<div class='userInfo'>");
                sb.Append("<div class='userTitle'>" + t02.t01.nm_entidade + "</div>");
                sb.Append("<div class='userName'><a href=\"javascript:switchMenu('" + page + "userID" + index +
                    "');\" title='Detalhes de " + t02.nm_nome +
                    "' class=link >" + t02.nm_nome + "</a></div>");

                sb.Append("<div id='"+ page + "userID" + index + "' style='display:none'>");
                sb.Append("<div class='userMail'><a href=mailto:" + t02.nm_email + ">" +
                    t02.nm_email + "</a></div>");
                if ((t02.nm_telefone.Length > 0) || (t02.nm_celular.Length > 0))
                {
                    sb.Append("<div class='userFone'>");

                    if (t02.nm_telefone.Length > 0)
                    {
                        //sb.Append(" (" + NM_DDDT + ") " + NM_TELEFONE);

                        sb.Append(String.Format("{0:(##) ####-####}", Convert.ToInt64(t02.nm_telefone)) + " ");
                    }
                    if (t02.nm_celular.Length > 0)
                    {
                        //sb.Append(" (" + NM_DDDT + ") " + NM_TELEFONE);
                        sb.Append(String.Format("{0:(##) ####-####}", Convert.ToInt64(t02.nm_celular)) + " ");
                    }
                    sb.Append("</div>"); //userFone
                }
                sb.Append("</div>"); //userID
                sb.Append("</div>"); //userInfo
            }
        }
        return sb.ToString();
    }

    public string MarcoRelacionadoRestricao(List<t09_marco> t09List)
    {
        StringBuilder sb = new StringBuilder();
        if (t09List.Count > 0)
        {
            sb.Append("<div class='marcoRel'>");
            sb.Append("<ul>");
            foreach (t09_marco t09 in t09List)
            {
                sb.Append("<li>");
                sb.Append(t09.ds_marco);
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            sb.Append("</div>"); //marcoRel
        }
        return sb.ToString();
    }

    public string RestricaoRelacionadoMarco(List<t07_restricao> t07List)
    {
        StringBuilder sb = new StringBuilder();
        if (t07List.Count > 0)
        {
            sb.Append("<div class='marcoRel'>");
            sb.Append("<ul>");
            foreach (t07_restricao t07 in t07List)
            {
                sb.Append("<li>");
                sb.Append(t07.ds_restricao);
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            sb.Append("</div>"); //marcoRel
        }
        return sb.ToString();
    }

    public string ProvidenciaRelacionadoRestricao(List<t23_providencia> t23List)
    {
        StringBuilder sb = new StringBuilder();
        if (t23List.Count > 0)
        {
            sb.Append("<div class='provRel'>");
            sb.Append("<ul>");
            foreach (t23_providencia t23 in t23List)
            {
                sb.Append("<li style='padding-top:10px;'>");
                sb.Append(t23.ds_providencia);
                sb.Append("<br />");
                sb.Append("<b>Responsável: </b>"+ new t02_usuarioAction().Retrieve(t23.t02_cd_usuario).nm_nome);
                sb.Append("<br />");
                sb.Append("<b>Data Limite: </b>" + String.Format("{0:dd/MM/yyyy}",t23.dt_limite));
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            sb.Append("</div>"); //provRel
        }
        return sb.ToString();
    }

    public string GetFlash(int height, int width, string srcSWF, string dataXML, int margin, string id)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<div id=\"flashcontent" + id + "\"></div>");
        sb.AppendLine("<script type=\"text/javascript\">");
        sb.AppendLine("// <![CDATA[");
        sb.AppendLine("var so = new SWFObject(\"" + srcSWF + "\", \"" + id + "\", \"" + width + "\", \"" + height + "\", \"0\", \"1\");");
        sb.AppendLine("so.addVariable(\"chartWidth\", \"" + (width - margin) + "\");");
        sb.AppendLine("so.addVariable(\"chartHeight\", \"" + (height - margin) + "\");");
        sb.AppendLine("so.addVariable(\"dataXML\", \"" + dataXML + "\");");
        sb.AppendLine("so.addParam(\"wmode\", \"transparent\");");
        sb.AppendLine("so.write(\"flashcontent" + id + "\");");
        sb.AppendLine("// ]]>");
        sb.AppendLine("</script>");

        /*
        sb.AppendLine("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" WIDTH=\"" + width + "\" HEIGHT=\"" + height + "\" id=\"" + id + "\" VIEWASTEXT>");
        sb.AppendLine("<param NAME=\"movie\" VALUE=\"" + srcSWF + "?chartWidth=" + (width - margin) + "&amp;chartHeight=" + (height - margin) + "&amp;dataXML=" + dataXML + "\">");
        sb.AppendLine("<param NAME=\"FlashVars\" VALUE=\"\">");
        sb.AppendLine("<param NAME=\"quality\" VALUE=\"high\">");
        sb.AppendLine("<param NAME=\"wmode\" value=\"transparent\">");
        sb.AppendLine("<param NAME=\"bgcolor\" VALUE=\"#FFFFFF\">");
        sb.AppendLine("<embed src=\"" + srcSWF + "?=" + (width - margin) + "&amp;chartHeight=" + (height - margin) + "&amp;dataXML=" + dataXML + "\" FlashVars=\"\" quality=\"high\" wmode=\"transparent\" bgcolor=\"#FFFFFF\" WIDTH=\"" + width + "\" HEIGHT=\"" + height + "\" NAME=\"" + id + "\" ALIGN TYPE=\"application/x-shockwave-flash\" PLUGINSPAGE=\"http://www.macromedia.com/go/getflashplayer\">");
        sb.AppendLine("</object>");
         */
        return sb.ToString();
    }

    public string GetMyFlash(int width, int height,string id, string value, string src)
    {
        StringBuilder sb = new StringBuilder();        
        sb.AppendLine("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" WIDTH=\"" + width + "\" HEIGHT=\"" + height + "\" id=\"" + id + "\" VIEWASTEXT>");
        sb.AppendLine("<param name=\"allowScriptAccess\" value=\"sameDomain\" />");
        sb.AppendLine("<param name='movie' value='"+ value +"' />");
        sb.AppendLine("<param name='menu' value='false' />");
        sb.AppendLine(" <param name='quality' value='high' />");
        sb.AppendLine("<param name=\"wmode\" value=\"transparent\">");
        sb.AppendLine("<param name=\"bgcolor\" VALUE=\"#ffffff\">");
        sb.AppendLine("<embed src='" + src + "' menu='false' quality='high' wmode='transparent' bgcolor='#ffffff' width='" + width + "' height='" + height + "' name='" + id + "' align='middle' allowScriptAccess='sameDomain' type='application/x-shockwave-flash' pluginspage='http://www.macromedia.com/go/getflashplayer'/>");       
        sb.AppendLine("</object>");           

        return sb.ToString();
    }

    public int NomeMes(int mes) {
        if (mes < 0)
        {
            mes = 11;
        }
        return mes;
    }

    public int NomeAno(int mes)
    {
        int ano;
        if ((DateTime.Now.Month - 2) < 0)
        {
            ano = (DateTime.Now.Year - 1);
        }
        else {
            ano = DateTime.Now.Year;
        }
        return ano;
    }    

    public string GetCurrentPageName()
    {
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
        return sRet;
    }

    public string ReplaceAcentoPorCaracterHTML(string strcomAcentos)
    {
        string strsemAcentos = strcomAcentos;
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[áàâãª]", "a");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[ÁÀÂÃ]", "A");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[éèê]", "e");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[ÉÈÊ]", "e");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[íìî]", "i");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[ÍÌÎ]", "I");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[óòôõº]", "o");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[ÓÒÔÕ]", "O");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[úùû]", "u");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[ÚÙÛ]", "U");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[ç]", "c");
        strsemAcentos = System.Text.RegularExpressions.Regex.Replace(strsemAcentos, "[Ç]", "C");
        return strsemAcentos;
    }

   /* #region INÍCIO EXIBIR MONITORAMENTO E RELATÓRIO DE SITUAÇÕES

    public bool MonitoramentoSituacoes(string cd_usuario, int cd_area)
    {
        int gerente = 0;
        int linhadecisoria = 0;
        int acao = 0;
        bool pass = false;

        if (Session("cd_usuario").ToString() != "Visitante")
        {
            //if ((Convert.ToBoolean(Session("fl_monitora"))) || (Convert.ToBoolean(Session("fl_admin"))))
            //{
            //    pass = true;
            //}            

            //consulta se é gerente
            t02_usuarioAction t02a = new t02_usuarioAction();
            gerente = t02a.ListRestricaoFinanceiro(cd_usuario, cd_area).Rows.Count;
            if (gerente > 0)
            {
                pass = true;
            }
            else
            {
                //consulta da linha decisória e ação
                linhadecisoria = t02a.ListFinanceiroLinhaGerencial(cd_usuario, Convert.ToInt32(Session("cd_projeto"))).Rows.Count;
                acao = t02a.ListRestricaoFinanceiroAcao(cd_usuario, Convert.ToInt32(Session("cd_projeto"))).Rows.Count;
                if ((linhadecisoria > 0) || (acao > 0))
                {
                    pass = true;
                }
            }
        }
        return pass;
    }

    #endregion FIM EXIBIR MONITORAMENTO E RELATÓRIO DE SITUAÇÕES*/


    #region INÍCIO AUTENTICAÇÂO FINANCEIRO

    public bool RestricaoFinanceiro(string cd_usuario,int cd_area) {
        int gerente = 0;
        int linhadecisoria = 0;
        int acao = 0;
        bool pass = false;

        if (Session("cd_usuario").ToString() != "Visitante")
        {
            if ((Convert.ToBoolean(Session("fl_monitora"))) || (Convert.ToBoolean(Session("fl_admin"))))
            {
                pass = true;
            }
            else
            {
                //consulta se é gerente e se pertence a area
                t02_usuarioAction t02a = new t02_usuarioAction();
                gerente = t02a.ListRestricaoFinanceiro(cd_usuario, cd_area).Rows.Count;
                if (gerente > 0)
                {
                    pass = true;
                }
                else
                {
                    //consulta da linha decisória e ação
                    linhadecisoria = t02a.ListFinanceiroLinhaGerencial(cd_usuario, Convert.ToInt32(Session("cd_projeto"))).Rows.Count;
                    acao = t02a.ListRestricaoFinanceiroAcao(cd_usuario, Convert.ToInt32(Session("cd_projeto"))).Rows.Count;
                    if ((linhadecisoria > 0)||(acao > 0))
                    {                        
                        pass = true;
                    }                    
                }
            }
        }
        return pass;
    }

    #endregion FIM AUTENTICAÇÂO FINANCEIRO


}
