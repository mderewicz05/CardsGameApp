namespace CardGamesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class Deck
{
    private readonly Random _random = new();
    public List<Card> Karty { get; private set; } = new();

    public Deck()
    {
        UtworzTalie();
        Tasuj();
    }
    private void UtworzTalie()
    {
        Karty.Clear();
        string[] kolory = { "♠", "♥", "♦", "♣" };
        var figury = new List<(string Figura, int Wartosc)>
        {
            ("2", 2),
            ("3", 3),
            ("4", 4),
            ("5", 5),
            ("6", 6),
            ("7", 7),
            ("8", 8),
            ("9", 9),
            ("10", 10),
            ("J", 11),
            ("Q", 12),
            ("K", 13),
            ("A", 14)
        };

        foreach (var kolor in kolory)
        {
            foreach (var figura in figury)
            {
                Karty.Add(new Card(kolor, figura.Figura, figura.Wartosc));
            }
        } }

    public void Tasuj()
    {
        Karty = Karty.OrderBy(_ => _random.Next()).ToList();
    }

    public Card DobierzKarte()
    {
        if (Karty.Count == 0)
        {
            UtworzTalie();
            Tasuj();
        }
        Card karta = Karty[0];
        Karty.RemoveAt(0);
        return karta;
    }
}