using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t08_acaoAction
/// </summary>
public class t08_acaoAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t08_acaoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t08_acao t08)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t08_acao (t03_cd_projeto, t02_cd_usuario, nm_acao, " +
            "ds_acao, dt_inicio, dt_fim, dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t03_cd_projeto, @t02_cd_usuario, @nm_acao, " +
            "@ds_acao, @dt_inicio, @dt_fim, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t08.t03_cd_projeto),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t08.t02_cd_usuario),
                MakeInParam("@nm_acao", SqlDbType.VarChar, 500, t08.nm_acao),
                MakeInParam("@ds_acao", SqlDbType.Text, 0, t08.ds_acao),
                MakeInParam("@dt_inicio", SqlDbType.DateTime, 0, t08.dt_inicio),
                MakeInParam("@dt_fim", SqlDbType.DateTime, 0, t08.dt_fim)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t08_acao t08)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t08_acao set " +
            "nm_acao=@nm_acao, t02_cd_usuario=@t02_cd_usuario, " +
            "ds_acao=@ds_acao, dt_inicio=@dt_inicio, dt_fim=@dt_fim, dt_alterado=getdate() " +
            "where t08_cd_acao=@t08_cd_acao";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t08_cd_acao", SqlDbType.Int, 0, t08.t08_cd_acao),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t08.t02_cd_usuario),
                MakeInParam("@nm_acao", SqlDbType.VarChar, 500, t08.nm_acao),
                MakeInParam("@ds_acao", SqlDbType.Text,0, t08.ds_acao),
                MakeInParam("@dt_inicio", SqlDbType.DateTime, 0, t08.dt_inicio),
                MakeInParam("@dt_fim", SqlDbType.DateTime, 0, t08.dt_fim)
            };
        return this.RunCommand(query, param);
    }

    public int UpdateAlteracao(int id_acao)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t08_acao set " +
            "dt_alterado=getdate() " +
            "where t08_cd_acao=" + id_acao;
        return this.RunCommand(query);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t08_acao set  " +
            " fl_deletado=1 " +
            " where t08_cd_acao =" + id;

        return this.RunCommand(query);
    }

    public t08_acao Retrieve(int id)
    {
        string query = "select * from t08_acao where t08_cd_acao=" + id;
        DataTable dt = this.GetDataTable(query);
        t08_acao t08 = new t08_acao();
        foreach (DataRow dr in dt.Rows)
        {
            t08.t08_cd_acao = int.Parse(dr["t08_cd_acao"].ToString());

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t08.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t08.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["nm_acao"] != DBNull.Value)
                t08.nm_acao = dr["nm_acao"].ToString();

            if (dr["ds_acao"] != DBNull.Value)
                t08.ds_acao = dr["ds_acao"].ToString();

            if (dr["dt_inicio"] != DBNull.Value)
                t08.dt_inicio = (DateTime)dr["dt_inicio"];

            if (dr["dt_fim"] != DBNull.Value)
                t08.dt_fim = (DateTime)dr["dt_fim"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t08.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t08.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t08.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t08;
    }

    public int RetrieveDatas(string inicio, string fim, int cd_projeto)
    {
        int result = 0;
        string query = "select * from t08_acao where t03_cd_projeto = @t03_cd_projeto " +
                       "and (dt_inicio < @dt_inicio "+
                       "or dt_fim > @dt_fim) and fl_deletado=0";
        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, cd_projeto),
                MakeInParam("@dt_inicio", SqlDbType.DateTime, 0, DateTime.Parse(inicio)),
                MakeInParam("@dt_fim", SqlDbType.DateTime, 0, DateTime.Parse(fim))
            };
        DataTable dt = this.GetDataTable(query, param);
        t08_acao t08 = new t08_acao();
        foreach (DataRow dr in dt.Rows)
        {
            t08.t08_cd_acao = int.Parse(dr["t08_cd_acao"].ToString());
            result = 1;
            break;
        }
        return result;
    }    

    public List<t08_acao> ListObjTodos(int cd_projeto)
    {
        string query = "select * from t08_acao "+
            " where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 order by dt_inicio desc ";
        DataTable dt = this.GetDataTable(query);
        List<t08_acao> t08list = new List<t08_acao>();
        foreach (DataRow dr in dt.Rows)
        {
            t08_acao t08 = new t08_acao();
            t08.t08_cd_acao = int.Parse(dr["t08_cd_acao"].ToString());
            t08.nm_acao = dr["nm_acao"].ToString();
            t08list.Add(t08);
        }
        return t08list;
    }

    public List<t08_acao> ListTodasAcoesComMetaVinculadas(int cd_projeto, int codPai)
    {
        string query = "select * from t08_acao t08" +
                       " inner join t29_acoes_vinculadas_projeto t29" +
                       " on t29.t08_cd_acao = t08.t08_cd_acao" +
                       " where t08.t08_cd_acao in (select t08_cd_acao from t10_produto where fl_deletado=0)" +
                       " and t29.t03_cd_projeto=" + cd_projeto +
                       " and t08.fl_deletado = 0 " +
                       " and t29.fl_deletado = 0 order by t08.nm_acao ";

        DataTable dt = this.GetDataTable(query);
        List<t08_acao> t08list = new List<t08_acao>();

        foreach (DataRow dr in dt.Rows)
        {
            if (codPai == int.Parse(dr["t03_cd_projeto"].ToString()))
            {

                t08_acao t08 = new t08_acao();
                t08.t08_cd_acao = int.Parse(dr["t08_cd_acao"].ToString());

                if (dr["t03_cd_projeto"] != DBNull.Value)
                {
                    t08.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);
                }

                if (dr["t02_cd_usuario"] != DBNull.Value)
                {
                    t08.t02_cd_usuario = dr["t02_cd_usuario"].ToString();
                }

                if (dr["nm_acao"] != DBNull.Value)
                {
                    t08.nm_acao = dr["nm_acao"].ToString();
                }

                if (dr["ds_acao"] != DBNull.Value)
                {
                    t08.ds_acao = dr["ds_acao"].ToString();
                }

                if (dr["dt_inicio"] != DBNull.Value)
                {
                    t08.dt_inicio = (DateTime)dr["dt_inicio"];
                }

                if (dr["dt_fim"] != DBNull.Value)
                {
                    t08.dt_fim = (DateTime)dr["dt_fim"];
                }

                if (dr["dt_cadastro"] != DBNull.Value)
                {
                    t08.dt_cadastro = (DateTime)dr["dt_cadastro"];
                }

                if (dr["dt_cadastro"] != DBNull.Value)
                {
                    t08.dt_cadastro = (DateTime)dr["dt_cadastro"];
                }

                if (dr["dt_alterado"] != DBNull.Value)
                {
                    t08.dt_alterado = (DateTime)dr["dt_alterado"];
                }

                t08list.Add(t08);
            }
        }

        return t08list;
    }

    public List<t08_acao> ListTodasAcoesComMeta(int cd_projeto, List<string> filtros)
    {
        string CodResponsavel = "";
        if (filtros[2] != "")
            CodResponsavel = " and t08.t02_cd_usuario ='" + filtros[2] + "'";

        string query = "select t02.nm_nome as nm_responsavel, t08.* from t08_acao t08 left join t02_usuario t02 on t02.t02_cd_usuario=t08.t02_cd_usuario " +
            " where t08.t03_cd_projeto=" + cd_projeto + CodResponsavel +
            " and t08.fl_deletado=0 order by t08.nm_acao ";
        DataTable dt = this.GetDataTable(query);
        List<t08_acao> t08list = new List<t08_acao>();

        foreach (DataRow dr in dt.Rows)
        {
            t08_acao t08 = new t08_acao();
            t08.t08_cd_acao = int.Parse(dr["t08_cd_acao"].ToString());

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t08.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t08.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["nm_responsavel"] != DBNull.Value)
                t08.nm_responsavel = dr["nm_responsavel"].ToString();

            if (dr["nm_acao"] != DBNull.Value)
                t08.nm_acao = dr["nm_acao"].ToString();

            if (dr["ds_acao"] != DBNull.Value)
                t08.ds_acao = dr["ds_acao"].ToString();

            if (dr["dt_inicio"] != DBNull.Value)
                t08.dt_inicio = (DateTime)dr["dt_inicio"];

            if (dr["dt_fim"] != DBNull.Value)
                t08.dt_fim = (DateTime)dr["dt_fim"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t08.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t08.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t08.dt_alterado = (DateTime)dr["dt_alterado"];

            t08list.Add(t08);
        }
        return t08list;
    }

    public List<t08_acao> ListObjTodosMeta(int cd_projeto)
    {
        string query = "select * from t08_acao " +
            " where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 order by nm_acao ";
        DataTable dt = this.GetDataTable(query);
        List<t08_acao> t08list = new List<t08_acao>();
        foreach (DataRow dr in dt.Rows)
        {
            t08_acao t08 = new t08_acao();
            t08.t08_cd_acao = int.Parse(dr["t08_cd_acao"].ToString());
            t08.nm_acao = dr["nm_acao"].ToString();
            t08list.Add(t08);
        }
        return t08list;
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select * from t08_acao "+
            " where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 order by dt_inicio desc";
        return this.GetDataTable(query);
    }
    public DataTable ListTodosDtInicioAsc(int cd_projeto)
    {
        string query = "select * from t08_acao " +
            " where t03_cd_projeto=" + cd_projeto +
            " and fl_deletado=0 order by dt_inicio asc";
        return this.GetDataTable(query);
    }

    public DataTable ListSomaValoresProduto(int ano_inicio,int ano_fim,int cd_acao)
    {
        string query = "select sum(vl_p1+vl_p2+vl_p3+vl_p4+vl_p5+vl_p6+vl_p7+vl_p8+vl_p9+vl_p10+vl_p11+vl_p12+ " +
                        "vl_r1+vl_r2+vl_r3+vl_r4+vl_r5+vl_r6+vl_r7+vl_r8+vl_r9+vl_r10+vl_r11+vl_r12)as valor " +
                        "from t17_vlproduto where t10_cd_produto in " +
                        "(select t10_cd_produto from t10_produto where t08_cd_acao = " + cd_acao + " and fl_deletado=0) " +
                        "and nu_ano not between " + ano_inicio + " and " + ano_fim + "";
        return this.GetDataTable(query);
    }

    public DataTable ListSomaValoresFinanceiro(int ano_inicio, int ano_fim, int cd_acao)
    {
        string query = "select " +
        "sum(vl_restopagar+vl_dotorcado+vl_assegurado+ " +

        "vl_planejado1+vl_planejado2+vl_planejado3+vl_planejado4+ " +
        "vl_planejado5+vl_planejado6+vl_planejado7+vl_planejado8+vl_planejado9+vl_planejado10+ " +
        "vl_planejado11+vl_planejado12+ " +

        "vl_provisionado1+vl_provisionado2+vl_provisionado3+ " +
        "vl_provisionado4+vl_provisionado5+vl_provisionado6+vl_provisionado7+ " +
        "vl_provisionado8+vl_provisionado9+vl_provisionado10+vl_provisionado11+ " +
        "vl_provisionado12+ " +

        "vl_empenhado1+vl_empenhado2+vl_empenhado3+vl_empenhado4+ " +
        "vl_empenhado5+vl_empenhado6+vl_empenhado7+vl_empenhado8+vl_empenhado9+vl_empenhado10+ " +
        "vl_empenhado11+vl_empenhado12+ " +

        "vl_liquidado1+vl_liquidado2+vl_liquidado3+ " +
        "vl_liquidado4+vl_liquidado5+vl_liquidado6+vl_liquidado7+vl_liquidado8+vl_liquidado9+ " +
        "vl_liquidado10+vl_liquidado11+vl_liquidado12+ " +

        "vl_revisado1+vl_revisado2+vl_revisado3+vl_revisado4+ " +
        "vl_revisado5+vl_revisado6+vl_revisado7+vl_revisado8+vl_revisado9+vl_revisado10+vl_revisado11+vl_revisado12+ " +

        "vl_empenhado1+vl_empenhado2+vl_empenhado3+vl_empenhado4+vl_empenhado5+ " +
        "vl_empenhado6+vl_empenhado7+vl_empenhado8+vl_empenhado9+vl_empenhado10+ " +
        "vl_empenhado11+vl_empenhado12) as vlt_ano  " +

        "from t18b_vlfinanceiro  " +
        "where t11_cd_financeiro in (select t11_cd_financeiro from t11_financeiro  " +
        "where t08_cd_acao = " + cd_acao + " and fl_deletado=0) " +
        "and nu_ano not between " + ano_inicio + " and " + ano_fim;

        return this.GetDataTable(query);
    }

    /// <summary>
    /// p.eduardo.silva - 20130705:
    /// Lista Responsáveis das ações
    /// </summary>
    /// <param name="cd_projeto">Projeto</param>
    /// <param name="cd_arearesultado">Área de Resultado</param>
    /// <returns></returns>
    internal object ListResponsavel()
    {
        string query = "SELECT DISTINCT t08.[t02_cd_usuario] ,t02.[nm_nome] " +
                        "  FROM [dbBH].[dbo].[t08_acao] t08 LEFT JOIN t02_usuario t02 ON t02.t02_cd_usuario=t08.t02_cd_usuario " +
                        "  WHERE t08.fl_deletado = 0 AND t02.nm_nome IS NOT NULL order by t02.[nm_nome]";
        return this.GetDataTable(query);
    }
}
