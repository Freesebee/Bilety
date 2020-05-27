using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Bilety
{
    public static class BiletSystem
    {
        private static List<Samolot> dostepne_samoloty;
        private static List<Osoba> lista_pasazerow;
        private static List<Trasa> lista_tras;

        static BiletSystem()
        {
            lista_tras = null;
            dostepne_samoloty = null;
            lista_pasazerow = null;
        }
        public static void DodajLot(Trasa trasa_lotu)
        {
            if (!lista_tras.Contains(trasa_lotu)) lista_tras.Add(trasa_lotu);
        }
        public static void UsunLot(Lot _lot)
        {
            throw new System.NotImplementedException();
        }
        public static void ZapiszStan()
        {
            throw new System.NotImplementedException();
        }

        public static void WczytajStan()
        {
            throw new System.NotImplementedException();
        }
        public static void DodajTrase()
        {
            throw new System.NotImplementedException();
        }

        public static void UsunTrase()
        {
            throw new System.NotImplementedException();
        }

        public static void DodajPasazer()
        {
            throw new System.NotImplementedException();
        }

        public static void UsunPasazera()
        {
            throw new System.NotImplementedException();
        }

        public static void DodajSamolot()
        {
            throw new System.NotImplementedException();
        }

        public static void UsunSamolot()
        {
            throw new System.NotImplementedException();
        }
    }
}