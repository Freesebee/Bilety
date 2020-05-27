using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilety
{
    public class Bilet
    {
        private Osoba pasazer;
        private Lot wybrany_lot;
        private Klient kupujacy;

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
    }
}