namespace CardGamesApp.Models;

public class Player
{
	public string Login { get; set; }

	public string? AvatarPath { get; set; }

	public Player(string login)
	{
		Login = login;
		AvatarPath = null;
	}

	public Player(string login, string? avatarPath)
	{
		Login = login;
		AvatarPath = avatarPath;
	}

	public override string ToString()
	{
		return Login;
	}
}