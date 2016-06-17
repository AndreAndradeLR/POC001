using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StatusProjeto
/// </summary>
public class StatusProjeto
{
	private int _qtdAzul;
	public int QtdAzul {
		get { return _qtdAzul; }
		set { _qtdAzul = value; }
	}
	
	private int _qtdAmarelo;
	public int QtdAmarelo {
		get { return _qtdAmarelo; }
		set { _qtdAmarelo = value; }
	}
	
    private int _qtdVermelho;
	public int QtdVermelho {
		get { return _qtdVermelho; }
		set { _qtdVermelho = value; }
	}
    
    private int _qtdVerde;
	public int QtdVerde {
		get { return _qtdVerde; }
		set { _qtdVerde = value; }
	}
    
    private int _qtdTotal;
	public int QtdTotal {
    	get {
            _qtdTotal = (_qtdVerde + _qtdAmarelo + _qtdAzul + _qtdVermelho);
            return _qtdTotal; 
        }
	}
    
    private double _percAzul;
	public double PercAzul {
		get 
        {
            if (_qtdTotal > 0)
                _percAzul = Math.Round(Convert.ToDouble((_qtdAzul * 100) / _qtdTotal));
            return _percAzul; 
        }
	}
    
	private double _percAmarelo;
	public double PercAmarelo {
        get
        {
            if (_qtdTotal > 0)
                _percAmarelo = Math.Round(Convert.ToDouble((_qtdAmarelo * 100) / _qtdTotal));
            return _percAmarelo;
        }
	}
	
    private double _percVermelho;
	public double PercVermelho {
        get
        {
            if (_qtdTotal > 0)
                _percVermelho = Math.Round(Convert.ToDouble((_qtdVermelho * 100) / _qtdTotal));
            return _percVermelho;
        }
	}
    
    private double _percVerde;
	public double PercVerde {
        get
        {
            if (_qtdTotal > 0)
                _percVerde = Math.Round(Convert.ToDouble((_qtdVerde * 100) / _qtdTotal));
            return _percVerde;
        }
	}
    
	
    
	public StatusProjeto()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string tbGraficoStatus()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (QtdTotal > 0)
        {
            double perc;
            sb.Append("<div class=\"barra_status\">");
            if (_qtdAzul != 0)
            {
                perc = PercAzul;
                sb.Append("<div class=\"item_status\" style=\"background:url('images/B.gif');width:" + perc + "%\" title='" + perc + "%'>&nbsp;</div>");
            }
            if (_qtdVerde != 0)
            {
                perc = PercVerde;
                sb.Append("<div class=\"item_status\" style=\"background:url('images/G.gif');width:" + perc + "%\" title='" + perc + "%'>&nbsp;</div>");
            }
            if (_qtdAmarelo != 0)
            {
                perc = PercAmarelo;
                sb.Append("<div class=\"item_status\" style=\"background:url('images/Y.gif');width:" + perc + "%\" title='" + perc + "%'>&nbsp;</div>");
            }
            if (_qtdVermelho != 0)
            {
                perc = PercVermelho;
                sb.Append("<div class=\"item_status\" style=\"background:url('images/R.gif');width:" + perc + "%\" title='" + perc + "%'>&nbsp;</div>");
            }
            sb.Append("</div>");
        }
        return sb.ToString();
    }
}
