using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

/// <summary>
/// Summary description for t20_acessoAction
/// </summary>
public class t29_temp_vlFinanceiroAction : SQLServerBase
{
    public t29_temp_vlFinanceiroAction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsertDB(t29_temp_vlFinanceiro t29)
    {
        try
        {
            string query = "INSERT INTO t29_temp_vlFinanceiro " +
                "(t03_cd_projeto,CodRelatorio, nm_fonte, nm_unidade, " +
                " PlanejadoMes, RevisadoMes, RealizadoMes, EmpenhadoMes, LiquidadoMes, " +
                " PlanejadoAcu, RevisadoAcu, RealizadoAcu, EmpenhadoAcu, LiquidadoAcu, " +
                " PlanejadoTot, RevisadoTot, RealizadoTot, EmpenhadoTot, LiquidadoTot, " +
                "dt_cadastro, dt_alterado) " +
                "VALUES(@t03_cd_projeto,@CodRelatorio, @nm_fonte, @nm_unidade, " +
                " @PlanejadoMes, @RevisadoMes, @RealizadoMes, @EmpenhadoMes, @LiquidadoMes, " +
                " @PlanejadoAcu, @RevisadoAcu, @RealizadoAcu, @EmpenhadoAcu, @LiquidadoAcu, " +
                " @PlanejadoTot, @RevisadoTot, @RealizadoTot, @EmpenhadoTot, @LiquidadoTot, " +
                "getdate(), getdate())";

            SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t29.t03_cd_projeto),
                MakeInParam("@CodRelatorio", SqlDbType.Int, 0, t29.CodRelatorio),
                MakeInParam("@nm_fonte", SqlDbType.VarChar, 50, t29.nm_fonte),
                MakeInParam("@nm_unidade", SqlDbType.VarChar, 50, t29.nm_unidade),
                
                MakeInParam("@PlanejadoMes", SqlDbType.Decimal, 0, t29.PlanejadoMes),
                MakeInParam("@RevisadoMes", SqlDbType.Decimal, 0, t29.RevisadoMes),
                MakeInParam("@RealizadoMes", SqlDbType.Decimal, 0, t29.RealizadoMes),
                MakeInParam("@EmpenhadoMes", SqlDbType.Decimal, 0, t29.EmpenhadoMes),
                MakeInParam("@LiquidadoMes", SqlDbType.Decimal, 0, t29.LiquidadoMes),

                MakeInParam("@PlanejadoAcu", SqlDbType.Decimal, 0, t29.PlanejadoAcu),
                MakeInParam("@RevisadoAcu", SqlDbType.Decimal, 0, t29.RevisadoAcu),
                MakeInParam("@RealizadoAcu", SqlDbType.Decimal, 0, t29.RealizadoAcu),
                MakeInParam("@EmpenhadoAcu", SqlDbType.Decimal, 0, t29.EmpenhadoAcu),
                MakeInParam("@LiquidadoAcu", SqlDbType.Decimal, 0, t29.LiquidadoAcu),

                MakeInParam("@PlanejadoTot", SqlDbType.Decimal, 0, t29.PlanejadoTot),
                MakeInParam("@RevisadoTot", SqlDbType.Decimal, 0, t29.RevisadoTot),
                MakeInParam("@RealizadoTot", SqlDbType.Decimal, 0, t29.RealizadoTot),
                MakeInParam("@EmpenhadoTot", SqlDbType.Decimal, 0, t29.EmpenhadoTot),
                MakeInParam("@LiquidadoTot", SqlDbType.Decimal, 0, t29.LiquidadoTot)

            };

