using System;
public class t25_usuarioperfil
{
	#region Declarations

	private int _t25_cd_usuarioperfil;
	private string _t02_cd_usuario;
	private int _t24_cd_perfil;
	private DateTime _dt_cadastro;

	#endregion

	#region Properties

	public int t25_cd_usuarioperfil
	{
		get { return _t25_cd_usuarioperfil; }
		set { _t25_cd_usuarioperfil = value; }
	}

	public string t02_cd_usuario
	{
		get { return _t02_cd_usuario; }
		set { _t02_cd_usuario = value; }
	}

	public int t24_cd_perfil
	{
		get { return _t24_cd_perfil; }
		set { _t24_cd_perfil = value; }
	}

	public DateTime dt_cadastro
	{
		get { return _dt_cadastro; }
		set { _dt_cadastro = value; }
	}


	#endregion

    public t25_usuarioperfil()
    {
    
    }

    public t25_usuarioperfil(string cd_usuario, int cd_perfil)
    {
        _t02_cd_usuario = cd_usuario;
        _t24_cd_perfil = cd_perfil;
    }

}
