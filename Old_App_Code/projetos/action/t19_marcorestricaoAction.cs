using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t19_marcorestricaoAction
/// </summary>
public class t19_marcorestricaoAction : SQLServerBase
{
	public t19_marcorestricaoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t19_marcorestricao t19)
    {
        //INSERT INTO t19_marcorestricao () VALUES(
        string query = "insert into t19_marcorestricao (t09_cd_marco, t07_cd_restricao, dt_cadastro) " +
            "values(@t09_cd_marco, @t07_cd_restricao, getdate())";

        SqlParameter[] param = new SqlParameter[]
            {
                 MakeInParam("@t07_cd_restricao", SqlDbType.Int, 0, t19.t07_cd_restricao),
                  MakeInParam("@t09_cd_marco", SqlDbType.Int, 0, t19.t09_cd_marco)
            };


        return this.RunCommand(query, param);
    }

    private int UpdateDB(t19_marcorestricao t19) //método indisponível
    {
        string query = "update t19_marcorestricao set " +
            "nm_entidade@nm_entidade, dt_alterado=getdate(), " +
            "where t19_cd_entidade=@t19_cd_entidade";

        SqlParameter[] param = new SqlParameter[]
            {
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int cd_restricao)
    {
        string query = "delete from t19_marcorestricao " +
            " where t07_cd_restricao=" + cd_restricao;

        return this.RunCommand(query);
    }

    public t19_marcorestricao Retrieve(int id)
    {
        string query = "select * from t19_marcorestricao where t19_cd_entidade=" + id;
        DataTable dt = this.GetDataTable(query);
        t19_marcorestricao t19 = new t19_marcorestricao();
        foreach (DataRow dr in dt.Rows)
        {


            if (dr["dt_cadastro"] != DBNull.Value)
                t19.dt_cadastro = (DateTime)dr["dt_cadastro"];


            break;
        }
        return t19;
    }

    public List<t19_marcorestricao> ListObjTodos(int cd_restricao)
    {
        string query = "select * from t19_marcorestricao " +
            " where t07_cd_restricao=" + cd_restricao;
        DataTable dt = this.GetDataTable(query);
        List<t19_marcorestricao> t19list = new List<t19_marcorestricao>();
        foreach (DataRow dr in dt.Rows)
        {
            t19_marcorestricao t19 = new t19_marcorestricao();
            t19list.Add(t19);
        }
        return t19list;
    }

    public DataTable ListTodos(int cd_restricao)
    {
        string query = "select * from t19_marcorestricao " +
            " where t07_cd_restricao=" + cd_restricao;
        return this.GetDataTable(query);
    }
    public DataTable ListTodosMarco(int cd_marco)
    {
        string query = "select * from t19_marcorestricao " +
            " where t09_cd_marco=" + cd_marco;
        return this.GetDataTable(query);
    }
}
