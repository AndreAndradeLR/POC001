using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t27_fonteActionBD
/// </summary>
public class t27_fonteAction:SQLServerBase
{
	public t27_fonteAction()
	{
		
	}

    public int InsertDB(t27_fonte t27)
    {
        string query = "insert into t27_fonte (nm_fonte, dt_cadastro, dt_alterado, fl_deletado) " + 
            "values(@nm_fonte, getdate(), getdate(), 0)";
        
        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@nm_fonte", SqlDbType.VarChar, 500, t27.nm_fonte),
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t27_fonte t27)
    {
        string query = "update t27_fonte set " +
            "nm_fonte=@nm_fonte, dt_alterado=getdate() " +
            "where t27_cd_fonte=@t27_cd_fonte";



        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t27_cd_fonte", SqlDbType.Int, 0, t27.t27_cd_fonte),
                MakeInParam("@nm_fonte", SqlDbType.VarChar, 500, t27.nm_fonte),
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        string query = "update t27_fonte set  " +
            " fl_deletado=1 "+
            " where t27_cd_fonte ="+id;
      
        return this.RunCommand(query);
    }
    
    public t27_fonte Retrieve(int id)
    {
        string query = "select * from t27_fonte where t27_cd_fonte=" + id;
        DataTable dt = this.GetDataTable(query);
        t27_fonte t27 = new t27_fonte();
        foreach (DataRow dr in dt.Rows)
        {
            t27.t27_cd_fonte = int.Parse(dr["t27_cd_fonte"].ToString());

            if (dr["nm_fonte"] != DBNull.Value)
                t27.nm_fonte = dr["nm_fonte"].ToString();
            
            if (dr["dt_cadastro"] != DBNull.Value)
                t27.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t27.dt_alterado = (DateTime)dr["dt_alterado"];

            

            break;
        }
        
        return t27;
    }

    public List<t27_fonte> ListObjTodos()
    {
        string query = "select * from t27_fonte where fl_deletado=0 order by nm_fonte ";
        DataTable dt = this.GetDataTable(query);
        List<t27_fonte> t27list = new List<t27_fonte>();
        foreach (DataRow dr in dt.Rows)
        {
            t27_fonte t27 = new t27_fonte();
            t27.t27_cd_fonte = int.Parse(dr["t27_cd_fonte"].ToString());
            t27.nm_fonte = dr["nm_fonte"].ToString();
            t27list.Add(t27);
        }
        return t27list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t27_fonte where fl_deletado=0 order by nm_fonte";
        return this.GetDataTable(query);
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "Select " +
                        "distinct t27_fonte.*  " +
                        "From t27_fonte Inner Join " +
                        "t11_financeiro On t27_fonte.t27_cd_fonte = t11_financeiro.t27_cd_fonte " +
                        "Inner Join " +
                        "t08_acao On t08_acao.t08_cd_acao = t11_financeiro.t08_cd_acao " +
                        "where t08_acao.fl_deletado=0 and t27_fonte.fl_deletado=0  "+
                        "and t11_financeiro.fl_deletado=0  " +
                        "and t08_acao.t03_cd_projeto=" + cd_projeto + " order by nm_fonte";        
        return this.GetDataTable(query);
    }

    public int QuantidadeFontesPorProjeto(int cd_projeto)
    {
        string query = "select COUNT(*) TotalFontes from t27_fonte where t27_cd_fonte in" +
                       "(select t27_cd_fonte from t11_financeiro where t08_cd_acao in " +
                       "(select t08_cd_acao from t08_acao where t03_cd_projeto=" + cd_projeto + " and fl_deletado=0)and fl_deletado=0)";
        return this.RunCommand(query);
    }
    
}
