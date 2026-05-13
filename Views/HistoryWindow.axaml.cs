using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CardGamesApp.Services;

namespace CardGamesApp.Views
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            DataContext = new { History = GameHistoryService.GetHistory() };
        
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
