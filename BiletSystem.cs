using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Bilety
{
    public static class BiletSystem
    {
        private static List<Samolot> dostepne_samoloty;
        private static List<Osoba> lista_pasazerow;
        private static List<Trasa> lista_tras;
        private static List<Firma> lista_firm;
        public static List<Klient> GetKlienci
        { 
            get 
            {//Zwrócenie utworzonej listy z elementów list firm i pasażerów
                List<Klient> L = new List<Klient>();
                foreach (Firma item in lista_firm)
                {
                    L.Add(item as Klient);
                }
                foreach (Osoba item in lista_pasazerow)
                {
                    L.Add(item as Klient);
                }
                return L;
            } 
        }
        public static List<Firma> GetFirmy { get => lista_firm; }
        public static List<Osoba> GetOsoby { get => lista_pasazerow; }
        static BiletSystem()
        {
            lista_tras = new List<Trasa>();
            dostepne_samoloty = new List<Samolot>();
            lista_pasazerow = new List<Osoba>();
            lista_firm = new List<Firma>();
        }
        public static void DodajLot(Trasa trasa_lotu)
        {
            throw new System.NotImplementedException();
        }
        public static void UsunLot()
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
        public static void RezerwujBilet(Klient kupujacy_bilet, Lot dany_lot, Osoba pasazer)
        {
            throw new System.NotImplementedException();
        }
        public static bool CzyTekst(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsLetter(c))
                    throw new NiepoprawnaInformacjaException("Podany tekst zawiera cyfry");
            }
            return true;
        }
        public static bool CzyNumer(string nr)
        {
            foreach (char c in nr)
            {
                if (!char.IsDigit(c))
                    throw new NiepoprawnaInformacjaException("Numer zawiera inne znaki niż cyfry");
            }
            return true;
        }
        private static bool CzyWystepujeNr<T>(string nr, List<T> lista)
        {
            if(CzyNumer(nr) && lista!=null) //Sprawdzenie poprawności numeru i listy
                foreach (T item in lista) //Sprawdzenie unikalności numeru w systemie
                {
                    if ((item as Klient).CzyTenSamUnikalnyNr(nr))
                    {
                        string komunikat = "";
                        if (item is Osoba)
                            komunikat = "W systemie istnieje pasazer o danym numerze paszportu";
                        else if (item is Firma)
                            komunikat = "W systemie istnieje firma o danym numerze KRS";
                        throw new DuplikatNumeruException(komunikat);
                    }
                }
            return false;
        }
        public static void DodajPasazera(string imie, string naziwsko, string nr_paszportu)
        {
            try
            {   //Sprawdzenie unikalności numeru paszportu
                if (!CzyWystepujeNr<Osoba>(nr_paszportu, lista_pasazerow)) 
                    lista_pasazerow.Add(new Osoba(imie, naziwsko, nr_paszportu));
            }
            catch (Exception e)
            {    //Poinformowanie o napotkanym błędzie przy wprowadzeniu informacji
                string komunikat = ""; 
                if (e is DuplikatNumeruException)
                    komunikat = "Podany numer paszportu wystepuje w systemie, wprowadz inny";
                else if (e is NiepoprawnaInformacjaException)
                    komunikat = "Numer paszportu moze zawierac jedynie cyfry, a nazwisko i imie jedynie litery";
                Console.WriteLine(komunikat);
                throw new BrakObiektuDoDodaniaException("Nie można dodac niepoprawnego obiektu");
            }
        }
        public static void UsunPasazera(Osoba pasazer)
        {
            lista_pasazerow.Remove(pasazer);
        }
        public static void DodajFirme(string nrKRS, string nazwa)
        {
            try
            {   //Sprawdzenie unikalności numeru KRS
                if (!CzyWystepujeNr<Firma>(nrKRS, lista_firm)) 
                    lista_firm.Add(new Firma(nrKRS, nazwa));
            }
            catch (Exception e)
            {   //Poinformowanie o napotkanym błędzie przy wprowadzeniu informacji
                string komunikat = "";
                if (e is DuplikatNumeruException)
                    komunikat = "Podany numer KRS wystepuje w systemie, wprowadz inny";
                else if (e is NiepoprawnaInformacjaException)
                    komunikat = "Numer KRS moze zawierac jedynie cyfry";
                Console.WriteLine(komunikat);
                throw new BrakObiektuDoDodaniaException("Nie można dodac niepoprawnego obiektu");
            }
        }
        public static void UsunFirme(Firma _firma)
        {
            lista_firm.Remove(_firma);
        }
        public static List<Klient> ZnajdzPoTekscie<T>(string tekst, List<T> lista)
        {
            //Znajdowanie Klienta (Firmy/Osoby) na podstawie wprowadzonych informacji
            List<Klient> pasujace = new List<Klient>();
            foreach(T item in lista)
            {
                if ((item as Klient).CzyZawieraZnaki(tekst))
                {
                    pasujace.Add(item as Klient);
                }
            }
            return pasujace;
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