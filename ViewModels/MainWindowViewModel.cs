using CardGamesApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardGamesApp.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
	private readonly PlayerService _playerService;

	[ObservableProperty]
	private ViewModelBase currentViewModel;

	public MainWindowViewModel()
	{
		_playerService = new PlayerService();
		currentViewModel = new MainMenuViewModel(this, _playerService);
	}

	public void ShowMainMenu()
	{
		CurrentViewModel = new MainMenuViewModel(this, _playerService);
	}

	public void ShowPlayers()
	{
		CurrentViewModel = new PlayerViewModel(this, _playerService);
	}

	public void ShowMemoryGame()
	{
		CurrentViewModel = new MemoryGameViewModel(this, _playerService);
	}

	public void ShowOczkoGame()
	{
		CurrentViewModel = new OczkoViewModel(this, _playerService);
	}

	public void ShowHigherLowerGame()
	{
		CurrentViewModel = new PlaceholderViewModel(
			this,
			"Gra 3: Higher or Lower",
			"Tutaj osoba 3 podłączy właściwą logikę i ekran gry Higher or Lower."
		);
	}

	public void ShowHistory()
	{
		CurrentViewModel = new PlaceholderViewModel(
			this,
			"Historia rozgrywek",
			"Tutaj osoba 3 podłączy osobne okno albo widok historii rozgrywek i wygranych."
		);
	}
}