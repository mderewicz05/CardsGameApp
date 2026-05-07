using CommunityToolkit.Mvvm.Input;

namespace CardGamesApp.ViewModels;

public partial class PlaceholderViewModel : ViewModelBase
{
	private readonly MainWindowViewModel _mainWindowViewModel;

	public string Title { get; }
	public string Description { get; }

	public PlaceholderViewModel(MainWindowViewModel mainWindowViewModel, string title, string description)
	{
		_mainWindowViewModel = mainWindowViewModel;
		Title = title;
		Description = description;
	}

	[RelayCommand]
	private void BackToMenu()
	{
		_mainWindowViewModel.ShowMainMenu();
	}
}