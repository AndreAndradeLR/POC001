using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t18b_vlfinanceiroAction
/// </summary>
public class t18b_vlfinanceiroAction : SQLServerBase
{
	public t18b_vlfinanceiroAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t18b_vlfinanceiro t18)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("insert into t18b_vlfinanceiro (t11_cd_financeiro, dt_cadastro, nu_ano");
        sb.Append(", vl_dotorcado, vl_restopagar, vl_assegurado");
        for (int i=1; i <= 12; i++)
        {
            sb.Append(",vl_planejado" + i + ",vl_provisionado" + i +
                ",vl_empenhado" + i + ",vl_liquidado" + i + ",vl_revisado" + i);
        }
        sb.Append(") values(@t11_cd_financeiro, getdate(), @nu_ano ");
        sb.Append(", @vl_dotorcado, @vl_restopagar, @vl_assegurado");
        for (int i = 1; i <= 12; i++)
        {
            sb.Append(",@vl_planejado" + i + ",@vl_provisionado" + i +
                ",@vl_empenhado" + i + ",@vl_liquidado" + i + ",@vl_revisado" + i);
        }
        sb.Append(");");

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t11_cd_financeiro", SqlDbType.Int, 0, t18.t11_cd_financeiro),
                MakeInParam("@nu_ano", SqlDbType.Int, 0, t18.nu_ano),
                MakeInParam("@vl_dotorcado", SqlDbType.Decimal, 0, t18.vl_dotorcado),
                MakeInParam("@vl_restopagar", SqlDbType.Decimal, 0, t18.vl_restopagar),
                MakeInParam("@vl_assegurado", SqlDbType.Decimal, 0, t18.vl_assegurado),
                MakeInParam("@vl_planejado1", SqlDbType.Decimal, 0, t18.vl_planejado1),
                MakeInParam("@vl_planejado2", SqlDbType.Decimal, 0, t18.vl_planejado2),
                MakeInParam("@vl_planejado3", SqlDbType.Decimal, 0, t18.vl_planejado3),
                MakeInParam("@vl_planejado4", SqlDbType.Decimal, 0, t18.vl_planejado4),
                MakeInParam("@vl_planejado5", SqlDbType.Decimal, 0, t18.vl_planejado5),
                MakeInParam("@vl_planejado6", SqlDbType.Decimal, 0, t18.vl_planejado6),
                MakeInParam("@vl_planejado7", SqlDbType.Decimal, 0, t18.vl_planejado7),
                MakeInParam("@vl_planejado8", SqlDbType.Decimal, 0, t18.vl_planejado8),
                MakeInParam("@vl_planejado9", SqlDbType.Decimal, 0, t18.vl_planejado9),
                MakeInParam("@vl_planejado10", SqlDbType.Decimal, 0, t18.vl_planejado10),
                MakeInParam("@vl_planejado11", SqlDbType.Decimal, 0, t18.vl_planejado11),
                MakeInParam("@vl_planejado12", SqlDbType.Decimal, 0, t18.vl_planejado12),
                MakeInParam("@vl_revisado1", SqlDbType.Decimal, 0, t18.vl_revisado1),
                MakeInParam("@vl_revisado2", SqlDbType.Decimal, 0, t18.vl_revisado2),
                MakeInParam("@vl_revisado3", SqlDbType.Decimal, 0, t18.vl_revisado3),
                MakeInParam("@vl_revisado4", SqlDbType.Decimal, 0, t18.vl_revisado4),
                MakeInParam("@vl_revisado5", SqlDbType.Decimal, 0, t18.vl_revisado5),
                MakeInParam("@vl_revisado6", SqlDbType.Decimal, 0, t18.vl_revisado6),
                MakeInParam("@vl_revisado7", SqlDbType.Decimal, 0, t18.vl_revisado7),
                MakeInParam("@vl_revisado8", SqlDbType.Decimal, 0, t18.vl_revisado8),
                MakeInParam("@vl_revisado9", SqlDbType.Decimal, 0, t18.vl_revisado9),
                MakeInParam("@vl_revisado10", SqlDbType.Decimal, 0, t18.vl_revisado10),
                MakeInParam("@vl_revisado11", SqlDbType.Decimal, 0, t18.vl_revisado11),
                MakeInParam("@vl_revisado12", SqlDbType.Decimal, 0, t18.vl_revisado12),
                MakeInParam("@vl_provisionado1", SqlDbType.Decimal, 0, t18.vl_provisionado1),
                MakeInParam("@vl_provisionado2", SqlDbType.Decimal, 0, t18.vl_provisionado2),
                MakeInParam("@vl_provisionado3", SqlDbType.Decimal, 0, t18.vl_provisionado3),
                MakeInParam("@vl_provisionado4", SqlDbType.Decimal, 0, t18.vl_provisionado4),
                MakeInParam("@vl_provisionado5", SqlDbType.Decimal, 0, t18.vl_provisionado5),
                MakeInParam("@vl_provisionado6", SqlDbType.Decimal, 0, t18.vl_provisionado6),
                MakeInParam("@vl_provisionado7", SqlDbType.Decimal, 0, t18.vl_provisionado7),
                MakeInParam("@vl_provisionado8", SqlDbType.Decimal, 0, t18.vl_provisionado8),
                MakeInParam("@vl_provisionado9", SqlDbType.Decimal, 0, t18.vl_provisionado9),
                MakeInParam("@vl_provisionado10", SqlDbType.Decimal, 0, t18.vl_provisionado10),
                MakeInParam("@vl_provisionado11", SqlDbType.Decimal, 0, t18.vl_provisionado11),
                MakeInParam("@vl_provisionado12", SqlDbType.Decimal, 0, t18.vl_provisionado12),
                MakeInParam("@vl_empenhado1", SqlDbType.Decimal, 0, t18.vl_empenhado1),
                MakeInParam("@vl_empenhado2", SqlDbType.Decimal, 0, t18.vl_empenhado2),
                MakeInParam("@vl_empenhado3", SqlDbType.Decimal, 0, t18.vl_empenhado3),
                MakeInParam("@vl_empenhado4", SqlDbType.Decimal, 0, t18.vl_empenhado4),
                MakeInParam("@vl_empenhado5", SqlDbType.Decimal, 0, t18.vl_empenhado5),
                MakeInParam("@vl_empenhado6", SqlDbType.Decimal, 0, t18.vl_empenhado6),
                MakeInParam("@vl_empenhado7", SqlDbType.Decimal, 0, t18.vl_empenhado7),
                MakeInParam("@vl_empenhado8", SqlDbType.Decimal, 0, t18.vl_empenhado8),
                MakeInParam("@vl_empenhado9", SqlDbType.Decimal, 0, t18.vl_empenhado9),
                MakeInParam("@vl_empenhado10", SqlDbType.Decimal, 0, t18.vl_empenhado10),
                MakeInParam("@vl_empenhado11", SqlDbType.Decimal, 0, t18.vl_empenhado11),
                MakeInParam("@vl_empenhado12", SqlDbType.Decimal, 0, t18.vl_empenhado12),
                MakeInParam("@vl_liquidado1", SqlDbType.Decimal, 0, t18.vl_liquidado1),
                MakeInParam("@vl_liquidado2", SqlDbType.Decimal, 0, t18.vl_liquidado2),
                MakeInParam("@vl_liquidado3", SqlDbType.Decimal, 0, t18.vl_liquidado3),
                MakeInParam("@vl_liquidado4", SqlDbType.Decimal, 0, t18.vl_liquidado4),
                MakeInParam("@vl_liquidado5", SqlDbType.Decimal, 0, t18.vl_liquidado5),
                MakeInParam("@vl_liquidado6", SqlDbType.Decimal, 0, t18.vl_liquidado6),
                MakeInParam("@vl_liquidado7", SqlDbType.Decimal, 0, t18.vl_liquidado7),
                MakeInParam("@vl_liquidado8", SqlDbType.Decimal, 0, t18.vl_liquidado8),
                MakeInParam("@vl_liquidado9", SqlDbType.Decimal, 0, t18.vl_liquidado9),
                MakeInParam("@vl_liquidado10", SqlDbType.Decimal, 0, t18.vl_liquidado10),
                MakeInParam("@vl_liquidado11", SqlDbType.Decimal, 0, t18.vl_liquidado11),
                MakeInParam("@vl_liquidado12", SqlDbType.Decimal, 0, t18.vl_liquidado12)
                
            };

        return this.RunCommand(sb.ToString(), param);
    }

    public int DeleteDB(int cd_financeiro)
    {
        string query = "delete from t18b_vlfinanceiro  " +
            " where t11_cd_financeiro =" + cd_financeiro;

        return this.RunCommand(query);
    }

    public List<t18b_vlfinanceiro> ListObjTodos(int cd_financeiro)
    {
        string query = "select * from t18b_vlfinanceiro " +
            " where t11_cd_financeiro =" + cd_financeiro + " order by nu_ano";
        DataTable dt = this.GetDataTable(query);
        List<t18b_vlfinanceiro> t18list = new List<t18b_vlfinanceiro>();
        foreach (DataRow dr in dt.Rows)
        {
            t18b_vlfinanceiro t18 = new t18b_vlfinanceiro();
            t18.t18b_cd_vlfinanceiro = int.Parse(dr["t18b_cd_vlfinanceiro"].ToString());

            if (dr["t11_cd_financeiro"] != DBNull.Value)
                t18.t11_cd_financeiro = int.Parse(dr["t11_cd_financeiro"].ToString());

            if (dr["dt_cadastro"] != DBNull.Value)
                t18.dt_cadastro = Convert.ToDateTime(dr["dt_cadastro"]);

            if (dr["nu_ano"] != DBNull.Value)
                t18.nu_ano = Convert.ToInt32(dr["nu_ano"]);

            if (dr["vl_dotorcado"] != DBNull.Value)
                t18.vl_dotorcado = Convert.ToDecimal(dr["vl_dotorcado"]);

            if (dr["vl_restopagar"] != DBNull.Value)
                t18.vl_restopagar = Convert.ToDecimal(dr["vl_restopagar"]);

            if (dr["vl_assegurado"] != DBNull.Value)
                t18.vl_assegurado = Convert.ToDecimal(dr["vl_assegurado"]);


            if (dr["vl_planejado1"] != DBNull.Value)
                t18.vl_planejado1 = Convert.ToDecimal(dr["vl_planejado1"]);

            if (dr["vl_planejado2"] != DBNull.Value)
                t18.vl_planejado2 = Convert.ToDecimal(dr["vl_planejado2"]);

            if (dr["vl_planejado3"] != DBNull.Value)
                t18.vl_planejado3 = Convert.ToDecimal(dr["vl_planejado3"]);

            if (dr["vl_planejado4"] != DBNull.Value)
                t18.vl_planejado4 = Convert.ToDecimal(dr["vl_planejado4"]);

            if (dr["vl_planejado5"] != DBNull.Value)
                t18.vl_planejado5 = Convert.ToDecimal(dr["vl_planejado5"]);

            if (dr["vl_planejado6"] != DBNull.Value)
                t18.vl_planejado6 = Convert.ToDecimal(dr["vl_planejado6"]);

            if (dr["vl_planejado7"] != DBNull.Value)
                t18.vl_planejado7 = Convert.ToDecimal(dr["vl_planejado7"]);

            if (dr["vl_planejado8"] != DBNull.Value)
                t18.vl_planejado8 = Convert.ToDecimal(dr["vl_planejado8"]);

            if (dr["vl_planejado9"] != DBNull.Value)
                t18.vl_planejado9 = Convert.ToDecimal(dr["vl_planejado9"]);

            if (dr["vl_planejado10"] != DBNull.Value)
                t18.vl_planejado10 = Convert.ToDecimal(dr["vl_planejado10"]);

            if (dr["vl_planejado11"] != DBNull.Value)
                t18.vl_planejado11 = Convert.ToDecimal(dr["vl_planejado11"]);

            if (dr["vl_planejado12"] != DBNull.Value)
                t18.vl_planejado12 = Convert.ToDecimal(dr["vl_planejado12"]);


            if (dr["vl_revisado1"] != DBNull.Value)
                t18.vl_revisado1 = Convert.ToDecimal(dr["vl_revisado1"]);

            if (dr["vl_revisado2"] != DBNull.Value)
                t18.vl_revisado2 = Convert.ToDecimal(dr["vl_revisado2"]);

            if (dr["vl_revisado3"] != DBNull.Value)
                t18.vl_revisado3 = Convert.ToDecimal(dr["vl_revisado3"]);

            if (dr["vl_revisado4"] != DBNull.Value)
                t18.vl_revisado4 = Convert.ToDecimal(dr["vl_revisado4"]);

            if (dr["vl_revisado5"] != DBNull.Value)
                t18.vl_revisado5 = Convert.ToDecimal(dr["vl_revisado5"]);

            if (dr["vl_revisado6"] != DBNull.Value)
                t18.vl_revisado6 = Convert.ToDecimal(dr["vl_revisado6"]);

            if (dr["vl_revisado7"] != DBNull.Value)
                t18.vl_revisado7 = Convert.ToDecimal(dr["vl_revisado7"]);

            if (dr["vl_revisado8"] != DBNull.Value)
                t18.vl_revisado8 = Convert.ToDecimal(dr["vl_revisado8"]);

            if (dr["vl_revisado9"] != DBNull.Value)
                t18.vl_revisado9 = Convert.ToDecimal(dr["vl_revisado9"]);

            if (dr["vl_revisado10"] != DBNull.Value)
                t18.vl_revisado10 = Convert.ToDecimal(dr["vl_revisado10"]);

            if (dr["vl_revisado11"] != DBNull.Value)
                t18.vl_revisado11 = Convert.ToDecimal(dr["vl_revisado11"]);

            if (dr["vl_revisado12"] != DBNull.Value)
                t18.vl_revisado12 = Convert.ToDecimal(dr["vl_revisado12"]);


            if (dr["vl_provisionado1"] != DBNull.Value)
                t18.vl_provisionado1 = Convert.ToDecimal(dr["vl_provisionado1"]);

            if (dr["vl_provisionado2"] != DBNull.Value)
                t18.vl_provisionado2 = Convert.ToDecimal(dr["vl_provisionado2"]);

            if (dr["vl_provisionado3"] != DBNull.Value)
                t18.vl_provisionado3 = Convert.ToDecimal(dr["vl_provisionado3"]);

            if (dr["vl_provisionado4"] != DBNull.Value)
                t18.vl_provisionado4 = Convert.ToDecimal(dr["vl_provisionado4"]);

            if (dr["vl_provisionado5"] != DBNull.Value)
                t18.vl_provisionado5 = Convert.ToDecimal(dr["vl_provisionado5"]);

            if (dr["vl_provisionado6"] != DBNull.Value)
                t18.vl_provisionado6 = Convert.ToDecimal(dr["vl_provisionado6"]);

            if (dr["vl_provisionado7"] != DBNull.Value)
                t18.vl_provisionado7 = Convert.ToDecimal(dr["vl_provisionado7"]);

            if (dr["vl_provisionado8"] != DBNull.Value)
                t18.vl_provisionado8 = Convert.ToDecimal(dr["vl_provisionado8"]);

            if (dr["vl_provisionado9"] != DBNull.Value)
                t18.vl_provisionado9 = Convert.ToDecimal(dr["vl_provisionado9"]);

            if (dr["vl_provisionado10"] != DBNull.Value)
                t18.vl_provisionado10 = Convert.ToDecimal(dr["vl_provisionado10"]);

            if (dr["vl_provisionado11"] != DBNull.Value)
                t18.vl_provisionado11 = Convert.ToDecimal(dr["vl_provisionado11"]);

            if (dr["vl_provisionado12"] != DBNull.Value)
                t18.vl_provisionado12 = Convert.ToDecimal(dr["vl_provisionado12"]);


            if (dr["vl_empenhado1"] != DBNull.Value)
                t18.vl_empenhado1 = Convert.ToDecimal(dr["vl_empenhado1"]);

            if (dr["vl_empenhado2"] != DBNull.Value)
                t18.vl_empenhado2 = Convert.ToDecimal(dr["vl_empenhado2"]);

            if (dr["vl_empenhado3"] != DBNull.Value)
                t18.vl_empenhado3 = Convert.ToDecimal(dr["vl_empenhado3"]);

            if (dr["vl_empenhado4"] != DBNull.Value)
                t18.vl_empenhado4 = Convert.ToDecimal(dr["vl_empenhado4"]);

            if (dr["vl_empenhado5"] != DBNull.Value)
                t18.vl_empenhado5 = Convert.ToDecimal(dr["vl_empenhado5"]);

            if (dr["vl_empenhado6"] != DBNull.Value)
                t18.vl_empenhado6 = Convert.ToDecimal(dr["vl_empenhado6"]);

            if (dr["vl_empenhado7"] != DBNull.Value)
                t18.vl_empenhado7 = Convert.ToDecimal(dr["vl_empenhado7"]);

            if (dr["vl_empenhado8"] != DBNull.Value)
                t18.vl_empenhado8 = Convert.ToDecimal(dr["vl_empenhado8"]);

            if (dr["vl_empenhado9"] != DBNull.Value)
                t18.vl_empenhado9 = Convert.ToDecimal(dr["vl_empenhado9"]);

            if (dr["vl_empenhado10"] != DBNull.Value)
                t18.vl_empenhado10 = Convert.ToDecimal(dr["vl_empenhado10"]);

            if (dr["vl_empenhado11"] != DBNull.Value)
                t18.vl_empenhado11 = Convert.ToDecimal(dr["vl_empenhado11"]);

            if (dr["vl_empenhado12"] != DBNull.Value)
                t18.vl_empenhado12 = Convert.ToDecimal(dr["vl_empenhado12"]);



            if (dr["vl_liquidado1"] != DBNull.Value)
                t18.vl_liquidado1 = Convert.ToDecimal(dr["vl_liquidado1"]);

            if (dr["vl_liquidado2"] != DBNull.Value)
                t18.vl_liquidado2 = Convert.ToDecimal(dr["vl_liquidado2"]);

            if (dr["vl_liquidado3"] != DBNull.Value)
                t18.vl_liquidado3 = Convert.ToDecimal(dr["vl_liquidado3"]);

            if (dr["vl_liquidado4"] != DBNull.Value)
                t18.vl_liquidado4 = Convert.ToDecimal(dr["vl_liquidado4"]);

            if (dr["vl_liquidado5"] != DBNull.Value)
                t18.vl_liquidado5 = Convert.ToDecimal(dr["vl_liquidado5"]);

            if (dr["vl_liquidado6"] != DBNull.Value)
                t18.vl_liquidado6 = Convert.ToDecimal(dr["vl_liquidado6"]);

            if (dr["vl_liquidado7"] != DBNull.Value)
                t18.vl_liquidado7 = Convert.ToDecimal(dr["vl_liquidado7"]);

            if (dr["vl_liquidado8"] != DBNull.Value)
                t18.vl_liquidado8 = Convert.ToDecimal(dr["vl_liquidado8"]);

            if (dr["vl_liquidado9"] != DBNull.Value)
                t18.vl_liquidado9 = Convert.ToDecimal(dr["vl_liquidado9"]);

            if (dr["vl_liquidado10"] != DBNull.Value)
                t18.vl_liquidado10 = Convert.ToDecimal(dr["vl_liquidado10"]);

            if (dr["vl_liquidado11"] != DBNull.Value)
                t18.vl_liquidado11 = Convert.ToDecimal(dr["vl_liquidado11"]);

            if (dr["vl_liquidado12"] != DBNull.Value)
                t18.vl_liquidado12 = Convert.ToDecimal(dr["vl_liquidado12"]);

            
            t18list.Add(t18);
        }
        return t18list;
    }


    public List<t18b_vlfinanceiro> ListObjTotal(int cd_acao)
    {
        string query = "select  " +
           "sum(vl_planejado1+vl_planejado2+vl_planejado3+vl_planejado4+vl_planejado5+vl_planejado6+vl_planejado7+vl_planejado8+vl_planejado9+vl_planejado10+vl_planejado11+vl_planejado12) as vl_planejado, " +
           "sum(vl_liquidado1+vl_liquidado2+vl_liquidado3+vl_liquidado4+vl_liquidado5+vl_liquidado6+vl_liquidado7+vl_liquidado8+vl_liquidado9+vl_liquidado10+vl_liquidado11+vl_liquidado12) as vl_liquidado " +
           "from t18b_vlfinanceiro t18 " +
           "left join t11_financeiro t11 on t11.t11_cd_financeiro=t18.t11_cd_financeiro " +
           "where t11.t08_cd_acao=" + cd_acao +
           " and t11.fl_deletado=0";
        DataTable dt = this.GetDataTable(query);
        List<t18b_vlfinanceiro> t18list = new List<t18b_vlfinanceiro>();
        foreach (DataRow dr in dt.Rows)
        {
            t18b_vlfinanceiro t18 = new t18b_vlfinanceiro();
            if (dr["vl_planejado"] != DBNull.Value)
                t18.vl_planejado1 = Convert.ToDecimal(dr["vl_planejado"]);

            if (dr["vl_liquidado"] != DBNull.Value)
                t18.vl_liquidado1 = Convert.ToDecimal(dr["vl_liquidado"]);

            t18list.Add(t18);
        }
        return t18list;
    }
    public List<t18b_vlfinanceiro> ListObjProjetoTotal(int cd_projeto)
    {
        string query = "select " +
           "sum(vl_planejado1+vl_planejado2+vl_planejado3+vl_planejado4+vl_planejado5+vl_planejado6+vl_planejado7+vl_planejado8+vl_planejado9+vl_planejado10+vl_planejado11+vl_planejado12) as vl_planejado, " +
           "sum(vl_liquidado1+vl_liquidado2+vl_liquidado3+vl_liquidado4+vl_liquidado5+vl_liquidado6+vl_liquidado7+vl_liquidado8+vl_liquidado9+vl_liquidado10+vl_liquidado11+vl_liquidado12) as vl_liquidado " +
           "from t18b_vlfinanceiro t18 " +
           "left join t11_financeiro t11 on t11.t11_cd_financeiro=t18.t11_cd_financeiro " +
           "left join t08_acao t08 on t08.t08_cd_acao=t11.t08_cd_acao " +
           "where (t08.t03_cd_projeto=" + cd_projeto + " and t08.fl_deletado=0) and t11.fl_deletado=0";
        DataTable dt = this.GetDataTable(query);
        List<t18b_vlfinanceiro> t18list = new List<t18b_vlfinanceiro>();
        foreach (DataRow dr in dt.Rows)
        {
            t18b_vlfinanceiro t18 = new t18b_vlfinanceiro();
            if (dr["vl_planejado"] != DBNull.Value)
                t18.vl_planejado1 = Convert.ToDecimal(dr["vl_planejado"]);

            if (dr["vl_liquidado"] != DBNull.Value)
                t18.vl_liquidado1 = Convert.ToDecimal(dr["vl_liquidado"]);

            t18list.Add(t18);
        }
        return t18list;
    }

    public DataTable ListTable() { 
        string query="select * from t18_vlfinanceiro order by t18_cd_vlfinanceiro";
        return this.GetDataTable(query);
    }

    public DataTable RelatorioExcel(int ano)
    {

        //StringBuilder sb = new StringBuilder();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("select t26.nm_area,t03.nm_projeto,t08.nm_acao,");

        sb.Append("t27.nm_fonte,t18b.nu_ano,t18b.vl_dotorcado,t18b.vl_assegurado,t18b.vl_restopagar,");

        sb.Append("t18b.vl_planejado1,t18b.vl_planejado2,t18b.vl_planejado3,t18b.vl_planejado4,t18b.vl_planejado5,");

        sb.Append("t18b.vl_planejado6,t18b.vl_planejado7,t18b.vl_planejado8,t18b.vl_planejado9,t18b.vl_planejado10,");

        sb.Append("t18b.vl_planejado11,t18b.vl_planejado12,");


        sb.Append("t18b.vl_provisionado1,t18b.vl_provisionado2,t18b.vl_provisionado3,t18b.vl_provisionado4,t18b.vl_provisionado5,");

        sb.Append("t18b.vl_provisionado6,t18b.vl_provisionado7,t18b.vl_provisionado8,t18b.vl_provisionado9,t18b.vl_provisionado10,");

        sb.Append("t18b.vl_provisionado11,t18b.vl_provisionado12,");


        sb.Append("t18b.vl_empenhado1,t18b.vl_empenhado2,t18b.vl_empenhado3,t18b.vl_empenhado4,t18b.vl_empenhado5,");

        sb.Append("t18b.vl_empenhado6,t18b.vl_empenhado7,t18b.vl_empenhado8,t18b.vl_empenhado9,t18b.vl_empenhado10,");

        sb.Append("t18b.vl_empenhado11,t18b.vl_empenhado12,");


        sb.Append("t18b.vl_liquidado1,t18b.vl_liquidado2,t18b.vl_liquidado3,t18b.vl_liquidado4,t18b.vl_liquidado5,");

        sb.Append("t18b.vl_liquidado6,t18b.vl_liquidado7,t18b.vl_liquidado8,t18b.vl_liquidado9,t18b.vl_liquidado10,");

        sb.Append("t18b.vl_liquidado11,t18b.vl_liquidado12,");


        sb.Append(" t18b.vl_revisado1,t18b.vl_revisado2,t18b.vl_revisado3,t18b.vl_revisado4,t18b.vl_revisado5, ");

        sb.Append("t18b.vl_revisado6,t18b.vl_revisado7,t18b.vl_revisado8,t18b.vl_revisado9,t18b.vl_revisado10, ");

        sb.Append("t18b.vl_revisado11,t18b.vl_revisado12 ");


        sb.Append("from t03_projeto t03 ");

        sb.Append("left join t26_arearesultado t26 ");

        sb.Append("on t26.t26_cd_arearesultado = t03.t26_cd_arearesultado ");

        sb.Append("left join t08_acao t08 ");

        sb.Append("on t08.t03_cd_projeto=t03.t03_cd_projeto ");

        sb.Append("left join t11_financeiro t11 ");

        sb.Append("on t11.t08_cd_acao=t08.t08_cd_acao ");

        sb.Append("left join t27_fonte t27 ");

        sb.Append("on t27.t27_cd_fonte=t11.t27_cd_fonte ");

        sb.Append("left join t18b_vlfinanceiro t18b ");

        sb.Append("on t18b.t11_cd_financeiro=t11.t11_cd_financeiro ");

        sb.Append("where t03.fl_deletado = 0 and t08.fl_deletado = 0 ");

        if (ano > 0)
        {
            sb.Append("and t18b.nu_ano = " + ano + " ");
        }

        sb.Append("and t27.fl_deletado = 0 and t11.fl_deletado = 0 ");

        sb.Append("order by t26.nm_area,t03.nm_projeto,t08.nm_acao ");        

        return this.GetDataTable(sb.ToString());

    }
 
}
