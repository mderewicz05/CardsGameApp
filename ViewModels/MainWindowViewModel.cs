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

	}

	public void ShowTwentyOneGame()
	{
		
	}

	public void ShowHigherLowerGame()
	{
		
	}

	public void ShowHistory()
	{
		
	}
}