using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for t16_agendaAction
/// </summary>
public class t16_agendaAction : SQLServerBase
{
    pageBase pb = new pageBase();
    public t16_agendaAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t16_agenda t16)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t16_agenda (t03_cd_projeto, "+
            "ds_agenda, dt_data, dt_cadastro, dt_alterado, fl_ativa) " +
            "values(@t03_cd_projeto, @ds_agenda, @dt_data, getdate(), getdate(), 1)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t16.t03_cd_projeto),
                MakeInParam("@ds_agenda", SqlDbType.Text, 0, t16.ds_agenda),
                MakeInParam("@dt_data", SqlDbType.DateTime, 0, t16.dt_data)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t16_agenda t16)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t16_agenda set " +
            "ds_agenda=@ds_agenda, dt_data=@dt_data, dt_alterado=getdate() " +
            "where t16_cd_agenda=@t16_cd_agenda";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t16_cd_agenda", SqlDbType.Int, 0, t16.t16_cd_agenda),
                MakeInParam("@ds_agenda", SqlDbType.Text, 0, t16.ds_agenda),
                MakeInParam("@dt_data", SqlDbType.DateTime, 0, t16.dt_data)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "delete from t16_agenda " +
            " where t16_cd_agenda =" + id;

        return this.RunCommand(query);
    }

    public t16_agenda Retrieve(int id)
    {
        string query = "select * from t16_agenda where t16_cd_agenda=" + id;
        DataTable dt = this.GetDataTable(query);
        t16_agenda t16 = new t16_agenda();
        foreach (DataRow dr in dt.Rows)
        {
            t16.t16_cd_agenda = int.Parse(dr["t16_cd_agenda"].ToString());

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t16.t03_cd_projeto = (int)dr["t03_cd_projeto"];

            if (dr["ds_agenda"] != DBNull.Value)
                t16.ds_agenda = dr["ds_agenda"].ToString();

            if (dr["dt_data"] != DBNull.Value)
                t16.dt_data = (DateTime)dr["dt_data"];

            if (dr["dt_cadastro"] != DBNull.Value)
                t16.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t16.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t16;
    }

    public List<t16_agenda> ListObjTodos()
    {
        string query = "select * from t16_agenda order by ds_agenda ";
        DataTable dt = this.GetDataTable(query);
        List<t16_agenda> t16list = new List<t16_agenda>();
        foreach (DataRow dr in dt.Rows)
        {
            t16_agenda t16 = new t16_agenda();
            t16.t16_cd_agenda = int.Parse(dr["t16_cd_agenda"].ToString());
            t16.ds_agenda = dr["ds_agenda"].ToString();
            t16list.Add(t16);
        }
        return t16list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t16_agenda where fl_ativa=1 order by dt_data desc";
        return this.GetDataTable(query);
    }

    public DataTable ListDoProjeto(int cd_projeto)
    {
        string query = "select * from t16_agenda where t03_cd_projeto="+cd_projeto+
            " order by dt_data desc";
        return this.GetDataTable(query);
    }

    public DataTable ListAtivasDoProjeto(int cd_projeto)
    {
        string query = "select * from t16_agenda "+
            " where fl_ativa=1 and t03_cd_projeto=" + cd_projeto + 
            " order by dt_data desc";
        return this.GetDataTable(query);
    }
}
