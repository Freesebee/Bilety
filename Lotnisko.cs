using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilety
{
    public class Lotnisko
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Miasto { get; private set; }
        public string Kraj { get; private set; }

        //Tworzy obiekt Lotnisko i automatycznie dodaje je na lista_lotnisk w BiletSystem
        public Lotnisko(int _x, int _y, string _kraj, string _miasto)
        {
            //Sprawdza, czy lotnisko o podanych współrzędnych jest już na liście:
            foreach (Lotnisko oLotnisko in BiletSystem.GetLotniska)
            {
                try
                {
                    if (_x == oLotnisko.X && _y == oLotnisko.Y)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Lotnisko o współrzędnych " +
                            $"({_x},{_y}) już istnieje.");
                    return; //jeśli lotnisko jest już na liście, przerywa konstruktor
                }
            }
            X = _x;
            Y = _y;
            Kraj = _kraj;
            Miasto = _miasto;
            Console.WriteLine($"Utworzono lotnisko {_kraj} {_miasto} ({_x},{_y})");
        }
        public override string ToString()
        {
            return "\nKraj: " + Kraj +
                "\nMiasto: " + Miasto +
                $"\nWspółrzędne: ({X},{Y})";
        }
    }
}