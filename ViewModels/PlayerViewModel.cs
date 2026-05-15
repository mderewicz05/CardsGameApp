using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using CardGamesApp.Models;
using CardGamesApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CardGamesApp.ViewModels;

public partial class PlayerViewModel : ViewModelBase
{
	private readonly MainWindowViewModel _mainWindowViewModel;
	private readonly PlayerService _playerService;
	private readonly AvatarService _avatarService = new();

	[ObservableProperty]
	private string newPlayerLogin = string.Empty;

	[ObservableProperty]
	private string message = string.Empty;

	[ObservableProperty]
	private Player? selectedPlayer;

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(HasAvatar))]
	[NotifyPropertyChangedFor(nameof(HasNoAvatar))]
	private string? avatarPath;

	[ObservableProperty]
	private Bitmap? avatarImage;

	public bool HasAvatar => AvatarImage is not null;

	public bool HasNoAvatar => AvatarImage is null;

	public ObservableCollection<Player> Players => _playerService.Players;

	public PlayerViewModel(MainWindowViewModel mainWindowViewModel, PlayerService playerService)
	{
		_mainWindowViewModel = mainWindowViewModel;
		_playerService = playerService;
	}

	[RelayCommand]
	private async Task ChooseAvatar(Window? owner)
	{
		if (owner is null)
		{
			Message = "Nie można otworzyć wyboru pliku.";
			return;
		}

		var selectedPath = await _avatarService.PickAvatarAsync(owner);

		if (!string.IsNullOrWhiteSpace(selectedPath))
		{
			AvatarPath = selectedPath;
			AvatarImage = new Bitmap(selectedPath);

			OnPropertyChanged(nameof(HasAvatar));
			OnPropertyChanged(nameof(HasNoAvatar));

			Message = "Wybrano avatar.";
		}
	}

	[RelayCommand]
	private void AddPlayer()
	{
		bool added = _playerService.AddPlayer(NewPlayerLogin, AvatarPath);

		if (added)
		{
			Message = $"Dodano gracza: {NewPlayerLogin.Trim()}";
			NewPlayerLogin = string.Empty;
			AvatarPath = null;
			AvatarImage = null;

			OnPropertyChanged(nameof(HasAvatar));
			OnPropertyChanged(nameof(HasNoAvatar));
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