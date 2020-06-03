using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilety
{
    public class Trasa
    {
        
        public Lotnisko Lotnisko_przylotu { get; private set; }
        public Lotnisko Lotnisko_wylotu { get; private set; }
        public double Odleglosc { get; private set; }
        private List<Lot> lista_lotow;

        public List<Lot> GetLoty { get => lista_lotow; }

        public Trasa(Lotnisko wylot, Lotnisko przylot)
        {
            Lotnisko_wylotu = wylot;
            Lotnisko_przylotu = przylot;
            Odleglosc = BiletSystem.LiczOdleglosc(wylot, przylot);
            lista_lotow = new List<Lot>();
            Console.WriteLine($"Utworzono trasę {wylot.Miasto} -> {przylot.Miasto}");
        }    
        
        public override string ToString()
        {
            return Lotnisko_wylotu.Miasto + " -> " + Lotnisko_przylotu.Miasto + " (" + Odleglosc + "km)";
        }
        public void DodajLot(DateTime data, int id)
        {
            if (Odleglosc>5000)
            {
                Console.WriteLine($"Zbyt duza odleglosc {Odleglosc}km!\nNasze samoloty latają najdalej 5000km");
                return;
            }
            lista_lotow.Add(new Lot(Lotnisko_wylotu, Lotnisko_przylotu, data, id));
        }
        public void PokazLoty()
        {
            if(lista_lotow.Count != 0)
            {
                foreach(Lot oLot in lista_lotow)
                {
                    Console.WriteLine(oLot.ToString());
                }
            } else Console.WriteLine("Brak lotow do wyswietlenia.");
        }
        public string DaneDoZapisu()
        {
            string wynik = $"{BiletSystem.GetLotniska.IndexOf(Lotnisko_wylotu)+1}," +
                $"{BiletSystem.GetLotniska.IndexOf(Lotnisko_przylotu)+1}";
            short i = 1;
            if(GetLoty.Count > 0)
            {
                wynik += ";";
            }
            foreach (Lot item in GetLoty)
            {
                wynik += item.DaneDoZapisu();
                if(i++ < GetLoty.Count())
                {
                    wynik += ".";
                }
            }
            return wynik;
        }
    }
}