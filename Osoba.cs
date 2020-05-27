using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Bilety
{
    public class Osoba : Klient
    {
        private string imie;
        private string nazwisko;
        private string nr_paszportu;
        private List<Bilet> bilety;

        public Osoba(string imie, string nazwisko, string _nr_paszportu)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            nr_paszportu = _nr_paszportu;
        }
        ~Osoba()
        {
            bilety.Clear();
            bilety = null;
        }
        public override string ToString()
        {
            return $"{nazwisko} {imie}";
        }
    }
}