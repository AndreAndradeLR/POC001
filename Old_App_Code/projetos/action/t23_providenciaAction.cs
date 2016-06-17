using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for t23_providenciaAction
/// </summary>
public class t23_providenciaAction:SQLServerBase
{
    pageBase pb = new pageBase();
    public int InsertDB(t23_providencia t23)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "INSERT INTO t23_providencia (t07_cd_restricao, t02_cd_usuario, ds_providencia, fl_gerente, dt_cadastro, dt_limite)  " +
            "values(@t07_cd_restricao, @t02_cd_usuario, @ds_providencia, @fl_gerente, getdate(), @dt_limite)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t07_cd_restricao", SqlDbType.Int, 0, t23.t07_cd_restricao),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t23.t02_cd_usuario),
                MakeInParam("@fl_gerente", SqlDbType.Bit, 0, t23.fl_gerente),
                MakeInParam("@ds_providencia", SqlDbType.Text, 0, t23.ds_providencia),
                MakeInParam("@dt_limite", SqlDbType.DateTime, 0, t23.dt_limite)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t23_providencia t23) //método indisponível 
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t23_providencia set " +
            "ds_providencia=@ds_providencia, t02_cd_usuario=@t02_cd_usuario, dt_limite=@dt_limite " +
            "where t23_cd_providencia=@t23_cd_providencia";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t23_cd_providencia", SqlDbType.Int, 0, t23.t23_cd_providencia),
                MakeInParam("@ds_providencia", SqlDbType.Text, 0, t23.ds_providencia),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t23.t02_cd_usuario),
                MakeInParam("@dt_limite", SqlDbType.DateTime, 0, t23.dt_limite)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "delete  from t23_providencia " +
            " where t23_cd_providencia =" + id;

        return this.RunCommand(query);
    }

    public t23_providencia Retrieve(int cd_providencia)
    {
        string query = "select * from t23_providencia where t23_cd_providencia=" + cd_providencia + "";            
        DataTable dt = this.GetDataTable(query);
        t23_providencia t23 = new t23_providencia();
        foreach (DataRow dr in dt.Rows)
        {
            t23.t23_cd_providencia = int.Parse(dr["t23_cd_providencia"].ToString());

            if (dr["ds_providencia"] != DBNull.Value)
                t23.ds_providencia = dr["ds_providencia"].ToString();
            if (dr["t02_cd_usuario"] != DBNull.Value)
                t23.t02_cd_usuario = dr["t02_cd_usuario"].ToString();
            if (dr["fl_gerente"] != DBNull.Value)
                t23.fl_gerente = Convert.ToBoolean(dr["fl_gerente"]);
            if (dr["dt_limite"] != DBNull.Value)
                t23.dt_limite = (DateTime)dr["dt_limite"];
            if (dr["dt_cadastro"] != DBNull.Value)
                t23.dt_cadastro = (DateTime)dr["dt_cadastro"];
            break;
        }
        return t23;
    }

    public t23_providencia RetrieveTop1(int cd_restricao)
    {
        string query = "select top 1 * from t23_providencia " +
            "where t07_cd_restricao=" + cd_restricao + "  order by dt_cadastro desc";
        DataTable dt = this.GetDataTable(query);
        t23_providencia t23 = new t23_providencia();
        foreach (DataRow dr in dt.Rows)
        {
            t23.t23_cd_providencia = int.Parse(dr["t23_cd_providencia"].ToString());

            if (dr["ds_providencia"] != DBNull.Value)
                t23.ds_providencia = dr["ds_providencia"].ToString();
            if (dr["t02_cd_usuario"] != DBNull.Value)
                t23.t02_cd_usuario = dr["t02_cd_usuario"].ToString();
            if (dr["fl_gerente"] != DBNull.Value)
                t23.fl_gerente = Convert.ToBoolean(dr["fl_gerente"]);
            if (dr["dt_limite"] != DBNull.Value)
                t23.dt_limite = (DateTime)dr["dt_limite"];
            if (dr["dt_cadastro"] != DBNull.Value)
                t23.dt_cadastro = (DateTime)dr["dt_cadastro"];
            break;
        }
        return t23;
    }

    public List<t23_providencia> ListObjTodos(int cd_restricao)
    {
        string query = "select * from t23_providencia "+
            "where t07_cd_restricao=" + cd_restricao + " order by dt_cadastro desc";
        DataTable dt = this.GetDataTable(query);
        List<t23_providencia> t23list = new List<t23_providencia>();
        foreach (DataRow dr in dt.Rows)
        {
            t23_providencia t23 = new t23_providencia();
            t23.t23_cd_providencia = int.Parse(dr["t23_cd_providencia"].ToString());

            if (dr["ds_providencia"] != DBNull.Value)
                t23.ds_providencia = dr["ds_providencia"].ToString();
            if (dr["t02_cd_usuario"] != DBNull.Value)
                t23.t02_cd_usuario = dr["t02_cd_usuario"].ToString();
            if (dr["fl_gerente"] != DBNull.Value)
                t23.fl_gerente = Convert.ToBoolean(dr["fl_gerente"]);
            if (dr["dt_limite"] != DBNull.Value)
                t23.dt_limite = (DateTime)dr["dt_limite"];
            if (dr["dt_cadastro"] != DBNull.Value)
                t23.dt_cadastro = (DateTime)dr["dt_cadastro"];
            t23list.Add(t23);
        }
        return t23list;
    }   

    public DataTable ListTodos(int cd_restricao)
    {
        string query = "select * from t23_providencia " +
            "where t07_cd_restricao=" + cd_restricao + " order by dt_cadastro desc";
        return this.GetDataTable(query);
    }
}
