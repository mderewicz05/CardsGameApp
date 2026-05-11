using CommunityToolkit.Mvvm.ComponentModel;

namespace CardGamesApp.Models;

public partial class MemoryCard : ObservableObject
{
    public int Id { get; set; }
    public string Symbol { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WyswietlanyTekst))]
    private bool czyOdkryta;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(WyswietlanyTekst))]
    private bool czyZnaleziona;

    public string WyswietlanyTekst
    {
        get
        {
            if (CzyOdkryta || CzyZnaleziona)
            {
                return Symbol;
            }

            return "?";
        }
    }

    public MemoryCard(int id, string symbol)
    {
        Id = id;
        Symbol = symbol;
    }
}