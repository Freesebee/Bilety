using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Text;

namespace Bilety
{
    public static class BiletSystem
    {
        public static int IdentyfikatorLotow {get ; private set; }
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
        public static List<Trasa> GetTrasy { get => lista_tras; }

        static BiletSystem()
        {
            lista_tras = new List<Trasa>();
            dostepne_samoloty = new List<Samolot>();
            lista_pasazerow = new List<Osoba>();
            lista_firm = new List<Firma>();
            lista_lotnisk = new List<Lotnisko>();
            IdentyfikatorLotow = 0;
        }

        //Zapisywanie stanu systemu --------------

        public static void ZapiszStan(string file_path)
        {
            ZapiszOsoby($@"{file_path}Osoby.txt");
            ZapiszSamoloty($@"{file_path}Samoloty.txt");
            ZapiszFirmy($@"{file_path}Firmy.txt");
            ZapiszLotniska($@"{file_path}Lotniska.txt");
            ZapiszTrasyZLotami($@"{file_path}Trasy_loty.txt");
            ZapiszBilety($@"{file_path}Bilety.txt");
            Console.WriteLine("Zapisano aktualny stan systemu");
            //Console.Clear(); <======== ODKOMENTUJ ABY CZYŚCIŁO KONSOLĘ
        }
        private static void ZapiszOsoby(string nazwa_pliku)
        {
            List<string> linie = new List<string>();
            foreach (Osoba item in GetOsoby)
            {
                linie.Add(item.DaneDoZapisu());
            }

            File.WriteAllLines(nazwa_pliku, linie);
        }
        private static void ZapiszFirmy(string nazwa_pliku)
        {
            List<string> linie = new List<string>();
            foreach (Firma item in GetFirmy)
            {
                linie.Add(item.DaneDoZapisu());
            }
            File.WriteAllLines(nazwa_pliku, linie);
        }
        private static void ZapiszSamoloty(string nazwa_pliku)
        {
            short liczba_boeing = 0, liczba_airbus = 0, liczba_bombardier = 0;
            foreach (Samolot item in GetSamoloty)
            {
                if (item is Bombardier) liczba_bombardier++;
                else if (item is Airbus) liczba_airbus++;
                else if (item is Boeing) liczba_boeing++;
            }
            List<string> linie = new List<string>();
            linie.Add($"boeing,{liczba_boeing},airbus,{liczba_airbus},bombardier,{liczba_bombardier}");

            File.WriteAllLines(nazwa_pliku, linie);
        }
        private static void ZapiszLotniska(string nazwa_pliku)
        {
            List<string> linie = new List<string>();
            foreach(Lotnisko item in GetLotniska)
            {
                linie.Add(item.DaneDoZapisu());
            }
            File.WriteAllLines(nazwa_pliku, linie);
        }
        private static void ZapiszTrasyZLotami(string nazwa_pliku)
        {
            List<string> linie = new List<string>();
            foreach (Trasa item in GetTrasy)
            {
                linie.Add(item.DaneDoZapisu());
            }
            File.WriteAllLines(nazwa_pliku, linie);
        }
        private static void ZapiszBilety(string nazwa_pliku)
        {
            List<string> linie = new List<string>();
            foreach(Trasa T in GetTrasy)
            {
                foreach(Lot L in T.GetLoty)
                {
                    foreach(Bilet B in L.GetBilety)
                    {
                        linie.Add(B.DaneDoZapisu());
                    }
                }
            }
            File.WriteAllLines(nazwa_pliku, linie);
        }

        //Wczytywanie statu systemu --------------
        
