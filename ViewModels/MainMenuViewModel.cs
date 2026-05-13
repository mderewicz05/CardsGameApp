using CardGamesApp.Services;
using CommunityToolkit.Mvvm.Input;

namespace CardGamesApp.ViewModels;

public partial class MainMenuViewModel : ViewModelBase
{
	private readonly MainWindowViewModel _mainWindowViewModel;
	private readonly PlayerService _playerService;

	public int PlayerCount => _playerService.Players.Count;

	public MainMenuViewModel(MainWindowViewModel mainWindowViewModel, PlayerService playerService)
	{
		_mainWindowViewModel = mainWindowViewModel;
		_playerService = playerService;
	}

	[RelayCommand]
	private void GoToPlayers()
	{
		_mainWindowViewModel.ShowPlayers();
	}

	[RelayCommand]
	private void GoToMemoryGame()
	{
		_mainWindowViewModel.ShowMemoryGame();
	}

	[RelayCommand]
	private void GoToOczkoGame()
	{
		_mainWindowViewModel.ShowOczkoGame();
	}

	[RelayCommand]
	private void GoToHigherLowerGame()
	{
        _mainWindowViewModel.ShowHigherOrLower();
    }

    [RelayCommand]
	private void GoToHistory()
	{
        _mainWindowViewModel.ShowHistory();
    }
}
