using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class ucNavegacao : System.Web.UI.UserControl
{
    bool _projetos;
    bool _arvore;
    bool _responsaveis;
    bool _resultado;
    bool _projFin;
    bool _premissas;
    bool _colaboradores;
    bool _agenda;
    bool _agendaProjetos;
    bool _noticias;
    bool _noticiasProjetos;
    bool _parceiros;
    bool _documentos;
    bool _detalhamento;
    bool _acao;
    bool _financeiro;
    bool _produto;
    bool _relProduto;
    bool _acaoGraf;
    bool _acaoFin;
    bool _restricao;
    bool _restricaoSup;
    
    public bool arvore
    {
        get { return _arvore; }
        set { _arvore = value; }
    }

    public bool responsaveis
    {
        get { return _responsaveis; }
        set { _responsaveis = value; }
    }
    public bool projetos
    {
        get { return _projetos; }
        set { _projetos = value; }
    }
    public bool resultado
    {
        get { return _resultado; }
        set { _resultado = value; }
    }

    public bool projFin
    {
        get { return _projFin; }
        set { _projFin = value; }
    }
    public bool premissas
    {
        get { return _premissas; }
        set { _premissas = value; }
    }
    public bool colaboradores
    {
        get { return _colaboradores; }
        set { _colaboradores = value; }
    }
    public bool agenda
    {
        get { return _agenda; }
        set { _agenda = value; }
    }
    public bool agendaProjetos
    {
        get { return _agendaProjetos; }
        set { _agendaProjetos = value; }
    }
    public bool noticias
    {
        get { return _noticias; }
        set { _noticias = value; }
    }
    public bool noticiasProjetos
    {
        get { return _noticiasProjetos; }
        set { _noticiasProjetos = value; }
    }
    public bool parceiros
    {
        get { return _parceiros; }
        set { _parceiros = value; }
    }
    public bool documentos
    {
        get { return _documentos; }
        set { _documentos = value; }
    }

    public bool detalhamento
    {
        get { return _detalhamento; }
        set { _detalhamento = value; }
    }
    public bool acao
    {
        get { return _acao; }
        set { _acao = value; }
    }
    public bool financeiro
    {
        get { return _financeiro; }
        set { _financeiro = value; }
    }
    public bool produto
    {
        get { return _produto; }
        set { _produto = value; }
    }

    public bool relProduto
    {
        get { return _relProduto; }
        set { _relProduto = value; }
    }

    public bool acaoGraf
    {
        get { return _acaoGraf; }
        set { _acaoGraf = value; }
    }

    public bool acaoFin
    {
        get { return _acaoFin; }
        set { _acaoFin = value; }
    }
    public bool restricao
    {
        get { return _restricao; }
        set { _restricao = value; }
    }
    public bool restricaoSup
    {
        get { return _restricaoSup; }
        set { _restricaoSup = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        pageBase pb = new pageBase();
        linkArvore.Visible = false;
        linkProjetos.Visible = false;
        linkResp.Visible = false;
        linkResultado.Visible = false;
        linkDetalhamento.Visible = false;
        linkRestricao.Visible = false;
        linkAcao.Visible = false;
        linkProjFin.Visible = false;
        linkAcaoFin.Visible = false;
        linkColaboradores.Visible = false;
        linkAgenda.Visible = false;
        linkNoticias.Visible = false;
        linkParceiros.Visible = false;
        linkDocumentos.Visible = false;
        linkAgendaProjetos.Visible = false;
        linkNoticiasProjetos.Visible = false;
        linkAcaoGraf.Visible = false;
        linkAcaoFin.Visible = false;
        linkProduto.Visible = false;
        linkRelProduto.Visible = false;
        linkFinanceiro.Visible = false;
        linkRestricaoSup.Visible = false;
        if (_projetos)
        {
            linkProjetos.Visible = true;
            linkProjetos.NavigateUrl = "";
            linkProjetos.Font.Bold = true;
        }
        else if (_noticiasProjetos)
        {
            linkNoticiasProjetos.Visible = true;
            linkNoticiasProjetos.NavigateUrl = "";
            linkNoticiasProjetos.Font.Bold = true;
        }
        else if (_agendaProjetos)
        {
            linkAgendaProjetos.Visible = true;
            linkAgendaProjetos.NavigateUrl = "";
            linkAgendaProjetos.Font.Bold = true;
        }
        else if (_arvore)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkArvore.NavigateUrl ="";
            linkArvore.Font.Bold = true;
        }
        else if (_responsaveis)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkResp.Visible = true;
            linkResp.NavigateUrl = "";
            linkResp.Font.Bold = true;
        }
        else if (_resultado)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkResultado.Visible = true;
            linkResultado.NavigateUrl = "";
            linkResultado.Font.Bold = true;
        }
        else if (_projFin)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkProjFin.Visible = true;
            linkProjFin.NavigateUrl = "";
            linkProjFin.Font.Bold = true;
        }
        else if (_colaboradores)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkColaboradores.Visible = true;
            linkColaboradores.NavigateUrl = "";
            linkColaboradores.Font.Bold = true;
        }
        else if (_agenda)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkAgenda.Visible = true;
            linkAgenda.NavigateUrl = "";
            linkAgenda.Font.Bold = true;
        }
        else if (_noticias)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkNoticias.Visible = true;
            linkNoticias.NavigateUrl = "";
            linkNoticias.Font.Bold = true;
        }
        else if (_parceiros)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkParceiros.Visible = true;
            linkParceiros.NavigateUrl = "";
            linkParceiros.Font.Bold = true;
        }
        else if (_documentos)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDocumentos.Visible = true;
            linkDocumentos.NavigateUrl = "";
            linkDocumentos.Font.Bold = true;
        }
        else if (_detalhamento)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;
            linkDetalhamento.NavigateUrl = "";
            linkDetalhamento.Font.Bold = true;
        }
        else if (_acao)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;
            linkAcao.Visible = true;
            linkAcao.NavigateUrl = "";
            linkAcao.Font.Bold = true;
        }
        else if (_acaoGraf)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;
            linkAcaoGraf.Visible = true;
            linkAcaoGraf.NavigateUrl = "";
            linkAcaoGraf.Font.Bold = true;

        }
        else if (_acaoFin)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;
            linkAcaoFin.Visible = true;
            linkAcaoFin.NavigateUrl = "";
            linkAcaoFin.Font.Bold = true;

        }
        else if (_financeiro)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;
            linkAcao.Visible = true;
            linkFinanceiro.Visible = true;
            linkFinanceiro.NavigateUrl = "";
            linkFinanceiro.Font.Bold = true;

        }
        else if (_produto)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;
            linkAcao.Visible = true;
            linkProduto.Visible = true;
            linkProduto.NavigateUrl = "";
            linkProduto.Font.Bold = true;
        }

        else if (_relProduto)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;            
            linkRelProduto.Visible = true;
            linkRelProduto.NavigateUrl = "";
            linkRelProduto.Font.Bold = true;
        }           

        else if (_restricao)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;
            linkRestricao.Visible = true;
            linkRestricao.NavigateUrl = "";
            linkRestricao.Font.Bold = true;
        }
        else if (_restricaoSup)
        {
            linkProjetos.Visible = true;
            linkArvore.Visible = true;
            linkDetalhamento.Visible = true;
            linkRestricaoSup.Visible = true;
            linkRestricaoSup.NavigateUrl = "";
            linkRestricaoSup.Font.Bold = true;
        }
        else
        {
            //linkFiltro.NavigateUrl = "";
            //linkFiltro.Font.Bold = true;
        }

    }
}
