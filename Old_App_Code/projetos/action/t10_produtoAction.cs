using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Text;
/// <summary>
/// Summary description for t10_produtoAction
/// </summary>
public class t10_produtoAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t10_produtoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t10_produto t10)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "INSERT INTO t10_produto (t08_cd_acao, ds_produto, nm_medida, nu_ordem, dt_cadastro, dt_alterado, fl_deletado) " +
            "values(@t08_cd_acao, @ds_produto, @nm_medida, @nu_ordem, getdate(), getdate(), 0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t08_cd_acao", SqlDbType.Int, 0, t10.t08_cd_acao),
                MakeInParam("@nu_ordem", SqlDbType.Int, 0, t10.nu_ordem),
                MakeInParam("@ds_produto", SqlDbType.Text, 0, t10.ds_produto),
                MakeInParam("@nm_medida", SqlDbType.VarChar, 50, t10.nm_medida)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t10_produto t10)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t10_produto set " +
            "ds_produto=@ds_produto,nm_medida=@nm_medida,nu_ordem=@nu_ordem, dt_alterado=getdate() " +
            "where t10_cd_produto=@t10_cd_produto";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t10_cd_produto", SqlDbType.Int, 0, t10.t10_cd_produto),
                MakeInParam("@nu_ordem", SqlDbType.Int, 0, t10.nu_ordem),
                MakeInParam("@ds_produto", SqlDbType.Text, 0, t10.ds_produto),
                MakeInParam("@nm_medida", SqlDbType.VarChar, 50, t10.nm_medida)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t10_produto set  " +
            " fl_deletado=1 " +
            " where t10_cd_produto =" + id;

        return this.RunCommand(query);
    }

    public t10_produto Retrieve(int id)
    {
        string query = "select * from t10_produto where t10_cd_produto=" + id +
                       " and fl_deletado = 0" ;
        DataTable dt = this.GetDataTable(query);
        t10_produto t10 = new t10_produto();
        foreach (DataRow dr in dt.Rows)
        {
            t10.t10_cd_produto = int.Parse(dr["t10_cd_produto"].ToString());

            t10.t17 = new t17_vlprodutoAction().ListObjTodos(t10.t10_cd_produto);

            if (dr["nm_medida"] != DBNull.Value)
                t10.nm_medida = dr["nm_medida"].ToString();

            if (dr["nu_ordem"] != DBNull.Value)
                t10.nu_ordem = int.Parse(dr["nu_ordem"].ToString());

            if (dr["ds_produto"] != DBNull.Value)
                t10.ds_produto = dr["ds_produto"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t10.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t10.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t10;
    }
    public int RetrieveIDENTITY(t10_produto t10)
    {
        string query = "select top 1 * from t10_produto where nm_medida='" + t10.nm_medida +
            "' and t08_cd_acao=" + t10.t08_cd_acao + " and fl_deletado=0 order by t10_cd_produto desc";
        DataTable dt = this.GetDataTable(query);
        int identity = 0;
        foreach (DataRow dr in dt.Rows)
        {
            identity = int.Parse(dr["t10_cd_produto"].ToString());
            break;
        }
        return identity;
    }

    public t10_produto RetrieveMax(int cod_acao)
    {
        string query = "select MAX(nu_ordem)as ordem from t10_produto " +
            "where t08_cd_acao = " + cod_acao; 
        DataTable dt = this.GetDataTable(query);
        t10_produto t10 = new t10_produto();
        foreach (DataRow dr in dt.Rows)
        {

            if (dr["ordem"] != DBNull.Value)
                t10.nu_ordem = Convert.ToInt32(dr["ordem"]);

            break;
        }
        return t10;
    }

    public List<t10_produto> ListObjTodosMetas(int cd_acao)
    {
        string query = "select * from t10_produto " +
            "where t08_cd_acao=" + cd_acao + " and " +
            "fl_deletado=0 ";
        DataTable dt = this.GetDataTable(query);
        List<t10_produto> t10list = new List<t10_produto>();
        foreach (DataRow dr in dt.Rows)
        {
            t10_produto t10 = new t10_produto();
            t10.t10_cd_produto = int.Parse(dr["t10_cd_produto"].ToString());

            t10.t17 = new t17_vlprodutoAction().ListObjTodos(t10.t10_cd_produto);

            if (dr["nm_medida"] != DBNull.Value)
                t10.nm_medida = dr["nm_medida"].ToString();

            if (dr["nu_ordem"] != DBNull.Value)
                t10.nu_ordem = int.Parse(dr["nu_ordem"].ToString());

            if (dr["ds_produto"] != DBNull.Value)
                t10.ds_produto = dr["ds_produto"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t10.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t10.dt_alterado = (DateTime)dr["dt_alterado"];

            //popula relacionamento com valores
            t10.t17 = new t17_vlprodutoAction().ListObjTodos(t10.t10_cd_produto);

            t10list.Add(t10);
        }
        return t10list;
    }

    public List<t10_produto> ListObjTodos(int cd_acao)
    {
        string query = "select * from t10_produto " +
            "where t08_cd_acao=" + cd_acao + " and " +
            "fl_deletado=0 ";
        DataTable dt = this.GetDataTable(query);
        List<t10_produto> t10list = new List<t10_produto>();
        foreach (DataRow dr in dt.Rows)
        {
            t10_produto t10 = new t10_produto();
            t10.t10_cd_produto = int.Parse(dr["t10_cd_produto"].ToString());
            t10.ds_produto = dr["ds_produto"].ToString();
            t10.nm_medida = dr["nm_medida"].ToString();
            t10.t17 = new t17_vlprodutoAction().ListObjTodos(t10.t10_cd_produto);
            t10list.Add(t10);
        }
        return t10list;
    }

    public DataTable ListTodos(int cd_acao)
    {
        string query = "select * from t10_produto "+
            "where t08_cd_acao="+cd_acao+ " and "+
            "fl_deletado=0 order by nu_ordem, "+
            "convert(varchar(50), ds_produto)";
        return this.GetDataTable(query);
    }      

    public DataTable ListTodosAcoes(int cd_acao)
    {
        string query = "select * from t10_produto " +
                       "where fl_deletado=0 and t08_cd_acao = " + cd_acao;

        return this.GetDataTable(query);
    }

}
