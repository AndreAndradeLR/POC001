using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t05_situacaoAction
/// </summary>
public class t05_situacaoAction : SQLServerBase
{
    public t05_situacaoAction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsertDB(t05_situacao t05)
    {
        string query = "insert into t05_situacao " +
            "(t03_cd_projeto, ds_situacao, dt_cadastro, dt_alterado,fl_deletado) " +
            "values(@t03_cd_projeto, @ds_situacao, getdate(), getdate(),0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t05.t03_cd_projeto),
                MakeInParam("@ds_situacao", SqlDbType.Text, 0, t05.ds_situacao)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t05_situacao t05)
    {
        string query = "update t05_situacao set " +
            "ds_situacao@ds_situacao, dt_alterado=getdate(), " +
            "where t05_cd_situacao=@t05_cd_situacao";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t05_cd_situacao", SqlDbType.Int, 0, t05.t05_cd_situacao),
                MakeInParam("@ds_situacao", SqlDbType.VarChar, 500, t05.ds_situacao),
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        string query = "update t05_situacao set  " +
            " fl_deletado=1 " +
            " where t05_cd_situacao =" + id;

        return this.RunCommand(query);
    }

    public t05_situacao Retrieve(int id)
    {
        string query = "select * from t05_situacao where fl_deletado=0 " +
                       "and t05_cd_situacao=" + id;
        DataTable dt = this.GetDataTable(query);
        t05_situacao t05 = new t05_situacao();
        foreach (DataRow dr in dt.Rows)
        {
            t05.t05_cd_situacao = int.Parse(dr["t05_cd_situacao"].ToString());

            if (dr["ds_situacao"] != DBNull.Value)
                t05.ds_situacao = dr["ds_situacao"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t05.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t05.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t05;
    }
    public t05_situacao RetrieveByProjeto(int cd_projeto)
    {
        string query = "select top 1 * from t05_situacao where t03_cd_projeto=@t03_cd_projeto " +
            " and fl_deletado=0 " +
            "order by t05_cd_situacao desc";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, cd_projeto)
                
            };

        DataTable dt = this.GetDataTable(query, param);
        t05_situacao t05 = new t05_situacao();
        foreach (DataRow dr in dt.Rows)
        {
            t05.t05_cd_situacao = int.Parse(dr["t05_cd_situacao"].ToString());

            if (dr["ds_situacao"] != DBNull.Value)
                t05.ds_situacao = dr["ds_situacao"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t05.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t05.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t05;
    }

    public List<t05_situacao> ListObjTodos()
    {
        string query = "select * from t05_situacao where fl_deletado=0 order by ds_situacao ";
        DataTable dt = this.GetDataTable(query);
        List<t05_situacao> t05list = new List<t05_situacao>();
        foreach (DataRow dr in dt.Rows)
        {
            t05_situacao t05 = new t05_situacao();
            t05.t05_cd_situacao = int.Parse(dr["t05_cd_situacao"].ToString());
            t05.ds_situacao = dr["ds_situacao"].ToString();
            t05list.Add(t05);
        }
        return t05list;
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select * from t05_situacao " +
            "where fl_deletado=0 and t03_cd_projeto=" + cd_projeto + " " +
            "order by dt_cadastro desc";
        return this.GetDataTable(query);
    }

    /// <summary>
    /// p.eduardo.silva - 20130612:
    /// Lista Situacão de acordo com o projeto, e data da situação
    /// </summary>
    /// <param name="cd_projeto">Projeto</param>
    /// <param name="mes">Mês da situação</param>
    /// <param name="ano">Ano da situação</param>
    /// <returns></returns>
    public DataTable ListProjeto(int cd_projeto, int mes = 0, int ano = 0)
    {
        string filter = "";
        if (ano > 0 && mes > 0)
        {
            filter = string.Format(" and [dt_cadastro]>= '{0}-{1}-01 00:00:00.000' ", ano, mes);
        }

        string query = "select * from t05_situacao " +
            "where fl_deletado=0 and t03_cd_projeto=" + cd_projeto +
            filter + " order by dt_cadastro desc";
        return this.GetDataTable(query);
    }

    /// <summary>
    /// p.eduardo.silva - 20130612:
    /// Lista os anos das situacões de acordo com os projetos cadastrados
    /// </summary>
    /// <returns></returns>
    public DataTable ListAnoSituacaoProjeto()
    {
        string query = "SELECT DISTINCT YEAR(dt_cadastro) ANO FROM [t05_situacao] " +
            " where t03_cd_projeto in (SELECT t03_cd_projeto FROM t03_projeto where fl_deletado=0) ORDER BY YEAR(dt_cadastro) DESC ";
        return this.GetDataTable(query);
    }
}
