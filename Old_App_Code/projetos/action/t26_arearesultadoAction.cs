using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Text;
/// <summary>
/// Summary description for t26_arearesultadoActionBD
/// </summary>
public class t26_arearesultadoAction : SQLServerBase
{
    public t26_arearesultadoAction()
    {

    }

    public int InsertDB(t26_arearesultado t26)
    {
        string query = "insert into t26_arearesultado (nm_area, dt_cadastro, dt_alterado, fl_deletado, nm_arquivo) " +
            "values(@nm_area, getdate(), getdate(), 0, @nm_arquivo)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@nm_area", SqlDbType.VarChar, 500, t26.nm_area),
                MakeInParam("@nm_arquivo", SqlDbType.VarChar, 100, t26.nm_arquivo),
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t26_arearesultado t26)
    {
        string query = "update t26_arearesultado set " +
        "nm_area=@nm_area, dt_alterado=getdate() " +
        "where t26_cd_arearesultado=@t26_cd_arearesultado ";

        SqlParameter[] param = new SqlParameter[]
            {
               MakeInParam("@t26_cd_arearesultado", SqlDbType.Int, 0, t26.t26_cd_arearesultado),
               MakeInParam("@nm_area", SqlDbType.VarChar, 500, t26.nm_area),                              
            };
        return this.RunCommand(query, param);
    }

    public int UpdateArquivo(t26_arearesultado t26)
    {
        string query = "update t26_arearesultado set " +
           "nm_area=@nm_area, dt_alterado=getdate()," +
           "nm_arquivo=@nm_arquivo " +
           "where t26_cd_arearesultado=@t26_cd_arearesultado ";

        SqlParameter[] param = new SqlParameter[]
            {
               MakeInParam("@t26_cd_arearesultado", SqlDbType.Int, 0, t26.t26_cd_arearesultado),
               MakeInParam("@nm_area", SqlDbType.VarChar, 500, t26.nm_area),               
               MakeInParam("@nm_arquivo", SqlDbType.VarChar, 100, t26.nm_arquivo),
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        string query = "update t26_arearesultado set  " +
            " fl_deletado=1 " +
            " where t26_cd_arearesultado =" + id;

        return this.RunCommand(query);
    }

    public t26_arearesultado Retrieve(int id)
    {
        string query = "select * from t26_arearesultado where t26_cd_arearesultado=" + id;
        DataTable dt = this.GetDataTable(query);
        t26_arearesultado t26 = new t26_arearesultado();
        foreach (DataRow dr in dt.Rows)
        {
            t26.t26_cd_arearesultado = int.Parse(dr["t26_cd_arearesultado"].ToString());

            if (dr["nm_area"] != DBNull.Value)
                t26.nm_area = dr["nm_area"].ToString();

            if (dr["nm_arquivo"] != DBNull.Value)
                t26.nm_arquivo = dr["nm_arquivo"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t26.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t26.dt_alterado = (DateTime)dr["dt_alterado"];



            break;
        }

        return t26;
    }

    public List<t26_arearesultado> ListObjTodos()
    {
        string query = "select * from t26_arearesultado where fl_deletado=0 order by nu_ordem ";
        DataTable dt = this.GetDataTable(query);
        List<t26_arearesultado> t26list = new List<t26_arearesultado>();
        foreach (DataRow dr in dt.Rows)
        {
            t26_arearesultado t26 = new t26_arearesultado();
            t26.t26_cd_arearesultado = int.Parse(dr["t26_cd_arearesultado"].ToString());
            t26.nm_area = dr["nm_area"].ToString();
            t26.nm_arquivo = dr["nm_arquivo"].ToString();
            t26list.Add(t26);
        }
        return t26list;
    }

    public DataTable ListTodos()
    {
        string query = "select * from t26_arearesultado where fl_deletado=0 order by nu_ordem";
        return this.GetDataTable(query);
    }

    public int ListTodosDoProjeto(int cd_projeto)
    {
        int index = 0;
        string query = "select t26_cd_arearesultado from t26_arearesultado " +
                       " where t26_cd_arearesultado in (select t26_cd_arearesultado from t03_projeto where t03_cd_projeto='" + cd_projeto + "') " +
                       " and fl_deletado=0 order by nu_ordem";

        DataTable dt = this.GetDataTable(query);
        foreach (DataRow dr in dt.Rows)
        {
            index = int.Parse(dr["t26_cd_arearesultado"].ToString());
            break;
        }
        return index;
    }

    /// <summary>
    /// p.eduardo.silva - 20130612:
    /// Lista todas as áreas em que o usuário é responsavel por algum projeto
    /// </summary>
    /// <param name="cd_responsavel"></param>
    /// <returns></returns>
    public DataTable ListTodosDoResponsavel(string cd_responsavel)
    {
        string query = "select t26_cd_arearesultado ,nm_area from t26_arearesultado " +
                       " where t26_cd_arearesultado in (select t26_cd_arearesultado from t03_projeto where fl_deletado=0 " +
                       "   and t03_cd_projeto in (select t03_cd_projeto from t06_colaborador where t02_cd_usuario='" + cd_responsavel + "'))" +
                       " and fl_deletado=0 order by nm_area";

        return this.GetDataTable(query);
    }
    
    public DataTable ListAllDoProjeto(int cd_projeto)
    {
        string query = "select distinct t26.t26_cd_arearesultado,nm_area,t26.nu_ordem from t03_projeto t3  " +
                       "left join t26_arearesultado t26  on t3.t26_cd_arearesultado = t26.t26_cd_arearesultado " +
                       "where t3.t03_cd_projeto " +
                       "in(select t08.t03_cd_projeto from t08_acao t08 inner join dbo.t29_acoes_vinculadas_projeto t29 on t29.t08_cd_acao=t08.t08_cd_acao " +
                       " where t29.t03_cd_projeto ='" + cd_projeto + "' and t29.fl_deletado=0) " +
                       " order by t26.nu_ordem";

        return this.GetDataTable(query);
    }
}
