using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t20_acessoAction
/// </summary>
public class t20_acessoAction : SQLServerBase
{
	public t20_acessoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t20_acesso t20)
    {
        string query = "INSERT INTO t20_acesso (t02_cd_usuario, nm_ip, dt_data)  " +
            "VALUES(@t02_cd_usuario, @nm_ip, getdate())";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t20.t02_cd_usuario),
                MakeInParam("@nm_ip", SqlDbType.VarChar, 100, t20.nm_ip)
            };


        return this.RunCommand(query, param);
    }


    public List<t20_acesso> ListObjTodos()
    {
        string query = "select * from t20_acesso order by dt_data desc";
        DataTable dt = this.GetDataTable(query);
        List<t20_acesso> t20list = new List<t20_acesso>();
        foreach (DataRow dr in dt.Rows)
        {
            t20_acesso t20 = new t20_acesso();
            t20list.Add(t20);
        }
        return t20list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t20_acesso order by dt_data desc";
        return this.GetDataTable(query);
    }
}
