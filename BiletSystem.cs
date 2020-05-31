using System;
using System.Collections.Generic;

namespace Bilety
{
    public static class BiletSystem
    {
        private static List<Samolot> dostepne_samoloty;
        private static List<Osoba> lista_pasazerow;
        private static List<Trasa> lista_tras;
        private static List<Firma> lista_firm;
        private static List<Lotnisko> lista_lotnisk;
        public static List<Klient> GetKlienci
        {
            get //Zwrócenie utworzonej listy z elementów list firm i pasażerów
            {
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
        public static List<Lotnisko> GetLotniska { get => lista_lotnisk; }
        public static List<Samolot> GetSamoloty { get => dostepne_samoloty; }

        static BiletSystem()
        {
            lista_tras = new List<Trasa>();
            dostepne_samoloty = new List<Samolot>();
            lista_pasazerow = new List<Osoba>();
            lista_firm = new List<Firma>();
        }

        public static void DodajLotnisko(int x, int y, string kraj, string miasto)
        {
            lista_lotnisk.Add(new Lotnisko(x,y,kraj,miasto));
        }
        public static void UsunLotnisko(int x, int y)
        {
            Lotnisko dane_lotnisko = lista_lotnisk.Find(oElement => oElement.X == x && oElement.Y == y);
            if (dane_lotnisko != null) //czy lotnisko zostało znalezione na liście
            {
                Trasa uzywana_trasa = lista_tras.Find(lTrasy => lTrasy.Lotnisko_wylotu == dane_lotnisko || lTrasy.Lotnisko_przylotu == dane_lotnisko);
                if (uzywana_trasa != null) //czy lotnisko jest używane na trasie
                {
                    Console.WriteLine($"\nBłąd > Lotnisko o współżędnych ({x},{y})" +
                        " jest używane na trasie i nie może zostać usunięte.");
                }
                else //lotnisko jest na liście i nie jest używane na trasie
                {
                    //List<T>.Remove() zwraca 1, jeśli pomyślnie usunie element, w przeciwnym wypadku zwraca 0
                    if (lista_lotnisk.Remove(dane_lotnisko))
                        Console.WriteLine("\nUsunięto lotnisko" + dane_lotnisko.ToString());
                    else Console.WriteLine("\nBłąd > nie usunięto lotniska.");
                }
            } //lotnisko nie zostało znalezione na liście
            else Console.WriteLine($"\nBłąd > Lotnisko o współżędnych ({x},{y}) nie istnieje.");
        }
        public static void PokazLotniska()
        {
            if (lista_lotnisk.Count != 0)
            {
                Console.WriteLine("\nDostępne lotniska:");
                foreach (Lotnisko oLotnisko in lista_lotnisk)
                {
                    Console.WriteLine(oLotnisko.ToString());
                }
            }
            else Console.WriteLine("Brak lotnisk do wyświetlenia.");
        }
        public static void DodajLot(Trasa trasa, DateTime czas_wylot)
        {
            trasa.GetLoty.Add(new Lot(czas_wylot));
        }
        public static void UsunLot(Lot lot)
        {
            //Powinno również usuwać bilety
            //z listy biletów danego lotu
            throw new NotImplementedException();
        }
        public static void ZapiszStan()
        {
            throw new System.NotImplementedException();
        }
        public static void WczytajStan()
        {
            throw new System.NotImplementedException();
        }
        public static void DodajTrase(Lotnisko wylot, Lotnisko przylot)
        {
            lista_tras.Add(new Trasa(wylot, przylot));
        }
        public static void UsunTrase()
        {
            throw new System.NotImplementedException();
        }
        public static void RezerwujBilet(Klient kupujacy_bilet, Lot dany_lot, Osoba pasazer)
        {
            try
            {
                foreach (Bilet bilet in dany_lot.GetBilety)
                {
                    if (pasazer.CzyPosiadaTakiBilet(bilet))
                    {
                        throw new NiepoprawnaInformacjaException("Pasazer juz posiada taki bilet");
                    }
                }
                Bilet B = new Bilet(pasazer, dany_lot, kupujacy_bilet);
                dany_lot.GetBilety.Add(B);
                pasazer.PrzekazBilet(B);
                Console.WriteLine("Pomyslnie zarezerwowano bilet");
            }
            catch(Exception)
            {
                Console.WriteLine("Nie udalo sie zarezerwowac biletu");
            }
        }
        public static bool CzyTekst(string text)
        {
            
            try
            {
                if (text == "")
                {
                    foreach (char c in text)
                    {
                        if (!char.IsLetter(c))
                            return false;
                    }
                    return true;
                }
                else return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CzyNumer(string nr)
        {
            try
            {
                if(nr!="")
                {
                    foreach (char c in nr)
                    {
                        if (!char.IsDigit(c))
                            return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool CzyWystepujeNazwaFirmy(string nazwa)
        {
            foreach (Firma item in GetFirmy)
            {
                if (item.CzyTaSamaNazwaFirmy(nazwa))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool CzyWystepujeNrKlienta<T>(string nr, List<T> lista)
        {
            if(CzyNumer(nr) && lista!=null) //Sprawdzenie poprawności numeru i listy
            {
                foreach (T item in lista) //Sprawdzenie unikalności numeru w systemie
                {
                    if (item is Osoba)
                    {
                        if ((item as Osoba).CzyTenSamUnikalnyNr(nr))
                        {
                            Console.WriteLine("W systemie istnieje pasazer o danym numerze paszportu");
                            return true;
                        }
                    }
                    else if (item is Firma)
                    {
                        if ((item as Firma).CzyTenSamUnikalnyNr(nr))
                        {
                            Console.WriteLine("W systemie istnieje firma o danym numerze KRS");
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static void DodajPasazera(string imie, string naziwsko, string nr_paszportu)
        {
            try  //Sprawdzenie unikalności numeru paszportu
            {   
                if (!CzyWystepujeNrKlienta<Osoba>(nr_paszportu, lista_pasazerow))
                {
                    lista_pasazerow.Add(new Osoba(imie, naziwsko, nr_paszportu));
                }
                Console.WriteLine("Pomyslnie dodano pasazera do systemu");
            }
            catch (DuplikatNumeruException)
            {
                Console.WriteLine("Podany numer paszportu wystepuje w systemie, wprowadz inny");
            }
            catch (NiepoprawnaInformacjaException)
            {
                Console.WriteLine("Numer paszportu moze zawierac " +
                    "jedynie cyfry, a nazwisko i imie jedynie litery");
            }
        }
        public static void UsunPasazeraPoNumerze(string nr)
        {
            try
            {
                if (CzyWystepujeNrKlienta(nr, lista_pasazerow))
                {
                    foreach (Osoba item in ZnajdzPoTekscie(nr, lista_pasazerow))
                    {
                        lista_pasazerow.Remove(item);
                    }
                    Console.WriteLine("Pomyslnie usunieto pasazera z systemu");
                }
                else
                {
                    Console.WriteLine("Brak pasazera o podanym numerze");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Nie udalo sie usunac pasazera");
            }
        }
        public static void DodajFirme(string nrKRS, string nazwa)
        {
            try //Sprawdzenie unikalności numeru KRS i nazwy
            {
                if (!CzyWystepujeNrKlienta<Firma>(nrKRS, lista_firm)
                            && !CzyWystepujeNazwaFirmy(nazwa))
                {
                    lista_firm.Add(new Firma(nrKRS, nazwa));
                } 
                Console.WriteLine("Pomyslnie dodano firme do systemu");
            }
            catch(DuplikatNumeruException)
            {
                Console.WriteLine("Podany numer KRS wystepuje w systemie, wprowadz inny");
            }
            catch (NiepoprawnaInformacjaException)
            {
                Console.WriteLine("Numer KRS moze zawierac jedynie cyfry");
            }
        }
        public static void UsunFirmePoNumerze(string nr)
        {
            try
            {
                if (CzyWystepujeNrKlienta(nr, lista_firm))
                {
                    foreach (Firma item in ZnajdzPoTekscie(nr, lista_firm))
                    {
                        lista_firm.Remove(item);
                    }
                    Console.WriteLine("Pomyslnie usunieto firme z systemu");
                }
                else
                {
                    Console.WriteLine("Brak firmy o podanym numerze KRS");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Nie udalo sie usunac firmy");
            }
        }
        public static List<Klient> ZnajdzPoTekscie<T>(string tekst, List<T> lista)
        {
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