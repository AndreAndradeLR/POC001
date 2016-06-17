using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t28_documentoAction
/// </summary>
public class t28_documentoAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t28_documentoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertDocumento(t28_documento t28)
    {
        //atualiza data de atualização da ação e do projeto        
        new t08_acaoAction().UpdateAlteracao(Convert.ToInt32(pb.Session("cd_acao")));
        string query = "insert into t28_documento (t08_cd_acao, nm_documento, " +
            "nm_arquivo, dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t08_cd_acao, @nm_documento,"+
            "@nm_arquivo, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t08_cd_acao", SqlDbType.Int, 0, t28.t08_cd_acao),
                MakeInParam("@nm_documento", SqlDbType.VarChar, 1000, t28.nm_documento),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 500, t28.nm_arquivo)
            };


        return this.RunCommand(query, param);
    }    

    public int UpdateDB(t28_documento t28)
    {
        //atualiza data de atualização da ação e do projeto        
        new t08_acaoAction().UpdateAlteracao(Convert.ToInt32(pb.Session("cd_acao")));
        string query = "update t28_documento set " +
            "nm_documento=@nm_documento, " +
            "dt_alterado=getdate() " +
            "where t28_cd_documento=@t28_cd_documento";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t28_cd_documento", SqlDbType.Int, 0, t28.t28_cd_documento),
                MakeInParam("@nm_documento", SqlDbType.VarChar, 1000, t28.nm_documento),
            };
        return this.RunCommand(query, param);
    }
    public int UpdateUploadDB(t28_documento t28)
    {
        //atualiza data de atualização da ação e do projeto        
        new t08_acaoAction().UpdateAlteracao(Convert.ToInt32(pb.Session("cd_acao")));
        string query = "update t28_documento set " +
            " nm_documento=@nm_documento," +
            "nm_arquivo=@nm_arquivo, dt_alterado=getdate() " +
            "where t28_cd_documento=@t28_cd_documento";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t28_cd_documento", SqlDbType.Int, 0, t28.t28_cd_documento),
               
                MakeInParam("@nm_documento", SqlDbType.VarChar, 1000, t28.nm_documento),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 500, t28.nm_arquivo)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização da ação e do projeto        
        new t08_acaoAction().UpdateAlteracao(Convert.ToInt32(pb.Session("cd_acao")));
        string query = "update t28_documento set  " +
            " fl_deletado=1 " +
            " where t28_cd_documento =" + id;

        return this.RunCommand(query);
    }

    public t28_documento Retrieve(int id)
    {
        string query = "select * from t28_documento where t28_cd_documento=" + id;
        DataTable dt = this.GetDataTable(query);
        t28_documento t28 = new t28_documento();
        foreach (DataRow dr in dt.Rows)
        {
            t28.t28_cd_documento = int.Parse(dr["t28_cd_documento"].ToString());

            if (dr["nm_documento"] != DBNull.Value)
                t28.nm_documento = dr["nm_documento"].ToString();

            if (dr["nm_arquivo"] != DBNull.Value)
                t28.nm_arquivo = dr["nm_arquivo"].ToString();


            if (dr["dt_cadastro"] != DBNull.Value)
                t28.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t28.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t28;
    }

    public List<t28_documento> ListObjTodos()
    {
        string query = "select * from t28_documento where fl_deletado=0 order by nm_documento ";
        DataTable dt = this.GetDataTable(query);
        List<t28_documento> t28list = new List<t28_documento>();
        foreach (DataRow dr in dt.Rows)
        {
            t28_documento t28 = new t28_documento();
            t28.t28_cd_documento = int.Parse(dr["t28_cd_documento"].ToString());
            t28.nm_documento = dr["nm_documento"].ToString();
            t28list.Add(t28);
        }
        return t28list;
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select * from t28_documento "+
            " where fl_deletado=0 and t08_cd_acao=" + cd_projeto + 
            " order by nm_documento";
        return this.GetDataTable(query);
    }    
}
