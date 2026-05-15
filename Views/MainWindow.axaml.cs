using Avalonia.Controls;
using CardGamesApp.ViewModels;

namespace CardGamesApp.Views;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		DataContext = new MainWindowViewModel();
	}
}