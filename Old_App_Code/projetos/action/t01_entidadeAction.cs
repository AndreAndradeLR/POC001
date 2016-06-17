using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t01_entidadeActionBD
/// </summary>
public class t01_entidadeAction:SQLServerBase
{
	public t01_entidadeAction()
	{
		
	}

    public int InsertDB(t01_entidade t01)
    {
        string query = "insert into t01_entidade (nm_entidade, dt_cadastro, dt_alterado, fl_deletado) " + 
            "values(@nm_entidade, getdate(), getdate(), 0)";
        
        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@nm_entidade", SqlDbType.VarChar, 500, t01.nm_entidade),
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t01_entidade t01)
    {
        string query = "update t01_entidade set " +
            "nm_entidade=@nm_entidade, dt_alterado=getdate() " +
            "where t01_cd_entidade=@t01_cd_entidade";



        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t01_cd_entidade", SqlDbType.Int, 0, t01.t01_cd_entidade),
                MakeInParam("@nm_entidade", SqlDbType.VarChar, 500, t01.nm_entidade),
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        string query = "update t01_entidade set  " +
            " fl_deletado=1 "+
            " where t01_cd_entidade ="+id;
      
        return this.RunCommand(query);
    }
    
    public t01_entidade Retrieve(int id)
    {
        string query = "select * from t01_entidade where t01_cd_entidade=" + id;
        DataTable dt = this.GetDataTable(query);
        t01_entidade t01 = new t01_entidade();
        foreach (DataRow dr in dt.Rows)
        {
            t01.t01_cd_entidade = int.Parse(dr["t01_cd_entidade"].ToString());

            if (dr["nm_entidade"] != DBNull.Value)
                t01.nm_entidade = dr["nm_entidade"].ToString();
            
            if (dr["dt_cadastro"] != DBNull.Value)
                t01.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t01.dt_alterado = (DateTime)dr["dt_alterado"];

            

            break;
        }
        
        return t01;
    }

    public List<t01_entidade> ListObjTodos()
    {
        string query = "select * from t01_entidade where fl_deletado=0 order by nm_entidade ";
        DataTable dt = this.GetDataTable(query);
        List<t01_entidade> t01list = new List<t01_entidade>();
        foreach (DataRow dr in dt.Rows)
        {
            t01_entidade t01 = new t01_entidade();
            t01.t01_cd_entidade = int.Parse(dr["t01_cd_entidade"].ToString());
            t01.nm_entidade = dr["nm_entidade"].ToString();
            t01list.Add(t01);
        }
        return t01list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t01_entidade where fl_deletado=0 order by nm_entidade";
        return this.GetDataTable(query);
    }
    
}
