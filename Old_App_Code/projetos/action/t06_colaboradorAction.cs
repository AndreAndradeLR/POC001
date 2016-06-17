using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t06_colaboradorAction
/// </summary>
public class t06_colaboradorAction : SQLServerBase
{
    public t06_colaboradorAction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsertDB(t06_colaborador t06)
    {
        string query = "insert into t06_colaborador (" +
            "t02_cd_usuario, " +
            "t03_cd_projeto, " +
            "dt_cadastro, " +
            "dt_alterado) " +
            "values(@t02_cd_usuario, " +
            "@t03_cd_projeto, " +
            "getdate(), getdate())";

        SqlParameter[] param = new SqlParameter[]
            {                
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t06.t02_cd_usuario),
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t06.t03_cd_projeto)

            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t06_colaborador t06)
    {
        string query = "update t06_colaborador set " +
            "t02_cd_usuario=@t02_cd_usuario, " +
            "dt_alterado=getdate() " +
            "where t06_cd_colaborador=@t06_cd_colaborador";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t06_cd_colaborador", SqlDbType.Int, 0, t06.t06_cd_colaborador),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t06.t02_cd_usuario)                
            };
        return this.RunCommand(query, param);
    }
    public int UpdateOrdem(int id, string ordem)
    {
        string query = "update t06_colaborador set " +
            "nu_ordem=" + ordem + " where t06_cd_colaborador=" + id;

        return this.RunCommand(query);
    }

    public int DeleteDB(int id)
    {
        string query = "delete from t06_colaborador " +
            " where t06_cd_colaborador =" + id;

        return this.RunCommand(query);
    }

    public t06_colaborador Retrieve(int id)
    {
        string query = "select * from t06_colaborador where t06_cd_colaborador=" + id;
        DataTable dt = this.GetDataTable(query);
        t06_colaborador t06 = new t06_colaborador();
        foreach (DataRow dr in dt.Rows)
        {
            t06.t06_cd_colaborador = int.Parse(dr["t06_cd_colaborador"].ToString());

            if (dr["nm_funcao"] != DBNull.Value)
                t06.nm_funcao = dr["nm_funcao"].ToString();

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t06.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t06.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t06.dt_alterado = (DateTime)dr["dt_alterado"];

            break;
        }
        return t06;
    }

    public List<t06_colaborador> ListObjTodos(int cd_projeto)
    {
        string query = "select * from t06_colaborador " +
            "where t03_cd_projeto=" + cd_projeto +
            " order by nu_ordem";
        DataTable dt = this.GetDataTable(query);
        List<t06_colaborador> t06list = new List<t06_colaborador>();
        foreach (DataRow dr in dt.Rows)
        {
            t06_colaborador t06 = new t06_colaborador();
            t06.t06_cd_colaborador = int.Parse(dr["t06_cd_colaborador"].ToString());
            t06.nm_funcao = dr["nm_funcao"].ToString();
            t06.t02_cd_usuario = dr["t02_cd_usuario"].ToString();
            t06.t02 = new t02_usuarioAction().Retrieve(t06.t02_cd_usuario);
            t06list.Add(t06);
        }
        return t06list;
    }

    /// <summary>
    /// p.eduardo.silva - 20130612:
    /// Lista Responsável de acordo com o projeto ou área de resultado
    /// </summary>
    /// <param name="cd_projeto">Projeto</param>
    /// <param name="cd_arearesultado">Área de Resultado</param>
    /// <returns></returns>
    public DataTable ListResponsavel(int cd_projeto = 0, int cd_arearesultado = 0)
    {
        string filter = string.Empty;
        if (cd_projeto > 0)
            filter = " and t03_cd_projeto=" + cd_projeto;

        else if (cd_arearesultado > 0)
        {
            filter = " and t03_cd_projeto in (select t03_cd_projeto from t03_projeto t03 " +
                " where fl_deletado=0 and t26_cd_arearesultado = " + cd_arearesultado + ")";
        }
        string query = "select distinct t02.t02_cd_usuario, t02.nm_nome  " +
            " from t06_colaborador t06 left join t02_usuario t02 " +
            "   on t06.t02_cd_usuario=t02.t02_cd_usuario where t02.t02_cd_usuario is not null" +
            filter + " order by nm_nome";
        return this.GetDataTable(query);
    }

    public DataTable ListTodos(int cd_projeto)
    {
        string query = "select t6.*, t2.nm_nome, t1.nm_entidade from t06_colaborador t6 " +
            "left join t02_usuario t2 on t2.t02_cd_usuario=t6.t02_cd_usuario " +
            "left join t01_entidade t1 on t1.t01_cd_entidade=t2.t01_cd_entidade " +
            "where t6.t03_cd_projeto=" + cd_projeto +
            " order by t6.nu_ordem";
        return this.GetDataTable(query);
    }
}
