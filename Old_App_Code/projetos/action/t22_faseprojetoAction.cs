using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t22_faseprojetoAction
/// </summary>
public class t22_faseprojetoAction: SQLServerBase
{
	public t22_faseprojetoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertDB(t22_faseprojeto t22)
    {
        string query = "INSERT INTO t22_faseprojeto (t21_cd_fase, t03_cd_projeto, dt_cadastro, dt_alterado) " +
            "values(@t21_cd_fase, @t03_cd_projeto, getdate(), getdate())";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t22.t03_cd_projeto),
                MakeInParam("@t21_cd_fase", SqlDbType.Int, 0, t22.t21_cd_fase)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t22_faseprojeto t22)
    {
        string query = "update t22_faseprojeto set " +
            "nm_entidade@nm_entidade, dt_alterado=getdate(), " +
            "where t22_cd_entidade=@t22_cd_entidade";

        SqlParameter[] param = new SqlParameter[]
            {
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        string query = "update t22_faseprojeto set  " +
            " fl_deletado=1 " +
            " where t22_cd_entidade =" + id;

        return this.RunCommand(query);
    }

    public t22_faseprojeto Retrieve(int cd_projeto)
    {
        string query = "select top 1 * from t22_faseprojeto "+
            "where t03_cd_projeto=" + cd_projeto + " order by dt_cadastro desc";
        DataTable dt = this.GetDataTable(query);
        t22_faseprojeto t22 = new t22_faseprojeto();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["t21_cd_fase"] != DBNull.Value)
                t22.t21_cd_fase = Convert.ToInt32(dr["t21_cd_fase"]);

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t22.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);
            break;
        }
        return t22;
    }

    public List<t22_faseprojeto> ListObjTodos()
    {
        string query = "select * from t22_faseprojeto order by nm_entidade ";
        DataTable dt = this.GetDataTable(query);
        List<t22_faseprojeto> t22list = new List<t22_faseprojeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t22_faseprojeto t22 = new t22_faseprojeto();
            t22list.Add(t22);
        }
        return t22list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t22_faseprojeto order by nm_entidade";
        return this.GetDataTable(query);
    }
}
