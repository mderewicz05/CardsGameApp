namespace CardGamesApp.Models;

public class Card
{
    public string Kolor { get; set; }
    public string Figura { get; set; }
    public int Wartosc { get; set; }

    
    
    public Card(string kolor, string figura, int wartosc)
    {
        Kolor = kolor;
        Figura = figura;
        Wartosc = wartosc;
    }
    public override string ToString()
    {
        return $"{Figura} {Kolor}";
    }
}