            return this.RunCommand(query, param);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public int UpdateDB(t29_temp_vlFinanceiro t29)
    {
        try
        {
            string query = "update t29_temp_vlFinanceiro set nm_unidade=@nm_unidade," +
                " PlanejadoMes=@PlanejadoMes, RevisadoMes=@RevisadoMes, RealizadoMes=@RealizadoMes, EmpenhadoMes=@EmpenhadoMes, LiquidadoMes=@LiquidadoMes, " +
                " PlanejadoAcu=@PlanejadoAcu, RevisadoAcu=@RevisadoAcu, RealizadoAcu=@RealizadoAcu, EmpenhadoAcu=@EmpenhadoAcu, LiquidadoAcu=@LiquidadoAcu, " +
                " PlanejadoTot=@PlanejadoTot, RevisadoTot=@RevisadoTot, RealizadoTot=@RealizadoTot, EmpenhadoTot=@EmpenhadoTot, LiquidadoTot=@LiquidadoTot, " +
                " dt_alterado=getdate() " +
                "where t03_cd_projeto=@t03_cd_projeto and CodRelatorio=@CodRelatorio and nm_fonte=@nm_fonte";

            SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t29.t03_cd_projeto),
                MakeInParam("@CodRelatorio", SqlDbType.Int, 0, t29.CodRelatorio),
                MakeInParam("@nm_fonte", SqlDbType.VarChar, 50, t29.nm_fonte),
                MakeInParam("@nm_unidade", SqlDbType.VarChar, 50, t29.nm_unidade),
                
                MakeInParam("@PlanejadoMes", SqlDbType.Decimal, 0, t29.PlanejadoMes),
                MakeInParam("@RevisadoMes", SqlDbType.Decimal, 0, t29.RevisadoMes),
                MakeInParam("@RealizadoMes", SqlDbType.Decimal, 0, t29.RealizadoMes),
                MakeInParam("@EmpenhadoMes", SqlDbType.Decimal, 0, t29.EmpenhadoMes),
                MakeInParam("@LiquidadoMes", SqlDbType.Decimal, 0, t29.LiquidadoMes),

                MakeInParam("@PlanejadoAcu", SqlDbType.Decimal, 0, t29.PlanejadoAcu),
                MakeInParam("@RevisadoAcu", SqlDbType.Decimal, 0, t29.RevisadoAcu),
                MakeInParam("@RealizadoAcu", SqlDbType.Decimal, 0, t29.RealizadoAcu),
                MakeInParam("@EmpenhadoAcu", SqlDbType.Decimal, 0, t29.EmpenhadoAcu),
                MakeInParam("@LiquidadoAcu", SqlDbType.Decimal, 0, t29.LiquidadoAcu),

                MakeInParam("@PlanejadoTot", SqlDbType.Decimal, 0, t29.PlanejadoTot),
                MakeInParam("@RevisadoTot", SqlDbType.Decimal, 0, t29.RevisadoTot),
                MakeInParam("@RealizadoTot", SqlDbType.Decimal, 0, t29.RealizadoTot),
                MakeInParam("@EmpenhadoTot", SqlDbType.Decimal, 0, t29.EmpenhadoTot),
                MakeInParam("@LiquidadoTot", SqlDbType.Decimal, 0, t29.LiquidadoTot)

            };
            return this.RunCommand(query, param);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public List<t29_temp_vlFinanceiro> ListObjTodos(int cd_projeto, int CodRelatorio)
    {
        string query = "select * from  t29_temp_vlFinanceiro where t03_cd_projeto=" + cd_projeto + " and CodRelatorio=" + CodRelatorio + " order by nm_fonte ";
        DataTable dt = this.GetDataTable(query);
        List<t29_temp_vlFinanceiro> t29list = new List<t29_temp_vlFinanceiro>();
        foreach (DataRow dr in dt.Rows)
        {
            t29_temp_vlFinanceiro t29_temp = new t29_temp_vlFinanceiro();
            t29_temp.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);
            t29_temp.CodRelatorio = Convert.ToInt32(dr["CodRelatorio"]);

            t29_temp.nm_fonte = dr["nm_fonte"].ToString();
            t29_temp.nm_unidade = dr["nm_unidade"].ToString();

            t29_temp.PlanejadoMes = Convert.ToDecimal(dr["PlanejadoMes"]);
            t29_temp.RevisadoMes = Convert.ToDecimal(dr["RevisadoMes"]);
            t29_temp.RealizadoMes = Convert.ToDecimal(dr["RealizadoMes"]);
            t29_temp.EmpenhadoMes = Convert.ToDecimal(dr["EmpenhadoMes"]);
            t29_temp.LiquidadoMes = Convert.ToDecimal(dr["LiquidadoMes"]);

            t29_temp.PlanejadoAcu = Convert.ToDecimal(dr["PlanejadoAcu"]);
            t29_temp.RevisadoAcu = Convert.ToDecimal(dr["RevisadoAcu"]);
            t29_temp.RealizadoAcu = Convert.ToDecimal(dr["RealizadoAcu"]);
            t29_temp.EmpenhadoAcu = Convert.ToDecimal(dr["EmpenhadoAcu"]);
            t29_temp.LiquidadoAcu = Convert.ToDecimal(dr["LiquidadoAcu"]);

            t29_temp.PlanejadoTot = Convert.ToDecimal(dr["PlanejadoTot"]);
            t29_temp.RevisadoTot = Convert.ToDecimal(dr["RevisadoTot"]);
            t29_temp.RealizadoTot = Convert.ToDecimal(dr["RealizadoTot"]);
            t29_temp.EmpenhadoTot = Convert.ToDecimal(dr["EmpenhadoTot"]);
            t29_temp.LiquidadoTot = Convert.ToDecimal(dr["LiquidadoTot"]);
            t29list.Add(t29_temp);
        }
        return t29list;
    }

    public List<t29_temp_vlFinanceiro> ListObjTodos(int cd_projeto, string nm_fonte, int CodRelatorio)
    {
        string query = "select * from  t29_temp_vlFinanceiro where t03_cd_projeto=" + cd_projeto + " and CodRelatorio=" + CodRelatorio + " and nm_fonte='" + nm_fonte + "' order by nm_fonte";
        DataTable dt = this.GetDataTable(query);
        List<t29_temp_vlFinanceiro> t29list = new List<t29_temp_vlFinanceiro>();
        foreach (DataRow dr in dt.Rows)
        {
            t29_temp_vlFinanceiro t29_temp = new t29_temp_vlFinanceiro();
            t29_temp.t03_cd_projeto = Convert.ToInt32(dr["t03_cd_projeto"]);
            t29_temp.CodRelatorio = Convert.ToInt32(dr["CodRelatorio"]);

            t29_temp.nm_fonte = dr["nm_fonte"].ToString();
            t29_temp.nm_unidade = dr["nm_unidade"].ToString();

            t29_temp.PlanejadoMes = Convert.ToDecimal(dr["PlanejadoMes"]);
            t29_temp.RevisadoMes = Convert.ToDecimal(dr["RevisadoMes"]);
            t29_temp.RealizadoMes = Convert.ToDecimal(dr["RealizadoMes"]);
            t29_temp.EmpenhadoMes = Convert.ToDecimal(dr["EmpenhadoMes"]);
            t29_temp.LiquidadoMes = Convert.ToDecimal(dr["LiquidadoMes"]);

            t29_temp.PlanejadoAcu = Convert.ToDecimal(dr["PlanejadoAcu"]);
            t29_temp.RevisadoAcu = Convert.ToDecimal(dr["RevisadoAcu"]);
            t29_temp.RealizadoAcu = Convert.ToDecimal(dr["RealizadoAcu"]);
            t29_temp.EmpenhadoAcu = Convert.ToDecimal(dr["EmpenhadoAcu"]);
            t29_temp.LiquidadoAcu = Convert.ToDecimal(dr["LiquidadoAcu"]);

            t29_temp.PlanejadoTot = Convert.ToDecimal(dr["PlanejadoTot"]);
            t29_temp.RevisadoTot = Convert.ToDecimal(dr["RevisadoTot"]);
            t29_temp.RealizadoTot = Convert.ToDecimal(dr["RealizadoTot"]);
            t29_temp.EmpenhadoTot = Convert.ToDecimal(dr["EmpenhadoTot"]);
            t29_temp.LiquidadoTot = Convert.ToDecimal(dr["LiquidadoTot"]);
            t29list.Add(t29_temp);
        }
        return t29list;
    }


    public bool ExisteFonte(int cd_projeto, string NomeFonte, int CodRelatorio)
    {
        string query = "select * from  t29_temp_vlFinanceiro where t03_cd_projeto= " + cd_projeto + " and CodRelatorio=" + CodRelatorio + " and nm_fonte='" + NomeFonte + "' order by dt_cadastro desc";
        DataTable dt = this.GetDataTable(query);
        bool result = false;
        if (dt.Rows.Count > 0)
        {
            result = true;
        }
        return result;
    }

    public int DeleteDB(int cd_projeto, int CodRelatorio)
    {
        string query = "delete t29_temp_vlFinanceiro where t03_cd_projeto= " + cd_projeto + " and CodRelatorio=" + CodRelatorio + "";
        return this.RunCommand(query);
    }

}
