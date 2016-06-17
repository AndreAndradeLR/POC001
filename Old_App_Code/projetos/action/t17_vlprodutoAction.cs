using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t17_vlprodutoAction
/// </summary>
public class t17_vlprodutoAction : SQLServerBase
{
	public t17_vlprodutoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t17_vlproduto t17)
    {
        string query = "INSERT INTO t17_vlproduto (t10_cd_produto, nu_ano, vl_p1, vl_p2,"+
            " vl_p3, vl_p4, vl_p5, vl_p6, vl_p7, vl_p8, vl_p9, vl_p10, vl_p11, vl_p12, "+
            "vl_r1, vl_r2, vl_r3, vl_r4, vl_r5, vl_r6, vl_r7, vl_r8, vl_r9, vl_r10, vl_r11, vl_r12," +
            " dt_cadastro, dt_alterado) " +
            "values(@t10_cd_produto, @nu_ano, @vl_p1, @vl_p2, @vl_p3, @vl_p4, @vl_p5, "+
            "@vl_p6, @vl_p7, @vl_p8, @vl_p9, @vl_p10, @vl_p11, @vl_p12, "+
            "@vl_r1, @vl_r2, @vl_r3, @vl_r4, @vl_r5, @vl_r6, @vl_r7, @vl_r8, @vl_r9, @vl_r10, @vl_r11, @vl_r12," +
            " getdate(), getdate())";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t10_cd_produto", SqlDbType.Int, 0, t17.t10_cd_produto),
                MakeInParam("@nu_ano", SqlDbType.Int, 0, t17.nu_ano),
                MakeInParam("@vl_p1", SqlDbType.Decimal, 0, t17.vl_p1),
                MakeInParam("@vl_p2", SqlDbType.Decimal, 0, t17.vl_p2),
                MakeInParam("@vl_p3", SqlDbType.Decimal, 0, t17.vl_p3),
                MakeInParam("@vl_p4", SqlDbType.Decimal, 0, t17.vl_p4),
                MakeInParam("@vl_p5", SqlDbType.Decimal, 0, t17.vl_p5),
                MakeInParam("@vl_p6", SqlDbType.Decimal, 0, t17.vl_p6),
                MakeInParam("@vl_p7", SqlDbType.Decimal, 0, t17.vl_p7),
                MakeInParam("@vl_p8", SqlDbType.Decimal, 0, t17.vl_p8),
                MakeInParam("@vl_p9", SqlDbType.Decimal, 0, t17.vl_p9),
                MakeInParam("@vl_p10", SqlDbType.Decimal, 0, t17.vl_p10),
                MakeInParam("@vl_p11", SqlDbType.Decimal, 0, t17.vl_p11),
                MakeInParam("@vl_p12", SqlDbType.Decimal, 0, t17.vl_p12),
                MakeInParam("@vl_r1", SqlDbType.Decimal, 0, t17.vl_r1),
                MakeInParam("@vl_r2", SqlDbType.Decimal, 0, t17.vl_r2),
                MakeInParam("@vl_r3", SqlDbType.Decimal, 0, t17.vl_r3),
                MakeInParam("@vl_r4", SqlDbType.Decimal, 0, t17.vl_r4),
                MakeInParam("@vl_r5", SqlDbType.Decimal, 0, t17.vl_r5),
                MakeInParam("@vl_r6", SqlDbType.Decimal, 0, t17.vl_r6),
                MakeInParam("@vl_r7", SqlDbType.Decimal, 0, t17.vl_r7),
                MakeInParam("@vl_r8", SqlDbType.Decimal, 0, t17.vl_r8),
                MakeInParam("@vl_r9", SqlDbType.Decimal, 0, t17.vl_r9),
                MakeInParam("@vl_r10", SqlDbType.Decimal, 0, t17.vl_r10),
                MakeInParam("@vl_r11", SqlDbType.Decimal, 0, t17.vl_r11),
                MakeInParam("@vl_r12", SqlDbType.Decimal, 0, t17.vl_r12)
            };


        return this.RunCommand(query, param);
    }

    private int UpdateDB(t17_vlproduto t17)
    {
        string query = "update t17_vlproduto set " +
            "nm_vlproduto@nm_vlproduto, dt_alterado=getdate(), " +
            "where t17_cd_vlproduto=@t17_cd_vlproduto";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t17_cd_vlproduto", SqlDbType.Int, 0, t17.t17_cd_vlproduto),
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int cd_produto)
    {
        string query = "delete from t17_vlproduto " +
            " where t10_cd_produto="+ cd_produto;

        return this.RunCommand(query);
    }

    public t17_vlproduto Retrieve(int id)
    {
        string query = "select * from t17_vlproduto where t17_cd_vlproduto=" + id;
        DataTable dt = this.GetDataTable(query);
        t17_vlproduto t17 = new t17_vlproduto();
        foreach (DataRow dr in dt.Rows)
        {
            t17.t17_cd_vlproduto = int.Parse(dr["t17_cd_vlproduto"].ToString());

            if (dr["nu_ano"] != DBNull.Value)
                t17.nu_ano = Convert.ToInt32(dr["nu_ano"]);

            if (dr["dt_cadastro"] != DBNull.Value)
                t17.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t17.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t17;
    }

    public List<t17_vlproduto> ListObjTodos(int cd_produto)
    {
        string query = "select * from t17_vlproduto "+
            "where t10_cd_produto=" + cd_produto + " order by nu_ano";
        DataTable dt = this.GetDataTable(query);
        List<t17_vlproduto> t17list = new List<t17_vlproduto>();
        foreach (DataRow dr in dt.Rows)
        {
            t17_vlproduto t17 = new t17_vlproduto();
            t17.t17_cd_vlproduto = int.Parse(dr["t17_cd_vlproduto"].ToString());

            t17.t10_cd_produto = int.Parse(dr["t10_cd_produto"].ToString());
            
            if (dr["nu_ano"] != DBNull.Value)
                t17.nu_ano = Convert.ToInt32(dr["nu_ano"]);

            if (dr["vl_p1"] != DBNull.Value)
                t17.vl_p1 = Convert.ToDecimal(dr["vl_p1"]);

            if (dr["vl_p2"] != DBNull.Value)
                t17.vl_p2 = Convert.ToDecimal(dr["vl_p2"]);

            if (dr["vl_p3"] != DBNull.Value)
                t17.vl_p3 = Convert.ToDecimal(dr["vl_p3"]);

            if (dr["vl_p4"] != DBNull.Value)
                t17.vl_p4 = Convert.ToDecimal(dr["vl_p4"]);

            if (dr["vl_p5"] != DBNull.Value)
                t17.vl_p5 = Convert.ToDecimal(dr["vl_p5"]);

            if (dr["vl_p6"] != DBNull.Value)
                t17.vl_p6 = Convert.ToDecimal(dr["vl_p6"]);

            if (dr["vl_p7"] != DBNull.Value)
                t17.vl_p7 = Convert.ToDecimal(dr["vl_p7"]);

            if (dr["vl_p8"] != DBNull.Value)
                t17.vl_p8 = Convert.ToDecimal(dr["vl_p8"]);

            if (dr["vl_p9"] != DBNull.Value)
                t17.vl_p9 = Convert.ToDecimal(dr["vl_p9"]);
            
            if (dr["vl_p10"] != DBNull.Value)
                t17.vl_p10 = Convert.ToDecimal(dr["vl_p10"]);

            if (dr["vl_p11"] != DBNull.Value)
                t17.vl_p11 = Convert.ToDecimal(dr["vl_p11"]);

            if (dr["vl_p12"] != DBNull.Value)
                t17.vl_p12 = Convert.ToDecimal(dr["vl_p12"]);

            if (dr["vl_r1"] != DBNull.Value)
                t17.vl_r1 = Convert.ToDecimal(dr["vl_r1"]);

            if (dr["vl_r2"] != DBNull.Value)
                t17.vl_r2 = Convert.ToDecimal(dr["vl_r2"]);

            if (dr["vl_r3"] != DBNull.Value)
                t17.vl_r3 = Convert.ToDecimal(dr["vl_r3"]);

            if (dr["vl_r4"] != DBNull.Value)
                t17.vl_r4 = Convert.ToDecimal(dr["vl_r4"]);

            if (dr["vl_r5"] != DBNull.Value)
                t17.vl_r5 = Convert.ToDecimal(dr["vl_r5"]);

            if (dr["vl_r6"] != DBNull.Value)
                t17.vl_r6 = Convert.ToDecimal(dr["vl_r6"]);

            if (dr["vl_r7"] != DBNull.Value)
                t17.vl_r7 = Convert.ToDecimal(dr["vl_r7"]);

            if (dr["vl_r8"] != DBNull.Value)
                t17.vl_r8 = Convert.ToDecimal(dr["vl_r8"]);

            if (dr["vl_r9"] != DBNull.Value)
                t17.vl_r9 = Convert.ToDecimal(dr["vl_r9"]);

            if (dr["vl_r10"] != DBNull.Value)
                t17.vl_r10 = Convert.ToDecimal(dr["vl_r10"]);

            if (dr["vl_r11"] != DBNull.Value)
                t17.vl_r11 = Convert.ToDecimal(dr["vl_r11"]);

            if (dr["vl_r12"] != DBNull.Value)
                t17.vl_r12 = Convert.ToDecimal(dr["vl_r12"]);

            t17list.Add(t17);
        }
        return t17list;
    }

    public DataTable ListTodos(int cd_produto)
    {
        string query = "select * from t17_vlproduto " +
            "where t10_cd_produto=" + cd_produto + " order by nu_ano";
        return this.GetDataTable(query);
    }
}
