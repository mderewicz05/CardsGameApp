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

	public void ShowWarGame()
	{
		CurrentViewModel = new PlaceholderViewModel(
			this,
			"Gra 1: Wojna",
			"Tutaj osoba 2 podłączy właściwą logikę i ekran gry Wojna."
		);
	}

	public void ShowTwentyOneGame()
	{
		CurrentViewModel = new PlaceholderViewModel(
			this,
			"Gra 2: Oczko / 21",
			"Tutaj osoba 2 podłączy właściwą logikę i ekran gry Oczko / 21."
		);
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