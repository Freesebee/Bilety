﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Bilety
{
    public class Lot
    {
        public int IdLotu {get; private set; }
        public int LiczbaMiejsc {get; private set; }
        public Samolot samolot { get; private set; }
        public Lotnisko Lotnisko_przylotu { get; private set; }
        public Lotnisko Lotnisko_wylotu { get; private set; }
        public DateTime czas_wylotu  { get; private set; }
        public DateTime czas_przylotu  { get; private set; }
        public TimeSpan CzasLotu { get; private set; }
        private List<Bilet> bilety;
        public List<Bilet> GetBilety { get => bilety; }

        public Lot(Lotnisko wylot, Lotnisko przylot, DateTime czaswylotu, int ID)
        {
            samolot = DobierzSamolot(wylot, przylot);
            Lotnisko_wylotu = wylot;
            Lotnisko_przylotu = przylot;
            czas_wylotu = czaswylotu;
            CzasLotu = LiczCzasLotu(BiletSystem.LiczOdleglosc(wylot, przylot));
            czas_przylotu = czas_wylotu + CzasLotu;
            LiczbaMiejsc = samolot.liczba_miejsc;
            IdLotu = ID;
            bilety = new List<Bilet>();
        }
        ~Lot()
        {
            bilety.Clear();
            bilety = null;
        }
        public override string ToString()
        {
            return $"\nID: {IdLotu}\nWylot: {czas_wylotu}\nCzas lotu: {CzasLotu}\nWolnych miejsc: {LiczbaMiejsc}";
        }
        private Samolot DobierzSamolot(Lotnisko wylot, Lotnisko przylot)
        {
            double odleglosc = BiletSystem.LiczOdleglosc(wylot, przylot);
            if (odleglosc > 0 && odleglosc <= 500) 
            {
                return BiletSystem.DodajSamolot("bombardier");
            } else if (odleglosc > 500 && odleglosc <= 1200) 
            {
                return BiletSystem.DodajSamolot("airbus");
            } else if (odleglosc > 1200 && odleglosc <= 5000) 
            {
                return BiletSystem.DodajSamolot("boeing");
            } else 
            {
                Console.WriteLine("Kurde, przypał. Za daleka trasa, nie mamy takich samolotów :( ");
                return null;
            }
        }

        public void ZarezerwujMiejsce()
        {
            LiczbaMiejsc--;
        }

        public TimeSpan LiczCzasLotu(double droga)
        {
            double predkosc = samolot.predkosc; //km na godzine
            int godzina, minuta;
            double czas = Math.Round((droga / predkosc), 2); //czas w godzinach, z resztą dziesiętną
            godzina = (int)Math.Floor(czas);
            minuta = (int)Math.Floor((czas - godzina) * 60);
            TimeSpan czasSpan = new TimeSpan(godzina, minuta, 0);
            return czasSpan;
        }
        public string CzasLotuToString()
        {
            return $"{CzasLotu.Hours}h {CzasLotu.Minutes}m";
        }
    }
}