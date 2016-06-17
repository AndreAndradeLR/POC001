using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
/// <summary>
/// Summary description for t30_AcoesVinculadasProjetoAction
/// </summary>
public class t30_AcoesVinculadasProjetoAction : SQLServerBase
{
    pageBase pb = new pageBase();

    public t30_AcoesVinculadasProjetoAction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int InsertDB(t30_AcoesVinculadasProjeto t30)
    {
        string query = "insert into t29_acoes_vinculadas_projeto (t08_cd_acao, t03_cd_projeto, fl_deletado) " +
            "values(@t08_cd_acao, @t03_cd_projeto, 0)";

        SqlParameter[] param = new SqlParameter[]
        {
            MakeInParam("@t08_cd_acao", SqlDbType.Int, 0, t30.t08_cd_acao),
            MakeInParam("@t03_cd_projeto", SqlDbType.Int, 0, t30.t03_cd_projeto)
        };

        return this.RunCommand(query, param);
    }

    public int DeleteDB(int id)
    {
        //exclusao logica
        string query = "update t29_acoes_vinculadas_projeto set  " +
            " fl_deletado=1 " +
            " where cd_acoes_vinculadas_projeto =" + id;

        return this.RunCommand(query);
    }

    public t30_AcoesVinculadasProjeto Retrieve(int id)
    {
        string query = "select * from t29_acoes_vinculadas_projeto where cd_acoes_vinculadas_projeto=" + id;
        DataTable dt = this.GetDataTable(query);
        t30_AcoesVinculadasProjeto t30 = new t30_AcoesVinculadasProjeto();
        foreach (DataRow dr in dt.Rows)
        {
            t30.cd_acoes_vinculadas_projeto = int.Parse(dr["cd_acoes_vinculadas_projeto"].ToString());
            t30.t03_cd_projeto = int.Parse(dr["t03_cd_projeto"].ToString());
            t30.t08_cd_acao = int.Parse(dr["t08_cd_acao"].ToString());

            break;
        }
        return t30;
    }
    
    public DataTable ListTodos(int t08_cd_acao)
    {
        string query = "select a.cd_acoes_vinculadas_projeto, a.t03_cd_projeto, p.nm_projeto ";
        query += " from t29_acoes_vinculadas_projeto a inner join t03_projeto p ";
        query += " on a.t03_cd_projeto = p.t03_cd_projeto";
        query += " where a.fl_deletado=0 ";
        query += " and a.t08_cd_acao = " +  t08_cd_acao + "";
        query += " order by p.nm_projeto asc";
            
        return this.GetDataTable(query);
    }

    public DataTable ListTodosProjetos()
    {
        string query = "select distinct t29.t03_cd_projeto, p.nm_projeto ";
        query += " from t29_acoes_vinculadas_projeto t29 left join t03_projeto p ";
        query += " on t29.t03_cd_projeto = p.t03_cd_projeto";
        query += " where t29.fl_deletado=0 ";
        query += " order by p.nm_projeto asc";

        return this.GetDataTable(query);
    }

    public DataTable ListTodosProjetosDoGerente(string cd_responsavel)
    {
        string query = "select distinct t29.t03_cd_projeto, p.nm_projeto ";
        query += " from t29_acoes_vinculadas_projeto t29 left join t03_projeto p ";
        query += " on t29.t03_cd_projeto = p.t03_cd_projeto";
        query += " where t29.fl_deletado=0 and p.t02_cd_usuario='" + cd_responsavel + "'";
        query += " order by p.nm_projeto asc";

        return this.GetDataTable(query);
    }

    public DataTable LisProjPaiAcao(int Cod)
    {
        string qryPai = "select * from t08_acao t08";
        qryPai += " inner join dbo.t29_acoes_vinculadas_projeto t29";
        qryPai += " on t29.t08_cd_acao=t08.t08_cd_acao";
        qryPai += " where t29.t03_cd_projeto = " + Cod;
        qryPai += " order by t08.t03_cd_projeto";

        return this.GetDataTable(qryPai);
    }

    public List<t03_projeto> LisProjPaiAcaoVinculada(int Cod, string CodArea)
    {
        string qry = "select * from t03_projeto t3 left join t26_arearesultado t26  " +
            "on t3.t26_cd_arearesultado = t26.t26_cd_arearesultado "; 
			
        qry += " where t3.t03_cd_projeto";
        qry += " in(select t08.t03_cd_projeto from t08_acao t08";
        qry += " inner join dbo.t29_acoes_vinculadas_projeto t29";
        qry += " on t29.t08_cd_acao=t08.t08_cd_acao";
        qry += " where t29.t03_cd_projeto = " + Cod + " and t29.fl_deletado=0)";
        if (!String.IsNullOrEmpty(CodArea))
        {
            qry += " and t3.t26_cd_arearesultado = " + Convert.ToInt32(CodArea);
        }
        qry += " order by t26.nm_area";

        DataTable dt = this.GetDataTable(qry);
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

    public DataTable LisProjAcaoVinculadaFiltro(int CodPro, int CodArea)
    {
       string qry1 =  "select * from t08_acao t08";
       qry1 += " inner join t29_acoes_vinculadas_projeto t29";
       qry1 += " on t29.t08_cd_acao = t08.t08_cd_acao";
       qry1 += " inner join t03_projeto t03";
       qry1 += " on t03.t03_cd_projeto = t08.t03_cd_projeto";
       qry1 += " where t08.t08_cd_acao in (select t08_cd_acao from t10_produto where fl_deletado=0)";
       qry1 += " and t29.t03_cd_projeto = " + CodPro; 
       qry1 += " and t08.fl_deletado = 0";
       qry1 += " and t29.fl_deletado = 0";
       qry1 += " and t03.t26_cd_arearesultado = " + CodArea;
       qry1 += " order by t08.t03_cd_projeto, t08.nm_acao"; 

       return this.GetDataTable(qry1);
    }
}
