using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ScreenResolution
/// </summary>
public class ScreenResolution
{
	private string _browser;
	public string Browser {
		get { return _browser; }
		set { _browser = value; }
	}
	
	private string _brow_ver;
	public string Brow_ver {
		get { return _brow_ver; }
		set { _brow_ver = value; }
	}
	
	private int _height;
	public int Height {
		get { return _height; }
		set { _height = value; }
	}
	
	private int _width;
	public int Width {
		get { return _width; }
		set { _width = value; }
	}
	
	private string _color;
	public string Color {
		get { return _color; }
		set { _color = value; }
	}

    private int _widthWeb;
    public int WidthWeb
    {
        get
        {
            int perc = 0;
            if (_width > 0)
            {
                perc = Convert.ToInt32(_width * 0.09);
                _widthWeb = _width - perc;
            }
            return _widthWeb;
        }
    }

	public ScreenResolution(string browser, string brow_ver, 
	                        int height, int width, string color)
	{
		this.Browser = browser;
		this.Brow_ver = brow_ver;
		this.Height = height;
		this.Width = width;
		this.Color = color;
	}
	
	public ScreenResolution()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
