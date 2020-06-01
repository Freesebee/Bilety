using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bilety
{
    public class Osoba : Klient
    {
        private string imie;
        private string nazwisko;
        private string nr_paszportu;
        private List<Bilet> bilety;
        public override string GetNr { get => nr_paszportu; }

        public Osoba(string _imie, string _nazwisko, string _nr_paszportu)
        {
            if(!BiletSystem.CzyNumer(_nr_paszportu))
            {
                throw new NiepoprawnaInformacjaException("Numer paszportu musi zawierac tylko cyfry");
            }
            if (!BiletSystem.CzyTekst(_imie))
            {
                throw new NiepoprawnaInformacjaException("Imie moze zawierac tylko litery");
            }
            if (!BiletSystem.CzyTekst(_nazwisko))
            {
                throw new NiepoprawnaInformacjaException("Nazwisko moze zawierac tylko litery");
            }
            imie = _imie;
            nazwisko = _nazwisko;
            nr_paszportu = _nr_paszportu;
            bilety = new List<Bilet>();
        }
        ~Osoba()
        {
            if (bilety != null) bilety.Clear();
            bilety = null;
        }
        public void PrzekazBilet(Bilet b)
        {
            bilety.Add(b);
        }
        public bool CzyPosiadaTakiBilet(Bilet b)
        {
            foreach (Bilet bilet in bilety)
            {
                if (bilet == b)
                    return true;
            }
            return false;
        }
        public override bool CzyZawieraZnaki(string tekst)
        {
            try
            {
                if (imie.Contains(tekst)
                || nazwisko.Contains(tekst)
                || nr_paszportu.Contains(tekst))
                    return true;
                else return false;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public override bool CzyTenSamUnikalnyNr(string nr)
        {
            if (string.Equals(nr, nr_paszportu)) return true;
            else return false;
        }
        public override string ToString()
        {
            return $"Numer paszportu:{nr_paszportu} - {nazwisko} {imie}";
        }
        public override string DaneDoZapisu()
        {
            return $"{imie},{nazwisko},{nr_paszportu}";
        }
        public static bool operator == (Osoba a, Osoba b)
        {
            if (a.nr_paszportu == b.nr_paszportu) return true;
            else return false;
        }
        public static bool operator != (Osoba a, Osoba b)
        {
            if (a.nr_paszportu != b.nr_paszportu) return true;
            else return false;
        }
    }
}