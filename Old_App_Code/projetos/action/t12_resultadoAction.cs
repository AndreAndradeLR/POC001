using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t12_resultadoAction
/// </summary>
public class t12_resultadoAction : SQLServerBase
{
    pageBase pb = new pageBase();
	public t12_resultadoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t12_resultado t12)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "insert into t12_resultado "+
            "(t03_cd_projeto, nm_resultado, ds_resultado,"+
            "nm_medida, nu_ano, vl_t0, fl_acumulado, "+
            "dt_cadastro, dt_alterado, fl_deletado, nu_ordem, nm_respmedicao, nm_fonte) " +
            "values(@t03_cd_projeto, @nm_resultado, @ds_resultado," +
            "@nm_medida, @nu_ano, @vl_t0, @fl_acumulado, " +
            "getdate(), getdate(), 0, @nu_ordem, @nm_respmedicao, @nm_fonte) ";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t12.t03_cd_projeto),
                MakeInParam("@nm_resultado", SqlDbType.VarChar, 500, t12.nm_resultado),
                MakeInParam("@ds_resultado", SqlDbType.Text, 0, t12.ds_resultado),
                MakeInParam("@nm_medida", SqlDbType.VarChar, 200, t12.nm_medida),
                MakeInParam("@nm_respmedicao", SqlDbType.VarChar, 500, t12.nm_respmedicao),
                MakeInParam("@nm_fonte", SqlDbType.VarChar, 500, t12.nm_fonte),
                MakeInParam("@nu_ano", SqlDbType.Int, 0, t12.nu_ano),
                MakeInParam("@vl_t0", SqlDbType.Decimal, 0, t12.vl_t0),
                MakeInParam("@fl_acumulado", SqlDbType.Bit, 0, t12.fl_acumulado),
                MakeInParam("@nu_ordem", SqlDbType.Int, 0, t12.nu_ordem)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t12_resultado t12)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t12_resultado set " +
            "nm_resultado=@nm_resultado, "+
            "ds_resultado=@ds_resultado, "+
            "nm_medida=@nm_medida, " +
            "nu_ano=@nu_ano, " +
            "vl_t0=@vl_t0, " +
            "fl_acumulado=@fl_acumulado, " +
            "dt_alterado=getdate(), " +
            "nu_ordem=@nu_ordem, "+
            "nm_respmedicao=@nm_respmedicao, " +
            "nm_fonte=@nm_fonte " +
            "where t12_cd_resultado=@t12_cd_resultado";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t12_cd_resultado", SqlDbType.Int, 0, t12.t12_cd_resultado),
                MakeInParam("@nm_resultado", SqlDbType.VarChar, 500, t12.nm_resultado),
                MakeInParam("@ds_resultado", SqlDbType.Text, 0, t12.ds_resultado),
                MakeInParam("@nm_medida", SqlDbType.VarChar, 200, t12.nm_medida),
                MakeInParam("@nm_respmedicao", SqlDbType.VarChar, 500, t12.nm_respmedicao),
                MakeInParam("@nm_fonte", SqlDbType.VarChar, 500, t12.nm_fonte),
                MakeInParam("@nu_ano", SqlDbType.Int, 0, t12.nu_ano),                
                MakeInParam("@vl_t0", SqlDbType.Decimal, 0, t12.vl_t0),
                MakeInParam("@fl_acumulado", SqlDbType.Bit, 0, t12.fl_acumulado),
                MakeInParam("@nu_ordem", SqlDbType.Int, 0, t12.nu_ordem)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //atualiza data de atualização do projeto        
        new t03_projetoAction().UpdateAtualizaDB(Convert.ToInt32(pb.Session("cd_projeto")));
        string query = "update t12_resultado set  " +
            " fl_deletado=1 " +
            " where t12_cd_resultado =" + id;

        return this.RunCommand(query);
    }

    public t12_resultado Retrieve(int id)
    {
        string query = "select * from t12_resultado where t12_cd_resultado=" + id ;
        DataTable dt = this.GetDataTable(query);
        t12_resultado t12 = new t12_resultado();
        foreach (DataRow dr in dt.Rows)
        {
            t12.t12_cd_resultado = int.Parse(dr["t12_cd_resultado"].ToString());

            t12.t13 = new t13_vlresultadoAction().ListObjTodos(t12.t12_cd_resultado);

            if (dr["nm_resultado"] != DBNull.Value)
                t12.nm_resultado = dr["nm_resultado"].ToString();

            if (dr["ds_resultado"] != DBNull.Value)
                t12.ds_resultado = dr["ds_resultado"].ToString();

            if (dr["t03_cd_projeto"] != DBNull.Value)
                t12.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);

            if (dr["nm_medida"] != DBNull.Value)
                t12.nm_medida = dr["nm_medida"].ToString();

            if (dr["nu_ano"] != DBNull.Value)
                t12.nu_ano = Convert.ToInt32(dr["nu_ano"]);

            if (dr["vl_t0"] != DBNull.Value)
                t12.vl_t0 = Convert.ToDecimal(dr["vl_t0"]);

            if (dr["fl_acumulado"] != DBNull.Value)
                t12.fl_acumulado = Convert.ToBoolean(dr["fl_acumulado"]);

            if (dr["dt_cadastro"] != DBNull.Value)
                t12.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t12.dt_alterado = (DateTime)dr["dt_alterado"];

            if (dr["nu_ordem"] != DBNull.Value)
                t12.nu_ordem = Convert.ToInt32(dr["nu_ordem"]);

            if (dr["nm_respmedicao"] != DBNull.Value)
                t12.nm_respmedicao = dr["nm_respmedicao"].ToString();

            if (dr["nm_fonte"] != DBNull.Value)
                t12.nm_fonte = dr["nm_fonte"].ToString();

            break;
        }
        return t12;
    }

    public t12_resultado RetrieveMax(int cod_projeto)
    {
        string query = "select MAX(nu_ordem)as ordem from t12_resultado "+
            "where t03_cd_projeto = " + cod_projeto + "";
        DataTable dt = this.GetDataTable(query);
        t12_resultado t12 = new t12_resultado();
        foreach (DataRow dr in dt.Rows)
        {

            if (dr["ordem"] != DBNull.Value)
                t12.nu_ordem = Convert.ToInt32(dr["ordem"]);

            break;
        }
        return t12;
    }

    public int RetrieveIDENTITY(t12_resultado t12)
    {
        string query = "select top 1 * from t12_resultado where nm_resultado='" + t12.nm_resultado +
            "' and fl_deletado=0 order by t12_cd_resultado desc";
        DataTable dt = this.GetDataTable(query);
        int identity = 0;
        foreach (DataRow dr in dt.Rows)
        {
            identity = int.Parse(dr["t12_cd_resultado"].ToString());
            break;
        }
        return identity;
    }   

    public List<t12_resultado> ListObjTodos()
    {
        string query = "select * from t12_resultado where fl_deletado=0 order by t12_cd_resultado desc";
        DataTable dt = this.GetDataTable(query);
        List<t12_resultado> t12list = new List<t12_resultado>();
        foreach (DataRow dr in dt.Rows)
        {
            t12_resultado t12 = new t12_resultado();
            t12.t12_cd_resultado = int.Parse(dr["t12_cd_resultado"].ToString());
            t12.nm_resultado = dr["nm_resultado"].ToString();
            t12list.Add(t12);
        }
        return t12list;
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select * from t12_resultado "+
            " where fl_deletado=0 and t03_cd_projeto="+cd_projeto+
            " order by nu_ordem";
        return this.GetDataTable(query);
    }
}
