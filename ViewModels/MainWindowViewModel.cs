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
		CurrentViewModel = new MainMenuViewModel(this, _playerService);
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

	public void ShowHigherOrLower()
	{
        CurrentViewModel = new HigherOrLowerViewModel(this, _playerService);
    }

	public void ShowHistory()
	{
        var historyWin = new CardGamesApp.Views.HistoryWindow();
        historyWin.Show();
	}
}