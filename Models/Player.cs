namespace CardGamesApp.Models;

public class Player
{
	public string Login { get; set; }

	public Player(string login)
	{
		Login = login;
	}

	public override string ToString()
	{
		return Login;
	}
}