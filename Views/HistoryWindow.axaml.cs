using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CardGamesApp.Views
{
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
