using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t04_parceiroAction
/// </summary>
public class t04_parceiroAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t04_parceiroAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t04_parceiro t04)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "INSERT INTO t04_parceiro (t03_cd_projeto, t01_cd_entidade, nm_nome, ds_atuacao, dt_cadastro, dt_alterado) " +
            "values(@t03_cd_projeto, @t01_cd_entidade, @nm_nome, @ds_atuacao, getdate(), getdate())";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t04.t03_cd_projeto),
                MakeInParam("@t01_cd_entidade", SqlDbType.Int, 0, t04.t01_cd_entidade),
                MakeInParam("@nm_nome", SqlDbType.VarChar, 200, t04.nm_nome),
                MakeInParam("@ds_atuacao", SqlDbType.Text, 0, t04.ds_atuacao)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t04_parceiro t04)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t04_parceiro set " +
            "nm_nome=@nm_nome, "+
            "t01_cd_entidade=@t01_cd_entidade, " +
            "ds_atuacao=@ds_atuacao, " +
            "dt_alterado=getdate() " +
            "where t04_cd_parceiro=@t04_cd_parceiro";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t04_cd_parceiro", SqlDbType.Int, 0, t04.t04_cd_parceiro),
                MakeInParam("@t01_cd_entidade", SqlDbType.Int, 0, t04.t01_cd_entidade),
                MakeInParam("@nm_nome", SqlDbType.VarChar, 200, t04.nm_nome),
                MakeInParam("@ds_atuacao", SqlDbType.Text, 0, t04.ds_atuacao)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "delete from t04_parceiro " +
            " where t04_cd_parceiro=" + id;

        return this.RunCommand(query);
    }

    public t04_parceiro Retrieve(int id)
    {
        string query = "select * from t04_parceiro where t04_cd_parceiro=" + id;
        DataTable dt = this.GetDataTable(query);
        t04_parceiro t04 = new t04_parceiro();
        foreach (DataRow dr in dt.Rows)
        {
            t04.t04_cd_parceiro = (int)dr["t04_cd_parceiro"];

            if (dr["nm_nome"] != DBNull.Value)
                t04.nm_nome = dr["nm_nome"].ToString();
            
            if (dr["t01_cd_entidade"] != DBNull.Value)
                t04.t01_cd_entidade = (int)dr["t01_cd_entidade"];
            
            if (dr["t03_cd_projeto"] != DBNull.Value)
                t04.t03_cd_projeto = (int)dr["t03_cd_projeto"];

            if (dr["ds_atuacao"] != DBNull.Value)
                t04.ds_atuacao = dr["ds_atuacao"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t04.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t04.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t04;
    }

    public List<t04_parceiro> ListObjTodos(int cd_projeto)
    {
        string query = "select t4.*, t1.nm_entidade from t04_parceiro t4 "+
            "left join t01_entidade t1 on t1.t01_cd_entidade=t4.t01_cd_entidade "+
            " order by t1.nm_entidade ";
        DataTable dt = this.GetDataTable(query);
        List<t04_parceiro> t04list = new List<t04_parceiro>();
        foreach (DataRow dr in dt.Rows)
        {
            t04_parceiro t04 = new t04_parceiro();
            t04.t04_cd_parceiro = int.Parse(dr["t04_cd_parceiro"].ToString());
            t04.nm_nome = dr["nm_nome"].ToString();
            t04list.Add(t04);
        }
        return t04list;
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select t4.*, t1.nm_entidade from t04_parceiro t4 " +
            "left join t01_entidade t1 on t1.t01_cd_entidade=t4.t01_cd_entidade " +
            "where t4.t03_cd_projeto="+ cd_projeto + " order by t1.nm_entidade ";
        return this.GetDataTable(query);
    }
}
