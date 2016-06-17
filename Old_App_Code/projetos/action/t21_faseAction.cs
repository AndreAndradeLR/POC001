using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t21_faseAction
/// </summary>
public class t21_faseAction: SQLServerBase
{
	public t21_faseAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertDB(t21_fase t21)
    {
        string query = "insert into t21_fase (nm_fase, dt_cadastro, dt_alterado) " +
            "values(@nm_entidade, getdate(), getdate())";

        SqlParameter[] param = new SqlParameter[]
            {
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t21_fase t21)
    {
        string query = "update t21_fase set " +
            "nm_fase@nm_fase, dt_alterado=getdate(), " +
            "where t21_cd_fase=@t21_cd_fase";

        SqlParameter[] param = new SqlParameter[]
            {
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        string query = "update t21_fase set  " +
            " where t21_cd_fase =" + id;

        return this.RunCommand(query);
    }

    public t21_fase Retrieve(int id)
    {
        string query = "select * from t21_fase where t21_cd_fase=" + id;
        DataTable dt = this.GetDataTable(query);
        t21_fase t21 = new t21_fase();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["t21_cd_fase"] != DBNull.Value)
                t21.t21_cd_fase = Convert.ToInt32(dr["t21_cd_fase"]);

            if (dr["nm_fase"] != DBNull.Value)
                t21.nm_fase = dr["nm_fase"].ToString();
           
            if (dr["fl_fase"] != DBNull.Value)
                t21.fl_fase = dr["fl_fase"].ToString();

            break;
        }
        return t21;
    }

    public List<t21_fase> ListObjTodos()
    {
        string query = "select * from t21_fase order by nm_fase";
        DataTable dt = this.GetDataTable(query);
        List<t21_fase> t21list = new List<t21_fase>();
        foreach (DataRow dr in dt.Rows)
        {
            t21_fase t21 = new t21_fase();
            t21list.Add(t21);
        }
        return t21list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t21_fase order by nm_fase";
        return this.GetDataTable(query);
    }
}
