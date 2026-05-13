using Avalonia.Controls;
using CardGamesApp.ViewModels;

namespace CardGamesApp.Views
{
    public partial class HigherOrLowerView : UserControl
    {
        public HigherOrLowerView()
        {
            InitializeComponent();
            DataContext = new HigherOrLowerViewModel();
        }
    }
}