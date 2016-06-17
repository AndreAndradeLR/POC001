using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t11_financeiroAction
/// </summary>
public class t11_financeiroAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t11_financeiroAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t11_financeiro t11)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t11_financeiro (t08_cd_acao, t27_cd_fonte, dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t08_cd_acao, @t27_cd_fonte, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t08_cd_acao", SqlDbType.Int, 0, t11.t08_cd_acao),
                MakeInParam("@t27_cd_fonte", SqlDbType.Int, 0, t11.t27_cd_fonte)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t11_financeiro t11)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t11_financeiro set " +
            "t27_cd_fonte=@t27_cd_fonte, dt_alterado=getdate() " +
            "where t11_cd_financeiro=@t11_cd_financeiro";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t11_cd_financeiro", SqlDbType.Int, 0, t11.t11_cd_financeiro),
                MakeInParam("@t27_cd_fonte", SqlDbType.Int, 0, t11.t27_cd_fonte)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t11_financeiro set  " +
            " fl_deletado=1 " +
            " where t11_cd_financeiro =" + id;

        return this.RunCommand(query);
    }

    public t11_financeiro Retrieve(int id)
    {
        string query = "select * from t11_financeiro where t11_cd_financeiro=" + id;
        DataTable dt = this.GetDataTable(query);
        t11_financeiro t11 = new t11_financeiro();
        foreach (DataRow dr in dt.Rows)
        {
            t11.t11_cd_financeiro = int.Parse(dr["t11_cd_financeiro"].ToString());
            t11.t27_cd_fonte = int.Parse(dr["t27_cd_fonte"].ToString());
            t11.t18l = new t18b_vlfinanceiroAction().ListObjTodos(t11.t11_cd_financeiro);

            if (dr["dt_cadastro"] != DBNull.Value)
                t11.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t11.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t11;
    }
    public int RetrieveIDENTITY(t11_financeiro t11)
    {
        string query = "select top 1 * from t11_financeiro where t27_cd_fonte=" + t11.t27_cd_fonte +
            " and t08_cd_acao=" + t11.t08_cd_acao + " and fl_deletado=0 order by t11_cd_financeiro desc";
        DataTable dt = this.GetDataTable(query);
        int identity = 0;
        foreach (DataRow dr in dt.Rows)
        {
            identity = int.Parse(dr["t11_cd_financeiro"].ToString());
            break;
        }
        return identity;
    }
    public List<t11_financeiro> ListObjTodos(int cd_acao)
    {
        string query = "select * from t11_financeiro " +
            "where t08_cd_acao=" + cd_acao +
            " and fl_deletado=0  order by t27_cd_fonte";
        DataTable dt = this.GetDataTable(query);
        List<t11_financeiro> t11list = new List<t11_financeiro>();
        foreach (DataRow dr in dt.Rows)
        {
            t11_financeiro t11 = new t11_financeiro();
            t11.t11_cd_financeiro = int.Parse(dr["t11_cd_financeiro"].ToString());
            t11.t27_cd_fonte = int.Parse(dr["t27_cd_fonte"].ToString());
            t11.t18l = new t18b_vlfinanceiroAction().ListObjTodos(t11.t11_cd_financeiro);
            t11list.Add(t11);
        }
        return t11list;
    }

    public DataTable ListTodos(int cd_acao)
    {
        string query = "select * from t11_financeiro t11 " +
            " left join t27_fonte t27 on t27.t27_cd_fonte=t11.t27_cd_fonte " +
            "where t11.t08_cd_acao=" + cd_acao +
            " and t11.fl_deletado=0  order by t27.nm_fonte";
        return this.GetDataTable(query);
    }
    public DataTable ListProjetoAnoFonteFin(int cd_projeto)
    {
        string query = "select " +
        "nu_ano, " +
        "t27.nm_fonte, t27.t27_cd_fonte, " +
        "sum(vl_restopagar) as vl_restopagar, " +
        "sum(vl_dotorcado) as vl_dotorcado, " +
        "sum(vl_assegurado) as vl_assegurado, " +
        "sum(vl_planejado1+vl_planejado2+vl_planejado3+vl_planejado4+vl_planejado5+vl_planejado6+vl_planejado7+vl_planejado8+vl_planejado9+vl_planejado10+vl_planejado11+vl_planejado12) as vl_planejado, " +
        "sum(vl_provisionado1+vl_provisionado2+vl_provisionado3+vl_provisionado4+vl_provisionado5+vl_provisionado6+vl_provisionado7+vl_provisionado8+vl_provisionado9+vl_provisionado10+vl_provisionado11+vl_provisionado12) as vl_provisionado, " +
        "sum(vl_empenhado1+vl_empenhado2+vl_empenhado3+vl_empenhado4+vl_empenhado5+vl_empenhado6+vl_empenhado7+vl_empenhado8+vl_empenhado9+vl_empenhado10+vl_empenhado11+vl_empenhado12) as vl_empenhado, " +
        "sum(vl_liquidado1+vl_liquidado2+vl_liquidado3+vl_liquidado4+vl_liquidado5+vl_liquidado6+vl_liquidado7+vl_liquidado8+vl_liquidado9+vl_liquidado10+vl_liquidado11+vl_liquidado12) as vl_liquidado, " +
        "sum(vl_revisado1+vl_revisado2+vl_revisado3+vl_revisado4+vl_revisado5+vl_revisado6+vl_revisado7+vl_revisado8+vl_revisado9+vl_revisado10+vl_revisado11+vl_revisado12) as vl_revisado, " +
        "(sum(vl_planejado1+vl_planejado2+vl_planejado3+vl_planejado4+vl_planejado5+vl_planejado6+vl_planejado7+vl_planejado8+vl_planejado9+vl_planejado10+vl_planejado11+vl_planejado12)-sum(vl_empenhado1+vl_empenhado2+vl_empenhado3+vl_empenhado4+vl_empenhado5+vl_empenhado6+vl_empenhado7+vl_empenhado8+vl_empenhado9+vl_empenhado10+vl_empenhado11+vl_empenhado12)) as vl_disponivel " +
        "from t18b_vlfinanceiro t18 " +
        "left join t11_financeiro t11 on t11.t11_cd_financeiro=t18.t11_cd_financeiro " +
        "left join t27_fonte t27 on t27.t27_cd_fonte=t11.t27_cd_fonte  " +
        "left join t08_acao t08 on t08.t08_cd_acao=t11.t08_cd_acao " +
        "where (t08.t03_cd_projeto=" + cd_projeto + " and t08.fl_deletado=0) and t11.fl_deletado=0  " +
        "group by nu_ano, t27.nm_fonte, t27.t27_cd_fonte " +
        "order by nu_ano, t27.nm_fonte, t27.t27_cd_fonte ";
        return this.GetDataTable(query);
    }

    public DataTable ListProjetoAnoFin(int cd_projeto)
    {
        string query = "select " +
        "nu_ano, " +
        "sum(vl_planejado1+vl_planejado2+vl_planejado3+vl_planejado4+vl_planejado5+vl_planejado6+vl_planejado7+vl_planejado8+vl_planejado9+vl_planejado10+vl_planejado11+vl_planejado12) as vl_planejado, " +
        "sum(vl_liquidado1+vl_liquidado2+vl_liquidado3+vl_liquidado4+vl_liquidado5+vl_liquidado6+vl_liquidado7+vl_liquidado8+vl_liquidado9+vl_liquidado10+vl_liquidado11+vl_liquidado12) as vl_liquidado " +
        "from t18b_vlfinanceiro t18 " +
        "left join t11_financeiro t11 on t11.t11_cd_financeiro=t18.t11_cd_financeiro " +
        "left join t08_acao t08 on t08.t08_cd_acao=t11.t08_cd_acao " +
        "where (t08.t03_cd_projeto=" + cd_projeto + " and t08.fl_deletado=0) and t11.fl_deletado=0  " +
        "group by nu_ano " +
        "order by nu_ano ";
        return this.GetDataTable(query);
    }

    public DataTable ListAcaoFonteFinanceiro(int cd_fonte, int ano, int cd_projeto)
    {
        string query = "select " +
        "nm_acao,  " +
        "sum(vl_restopagar) as vl_restopagar, " +
        "sum(vl_dotorcado) as vl_dotorcado, " +
        "sum(vl_assegurado) as vl_assegurado, " +

        "sum(vl_planejado1) as vl_planejado1,sum(vl_planejado2) as vl_planejado2,sum(vl_planejado3) as vl_planejado3" +
        ",sum(vl_planejado4) as vl_planejado4,sum(vl_planejado5) as vl_planejado5,sum(vl_planejado6) as vl_planejado6" +
        ",sum(vl_planejado7) as vl_planejado7,sum(vl_planejado8) as vl_planejado8,sum(vl_planejado9) as vl_planejado9" +
        ",sum(vl_planejado10) as vl_planejado10,sum(vl_planejado11) as vl_planejado11,sum(vl_planejado12) as vl_planejado12, " +

        "sum(vl_revisado1) as vl_revisado1,sum(vl_revisado2) as vl_revisado2,sum(vl_revisado3) as vl_revisado3" +
        ",sum(vl_revisado4) as vl_revisado4,sum(vl_revisado5) as vl_revisado5,sum(vl_revisado6) as vl_revisado6" +
        ",sum(vl_revisado7) as vl_revisado7,sum(vl_revisado8) as vl_revisado8,sum(vl_revisado9) as vl_revisado9" +
        ",sum(vl_revisado10) as vl_revisado10,sum(vl_revisado11) as vl_revisado11,sum(vl_revisado12) as vl_revisado12, " +

        "sum(vl_provisionado1) as vl_provisionado1,sum(vl_provisionado2) as vl_provisionado2,sum(vl_provisionado3) as vl_provisionado3" +
        ",sum(vl_provisionado4) as vl_provisionado4,sum(vl_provisionado5) as vl_provisionado5,sum(vl_provisionado6) as vl_provisionado6" +
        ",sum(vl_provisionado7) as vl_provisionado7,sum(vl_provisionado8) as vl_provisionado8,sum(vl_provisionado9) as vl_provisionado9" +
        ",sum(vl_provisionado10) as vl_provisionado10,sum(vl_provisionado11) as vl_provisionado11,sum(vl_provisionado12) as vl_provisionado12, " +

        "sum(vl_empenhado1) as vl_empenhado1,sum(vl_empenhado2) as vl_empenhado2,sum(vl_empenhado3) as vl_empenhado3" +
        ",sum(vl_empenhado4) as vl_empenhado4,sum(vl_empenhado5) as vl_empenhado5,sum(vl_empenhado6) as vl_empenhado6" +
        ",sum(vl_empenhado7) as vl_empenhado7,sum(vl_empenhado8) as vl_empenhado8,sum(vl_empenhado9) as vl_empenhado9" +
        ",sum(vl_empenhado10) as vl_empenhado10,sum(vl_empenhado11) as vl_empenhado11,sum(vl_empenhado12) as vl_empenhado12, " +

        "sum(vl_liquidado1) as vl_liquidado1,sum(vl_liquidado2) as vl_liquidado2,sum(vl_liquidado3) as vl_liquidado3" +
        ",sum(vl_liquidado4) as vl_liquidado4,sum(vl_liquidado5) as vl_liquidado5,sum(vl_liquidado6) as vl_liquidado6" +
        ",sum(vl_liquidado7) as vl_liquidado7,sum(vl_liquidado8) as vl_liquidado8,sum(vl_liquidado9) as vl_liquidado9" +
        ",sum(vl_liquidado10) as vl_liquidado10,sum(vl_liquidado11) as vl_liquidado11,sum(vl_liquidado12) as vl_liquidado12 " +
        

        //"((vl_planejado1+vl_planejado2+vl_planejado3+vl_planejado4+vl_planejado5+vl_planejado6+vl_planejado7+vl_planejado8+vl_planejado9+vl_planejado10+vl_planejado11+vl_planejado12)-sum(vl_empenhado1+vl_empenhado2+vl_empenhado3+vl_empenhado4+vl_empenhado5+vl_empenhado6+vl_empenhado7+vl_empenhado8+vl_empenhado9+vl_empenhado10+vl_empenhado11+vl_empenhado12)) as vl_disponivel " +
        " from t08_acao Inner Join " +
        "t11_financeiro On t11_financeiro.t08_cd_acao = t08_acao.t08_cd_acao Inner Join " +
        "t18b_vlfinanceiro On t18b_vlfinanceiro.t11_cd_financeiro = " +
        "t11_financeiro.t11_cd_financeiro where";
        if (cd_fonte > 0){
            query += " t27_cd_fonte=" + cd_fonte + " and ";
        }
        query += " nu_ano=" + ano +
        " and t08_acao.fl_deletado=0 and t11_financeiro.fl_deletado=0 and t08_acao.t03_cd_projeto=" + cd_projeto + "  " +
        "group by nm_acao " +
        "order by nm_acao";        
        return this.GetDataTable(query);
    }

    public DataTable ListAcaoFinanceiroTotal(int cd_fonte, int ano, int cd_projeto)
    {
        string query = "select " +
        "sum(vl_restopagar) as vl_restopagar, " +
        "sum(vl_dotorcado) as vl_dotorcado, " +
        "sum(vl_assegurado) as vl_assegurado, " +

        "sum(vl_planejado1) as vl_planejado1,sum(vl_planejado2) as vl_planejado2,sum(vl_planejado3) as vl_planejado3" +
        ",sum(vl_planejado4) as vl_planejado4,sum(vl_planejado5) as vl_planejado5,sum(vl_planejado6) as vl_planejado6" +
        ",sum(vl_planejado7) as vl_planejado7,sum(vl_planejado8) as vl_planejado8,sum(vl_planejado9) as vl_planejado9" +
        ",sum(vl_planejado10) as vl_planejado10,sum(vl_planejado11) as vl_planejado11,sum(vl_planejado12) as vl_planejado12, " +

        "sum(vl_revisado1) as vl_revisado1,sum(vl_revisado2) as vl_revisado2,sum(vl_revisado3) as vl_revisado3" +
        ",sum(vl_revisado4) as vl_revisado4,sum(vl_revisado5) as vl_revisado5,sum(vl_revisado6) as vl_revisado6" +
        ",sum(vl_revisado7) as vl_revisado7,sum(vl_revisado8) as vl_revisado8,sum(vl_revisado9) as vl_revisado9" +
        ",sum(vl_revisado10) as vl_revisado10,sum(vl_revisado11) as vl_revisado11,sum(vl_revisado12) as vl_revisado12, " +

        "sum(vl_provisionado1) as vl_provisionado1,sum(vl_provisionado2) as vl_provisionado2,sum(vl_provisionado3) as vl_provisionado3" +
        ",sum(vl_provisionado4) as vl_provisionado4,sum(vl_provisionado5) as vl_provisionado5,sum(vl_provisionado6) as vl_provisionado6" +
        ",sum(vl_provisionado7) as vl_provisionado7,sum(vl_provisionado8) as vl_provisionado8,sum(vl_provisionado9) as vl_provisionado9" +
        ",sum(vl_provisionado10) as vl_provisionado10,sum(vl_provisionado11) as vl_provisionado11,sum(vl_provisionado12) as vl_provisionado12, " +

        "sum(vl_empenhado1) as vl_empenhado1,sum(vl_empenhado2) as vl_empenhado2,sum(vl_empenhado3) as vl_empenhado3" +
        ",sum(vl_empenhado4) as vl_empenhado4,sum(vl_empenhado5) as vl_empenhado5,sum(vl_empenhado6) as vl_empenhado6" +
        ",sum(vl_empenhado7) as vl_empenhado7,sum(vl_empenhado8) as vl_empenhado8,sum(vl_empenhado9) as vl_empenhado9" +
        ",sum(vl_empenhado10) as vl_empenhado10,sum(vl_empenhado11) as vl_empenhado11,sum(vl_empenhado12) as vl_empenhado12, " +

        "sum(vl_liquidado1) as vl_liquidado1,sum(vl_liquidado2) as vl_liquidado2,sum(vl_liquidado3) as vl_liquidado3" +
        ",sum(vl_liquidado4) as vl_liquidado4,sum(vl_liquidado5) as vl_liquidado5,sum(vl_liquidado6) as vl_liquidado6" +
        ",sum(vl_liquidado7) as vl_liquidado7,sum(vl_liquidado8) as vl_liquidado8,sum(vl_liquidado9) as vl_liquidado9" +
        ",sum(vl_liquidado10) as vl_liquidado10,sum(vl_liquidado11) as vl_liquidado11,sum(vl_liquidado12) as vl_liquidado12 " +        

        " from t08_acao Inner Join " +
        "t11_financeiro On t11_financeiro.t08_cd_acao = t08_acao.t08_cd_acao Inner Join " +
        "t18b_vlfinanceiro On t18b_vlfinanceiro.t11_cd_financeiro = " +
        "t11_financeiro.t11_cd_financeiro where";
        if (cd_fonte > 0)
        {
            query += " t27_cd_fonte=" + cd_fonte + " and ";
        }
        query += " nu_ano=" + ano +
        " and t08_acao.fl_deletado=0 and t11_financeiro.fl_deletado=0  and t08_acao.t03_cd_projeto=" + cd_projeto;

        return this.GetDataTable(query);
    }

    public DataTable RetrieveFonteCad(int cd_acao)
    {
        string query = "select * from t27_fonte " +
                       "where t27_cd_fonte " +
                       "not in (select t11.t27_cd_fonte from t11_financeiro t11 " +
                       "left join t18b_vlfinanceiro t18 on " +
                       "t18.t11_cd_financeiro=t11.t11_cd_financeiro " +
                       "where t08_cd_acao=" + cd_acao + " " +
                       "and fl_deletado=0) " +
                       "and fl_deletado=0 " +
                       "order by nm_fonte";
        return this.GetDataTable(query);
    }

    public DataTable RetrieveFonteEdit(int cd_acao, int cd_financeiro)
    {
        string query = "select * from t27_fonte " +
                       "where t27_cd_fonte " +
                       "not in (select t11.t27_cd_fonte from t11_financeiro t11 " +
                       "left join t18b_vlfinanceiro t18 on " +
                       "t18.t11_cd_financeiro=t11.t11_cd_financeiro " +
                       "where t08_cd_acao=" + cd_acao + " " +
                       "and t11.t11_cd_financeiro <> " + cd_financeiro + " " +
                       "and fl_deletado=0) " +
                       "and fl_deletado=0 " +
                       "order by nm_fonte ";
        return this.GetDataTable(query);
    }

    public DataTable ListTodosAcoes(int cd_acao)
    {
        string query = "select * from t11_financeiro " +
                       "where fl_deletado=0 and t08_cd_acao = " + cd_acao;

        return this.GetDataTable(query);
    }

}