        public static void WczytajStan(string file_path)
        {
            WczytajOsoby($@"{file_path}Osoby.txt");
            WczytajFirmy(@"Firmy.txt");
            WczytajLotniska(@"Lotniska.txt");
            WczytajSamoloty(@"Samoloty.txt");
            WczytajTrasyZLotami(@"Trasy_loty.txt");
            WczytajBilety(@"Bilety.txt");
            //Console.Clear();
            Console.WriteLine("Pomyslnie wczytano wszystkie dane");
        }
        private static void WczytajOsoby(string nazwa_pliku)
        {
            if (File.Exists(nazwa_pliku))
            {
                List<string> linie = File.ReadAllLines(nazwa_pliku).ToList();
                foreach (string linia in linie)
                {
                    string[] slowa = linia.Split(',');
                    DodajOsobe(slowa[0], slowa[1], slowa[2]);
                }
                //Console.Clear();
                Console.WriteLine($"Pomyslnie wczytano {linie.Count()} osoby do systemu");
            }
            else
            {
                Console.WriteLine("Brak pliku zawierajacego liste osob do wczytania");
            }
        }
        private static void WczytajFirmy(string nazwa_pliku)
        {
            if (File.Exists(nazwa_pliku))
            {
                List<string> linie = File.ReadAllLines(nazwa_pliku).ToList();
  
                foreach (string linia in linie)
                {
                    string[] firma_paszporty = linia.Split(';');
                    string[] firma = firma_paszporty[0].Split(',');
                    string[] paszporty = firma_paszporty[1].Split(',');

                    DodajFirme(firma[0],firma[1]);

                    for (int i = 0; i < paszporty.Length; i++)
                    {
                        DodajKlientowFirmy(firma[0], paszporty[i]);
                    }
                }
               // Console.Clear();
                Console.WriteLine($"Pomyslnie wczytano {linie.Count()} firmy do systemu");
            }
            else
            {
                Console.WriteLine("Brak pliku zawierajacego liste firm do wczytania");
            }
        }
        private static void WczytajLotniska(string nazwa_pliku)
        {
            if (File.Exists(nazwa_pliku))
            {
                List<string> linie = File.ReadAllLines(nazwa_pliku).ToList();
                foreach (string linia in linie)
                {
                    string[] slowa = linia.Split(',');
                    DodajLotnisko(int.Parse(slowa[0]), int.Parse(slowa[1]), slowa[2], slowa[3]);
                }
                //Console.Clear();
                Console.WriteLine($"Pomyslnie wczytano {linie.Count()} lotniska do systemu");
            }
            else
            {
                Console.WriteLine("Brak pliku zawierajacego liste lotnisk do wczytania");
            }
        }
        private static void WczytajSamoloty(string nazwa_pliku)
        {
            if (File.Exists(nazwa_pliku))
            {
                List<string> linie = File.ReadAllLines(nazwa_pliku).ToList();
                foreach (string linia in linie)
                {
                    string[] slowa = linia.Split(',');
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < int.Parse(slowa[i*2+1]); j++)
                        {
                            DodajSamolot(slowa[i*2]);
                        }
                    }
                }
                //Console.Clear();
                Console.WriteLine($"Pomyslnie wczytano {GetSamoloty.Count()} samolotow do systemu");
            }
            else
            {
                Console.WriteLine("Brak pliku zawierajacego liste samolotow do wczytania");
            }
        }
        private static void WczytajTrasyZLotami(string nazwa_pliku)
        {
            if (File.Exists(nazwa_pliku))
            {
                List<string> linie = File.ReadAllLines(nazwa_pliku).ToList();
                short nr_lini = 0;
                int liczba_lotow = 0;
                foreach (string linia in linie)
                {
                    nr_lini++;
                    string[] slowa = linia.Split(';');
                    string[] trasa = slowa[0].Split(',');
                    DodajTrase(int.Parse(trasa[0]), int.Parse(trasa[1]));

                    if (slowa.Length >= 2)
                    {
                        string[] loty = slowa[1].Split('.');
                        for (int i = 0; i < loty.Length; i++)
                        {
                            string[] lot = loty[i].Split(',');

                            DodajLotNaTrasie(nr_lini, int.Parse(lot[0]),
                                int.Parse(lot[1]), int.Parse(lot[2]),
                                int.Parse(lot[3]), int.Parse(lot[4]));
                            liczba_lotow++;
                        }
                    } 
                }
                //Console.Clear();
                Console.WriteLine($"Pomyslnie wczytano {linie.Count()}" +
                    $" trasy i {liczba_lotow} loty do systemu");
            }
            else
            {
                Console.WriteLine("Brak pliku zawierajacego liste tras i lotow do wczytania");
            }
        }
        private static void WczytajBilety(string nazwa_pliku)
        {
            if (File.Exists(nazwa_pliku))
            {
                List<string> linie = File.ReadAllLines(nazwa_pliku).ToList();
                foreach (string linia in linie)
                {
                    string[] slowa = linia.Split(',');
                    RezerwujBilet(ZnajdzKonkretnegoKlienta(slowa[0], GetKlienci),
                        int.Parse(slowa[1]), 
                        ZnajdzKonkretnegoKlienta(slowa[2],GetOsoby) as Osoba);
                }
                //Console.Clear();
                Console.WriteLine($"Pomyslnie wczytano {linie.Count()} bilety do systemu");
            }
            else
            {
                Console.WriteLine("Brak pliku zawierajacego liste biletow do wczytania");
            }
        }

        //Rezerwacja ------------

        public static void RezerwujBiletyGrupie(Firma kupujacy_bilety, int _idLotu)
        {
            try
            {
                Lot dany_lot = ZnajdzLotPoID(_idLotu);
                if(dany_lot.LiczbaMiejsc > kupujacy_bilety.GetKlienci.Count())
                {
                    foreach (Osoba item in kupujacy_bilety.GetKlienci)
                    {
                        RezerwujBilet(kupujacy_bilety, _idLotu, item);
                    }
                }
                else
                {
                    throw new NiepoprawnaInformacjaException("Brak wystarczającej liczby miejsc na podany lot");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Nie udalo sie zarezerwowac grupie biletow:\n" + e.Message);
            }
           
        }
        public static void RezerwujBilet(Klient kupujacy_bilet, int _idLotu, Osoba pasazer)
        {
            //aby zarezerwowac bilet na dany lot, nalezy podac ID lotu. -Filip
            try
            {
                Lot dany_lot = ZnajdzLotPoID(_idLotu);
                if (dany_lot.LiczbaMiejsc > 0)
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
                    dany_lot.ZarezerwujMiejsce(); //pomniejsza liczbę miejsc w samolocie o 1
                    pasazer.PrzekazBilet(B);
                    Console.WriteLine("Pomyslnie zarezerwowano bilet");
                }
                else throw new NiepoprawnaInformacjaException("Brak miejsc na podany lot");
            }
            catch (Exception e)
            {
                Console.WriteLine("Nie udalo sie zarezerwowac biletu:\n" + e.Message);
            }
        }

        //Klienci (dla Firm i Osób) ----------

        public static bool CzyTekst(string text)
        {

            try
            {
                if (text != "")
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
                if (nr != "")
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
        public static void PokazKlientow<T>(List<T> lista) //wprowadz liste ktora chcesz wyswietlic
        {                                                  //Klientów, Firm lub Osób
            if (lista != null && lista.Count > 0)
            {
                foreach (T item in lista)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine("Brak podmiotow do wyswietlenia");
            }
        }
        public static List<Klient> ZnajdzPasujacePoTekscie<T>(string tekst, List<T> lista)
        {
            List<Klient> pasujace = new List<Klient>();
            foreach (T item in lista)
            {
                if ((item as Klient).CzyZawieraZnaki(tekst))
                {
                    pasujace.Add(item as Klient);
                }
            }
            return pasujace;
        }
        public static bool CzyWystepujeNrKlienta<T>(string nr, List<T> lista)
        {
            if (CzyNumer(nr) && lista != null) //Sprawdzenie poprawności numeru i listy
            {
                foreach (T item in lista) //Sprawdzenie unikalności numeru w systemie
                {
                    if (item is Osoba)
                    {
                        if ((item as Osoba).CzyTenSamUnikalnyNr(nr))
                        {
                            return true;
                        }
                    }
                    else if (item is Firma)
                    {
                        if ((item as Firma).CzyTenSamUnikalnyNr(nr))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static Klient ZnajdzKonkretnegoKlienta<T>(string nr, List<T> lista)
        { //w przypadku Firmy nr -> nr_KRS, a Osoby nr -> nr_paszportu
            foreach(T item in lista)
            {
                if(((item is Firma) && (item as Firma).CzyTenSamUnikalnyNr(nr))
                    || ((item is Osoba) && (item as Osoba).CzyTenSamUnikalnyNr(nr)))
                {
                    return item as Klient;
                } 
            }
            return null;
        }

        //Firmy -------------

        public static void DodajFirme(string nrKRS, string nazwa)
        {
            try //Sprawdzenie unikalności numeru KRS i nazwy
            {
                if (CzyWystepujeNrKlienta(nrKRS, lista_firm))
                {
                    throw new DuplikatNumeruException("Podany numer KRS wystepuje w systemie, wprowadz inny");
                }
                if (CzyWystepujeNazwaFirmy(nazwa))
                {
                    throw new DuplikatNumeruException("Podana nazwa firmy wystepuje w systemie, wprowadz inna");
                }
                lista_firm.Add(new Firma(nrKRS, nazwa));
                Console.WriteLine("Pomyslnie dodano firme do systemu");
            }
            catch (Exception e)
            {
                Console.WriteLine("Nie mozna dodac Firmy:\n" + e.Message);
            }
        }
        public static void UsunFirmePoNumerze(string nr)
        {
            try
            {
                if (CzyWystepujeNrKlienta(nr, lista_firm))
                {
                    foreach (Firma item in ZnajdzPasujacePoTekscie(nr, lista_firm))
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
        public static void DodajKlientowFirmy(string nrKRS_Firmy, string nr_paszportu_klienta)
        {
                if (CzyWystepujeNrKlienta(nrKRS_Firmy, GetFirmy)
                    && CzyWystepujeNrKlienta(nr_paszportu_klienta, GetOsoby))
                {
                    (ZnajdzKonkretnegoKlienta(nrKRS_Firmy, GetFirmy)
                     as Firma).DodajKlienta(ZnajdzKonkretnegoKlienta(nr_paszportu_klienta,
                         GetOsoby) as Osoba);
                }
                else
                {
                    Console.WriteLine("Niepoprawny numer firmy lub osoby");
                }
        }
        
        //Osoby -------------

        public static void DodajOsobe(string imie, string naziwsko, string nr_paszportu)
        {
            try  //Sprawdzenie unikalności numeru paszportu
            {
                if (CzyWystepujeNrKlienta<Osoba>(nr_paszportu, lista_pasazerow))
                {
                    throw new DuplikatNumeruException("Podany numer paszportu istnieje" +
                        " systemie - wprowadz inny");
                }
                lista_pasazerow.Add(new Osoba(imie, naziwsko, nr_paszportu));
                Console.WriteLine("Pomyslnie dodano Osobe do systemu");
            }
            catch (Exception e)
            {
                Console.WriteLine("Nie udalo sie dodac Osobe:\n" + e.Message);
            }
        }
        public static void UsunOsobePoNumerze(string nr)
        {
            try
            {
                if (CzyWystepujeNrKlienta(nr, lista_pasazerow))
                {
                    foreach (Osoba item in ZnajdzPasujacePoTekscie(nr, lista_pasazerow))
                    {
                        lista_pasazerow.Remove(item);
                    }
                    Console.WriteLine("Pomyslnie usunieto Osobe z systemu");
                }
                else
                {
                    Console.WriteLine("Brak Osobe o podanym numerze");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Nie udalo sie usunac Osobe");
            }
        }

        // Samoloty --------------

        public static Samolot DodajSamolot(string rodzaj)
        {
            if (rodzaj == "boeing" || rodzaj == "Boeing")
            {
                dostepne_samoloty.Add(new Boeing());
                Console.WriteLine("Utworzono " + dostepne_samoloty[dostepne_samoloty.Count-1].ToString());
                return dostepne_samoloty[dostepne_samoloty.Count-1];
            } else if (rodzaj == "airbus" || rodzaj == "Airbus")
            {
                dostepne_samoloty.Add(new Airbus());
                Console.WriteLine("Utworzono " + dostepne_samoloty[dostepne_samoloty.Count-1].ToString());
                return dostepne_samoloty[dostepne_samoloty.Count-1];
            } else if (rodzaj == "bombardier" || rodzaj == "Bombardier")
            {
                dostepne_samoloty.Add(new Bombardier());
                Console.WriteLine("Utworzono " + dostepne_samoloty[dostepne_samoloty.Count-1].ToString());
                return dostepne_samoloty[dostepne_samoloty.Count-1];
            } else 
            {
                Console.WriteLine("Podano złą nazwę samolotu.");
                return null;
            }
        }
        public static void UsunSamolot(int nrSamolotu)//Trzeba podać numer samolotu, wyświetlony przez PokazSamoloty()
        {
            int max = dostepne_samoloty.Count;
            if (max < 1) {Console.WriteLine("\nBłąd > Brak samolotow do usunięcia."); return;}
            if (nrSamolotu < 1 || nrSamolotu > max) {Console.WriteLine($"Błąd > Niepoprawny numer samolotu. ({nrSamolotu})"); return;}
            Console.WriteLine("\nPomyślnie usunięto samolot" + dostepne_samoloty[nrSamolotu-1].ToString());
            dostepne_samoloty.Remove(dostepne_samoloty[nrSamolotu-1]);
            //usuwa samolot, bez sprawdzania, czy można go usunąć, przez brak czasu
        }
        public static void PokazSamoloty()
        {
            int i=1;
            if (dostepne_samoloty.Count != 0)
            {
                Console.WriteLine("\nDostępne samoloty:");
                foreach (Samolot oSamolot in dostepne_samoloty)
                {
                    Console.WriteLine($"{i++}. " + oSamolot.ToString());
                }
            }
            else Console.WriteLine("Brak samolotów do wyświetlenia.");
        }

        // Lotniska --------------

        public static void DodajLotnisko(int x, int y, string kraj, string miasto)
        {
            //Sprawdza, czy lotnisko o podanych współrzędnych jest już na liście:
            foreach (Lotnisko oLotnisko in lista_lotnisk)
            {
                try
                {
                    if (x == oLotnisko.X && y == oLotnisko.Y)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine("Lotnisko o współrzędnych " +
                            $"({x},{y}) już istnieje.");
                    return; //jeśli lotnisko jest już na liście, przerywa metodę
                }
            }
            lista_lotnisk.Add(new Lotnisko(x,y,kraj,miasto));
        }
        public static void UsunLotnisko(int nrLotniska)
        {
            if (lista_lotnisk.Count > 0 && lista_lotnisk.Count >= nrLotniska && nrLotniska > 0)
            {
                Trasa uzywana_trasa = lista_tras.Find(lTrasy => lTrasy.Lotnisko_wylotu == lista_lotnisk[nrLotniska-1]
                                      || lTrasy.Lotnisko_przylotu == lista_lotnisk[nrLotniska-1]);
                if (uzywana_trasa != null) //czy lotnisko jest używane na trasie
                {
                    Console.WriteLine($"\nBłąd > Lotnisko o ID ({nrLotniska})" +
                        " jest używane na trasie i nie może zostać usunięte.");
                    return;
                }
                else //lotnisko jest na liście i nie jest używane na trasie
                {
                    //List<T>.Remove() zwraca 1, jeśli pomyślnie usunie element, w przeciwnym wypadku zwraca 0
                    if (lista_lotnisk.Remove(lista_lotnisk[nrLotniska-1]))
                        Console.WriteLine("\nUsunięto lotnisko");
                    else Console.WriteLine("\nBłąd > nie usunięto lotniska.");
                }
            }
            else Console.WriteLine($"\nBłąd > Zły numer lotniska.");
        }
        public static void PokazLotniska()
        {
            int i=1;
            if (lista_lotnisk.Count != 0)
            {
                Console.WriteLine("\nDostępne lotniska:");
                foreach (Lotnisko oLotnisko in lista_lotnisk)
                {
                    Console.WriteLine($"{i++}. " + oLotnisko.ToString());
                }
            }
            else Console.WriteLine("Brak lotnisk do wyświetlenia.");
        }

        // Trasy -----------------
        
        public static void DodajTrase(int nrW, int nrP)//Nalezy podać numery lotnisk (wyświetlone przez PokazLotniska()
        {
            int max = lista_lotnisk.Count;
            // Kolejne ify sprawdzają poprawność wprowadzonych numerów
            if (nrW == nrP) {Console.WriteLine("Błąd > Proszę podać dwa różne lotniska!"); return;}
            if (max < 2)
            {
                Console.WriteLine("Błąd > Brak wystarczającej liczby lotnisk!");
                return;
            }
            if (nrW <= 0 || nrP <= 0 )
            {
                Console.WriteLine("Błąd > Obie liczby muszą być większe niż 0!");
                return;
            }
            if (nrW > max || nrP > max )
            {
                Console.WriteLine($"Błąd > Nie ma lotniska o tak dużym numerze! Max {max}");
                return;
            }
            Lotnisko wylot, przylot;
            wylot = lista_lotnisk[nrW-1];
            przylot = lista_lotnisk[nrP-1];
            if (LiczOdleglosc(wylot,przylot) > 5000)
            {
                Console.WriteLine($"Zbyt duża odległość między lotniskami ({LiczOdleglosc(wylot,przylot)}km)");
                Console.WriteLine($"Nasze samoloty latają maxymalnie na 5000km.");
                return;
            }

            foreach(Trasa oTrasa in lista_tras) //Sprawdza, czy trasa już istnieje
            {
                if (wylot == oTrasa.Lotnisko_wylotu && przylot == oTrasa.Lotnisko_przylotu)
                {
                    Console.WriteLine("Błąd > Trasa |" + oTrasa.ToString() + "| już istnieje!");
                    return;
                }
            }
            lista_tras.Add(new Trasa(wylot, przylot));
        }
        public static void UsunTrase(int nrTrasy)
        {
            int max = lista_tras.Count;
            if (max < 1) {Console.WriteLine("\nBłąd > Brak tras do usunięcia."); return;}
            if (nrTrasy < 1 || nrTrasy > max) {Console.WriteLine($"\nBłąd > Niepoprawny numer trasy. ({nrTrasy})"); return;}
            nrTrasy--;
            if (lista_tras[nrTrasy].GetLoty.Count != 0)
            {
                Console.WriteLine("\nBłąd > Nie można usunąć trasy |" + lista_tras[nrTrasy].ToString() +
                    "| ponieważ są do niej przypisane loty!");
                return;
            }
            Console.WriteLine("\nPomyślnie usunięto trasę |" + lista_tras[nrTrasy].ToString() + "|.");
            lista_tras.Remove(lista_tras[nrTrasy]);
        }
        public static void PokazTrasy()
        {
            int i=1;
            if (lista_tras.Count != 0)
            {
                Console.WriteLine("\nDostępne trasy:");
                foreach (Trasa oTrasa in lista_tras)
                {
                    Console.WriteLine($"{i++}. " + oTrasa.ToString());
                }
            }
            else Console.WriteLine("Brak tras do wyświetlenia.");
        }

        // Loty -----------------

        public static void DodajLotNaTrasie(int nrTrasy, int dzien, int miesiac,
                                            int rok, int godzina, int minuta)
        {
            try
            {
                if (!SprawdzDate(dzien, miesiac, godzina, minuta))
                {
                    throw new NiepoprawnyNumerException("Niepoprawna data.");
                }
                if(nrTrasy <= 0 || nrTrasy > lista_tras.Count)
                {
                    throw new NiepoprawnyNumerException("Zły numer Trasy.");
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Podano niepoprawne dane.");
                return;
            }
            try 
            {
                if (DobierzSamolot(lista_tras[nrTrasy-1].Lotnisko_wylotu,lista_tras[nrTrasy-1].Lotnisko_przylotu) == null)
                {
                    throw new NiepoprawnyNumerException("Nie znaleziono odpowiedniego samolotu");
                } else
                lista_tras[nrTrasy-1].DodajLot(new DateTime(rok, miesiac, dzien, godzina, minuta, 0), ++IdentyfikatorLotow);
                if (lista_tras[nrTrasy-1]==null)
                {
                    throw new NiepoprawnaInformacjaException("Nie utworzono lotu.");
                }
                else Console.WriteLine("\nPomyślnie utworzono lot " + 
                lista_tras[nrTrasy-1].GetLoty[lista_tras[nrTrasy-1].GetLoty.Count-1].ToString());
            }
            catch(Exception)
            {
                Console.WriteLine("Nie utworzono lotu na trasie.");
            }
        }
        public static void DodajLotCyklicznie(int coIleDni, int ileLotow, int nrTrasy, int dzien,
                                              int miesiac, int rok, int godzina, int minuta)
        {
            try
            { //31 dzien każdego miesiąca to święto lotników i samoloty nie latają, elo
                if (!SprawdzDate(dzien, miesiac, godzina, minuta))
                {
                    throw new NiepoprawnyNumerException("Niepoprawna data.");
                }
                if(nrTrasy <= 0 || nrTrasy > lista_tras.Count)
                {
                    throw new NiepoprawnyNumerException("Zły numer Trasy.");
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Podano niepoprawne dane.");
                return;
            }

            
            TimeSpan cykl = new TimeSpan(coIleDni, 0, 0, 0); 
            DateTime DataPierwszegoLotu = (new DateTime(rok, miesiac, dzien, godzina, minuta, 0)) - cykl;
            for (int i = 0; i < ileLotow; i++)
            {
                lista_tras[nrTrasy-1].DodajLot(DataPierwszegoLotu += cykl, ++IdentyfikatorLotow);
                Console.WriteLine("\nPomyślnie utworzono lot " 
                    + lista_tras[nrTrasy-1].GetLoty[lista_tras[nrTrasy-1].GetLoty.Count-1].ToString());
            }
        }
        public static void UsunLot(int _idLotu)
        {
            Lot dany_lot = null;
            foreach (Trasa T in lista_tras)
            {
                dany_lot = T.GetLoty.Find(oLot => oLot.IdLotu == _idLotu);
                if (dany_lot != null && dany_lot.GetBilety.Count > 0)
                {                    
                    foreach(Bilet b in dany_lot.GetBilety)
                    {
                        b.GetPasazer.UsunBilet(b);
                    }
                    dany_lot.GetBilety.Clear();
                }
                if(T.GetLoty.Remove(dany_lot))
                {
                    Console.WriteLine("Pomyślnie usunięto lot");
                }
            }
            if (dany_lot == null)
            {
                Console.WriteLine("Coś poszło nie tak przy usuwaniu.");
            }
        }
        public static void PokazLoty(int nrTrasy)//na konkretnej trasie
        {
            try
            {
                if (nrTrasy > lista_tras.Count || nrTrasy < 0)
                    throw new NiepoprawnyNumerException("Podano niepoprawny numer trasy");             
            }
            catch(Exception) 
            {
                return;
            }
            Console.WriteLine("\nLoty na trasie |" + lista_tras[nrTrasy-1].ToString() + $"| ({lista_tras[nrTrasy-1].GetLoty.Count}):");
            lista_tras[nrTrasy-1].PokazLoty();
        }
        public static void PokazLoty()//pokazuje wszystkie loty w ogóle
        {
            if (lista_tras.Count > 0)
            {
                for (int i = 1; i <= lista_tras.Count; i++)
                {
                    PokazLoty(i);
                }
            }
            else
                Console.WriteLine("Brak Tras i lotów do wyświetlenia");
        }
        public static Lot ZnajdzLotPoID(int _idLotu)
        {
            foreach (Trasa T in lista_tras)
            {
                foreach(Lot L in T.GetLoty)
                {
                    if(L.IdLotu == _idLotu)
                    {
                        return L;
                    }
                }
            }
            throw new NiepoprawnyNumerException("Niepoprawne ID Lotu");
        }

        // Dodatkowe -----------

        public static double LiczOdleglosc(Lotnisko L1, Lotnisko L2)
        {
            double droga;
            int x1 = L1.X, y1 = L1.Y, x2 = L2.X, y2 = L2.Y;
            droga = Math.Round(Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2)), 2);
            return droga * 100; //jedna kratka w układzie współrzędnych odpowiada 100km
        }
        public static bool SprawdzDate(int dzien, int miesiac, int godzina, int minuta)
        {
             //31 dzien każdego miesiąca to święto lotników i samoloty nie latają, elo
            if (dzien == 31)
            {
                Console.WriteLine("31 dzień każdego miesiąca to święto lotników i samoloty nie latają");
                return false;
            } else if(dzien > 30 || miesiac > 12 || godzina > 23 || minuta > 59 
                || dzien <= 0 || miesiac <= 0 || godzina < 0 || minuta < 0)
            {
                return false;
            } else if (dzien > 28 && miesiac == 2)
            {
                return false;
            } else
                return true;
        }
        public static Samolot DobierzSamolot(Lotnisko wylot, Lotnisko przylot)
        {
            double odleglosc = LiczOdleglosc(wylot, przylot);
            if (odleglosc > 0 && odleglosc <= 500) 
            {
                Samolot samolot = GetSamoloty.Find(oSamolot => oSamolot is Bombardier && oSamolot.CzyWolny == true);
                if (samolot != null)
                {
                    return samolot;
                }
            }
            if (odleglosc > 0 && odleglosc <= 1200) 
            {
                Samolot samolot = GetSamoloty.Find(oSamolot => oSamolot is Airbus && oSamolot.CzyWolny == true);
                if (samolot != null)
                {
                    return samolot;
                }
            }
            if (odleglosc > 0 && odleglosc <= 5000) 
            {
                Samolot samolot = GetSamoloty.Find(oSamolot => oSamolot is Boeing && oSamolot.CzyWolny == true);
                if (samolot != null)
                {
                    return samolot;
                }
            }
            if (odleglosc > 5000)
            {
                Console.WriteLine("Za daleka trasa, samoloty latają maksymalnie 5000km ");
                return null;
            } else
            {
                Console.WriteLine($"Brak dostępnych samolotów, mogących pokonać tak daleką trasę {odleglosc}km");
                Console.WriteLine($"Zasięg Boeing: 5000km\nZasięg Airbus: 1200km\n zasięg Bombardier: 500km");
                return null;
            }

        }
    }
}