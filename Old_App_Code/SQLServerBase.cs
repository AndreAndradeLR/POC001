using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class SQLServerBase
{    
    protected int RunCommand(string query)
    {
        int result = 0;
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand(query, sqlConn);
        try
        {
            sqlConn.Open();
            cmd.CommandType = CommandType.Text;
            result = Convert.ToInt32(cmd.ExecuteNonQuery());
        }

        catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); }

        finally
        {
            sqlConn.Close();
            cmd.Dispose();
            sqlConn.Dispose();
        }
        return result;
    }

    protected int RunCommand(string query, SqlParameter[] prams)
    {
        int result = 0;
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand(query, sqlConn);
        try
        {
            sqlConn.Open();
            if ((prams != null))
            {
                foreach (SqlParameter parameter in prams)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            cmd.CommandType = CommandType.Text;
            result = Convert.ToInt32(cmd.ExecuteNonQuery());
        }

        catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message);}

        finally
        {
            sqlConn.Close();
            cmd.Dispose();
            sqlConn.Dispose();
        }
        return result;
    }




    protected SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
    }

    protected SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
    {
        return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
    }

    protected SqlParameter MakeParam(string ParamName, SqlDbType DbType, int Size, ParameterDirection Direction, object Value)
    {
        SqlParameter param = default(SqlParameter);
        if ((Size > 0))
        {
            param = new SqlParameter(ParamName, DbType, Size);
        }
        else
        {
            param = new SqlParameter(ParamName, DbType);
        }
        param.Direction = Direction;
        if ((!(Direction == ParameterDirection.Output & (Value == null))))
        {
            param.Value = Value;
        }
        return param;
    }

    protected DataTable GetDataTable(string query)
    {
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);
        DataTable dt = new DataTable();

        try
        {
            sqlConn.Open();
            da.SelectCommand.CommandType = CommandType.Text;
            int rows = da.Fill(dt);
        }

        catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); }

        finally
        {
            sqlConn.Close();
            da.Dispose();
            sqlConn.Dispose();
        }
        return dt;
    }

    protected DataTable GetDataTable(string query, SqlParameter[] prams)
    {
        SqlConnection sqlConn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);
        DataTable dt = new DataTable();

        try
        {
            sqlConn.Open();
            if ((prams != null))
            {
                foreach (SqlParameter parameter in prams)
                {
                    da.SelectCommand.Parameters.Add(parameter);
                }
            }
            da.SelectCommand.CommandType = CommandType.Text;
            int rows = da.Fill(dt);
        }

        catch (Exception ex) { System.Web.HttpContext.Current.Response.Write(ex.Message); }

        finally
        {
            sqlConn.Close();
            da.Dispose();
            sqlConn.Dispose();
        }
        return dt;

    }


}

