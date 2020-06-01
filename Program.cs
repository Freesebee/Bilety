using System;
using System.Collections.Generic;
using System.Linq;

namespace Bilety
{
    class Program
    {
        static void Main()
        {


            int Switch = 12, Switch1 = 12;
            int nrTrasy, _nrSamolotu ;
            string _file_path, _rodzaj;
            string _nrKRS, _nazwa;
            string _nr;
            string _nrKRS_Firmy; 
            string _nr_paszportu_klienta;
            string _imie, _nazwisko,  _nr_paszportu;
            List<T> _lista;
            int nrLotniska;
            int nrW , nrP;
            int nrTrasy;
            int coIleDni, ileLotow, nrTrasy, dzien, miesiac,rok, godzina, minuta;
            int _idLotu;
            Klient kupujacy_bilet; 
            Firma _kupujacy_bilety;
            int _idLotu; 
            Osoba pasazer;

            int x,  y; 
            string kraj,  miasto;

            while (Switch != 0)
            {
            Console.WriteLine("\n0. wyjscie");

            Console.WriteLine("1. Zarzadzanie samolotami");
            Console.WriteLine("2. Zarzadzanie klientami i firmami");
            Console.WriteLine("3. Zarzadzanie lotnikami");
            Console.WriteLine("4. Zarzadzanie trasami");
            Console.WriteLine("5. Generowanie lotow");

            Console.WriteLine("6. Rezerwacja biletow");
            Console.WriteLine("7. Zapis i odczyt");






                    Switch = Convert.ToInt32(Console.ReadLine());
                    switch (Switch)
                    {


                        case 1:
                           Console.WriteLine("1. Dodaj Samolot");
                           Console.WriteLine("2. Usun Samolot");
                           Console.WriteLine("3. Pokaz Samoloty");

                           Switch1 = Convert.ToInt32(Console.ReadLine());
                           switch (Switch1)
                            {
                            case 1:
                            Console.WriteLine("Jaki samolot chcesz dodac: Boeing, Airbus czy Bombardier\n");
                            _rodzaj = Console.ReadLine();
                            BiletSystem.DodajSamolot(_rodzaj);
                            Console.WriteLine(" \n");
                            break;

                            case 2:
                            Console.WriteLine("Podaj numer samolotu do usuniecia\n");
                            _rodzaj = Console.ReadLine();
                            BiletSystem.UsunSamolot(_nrSamolotu);
                            Console.WriteLine(" \n");
                            break;

                            case 3:
                            BiletSystem.PokazSamoloty();
                            Console.WriteLine(" \n");
                            break;

                            }

                        case 2:
                           Console.WriteLine("1. Dodaj Klienta");
                           Console.WriteLine("2. Usun Pasazera Po Numerze");
                           Console.WriteLine("3. Pokaz Klientow");
                           Console.WriteLine("4. Dodaj Firme");
                           Console.WriteLine("5. Usun Firme");
                           Console.WriteLine("6. Dodaj Klientow Firmy");

                           Switch1 = Convert.ToInt32(Console.ReadLine());
                           switch (Switch1)
                            {
                            case 1:
                            Console.WriteLine("Podaj imie klienta \n");
                            _imie = Console.ReadLine();
                            Console.WriteLine("Podaj nazwisko klienta \n");
                            _nazwisko = Console.ReadLine();
                            Console.WriteLine("Podaj nr paszportu klienta \n");
                            _nr_paszportu = Console.ReadLine();

                            BiletSystem.DodajPasazera(_imie, _nazwisko, _nr_paszportu);
                            Console.WriteLine(" \n");
                            break;

                            case 2:
                            Console.WriteLine("Podaj numer klienta do usuniecia\n");
                            _nr = Console.ReadLine();
                            BiletSystem.UsunPasazeraPoNumerze(_nr);
                            Console.WriteLine(" \n");
                            break;

                            case 3:
                            BiletSystem.PokazKlientow(_lista);
                            Console.WriteLine(" \n");
                            break;


                            case 4:
                            Console.WriteLine("Podaj numer KRS firmy \n");
                            _nrKRS = Console.ReadLine();
                            Console.WriteLine("Podaj nazwe firmy \n");
                            _nazwa = Console.ReadLine();
                            BiletSystem.DodajFirme(_nrKRS,_nazwa);
                            Console.WriteLine(" \n");
                            break;

                            case 5:
                            Console.WriteLine("Podaj numer firmy do usuniecia\n");
                            _nr = Console.ReadLine();
                            BiletSystem.UsunFirmePoNumerze(_nr);
                            Console.WriteLine(" \n");
                            break;

                            case 6:
                            Console.WriteLine("Podaj numer nrKRS Firmy\n");
                            _nrKRS_Firmy = Console.ReadLine();
                            Console.WriteLine("Podaj nr paszportu klienta \n");
                            _nr_paszportu_klienta = Console.ReadLine();
                            BiletSystem.DodajKlientowFirmy(_nrKRS_Firmy,_nr_paszportu_klienta);
                            Console.WriteLine(" \n");
                            break;

                            }


                        case 3:
                           Console.WriteLine("1. Dodaj Lotniko");
                           Console.WriteLine("2. Usun Lotnisko");
                           Console.WriteLine("3. Pokaz Lotniska");

                           Switch1 = Convert.ToInt32(Console.ReadLine());
                           switch (Switch1)
                            {
                            case 1:
                            Console.WriteLine("Podaj wsółrzędną x lotniska\n");
                            x = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj wsółrzędną x lotniska \n");
                            y = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj kraj w którym jest lotnisko \n");
                            kraj = Console.ReadLine();
                            Console.WriteLine("Podaj miasto w którym jest lotnisko \n");
                            miasto = Console.ReadLine();
                            BiletSystem.DodajLotnisko(x, y, kraj, miasto);
                            Console.WriteLine(" \n");
                            break;

                            case 2:
                            Console.WriteLine("Podaj numer lotniska do usuniecia\n");
                            nrLotniska = Convert.ToInt32(Console.ReadLine());
                            BiletSystem.UsunLotnisko(nrLotniska);
                            Console.WriteLine(" \n");
                            break;

                            case 3:
                            BiletSystem.PokazLotniska();
                            Console.WriteLine(" \n");
                            break;

                            }


                        case 4:
                           Console.WriteLine("1. Dodaj Trase");
                           Console.WriteLine("2. Usun Trase");
                           Console.WriteLine("3. Pokaz Trasy");

                           Switch1 = Convert.ToInt32(Console.ReadLine());
                           switch (Switch1)
                            {
                            case 1:
                            Console.WriteLine("Podaj numer lotniska z którego samolot wylatuje\n");
                            nrW = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj numer lotniska do którego samolot przylatuje \n");
                            nrP = Convert.ToInt32(Console.ReadLine());
                            BiletSystem.DodajTrase(nrW, nrP);
                            Console.WriteLine(" \n");
                            break;

                            case 2:
                            Console.WriteLine("Podaj numer trasy do usuniecia\n");
                            nrTrasy = Convert.ToInt32(Console.ReadLine());
                            BiletSystem.UsunTrase(nrTrasy);
                            Console.WriteLine(" \n");
                            break;

                            case 3:
                            BiletSystem.PokazTrasy();
                            Console.WriteLine(" \n");
                            break;

                            }

                        case 5:
                           Console.WriteLine("1. Dodaj Lot ");
                           Console.WriteLine("2. Dodaj Lot Cyklicznie");
                           Console.WriteLine("3. Usun Lot");
                           Console.WriteLine("4. Pokaz Loty po numerze");
                           Console.WriteLine("5. Pokaz wszystkie Loty ");

                           Switch1 = Convert.ToInt32(Console.ReadLine());
                           switch (Switch1)
                            {

                            case 1:
                            Console.WriteLine("Podaj nr trasy\n");
                            nrTrasy = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj dzień lotu \n");
                            dzien = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj miesiąc lotu\n");
                            miesiac = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj rok lotu \n");
                            rok = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj godzine lotu\n");
                            godzina = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj minute lotu \n");
                            minuta = Convert.ToInt32(Console.ReadLine());
                            BiletSystem.DodajLotNaTrasie(nrTrasy, dzien, miesiac,rok, godzina, minuta);
                            Console.WriteLine(" \n");
                            break;

                            case 2:
                            Console.WriteLine("Podaj co Ile Dni odbywa się lot\n");
                            coIleDni = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj ile Lotow \n");
                            ileLotow = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj nr trasy\n");
                            nrTrasy = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj dzień lotu \n");
                            dzien = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj miesiąc lotu\n");
                            miesiac = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj rok lotu \n");
                            rok = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj godzine lotu\n");
                            godzina = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj minute lotu \n");
                            minuta = Convert.ToInt32(Console.ReadLine());
                            BiletSystem.DodajLotCyklicznie(coIleDni, ileLotow, nrTrasy, dzien, miesiac,rok, godzina, minuta);
                            Console.WriteLine(" \n");
                            break;

                            case 3:
                            Console.WriteLine("Podaj id lotu do usuniecia\n");
                            _idLotu = Convert.ToInt32(Console.ReadLine());
                            BiletSystem.UsunLot(_idLotu);
                            Console.WriteLine(" \n");
                            break;

                            case 4:
                            Console.WriteLine("Podaj nr Trasy\n");
                            nrTrasy = Convert.ToInt32(Console.ReadLine());
                            BiletSystem.PokazLoty(nrTrasy);
                            Console.WriteLine(" \n");
                            break;

                            case 5:
                            BiletSystem.PokazLoty();
                            Console.WriteLine(" \n");
                            break;


                            }


                        case 6:
                           Console.WriteLine("1. Rezerwuj Bilety Grupie");
                           Console.WriteLine("2. Rezerwuj Bilet");

                           Switch1 = Convert.ToInt32(Console.ReadLine());
                           switch (Switch1)
                            {
                            case 1:
                            Console.WriteLine("Podaj numer numer KRS firmy\n");
                            _nrKRS = Console.ReadLine();
                           Firma dana = null;
            
                               foreach(Firma L in T.GetFima)
                               {
                                     if(L.nrKRS == _nrKRS)
                                     {
                                       dana = L;
                                     }
                               }
                           dana as Firma;
                           _kupujacy_bilety =  dana;

                            
                            Console.WriteLine("Podaj idLotu \n");
                            _idLotu = Convert.ToInt32(Console.ReadLine());
                            BiletSystem.RezerwujBiletyGrupie( _kupujacy_bilety, _idLotu);
                            Console.WriteLine(" \n");
                            break;



                            case 2:
                            Console.WriteLine("Podaj numer klienta\n");
                            _nr = Console.ReadLine();

                                foreach(T item in lista)
                                     {
                                        if(((item is Firma) && (item as Firma).CzyTenSamUnikalnyNr(_nr))
                                             || ((item is Osoba) && (item as Osoba).CzyTenSamUnikalnyNr(_nr)))
                                         {
                                              return item as Klient;
                                         } 
                                 }
                           kupujacy_bilet = item;
                            Console.WriteLine("Podaj id Lotu\n");
                            _idLotu = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Podaj numer klienta\n");
                            _nr = Console.ReadLine();
                                foreach(T item in lista)
                                {
                                       if( ((item is Osoba) && (item as Osoba).CzyTenSamUnikalnyNr(_nr)))
                                       {
                                             item as Osoba;
                                       } 
                                }
                           pasazer = item;
                            BiletSystem.RezerwujBilet(kupujacy_bilet,_idLotu,pasazer);
                            Console.WriteLine(" \n");
                            break;

                            }

                        case 7:
                           Console.WriteLine("1. Zapisz ");
                           Console.WriteLine("2. Wczytaj ");

                           Switch1 = Convert.ToInt32(Console.ReadLine());
                           switch (Switch1)
                            {
                            case 1:
                            BiletSystem.ZapiszStan("");
                            Console.WriteLine(" \n");
                            break;

                            case 2:
                            BiletSystem.WczytajStan("");
                            Console.WriteLine(" \n");
                            break;



                            }














                    }
            }











           // BiletSystem.WczytajStan("");

          //  BiletSystem.PokazLoty();

          //  BiletSystem.ZapiszStan("");
        }
    }
}
