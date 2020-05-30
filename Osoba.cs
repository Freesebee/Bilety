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

        public Osoba(string _imie, string _nazwisko, string _nr_paszportu)
        {
            BiletSystem.CzyNumer(_nr_paszportu); //Sprawdzenie poprawności numeru paszportu
            BiletSystem.CzyTekst(_imie); //Sprawdzenie poprawności imienia
            BiletSystem.CzyTekst(_nazwisko); //Sprawdzenie poprawności nazwiska
            imie = _imie;
            nazwisko = _nazwisko;
            nr_paszportu = _nr_paszportu;
            bilety = new List<Bilet>();
        }
        ~Osoba()
        {
            if(bilety!=null) bilety.Clear();
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
            if (string.Equals(nr,nr_paszportu)) return true;
            else return false;
        }
        public override string ToString()
        {
            return $"{nazwisko} {imie}";
        }
    }
}