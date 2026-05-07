using System.Collections.ObjectModel;
using CardGamesApp.Models;
using CardGamesApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Tmds.DBus.Protocol;

namespace CardGamesApp.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
	private readonly MainWindowViewModel _mainWindowViewModel;
	private readonly PlayerService _playerService;

	[ObservableProperty]
	private string newPlayerLogin = string.Empty;

	[ObservableProperty]
	private string message = string.Empty;

	[ObservableProperty]
	private Player? selectedPlayer;

	public ObservableCollection<Player> Players => _playerService.Players;

	public PlayerViewModel(MainWindowViewModel mainWindowViewModel, PlayerService playerService)
	{
		_mainWindowViewModel = mainWindowViewModel;
		_playerService = playerService;
	}

	[RelayCommand]
	private void AddPlayer()
	{
		bool added = _playerService.AddPlayer(NewPlayerLogin);

		if (added)
		{
			Message = $"Dodano gracza: {NewPlayerLogin.Trim()}";
			NewPlayerLogin = string.Empty;
		}
		else
		{
			Message = "Nie można dodać gracza. Login jest pusty albo już istnieje.";
		}
	}

	[RelayCommand]
	private void RemoveSelectedPlayer()
	{
		if (SelectedPlayer is null)
		{
			Message = "Najpierw wybierz gracza z listy.";
			return;
		}

		_playerService.RemovePlayer(SelectedPlayer);
		Message = "Usunięto gracza.";
		SelectedPlayer = null;
	}

	[RelayCommand]
	private void BackToMenu()
	{
		_mainWindowViewModel.ShowMainMenu();
	}
}