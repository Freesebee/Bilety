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
        public double CzasLotu { get; private set; }
        private List<Lot> lista_lotow = new List<Lot>();

        public Trasa(Lotnisko wylot, Lotnisko przylot)
        {
            Lotnisko_wylotu = wylot;
            Lotnisko_przylotu = przylot;
            Odleglosc = LiczOdleglosc();
            CzasLotu = CzasLotuNaTrasie(Odleglosc);
            Console.WriteLine($"Utworzono trasę {przylot.Miasto} -> {wylot.Miasto}");
        }
        public void DodajLot(Lot lot)
        {
            lista_lotow.Add(lot);
        }
        public void UsunLot(Lot lot)
        {
            throw new NotImplementedException();
        }
        public double LiczOdleglosc()
        {
            double droga;
            Lotnisko L1 = Lotnisko_wylotu, L2 = Lotnisko_przylotu;
            int x1 = L1.X, y1 = L1.Y, x2 = L2.X, y2 = L2.Y;
            droga = Math.Round(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)), 2);
            return droga * 100; //jedna kratka w układzie współrzędnych odpowiada 100km
        }
        public double CzasLotuNaTrasie(double droga)
        {
            double predkosc = 800; //km na godzine
            double czas = Math.Round((droga / predkosc), 2); //czas w godzinach, z resztą dziesiętną
            return czas;
        }
        public string CzasToString() //zwraca godzinę w formacie 5h 46m
        {
            double godzina, minuta;
            godzina = Math.Floor(CzasLotu);
            minuta = Math.Floor((CzasLotu - godzina) * 60);
            return $"{godzina}h {minuta}m";
        }
        public List<Lot> GetListaLotowNaTrasie()
        {
            return lista_lotow;
        }
    }
}