using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bilety
{
    public class Bilet
    {
        private Osoba pasazer;
        private Lot wybrany_lot;
        private Klient kupujacy;
        public Osoba GetPasazer { get => pasazer; }

        public Bilet(Osoba _pasazer, Lot _lot, Klient _kupujacy)
        {
            pasazer = _pasazer;
            wybrany_lot = _lot;
            kupujacy = _kupujacy;
        }
        public override string ToString()
        {
            return $"Kupujacy:{kupujacy} Pasazer:{pasazer} Lot:{wybrany_lot}";
        }
        public static bool operator == (Bilet a, Bilet b)
        {
            if (a.pasazer == b.pasazer && a.wybrany_lot == b.wybrany_lot)
                return true;
            else return false;
        }
        public static bool operator != (Bilet a, Bilet b)
        {
            if (a.pasazer == b.pasazer && a.wybrany_lot == b.wybrany_lot)
                return false;
            else return true;
        }
        public string DaneDoZapisu()
        {
            return $"{kupujacy.GetNr}," +
                $"{wybrany_lot.IdLotu},{pasazer.GetNr}";
        }
    }
}