using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t13_vlresultadoAction
/// </summary>
public class t13_vlresultadoAction : SQLServerBase
{
	public t13_vlresultadoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t13_vlresultado t13)
    {
        //INSERT INTO t13_vlresultado (t12_cd_resultado, nu_ano, vl_previsto, vl_realizado, dt_cadastro, dt_alterado) VALUES(
        string query = "insert into t13_vlresultado "+
            "(t12_cd_resultado, nu_ano, vl_previsto, vl_realizado, dt_cadastro, dt_alterado) " +
            "values(@t12_cd_resultado, @nu_ano, @vl_previsto, @vl_realizado, getdate(), getdate())";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t12_cd_resultado", SqlDbType.Int, 0, t13.t12_cd_resultado),
                MakeInParam("@nu_ano", SqlDbType.Int, 0, t13.nu_ano),
                MakeInParam("@vl_previsto", SqlDbType.Decimal, 0, t13.vl_previsto),
                MakeInParam("@vl_realizado", SqlDbType.Decimal, 0, t13.vl_realizado)
            };


        return this.RunCommand(query, param);
    }

    private int UpdateDB(t13_vlresultado t13) //método indisponível
    {
        string query = "update t13_vlresultado set " +
            "nm_vlresultado@nm_vlresultado, dt_alterado=getdate(), " +
            "where t13_cd_vlresultado=@t13_cd_vlresultado";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t13_cd_vlresultado", SqlDbType.Int, 0, t13.t13_cd_vlresultado),
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int cd_resultado)
    {
        string query = "delete from t13_vlresultado  " +
            " where t12_cd_resultado =" + cd_resultado;

        return this.RunCommand(query);
    }

    public  t13_vlresultado Retrieve(t12_resultado t12)
    {
        string query = "select * from t13_vlresultado "+
            " where t12_cd_resultado=" + t12.t12_cd_resultado +
            " and nu_ano="+ t12.nu_ano;

        DataTable dt = this.GetDataTable(query);
        t13_vlresultado t13 = new t13_vlresultado();
        foreach (DataRow dr in dt.Rows)
        {
            t13.t13_cd_vlresultado = int.Parse(dr["t13_cd_vlresultado"].ToString());

            if (dr["nu_ano"] != DBNull.Value)
                t13.nu_ano = Convert.ToInt32(dr["nu_ano"]);

            if (dr["vl_previsto"] != DBNull.Value)
                t13.vl_previsto = Convert.ToDecimal(dr["vl_previsto"]);

            if (dr["vl_realizado"] != DBNull.Value)
                t13.vl_realizado = Convert.ToDecimal(dr["vl_realizado"]);

            break;
        }
        return t13;
    }

    public List<t13_vlresultado> ListObjTodos(int cd_resultado)
    {
        string query = "select * from t13_vlresultado "+
              " where t12_cd_resultado =" + cd_resultado +
              " order by nu_ano ";
        DataTable dt = this.GetDataTable(query);
        List<t13_vlresultado> t13list = new List<t13_vlresultado>();
        foreach (DataRow dr in dt.Rows)
        {
            t13_vlresultado t13 = new t13_vlresultado();
            
            t13.t13_cd_vlresultado = int.Parse(dr["t13_cd_vlresultado"].ToString());
            
            if (dr["nu_ano"] != DBNull.Value)
                t13.nu_ano = Convert.ToInt32(dr["nu_ano"]);

            if (dr["vl_previsto"] != DBNull.Value)
                t13.vl_previsto = Convert.ToDecimal(dr["vl_previsto"]);

            if (dr["vl_realizado"] != DBNull.Value)
                t13.vl_realizado = Convert.ToDecimal(dr["vl_realizado"]);

            t13list.Add(t13);
        }
        return t13list;
    }

    public DataTable ListTodos(int cd_resultado)
    {
        string query = "select * from t13_vlresultado "+
            "where t12_cd_resultado =" + cd_resultado +
            " order by nu_ano";
        return this.GetDataTable(query);
    }
    public DataTable ListSomaValores(int dt_inicio,int dt_fim ,int cd_projeto)
    {
        string query = "select sum(vl_previsto+vl_realizado)as valor from t13_vlresultado "+
                       "where t12_cd_resultado in "+
                       "(select t12_cd_resultado from t12_resultado where t03_cd_projeto = " + cd_projeto + " and fl_deletado=0) " +
                       "and nu_ano not between " + dt_inicio + " and " + dt_fim + "";
        return this.GetDataTable(query);
    }
}
