using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Text;

/// <summary>
/// Summary description for t03_projetoAction
/// </summary>
public class t03_projetoAction : SQLServerBase
{
    public t03_projetoAction()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int InsertDB(t03_projeto t03)
    {
        string query = "insert into t03_projeto " +
            "(nm_projeto, " +
            "t01_cd_entidade, " +
            "t02_cd_usuario, " +
            "t02_cd_usuario_monitoramento, " +
            "dt_cadastro, " +
            "dt_alterado, " +
            "t26_cd_arearesultado, " +
            "fl_deletado) " +

            "values(@nm_projeto, " +
            "@t01_cd_entidade, " +
            "@t02_cd_usuario, " +
            "@t02_cd_usuario_monitoramento, " +
            "getdate(), " +
            "getdate(), " +
            "@t26_cd_arearesultado, " +
            "0)";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@nm_projeto", SqlDbType.VarChar, 500, t03.nm_projeto),
                MakeInParam("@t01_cd_entidade", SqlDbType.Int, 0, t03.t01_cd_entidade),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t03.t02_cd_usuario),
                MakeInParam("@t02_cd_usuario_monitoramento", SqlDbType.VarChar, 20, t03.t02_cd_usuario_monitoramento),
                MakeInParam("@t26_cd_arearesultado", SqlDbType.Int, 0, t03.t26_cd_arearesultado)
            };


        return this.RunCommand(query, param);
    }

    public int UpdateDB(t03_projeto t03)
    {
        string query = "update t03_projeto set " +
            "nm_projeto=@nm_projeto, t01_cd_entidade=@t01_cd_entidade, " +
            "t02_cd_usuario=@t02_cd_usuario, " +
            "t02_cd_usuario_monitoramento=@t02_cd_usuario_monitoramento, " +
            "t26_cd_arearesultado=@t26_cd_arearesultado, " +
            "dt_alterado=getdate() " +
            "where t03_cd_projeto=@t03_cd_projeto";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t03.t03_cd_projeto),
                MakeInParam("@nm_projeto", SqlDbType.VarChar, 500, t03.nm_projeto),
                MakeInParam("@t01_cd_entidade", SqlDbType.Int, 0, t03.t01_cd_entidade),
                MakeInParam("@t02_cd_usuario", SqlDbType.VarChar, 20, t03.t02_cd_usuario),
                MakeInParam("@t02_cd_usuario_monitoramento", SqlDbType.VarChar, 20, t03.t02_cd_usuario_monitoramento),
                MakeInParam("@t26_cd_arearesultado", SqlDbType.Int, 0, t03.t26_cd_arearesultado)
            };
        return this.RunCommand(query, param);
    }

    public int UpdateCampoTextDB(string nome_campo, string text, int cd_projeto)
    {
        string query = "update t03_projeto set " + nome_campo + "=@text, " +
            "dt_alterado=getdate() " +
            "where t03_cd_projeto=@t03_cd_projeto";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, cd_projeto),
                MakeInParam("@text" , SqlDbType.Text, 0, text)
            };
        return this.RunCommand(query, param);
    }

    public int UpdatePrazoDB(string inicio, string fim, int cd_projeto)
    {
        string query = "update t03_projeto set " +
            "dt_inicio=@dt_inicio, " +
            "dt_fim=@dt_fim, " +
            "dt_alterado=getdate() " +
            "where t03_cd_projeto=@t03_cd_projeto";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, cd_projeto),
                MakeInParam("@dt_inicio", SqlDbType.DateTime, 0, DateTime.Parse(inicio)),
                MakeInParam("@dt_fim", SqlDbType.DateTime, 0, DateTime.Parse(fim))
                
            };
        return this.RunCommand(query, param);
    }

    public int UpdateAtualizaDB(int cd_projeto)
    {
        string query = "update t03_projeto set " +
            "dt_atualizado=getdate() " +
            "where t03_cd_projeto=@t03_cd_projeto";

        SqlParameter[] param = new SqlParameter[]
            {
                MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, cd_projeto)
            };
        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        string query = "update t03_projeto set  " +
            " fl_deletado=1 " +
            " where t03_cd_projeto =" + id;

        return this.RunCommand(query);
    }

    public t03_projeto Retrieve(int id)
    {
        string query = "select * from t03_projeto where t03_cd_projeto=" + id;
        DataTable dt = this.GetDataTable(query);
        t03_projeto t03 = new t03_projeto();
        foreach (DataRow dr in dt.Rows)
        {
            t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());

            if (dr["t26_cd_arearesultado"] != DBNull.Value)
            {
                t03.t26_cd_arearesultado = Convert.ToInt32(dr["t26_cd_arearesultado"]);
                t03.t26 = new t26_arearesultadoAction().Retrieve(t03.t26_cd_arearesultado);
            }

            if (dr["nm_projeto"] != DBNull.Value)
                t03.nm_projeto = dr["nm_projeto"].ToString();

            if (dr["t01_cd_entidade"] != DBNull.Value)
                t03.t01_cd_entidade = Convert.ToInt32(dr["t01_cd_entidade"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t03.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["t02_cd_usuario_monitoramento"] != DBNull.Value)
                t03.t02_cd_usuario_monitoramento = dr["t02_cd_usuario_monitoramento"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t03.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t03.dt_alterado = (DateTime)dr["dt_alterado"];

            //Arvore

            if (dr["ds_publico"] != DBNull.Value)
                t03.ds_publico = dr["ds_publico"].ToString();

            if (dr["ds_objetivo"] != DBNull.Value)
                t03.ds_objetivo = dr["ds_objetivo"].ToString();

            if (dr["dt_inicio"] != DBNull.Value)
                t03.dt_inicio = (DateTime)dr["dt_inicio"];

            if (dr["dt_fim"] != DBNull.Value)
                t03.dt_fim = (DateTime)dr["dt_fim"];

            if (dr["dt_atualizado"] != DBNull.Value)
                t03.dt_atualizado = (DateTime)dr["dt_atualizado"];

            break;
        }
        return t03;
    }

    public int RetrieveIDENTITY(t03_projeto t03)
    {
        string query = "select top 1 * from t03_projeto where nm_projeto='" + t03.nm_projeto + "'";
        DataTable dt = this.GetDataTable(query);
        int identity = 0;
        foreach (DataRow dr in dt.Rows)
        {
            identity = int.Parse(dr["t03_cd_projeto"].ToString());
            break;
        }
        return identity;
    }

    public List<t03_projeto> ListObjTodos()
    {
        string query = "select * " +
            " from t03_projeto t03 where  fl_deletado=0 " +
            "order by nm_projeto ";
        DataTable dt = this.GetDataTable(query);
        List<t03_projeto> t03list = new List<t03_projeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t03_projeto t03 = new t03_projeto();
            t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());
            t03.nm_projeto = dr["nm_projeto"].ToString();
            t03list.Add(t03);
        }
        return t03list;
    }

    public DataTable ListTodos(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select * from t03_projeto t03 ");
        sb.Append("where  t03.fl_deletado=0 ");

        if (filtro.T01_cd_entidade_resp != null)
        {
            sb.Append("and t03.t01_cd_entidade=" + filtro.T01_cd_entidade_resp + " ");
        }
        if (filtro.T01_cd_entidade_parc != null)
        {
            sb.Append("and t03.t03_cd_projeto in (" +
                "select t03_cd_projeto from t04_parceiro where " +
                "t01_cd_entidade = " + filtro.T01_cd_entidade_parc + ") ");
        }
        if (filtro.T21_cd_fase != null)
        {
            sb.Append("and ( ");
            sb.Append("select top 1 t21_cd_fase from t22_faseprojeto  ");
            sb.Append("where t03_cd_projeto=t03.t03_cd_projeto ");
            sb.Append("order by dt_cadastro desc)=" + filtro.T21_cd_fase + " ");
        }
        if (filtro.T26_cd_arearesultado != null)
        {
            sb.Append("and t03.t26_cd_arearesultado=" + filtro.T26_cd_arearesultado + " ");
        }

        sb.Append("order by nm_projeto ");


        return this.GetDataTable(sb.ToString());
    }

    public DataTable ListTodos(FiltroProjeto filtro, string cd_projetos)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select * from t03_projeto t03 ");
        sb.Append("where t03.fl_deletado=0 ");

        if (cd_projetos != "")
        {
            sb.Append("and t03_cd_projeto in (" + cd_projetos + ") ");
        }
        if (filtro.T01_cd_entidade_resp != null)
        {
            sb.Append("and t03.t01_cd_entidade=" + filtro.T01_cd_entidade_resp + " ");
        }
        if (filtro.T01_cd_entidade_parc != null)
        {
            sb.Append("and t03.t03_cd_projeto in (" +
                "select t03_cd_projeto from t04_parceiro where " +
                "t01_cd_entidade = " + filtro.T01_cd_entidade_parc + ") ");
        }
        if (filtro.T21_cd_fase != null)
        {
            sb.Append("and ( ");
            sb.Append("select top 1 t21_cd_fase from t22_faseprojeto  ");
            sb.Append("where t03_cd_projeto=t03.t03_cd_projeto ");
            sb.Append("order by dt_cadastro desc)=" + filtro.T21_cd_fase + " ");
        }
        if (filtro.T26_cd_arearesultado != null)
        {
            sb.Append("and t03.t26_cd_arearesultado=" + filtro.T26_cd_arearesultado + " ");
        }

        sb.Append("order by nm_projeto ");


        return this.GetDataTable(sb.ToString());
    }

    /*
     * Mon = Monitoramento/Resumo Executivo 
     */

    private string CondicoesFiltroMon(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (filtro.array_cd_projeto != null)
        {
            sb.Append("and t03.t03_cd_projeto in(" + filtro.array_cd_projeto + ") ");
        }
        else
        {
            if (filtro.T03_cd_projeto != null)
            {
                sb.Append("and t03.t03_cd_projeto=" + filtro.T03_cd_projeto + " ");
            }
        }
        if (filtro.T01_cd_entidade_resp != null)
        {
            sb.Append("and t03.t01_cd_entidade=" + filtro.T01_cd_entidade_resp + " ");
        }
        if (filtro.T01_cd_entidade_parc != null)
        {
            sb.Append("and t03.t03_cd_projeto in (" +
                "select t03_cd_projeto from t04_parceiro where " +
                "t01_cd_entidade = " + filtro.T01_cd_entidade_parc + ") ");
        }
        if (filtro.T21_cd_fase != null)
        {
            sb.Append("and ( ");
            sb.Append("select top 1 t21_cd_fase from t22_faseprojeto  ");
            sb.Append("where t03_cd_projeto=t03.t03_cd_projeto ");
            sb.Append("order by dt_cadastro desc)=" + filtro.T21_cd_fase + " ");
        }
        if (filtro.T26_cd_arearesultado != null)
        {
            sb.Append("and t03.t26_cd_arearesultado=" + filtro.T26_cd_arearesultado + " ");
        }
        return sb.ToString();
    }

    public DataTable ListMon(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select * from t03_projeto t03 ");
        sb.Append("where  t03.fl_deletado=0 ");
        sb.Append(CondicoesFiltroMon(filtro));
        sb.Append("order by nm_projeto ");
        return this.GetDataTable(sb.ToString());
    }


    public List<t03_projeto> ListObjMon(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select * from t03_projeto t03 ");
        sb.Append("where  t03.fl_deletado=0 ");
        sb.Append(CondicoesFiltroMon(filtro));
        sb.Append("order by nm_projeto ");
        DataTable dt = this.GetDataTable(sb.ToString());
        List<t03_projeto> t03list = new List<t03_projeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t03_projeto t03 = new t03_projeto();
            t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());
            t03.nm_projeto = dr["nm_projeto"].ToString();
            t03list.Add(t03);
        }
        return t03list;
    }

    public List<t03_projeto> ListObjAtualizadoMon(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select dt_atualizado from t03_projeto t03 ");
        sb.Append("where  t03.fl_deletado=0 and (dt_atualizado is not null) ");
        sb.Append(CondicoesFiltroMon(filtro));
        sb.Append("order by dt_atualizado desc ");
        DataTable dt = this.GetDataTable(sb.ToString());
        List<t03_projeto> t03list = new List<t03_projeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t03_projeto t03 = new t03_projeto();
            if (dr["dt_atualizado"] != DBNull.Value)
                t03.dt_atualizado = (DateTime)dr["dt_atualizado"];
            t03list.Add(t03);
        }
        return t03list;
    }

    public DataTable ListMonAtualizaDuasSemanas(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select DateDiff(\"d\", dt_atualizado, getdate()) as dias, nm_projeto ");
        sb.Append("from t03_projeto t03 ");
        sb.Append("where  t03.fl_deletado=0 ");
        sb.Append(CondicoesFiltroMon(filtro));
        sb.Append("group by dt_atualizado, nm_projeto ");
        sb.Append("HAVING DateDiff(\"d\", dt_atualizado, getdate()) <= 14 ");
        sb.Append("order by dt_atualizado desc, nm_projeto");
        return this.GetDataTable(sb.ToString());
    }
    public DataTable ListMonAtualizaQuatroSemanas(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select DateDiff(\"d\", dt_atualizado, getdate()) as dias, nm_projeto ");
        sb.Append("from t03_projeto t03 ");
        sb.Append("where  t03.fl_deletado=0 ");
        sb.Append(CondicoesFiltroMon(filtro));
        sb.Append("group by dt_atualizado, nm_projeto ");
        sb.Append("HAVING DateDiff(\"d\", dt_atualizado, getdate()) between 15 and 28 ");
        sb.Append("order by dt_atualizado desc, nm_projeto");
        return this.GetDataTable(sb.ToString());
    }
    public DataTable ListMonAtualizaMaisQuatroSemanas(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select DateDiff(\"d\", dt_atualizado, getdate()) as dias, nm_projeto ");
        sb.Append("from t03_projeto t03 ");
        sb.Append("where  t03.fl_deletado=0 ");
        sb.Append(CondicoesFiltroMon(filtro));
        sb.Append("group by dt_atualizado, nm_projeto ");
        sb.Append("HAVING DateDiff(\"d\", dt_atualizado, getdate()) > 28");
        sb.Append("order by dt_atualizado desc, nm_projeto");
        return this.GetDataTable(sb.ToString());
    }

    public t03_projeto RetriveMonPeriodoAnalisado(FiltroProjeto filtro)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("select min(dt_inicio) as dt_inicio, max(dt_fim) as dt_fim from t03_projeto t03 ");
        sb.Append("where  t03.fl_deletado=0 ");
        sb.Append(CondicoesFiltroMon(filtro));
        DataTable dt = this.GetDataTable(sb.ToString());
        t03_projeto t03 = new t03_projeto();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["dt_inicio"] != DBNull.Value)
                t03.dt_inicio = (DateTime)dr["dt_inicio"];

            if (dr["dt_fim"] != DBNull.Value)
                t03.dt_fim = (DateTime)dr["dt_fim"];
        }
        return t03;
    }

    public t03_projeto RetriveDatas(int id)
    {
        string query = "select dt_inicio, dt_fim from t03_projeto " +
                       "where t03_cd_projeto=" + id + " ";
        DataTable dt = this.GetDataTable(query);
        t03_projeto t03 = new t03_projeto();
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["dt_inicio"] != DBNull.Value)
                t03.dt_inicio = (DateTime)dr["dt_inicio"];

            if (dr["dt_fim"] != DBNull.Value)
                t03.dt_fim = (DateTime)dr["dt_fim"];
        }
        return t03;
    }

    public DataTable ListTodos()
    {
        string query = "select *," +
            "nm_entidade=(select nm_entidade from t01_entidade where t01_cd_entidade=t03.t01_cd_entidade), " +
            "nm_area=(select nm_area from t26_arearesultado where t26_cd_arearesultado=t03.t26_cd_arearesultado), " +
            "nome_gerente=(select nm_nome from t02_usuario where t02_cd_usuario=t03.t02_cd_usuario), " +
            "nome_monitoramento=(select nm_nome from t02_usuario where t02_cd_usuario=t03.t02_cd_usuario_monitoramento) " +
            " from t03_projeto t03 where  fl_deletado=0 " +
            "order by nm_projeto ";
        return this.GetDataTable(query);
    }

    /// <summary>
    /// p.eduardo.silva - 20130612:
    /// Lista todos os projetos em que o usuario é responsavel
    /// </summary>
    /// <param name="cd_usuario"></param>
    /// <returns></returns>
    public DataTable ListTodosDoResponsavel(string cd_usuario)
    {
        string query = "select nm_projeto, t03_cd_projeto from t03_projeto t03 where  fl_deletado=0 and t03_cd_projeto in " +
            " (select t03_cd_projeto from t06_colaborador where t02_cd_usuario='" + cd_usuario + "')" +
            "order by nm_projeto ";
        return this.GetDataTable(query);
    }

    public DataTable ListTodosProjMenosSustent(string codProjSustentador, string codAcaoProjSust)
    {
        string query = "select *,";
        query += "nm_entidade=(select nm_entidade from t01_entidade where t01_cd_entidade=t03.t01_cd_entidade), ";
        query += "nm_area=(select nm_area from t26_arearesultado where t26_cd_arearesultado=t03.t26_cd_arearesultado), ";
        query += "nome_gerente=(select nm_nome from t02_usuario where t02_cd_usuario=t03.t02_cd_usuario), ";
        query += "nome_monitoramento=(select nm_nome from t02_usuario where t02_cd_usuario=t03.t02_cd_usuario_monitoramento) ";
        query += " from t03_projeto t03 ";
        query += " where t03_cd_projeto not in (select t29.t03_cd_projeto from t29_acoes_vinculadas_projeto t29 where t29.t08_cd_acao = " + codAcaoProjSust + " and t29.fl_deletado=0) ";
        query += " and fl_deletado=0 ";
        query += " and t03_cd_projeto <> " + codProjSustentador;
        query += " order by nm_projeto ";

        return this.GetDataTable(query);
    }

    public List<t03_projeto> ListTodosDaProjetosAreaResultado(int cd_area)
    {
        string query = "select t03_cd_projeto, nm_projeto from t03_projeto t03 where  fl_deletado=0 " +
            " and t26_cd_arearesultado='" + cd_area + "' " +
            "order by nm_projeto ";

        DataTable dt = this.GetDataTable(query);
        List<t03_projeto> lstProject = new List<t03_projeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t03_projeto t03 = new t03_projeto();
            t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());

            if (dr["nm_projeto"] != DBNull.Value)
                t03.nm_projeto = dr["nm_projeto"].ToString();

            lstProject.Add(t03);
        }
        return lstProject;
    }

    public List<t03_projeto> ListTodosProjetosRelRestricoes(List<string> lstFiltros)
    {
        try
        {
            string query = MontaQueryFiltrosRestricao(lstFiltros);
            DataTable dt = this.GetDataTable(query);
            List<t03_projeto> t03list = new List<t03_projeto>();
            foreach (DataRow dr in dt.Rows)
            {
                t03_projeto t03 = new t03_projeto();
                t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());

                if (dr["t26_cd_arearesultado"] != DBNull.Value)
                {
                    t03.t26_cd_arearesultado = Convert.ToInt32(dr["t26_cd_arearesultado"]);
                    t03.t26 = new t26_arearesultadoAction().Retrieve(t03.t26_cd_arearesultado);
                }

                if (dr["nm_projeto"] != DBNull.Value)
                    t03.nm_projeto = dr["nm_projeto"].ToString();

                if (dr["t01_cd_entidade"] != DBNull.Value)
                    t03.t01_cd_entidade = Convert.ToInt32(dr["t01_cd_entidade"]);

                if (dr["t02_cd_usuario"] != DBNull.Value)
                    t03.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

                if (dr["t02_cd_usuario_monitoramento"] != DBNull.Value)
                    t03.t02_cd_usuario_monitoramento = dr["t02_cd_usuario_monitoramento"].ToString();

                if (dr["dt_cadastro"] != DBNull.Value)
                    t03.dt_cadastro = (DateTime)dr["dt_cadastro"];

                if (dr["dt_alterado"] != DBNull.Value)
                    t03.dt_alterado = (DateTime)dr["dt_alterado"];

                //Arvore

                if (dr["ds_publico"] != DBNull.Value)
                    t03.ds_publico = dr["ds_publico"].ToString();

                if (dr["ds_objetivo"] != DBNull.Value)
                    t03.ds_objetivo = dr["ds_objetivo"].ToString();

                if (dr["dt_inicio"] != DBNull.Value)
                    t03.dt_inicio = (DateTime)dr["dt_inicio"];

                if (dr["dt_fim"] != DBNull.Value)
                    t03.dt_fim = (DateTime)dr["dt_fim"];

                if (dr["dt_atualizado"] != DBNull.Value)
                    t03.dt_atualizado = (DateTime)dr["dt_atualizado"];

                t03list.Add(t03);
            }
            return t03list;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public string MontaQueryFiltrosRestricao(List<string> lstFiltros)
    {
        string FiltroArea = "";
        string FiltroProjeto = "";
        string FiltroData = "";
        string FiltroResponsavel = "";
        string FiltroEstado = "";

        if (lstFiltros.Count() > 0)
        {
            if (lstFiltros[0] != "") FiltroArea = " and t3.t26_cd_arearesultado =" + lstFiltros[0];
            if (lstFiltros[1] != "") FiltroProjeto = " and t3.t03_cd_projeto =" + lstFiltros[1];
            if (lstFiltros[3] != "") FiltroResponsavel = " and t3.t03_cd_projeto in " +
                " (select t03_cd_projeto from t03_projeto where t02_cd_usuario='" + lstFiltros[3] + "')";

            switch (lstFiltros[2])
            {
                //Igual a Data Atual" Value="1"
                case "1":
                    FiltroData = " and CAST( ( STR( YEAR( dt_limite ) ) + '/' +  STR( MONTH( dt_limite ) ) + '/' + STR( DAY( dt_limite ) )  ) AS DATETIME ) " +
                                 "  =  CAST( ( STR( YEAR( GETDATE() ) ) + '/' +  STR( MONTH( GETDATE() ) ) + '/' + STR( DAY( GETDATE() ) )  ) AS DATETIME )";
                    break;
                //Maior ou Igual a Data Atual" Value="2"
                case "2":
                    FiltroData = " and CAST( ( STR( YEAR( dt_limite ) ) + '/' +  STR( MONTH( dt_limite ) ) + '/' + STR( DAY( dt_limite ) )  ) AS DATETIME ) " +
                                 "  >=  CAST( ( STR( YEAR( GETDATE() ) ) + '/' +  STR( MONTH( GETDATE() ) ) + '/' + STR( DAY( GETDATE() ) )  ) AS DATETIME )";
                    break;
                //"Menor ou Igual a Data Atual" Value="3"
                case "3":
                    FiltroData = " and CAST( ( STR( YEAR( dt_limite ) ) + '/' +  STR( MONTH( dt_limite ) ) + '/' + STR( DAY( dt_limite ) )  ) AS DATETIME ) " +
                                 "  <=  CAST( ( STR( YEAR( GETDATE() ) ) + '/' +  STR( MONTH( GETDATE() ) ) + '/' + STR( DAY( GETDATE() ) )  ) AS DATETIME )";
                    break;
                default:
                    FiltroData = "";
                    break;
            }

            if (lstFiltros[4] != "")
            {
                if (lstFiltros[4] == "Superadas")
                    FiltroEstado = " and dt_superada is not null";
                else
                    FiltroEstado = " and dt_superada is null";
            }

        }
        string sql = String.Format("select * from t03_projeto t3 left join t26_arearesultado t26  " +
            "on t3.t26_cd_arearesultado = t26.t26_cd_arearesultado " +
            "  where t3.t03_cd_projeto in (select t03_cd_projeto from t07_restricao where fl_deletado=0 {2} {3}) {0} {1} {4}", FiltroArea, FiltroProjeto, FiltroEstado, FiltroData, FiltroResponsavel);
        sql += " and t3.fl_deletado=0 order by t26.nu_ordem, t3.nm_projeto";

        return sql;
    }

    public List<t03_projeto> ListTodosProjetosComMarcos(List<String> lstFiltros)
    {
        string query = MontaQueryFiltrosMarco(lstFiltros);
        DataTable dt = this.GetDataTable(query);
        List<t03_projeto> t03list = new List<t03_projeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t03_projeto t03 = new t03_projeto();
            t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());

            if (dr["t26_cd_arearesultado"] != DBNull.Value)
            {
                t03.t26_cd_arearesultado = Convert.ToInt32(dr["t26_cd_arearesultado"]);
                t03.t26 = new t26_arearesultadoAction().Retrieve(t03.t26_cd_arearesultado);
            }

            if (dr["nm_projeto"] != DBNull.Value)
                t03.nm_projeto = dr["nm_projeto"].ToString();

            if (dr["t01_cd_entidade"] != DBNull.Value)
                t03.t01_cd_entidade = Convert.ToInt32(dr["t01_cd_entidade"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t03.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["t02_cd_usuario_monitoramento"] != DBNull.Value)
                t03.t02_cd_usuario_monitoramento = dr["t02_cd_usuario_monitoramento"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t03.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t03.dt_alterado = (DateTime)dr["dt_alterado"];

            //Arvore

            if (dr["ds_publico"] != DBNull.Value)
                t03.ds_publico = dr["ds_publico"].ToString();

            if (dr["ds_objetivo"] != DBNull.Value)
                t03.ds_objetivo = dr["ds_objetivo"].ToString();

            if (dr["dt_inicio"] != DBNull.Value)
                t03.dt_inicio = (DateTime)dr["dt_inicio"];

            if (dr["dt_fim"] != DBNull.Value)
                t03.dt_fim = (DateTime)dr["dt_fim"];

            if (dr["dt_atualizado"] != DBNull.Value)
                t03.dt_atualizado = (DateTime)dr["dt_atualizado"];

            t03list.Add(t03);
        }
        return t03list;
    }

    public string MontaQueryFiltrosMarco(List<string> lstFiltros)
    {
        string FiltroArea = "";
        string FiltroProjeto = "";
        string FiltroStatus = "";

        if (lstFiltros.Count() > 0)
        {
            if (lstFiltros[0] != "") FiltroArea = " and t3.t26_cd_arearesultado =" + lstFiltros[0];
            if (lstFiltros[1] != "") FiltroProjeto = " and t3.t03_cd_projeto =" + lstFiltros[1];

            if (lstFiltros[2] != "")
            {
                switch (lstFiltros[2])
                {
                    case "B":
                        FiltroStatus = " and fl_status='B' ";
                        break;
                    case "R":
                        FiltroStatus = " and fl_status='R' ";
                        break;
                    case "G":
                        FiltroStatus = " and fl_status='G' ";
                        break;
                    case "Y":
                        //FiltroStatus = " and fl_status='Y' ";                
                        FiltroStatus = " and ((fl_status='Y')";
                        FiltroStatus += " OR ";
                        FiltroStatus += " t09_cd_marco in(select t09_cd_marco from t19_marcorestricao ";
                        FiltroStatus += " where t07_cd_restricao in (select t07_cd_restricao from t07_restricao where fl_deletado=0) ))";
                        break;

                    default:
                        FiltroStatus = "";
                        break;
                }
            }

        }

        string sql = string.Format("select * from t03_projeto t3 left join t26_arearesultado t26  " +
            "on t3.t26_cd_arearesultado = t26.t26_cd_arearesultado " +
            "where t03_cd_projeto in (select t03_cd_projeto from t09_marco where fl_deletado=0 {2}) {0} {1} " +
                     " and t3.fl_deletado=0 order by t26.nu_ordem, t3.nm_projeto", FiltroArea, FiltroProjeto, FiltroStatus);
        return sql;
    }

    public List<t03_projeto> ListTodosProjetosAcoesVinculadas(string codArea, string codProj)
    {
        string CondicaoAreaResultado = "";
        string CondicaoProjeto = "";

        //filtra
        if (codArea != "")
        {
            //CondicaoAreaResultado = " and t03.t26_cd_arearesultado = " + codArea;
            Util.CodAreaResultado = codArea;
            CondicaoAreaResultado = "";
        }
        else
        {
            Util.CodAreaResultado = "";
        }

        if (codProj != "")
        {
            CondicaoProjeto = " and t29.t03_cd_projeto = " + codProj; //Util.CodProjetoSustentador; 
        }

        //query projetos
        string qryProjetos = "select * from t03_projeto t03";
        qryProjetos += " left join t26_arearesultado t26";
        qryProjetos += " on t03.t26_cd_arearesultado = t26.t26_cd_arearesultado";
        qryProjetos += " where t03.t03_cd_projeto in (select t29.t03_cd_projeto";
        qryProjetos += "                              from t29_acoes_vinculadas_projeto t29";
        qryProjetos += "                              where t29.fl_deletado = 0";
        qryProjetos += CondicaoProjeto + ")";
        qryProjetos += " and t03.fl_deletado = 0";
        qryProjetos += CondicaoAreaResultado;
        qryProjetos += " order by t26.nm_area"; //, t03.nm_projeto

        DataTable dt = this.GetDataTable(qryProjetos);
        List<t03_projeto> t03list = new List<t03_projeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t03_projeto t03 = new t03_projeto();
            t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());

            if (dr["t26_cd_arearesultado"] != DBNull.Value)
            {
                t03.t26_cd_arearesultado = Convert.ToInt32(dr["t26_cd_arearesultado"]);
                t03.t26 = new t26_arearesultadoAction().Retrieve(t03.t26_cd_arearesultado);
            }

            if (dr["nm_projeto"] != DBNull.Value)
            {
                t03.nm_projeto = dr["nm_projeto"].ToString();
            }

            if (dr["t01_cd_entidade"] != DBNull.Value)
            {
                t03.t01_cd_entidade = Convert.ToInt32(dr["t01_cd_entidade"]);
            }

            if (dr["t02_cd_usuario"] != DBNull.Value)
            {
                t03.t02_cd_usuario = dr["t02_cd_usuario"].ToString();
            }

            t03list.Add(t03);
        }

        return t03list;
    }

    public List<t03_projeto> listProjetoSustent(string cod)
    {
        StringBuilder qry = new StringBuilder();
        qry.AppendLine("select *");
        qry.AppendLine(" from t03_projeto");
        qry.AppendLine(" where t03_cd_projeto = " + cod);

        DataTable dt = this.GetDataTable(qry.ToString());
        List<t03_projeto> t03list = new List<t03_projeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t03_projeto t03 = new t03_projeto();

            t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());

            if (dr["t26_cd_arearesultado"] != DBNull.Value)
            {
                t03.t26_cd_arearesultado = Convert.ToInt32(dr["t26_cd_arearesultado"]);
                t03.t26 = new t26_arearesultadoAction().Retrieve(t03.t26_cd_arearesultado);
            }

            if (dr["nm_projeto"] != DBNull.Value)
                t03.nm_projeto = dr["nm_projeto"].ToString();

            if (dr["t01_cd_entidade"] != DBNull.Value)
                t03.t01_cd_entidade = Convert.ToInt32(dr["t01_cd_entidade"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t03.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["t02_cd_usuario_monitoramento"] != DBNull.Value)
                t03.t02_cd_usuario_monitoramento = dr["t02_cd_usuario_monitoramento"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t03.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t03.dt_alterado = (DateTime)dr["dt_alterado"];

            //Arvore

            if (dr["ds_publico"] != DBNull.Value)
                t03.ds_publico = dr["ds_publico"].ToString();

            if (dr["ds_objetivo"] != DBNull.Value)
                t03.ds_objetivo = dr["ds_objetivo"].ToString();

            if (dr["dt_inicio"] != DBNull.Value)
                t03.dt_inicio = (DateTime)dr["dt_inicio"];

            if (dr["dt_fim"] != DBNull.Value)
                t03.dt_fim = (DateTime)dr["dt_fim"];

            if (dr["dt_atualizado"] != DBNull.Value)
                t03.dt_atualizado = (DateTime)dr["dt_atualizado"];

            t03list.Add(t03);
        }

        return t03list;
    }

    public List<t03_projeto> ListTodosProjetosComAcoes(List<String> lstFiltros)
    {
        string CodAreaResultado = "";
        string CodProjeto = "";
        string CodResponsavel = "";        

        if (lstFiltros[0] != "")
            CodAreaResultado = "and t3.t26_cd_arearesultado=" + lstFiltros[0];

        if (lstFiltros[1] != "")
            CodProjeto = "and t3.t03_cd_projeto=" + lstFiltros[1];

        string where = "and t03_cd_projeto in (select t03_cd_projeto from t08_acao where fl_deletado=0 ) ";

        if (lstFiltros[2] != "")
            where = "and t03_cd_projeto in (select t03_cd_projeto from t08_acao where fl_deletado=0 and t02_cd_usuario='" + lstFiltros[2] +  "') ";            

        CodResponsavel = where;

        string query = String.Format("select * from t03_projeto  t3 " +
                                     "left join t26_arearesultado t26 on t3.t26_cd_arearesultado = t26.t26_cd_arearesultado " +
                                     "where " +
                                     "t3.fl_deletado=0 {0} {1} {2} order by t26.nm_area, t3.nm_projeto", CodResponsavel, CodAreaResultado, CodProjeto);


        DataTable dt = this.GetDataTable(query);
        List<t03_projeto> t03list = new List<t03_projeto>();
        foreach (DataRow dr in dt.Rows)
        {
            t03_projeto t03 = new t03_projeto();
            t03.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());

            if (dr["t26_cd_arearesultado"] != DBNull.Value)
            {
                t03.t26_cd_arearesultado = Convert.ToInt32(dr["t26_cd_arearesultado"]);
                t03.t26 = new t26_arearesultadoAction().Retrieve(t03.t26_cd_arearesultado);
            }

            if (dr["nm_projeto"] != DBNull.Value)
                t03.nm_projeto = dr["nm_projeto"].ToString();

            if (dr["t01_cd_entidade"] != DBNull.Value)
                t03.t01_cd_entidade = Convert.ToInt32(dr["t01_cd_entidade"]);

            if (dr["t02_cd_usuario"] != DBNull.Value)
                t03.t02_cd_usuario = dr["t02_cd_usuario"].ToString();

            if (dr["t02_cd_usuario_monitoramento"] != DBNull.Value)
                t03.t02_cd_usuario_monitoramento = dr["t02_cd_usuario_monitoramento"].ToString();

            if (dr["dt_cadastro"] != DBNull.Value)
                t03.dt_cadastro = (DateTime)dr["dt_cadastro"];

            if (dr["dt_alterado"] != DBNull.Value)
                t03.dt_alterado = (DateTime)dr["dt_alterado"];

            //Arvore

            if (dr["ds_publico"] != DBNull.Value)
                t03.ds_publico = dr["ds_publico"].ToString();

            if (dr["ds_objetivo"] != DBNull.Value)
                t03.ds_objetivo = dr["ds_objetivo"].ToString();

            if (dr["dt_inicio"] != DBNull.Value)
                t03.dt_inicio = (DateTime)dr["dt_inicio"];

            if (dr["dt_fim"] != DBNull.Value)
                t03.dt_fim = (DateTime)dr["dt_fim"];

            if (dr["dt_atualizado"] != DBNull.Value)
                t03.dt_atualizado = (DateTime)dr["dt_atualizado"];

            t03list.Add(t03);
        }
        return t03list;
    }

    /*
    * VALIDAÇÂO ACESSO
    */

    public DataTable ListGerenteArea(FiltroProjeto filtro, string cd_usuario)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append(" select * from t03_projeto t03 ");
        sb.Append(" where t03.fl_deletado = 0 ");
        sb.Append(" and (t02_cd_usuario='" + cd_usuario + "' ");
        sb.Append(" or t03_cd_projeto in(select t03_cd_projeto from t06_colaborador where t02_cd_usuario='" + cd_usuario + "' ) ");
        sb.Append(" or t03_cd_projeto in(select t03_cd_projeto from t08_acao where t02_cd_usuario='" + cd_usuario + "' ) ");

        if ((filtro.T26_cd_arearesultado == null) &&
           (filtro.T01_cd_entidade_resp == null) &&
           (filtro.T01_cd_entidade_parc == null) &&
           (filtro.T21_cd_fase == null))
        {
            sb.Append(")");
        }
        else
        {
            sb.Append(") AND (t03.fl_deletado = 0 ");

            if (filtro.T26_cd_arearesultado != null)
            {
                sb.Append(" and t03.t26_cd_arearesultado=" + filtro.T26_cd_arearesultado + " ");
            }

            if (filtro.T01_cd_entidade_resp != null)
            {
                sb.Append(" and t03.t01_cd_entidade=" + filtro.T01_cd_entidade_resp + " ");
            }
            if (filtro.T01_cd_entidade_parc != null)
            {
                sb.Append(" and t03.t03_cd_projeto in (" +
                    "select t03_cd_projeto from t04_parceiro where " +
                    "t01_cd_entidade = " + filtro.T01_cd_entidade_parc + ") ");
            }
            if (filtro.T21_cd_fase != null)
            {
                sb.Append(" and ( ");
                sb.Append(" select top 1 t21_cd_fase from t22_faseprojeto  ");
                sb.Append(" where t03_cd_projeto=t03.t03_cd_projeto ");
                sb.Append(" order by dt_cadastro desc)=" + filtro.T21_cd_fase + " ");
            }
            //fechamento do filtro
            sb.Append(" ) ");
        }
        sb.Append(" order by nm_projeto ");


        return this.GetDataTable(sb.ToString());
    }

    //verifica se pertence a linha decisoria ou se é coordenador
    public DataTable ListCoordenadorLinha(string cd_usuario)
    {
        string query = "select * from t03_projeto " +
                       "where t02_cd_usuario='" + cd_usuario + "' " +
                       "or t03_cd_projeto in(select t03_cd_projeto from t06_colaborador where t02_cd_usuario='" + cd_usuario + "') " +
                       "or t03_cd_projeto in(select t03_cd_projeto from t08_acao where t02_cd_usuario='" + cd_usuario + "') " +
                       "and fl_deletado=0";
        return this.GetDataTable(query);
    }

    /*
    * FIM VALIDAÇÂO ACESSO
    */

}
