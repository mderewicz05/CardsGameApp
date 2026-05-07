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
	private void GoToWarGame()
	{
		_mainWindowViewModel.ShowWarGame();
	}

	[RelayCommand]
	private void GoToTwentyOneGame()
	{
		_mainWindowViewModel.ShowTwentyOneGame();
	}

	[RelayCommand]
	private void GoToHigherLowerGame()
	{
		_mainWindowViewModel.ShowHigherLowerGame();
	}

	[RelayCommand]
	private void GoToHistory()
	{
		_mainWindowViewModel.ShowHistory();
	}
}
