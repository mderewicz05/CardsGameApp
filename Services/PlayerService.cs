using System.Collections.ObjectModel;
using CardGamesApp.Models;

namespace CardGamesApp.Services;

public class PlayerService
{
	public ObservableCollection<Player> Players { get; } = new();

	public bool AddPlayer(string login)
	{
		if (string.IsNullOrWhiteSpace(login))
		{
			return false;
		}

		login = login.Trim();

		foreach (var player in Players)
		{
			if (player.Login.ToLower() == login.ToLower())
			{
				return false;
			}
		}

		Players.Add(new Player(login));
		return true;
	}

	public void RemovePlayer(Player player)
	{
		Players.Remove(player);
	}
}