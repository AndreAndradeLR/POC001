using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t14_documentoAction
/// </summary>
public class t14_documentoAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t14_documentoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int InsertCrono(t14_documento t14)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t14_documento (t03_cd_projeto, nm_documento, " +
            "nm_arquivo, fl_cronograma, dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t03_cd_projeto, @nm_documento,"+
            "@nm_arquivo, 1, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t14.t03_cd_projeto),
                MakeInParam("@nm_documento", SqlDbType.VarChar, 1000, t14.nm_documento),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 500, t14.nm_arquivo)
            };


        return this.RunCommand(query, param);
    }
    public int InsertDoc(t14_documento t14)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t14_documento (t03_cd_projeto, nm_documento, " +
            "nm_arquivo, fl_outros, dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t03_cd_projeto, @nm_documento,"+
            "@nm_arquivo, 1, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                 MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t14.t03_cd_projeto),
                MakeInParam("@nm_documento", SqlDbType.VarChar, 1000, t14.nm_documento),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 500, t14.nm_arquivo)
            };


        return this.RunCommand(query, param);
    }
    public int InsertFoto(t14_documento t14)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t14_documento (t03_cd_projeto, nm_documento, " +
            "nm_arquivo, fl_foto, dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t03_cd_projeto, @nm_documento,"+
            "@nm_arquivo, 1, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                 MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t14.t03_cd_projeto),
                MakeInParam("@nm_documento", SqlDbType.VarChar, 1000, t14.nm_documento),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 500, t14.nm_arquivo)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t14_documento t14)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t14_documento set " +
            "nm_documento=@nm_documento, " +
            "dt_alterado=getdate(), " +
            "nu_ordem=@nu_ordem " +
            "where t14_cd_documento=@t14_cd_documento";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t14_cd_documento", SqlDbType.Int, 0, t14.t14_cd_documento),
                MakeInParam("@nm_documento", SqlDbType.VarChar, 1000, t14.nm_documento),
                MakeInParam("@nu_ordem", SqlDbType.Int, 0, t14.nu_ordem)
            };
        return this.RunCommand(query, param);
    }
    public int UpdateUploadDB(t14_documento t14)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t14_documento set " +
            " nm_documento=@nm_documento," +
            "nm_arquivo=@nm_arquivo, dt_alterado=getdate() " +
            "where t14_cd_documento=@t14_cd_documento";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t14_cd_documento", SqlDbType.Int, 0, t14.t14_cd_documento),
               
                MakeInParam("@nm_documento", SqlDbType.VarChar, 1000, t14.nm_documento),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 500, t14.nm_arquivo)
            };
        return this.RunCommand(query, param);
    }

    //Update para troca de arquivos

    public int UpdateToOutros(t14_documento t14)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t14_documento set fl_cronograma = 0, fl_foto = 0, fl_outros = 1 " +
            "where t14_cd_documento=@t14_cd_documento";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t14_cd_documento", SqlDbType.Int, 0, t14.t14_cd_documento),                
            };
        return this.RunCommand(query, param);
    }

    public int UpdateToCronograma(t14_documento t14)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t14_documento set fl_cronograma = 1, fl_foto = 0, fl_outros = 0 " +
            "where t14_cd_documento=@t14_cd_documento";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t14_cd_documento", SqlDbType.Int, 0, t14.t14_cd_documento),                
            };
        return this.RunCommand(query, param);
    }

    //

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t14_documento set  " +
            " fl_deletado=1 " +
            " where t14_cd_documento =" + id;

        return this.RunCommand(query);
    }

    public t14_documento Retrieve(int id)
    {
        string query = "select * from t14_documento where t14_cd_documento=" + id;
        DataTable dt = this.GetDataTable(query);
        t14_documento t14 = new t14_documento();
        foreach (DataRow dr in dt.Rows)
        {
            t14.t14_cd_documento = int.Parse(dr["t14_cd_documento"].ToString());

            if (dr["nm_documento"] != DBNull.Value)
                t14.nm_documento = dr["nm_documento"].ToString();

            if (dr["nm_arquivo"] != DBNull.Value)
                t14.nm_arquivo = dr["nm_arquivo"].ToString();


            if (dr["dt_cadastro"] != DBNull.Value)
                t14.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t14.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t14;
    }

    public List<t14_documento> ListObjTodos()
    {
        string query = "select * from t14_documento where fl_deletado=0 order by nm_documento ";
        DataTable dt = this.GetDataTable(query);
        List<t14_documento> t14list = new List<t14_documento>();
        foreach (DataRow dr in dt.Rows)
        {
            t14_documento t14 = new t14_documento();
            t14.t14_cd_documento = int.Parse(dr["t14_cd_documento"].ToString());
            t14.nm_documento = dr["nm_documento"].ToString();
            t14list.Add(t14);
        }
        return t14list;
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select * from t14_documento "+
            " where fl_deletado=0 and t03_cd_projeto=" + cd_projeto + 
            " order by nm_documento";
        return this.GetDataTable(query);
    }

    public DataTable ListFoto(int cd_projeto)
    {
        string query = "select * from t14_documento " +
            " where fl_deletado=0 and t03_cd_projeto=" + cd_projeto +
            " and fl_foto=1 order by nu_ordem, nm_documento";
        return this.GetDataTable(query);
    }

    public DataTable ListFoto3(int cd_projeto)
    {
        string query = "select top 3 * from t14_documento " +
            " where fl_deletado=0 and t03_cd_projeto=" + cd_projeto +
            " and fl_foto=1 order by nu_ordem, nm_documento";
        return this.GetDataTable(query);
    }

    public DataTable ListCrono(int cd_projeto)
    {
        string query = "select * from t14_documento " +
            " where fl_deletado=0 and t03_cd_projeto=" + cd_projeto +
            " and fl_cronograma=1 order by nm_documento";
        return this.GetDataTable(query);
    }
    public DataTable ListDoc(int cd_projeto)
    {
        string query = "select * from t14_documento " +
            " where fl_deletado=0 and t03_cd_projeto=" + cd_projeto +
            " and fl_outros=1 order by nm_documento";
        return this.GetDataTable(query);
    }
}
