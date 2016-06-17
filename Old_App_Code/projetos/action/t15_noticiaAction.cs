using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t15_noticiaAction
/// </summary>
public class t15_noticiaAction : SQLServerBase
{
    pageBase pb = new pageBase();
    public t15_noticiaAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t15_noticia t15)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t15_noticia (t03_cd_projeto, " +
            "ds_noticia, dt_data, nm_arquivo, dt_cadastro, dt_alterado, fl_ativa) " +
            "values(@t03_cd_projeto, @ds_noticia, @dt_data, @nm_arquivo, "+
            " getdate(), getdate(), 1)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t15.t03_cd_projeto),
                MakeInParam("@ds_noticia", SqlDbType.Text, 0, t15.ds_noticia),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 500, t15.nm_arquivo),
                MakeInParam("@dt_data", SqlDbType.DateTime, 0, t15.dt_data)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t15_noticia t15)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t15_noticia set " +
            "ds_noticia=@ds_noticia, dt_data=@dt_data, dt_alterado=getdate() " +
            "where t15_cd_noticia=@t15_cd_noticia";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t15_cd_noticia", SqlDbType.Int, 0, t15.t15_cd_noticia),
                MakeInParam("@ds_noticia", SqlDbType.Text, 0, t15.ds_noticia),
                MakeInParam("@dt_data", SqlDbType.DateTime, 0, t15.dt_data),

            };
        return this.RunCommand(query, param);
    }

    public int UpdateUploadDB(t15_noticia t15)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t15_noticia set nm_arquivo=@nm_arquivo, " +
            "ds_noticia=@ds_noticia, dt_data=@dt_data, dt_alterado=getdate() " +
            "where t15_cd_noticia=@t15_cd_noticia";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t15_cd_noticia", SqlDbType.Int, 0, t15.t15_cd_noticia),
                MakeInParam("@ds_noticia", SqlDbType.Text, 0, t15.ds_noticia),
                MakeInParam("@dt_data", SqlDbType.DateTime, 0, t15.dt_data),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 500, t15.nm_arquivo)

            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "delete from t15_noticia " +
            " where t15_cd_noticia =" + id;

        return this.RunCommand(query);
    }

    public t15_noticia Retrieve(int id)
    {
        string query = "select * from t15_noticia where t15_cd_noticia=" + id;
        DataTable dt = this.GetDataTable(query);
        t15_noticia t15 = new t15_noticia();
        foreach (DataRow dr in dt.Rows)
        {
            t15.t15_cd_noticia = int.Parse(dr["t15_cd_noticia"].ToString());

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t15.t03_cd_projeto = (int)dr["t03_cd_projeto"];

            if (dr["ds_noticia"] != DBNull.Value)
                t15.ds_noticia = dr["ds_noticia"].ToString();

            if (dr["dt_data"] != DBNull.Value)
                t15.dt_data = (DateTime)dr["dt_data"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t15.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t15.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t15;
    }

    public List<t15_noticia> ListObjTodos()
    {
        string query = "select * from t15_noticia order by ds_noticia ";
        DataTable dt = this.GetDataTable(query);
        List<t15_noticia> t15list = new List<t15_noticia>();
        foreach (DataRow dr in dt.Rows)
        {
            t15_noticia t15 = new t15_noticia();
            t15.t15_cd_noticia = int.Parse(dr["t15_cd_noticia"].ToString());
            t15.ds_noticia = dr["ds_noticia"].ToString();
            t15list.Add(t15);
        }
        return t15list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t15_noticia where fl_ativa=1 order by dt_data desc";
        return this.GetDataTable(query);
    }

    public DataTable ListDoProjeto(int cd_projeto)
    {
        string query = "select * from t15_noticia where t03_cd_projeto=" + cd_projeto +
            " order by dt_data desc";
        return this.GetDataTable(query);
    }

    public DataTable ListAtivasDoProjeto(int cd_projeto)
    {
        string query = "select * from t15_noticia " +
            " where fl_ativa=1 and t03_cd_projeto=" + cd_projeto +
            " order by dt_data desc";
        return this.GetDataTable(query);
    }
}
