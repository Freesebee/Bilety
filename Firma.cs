using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Bilety
{
    public class Firma : Klient
    {
        private string nr_KRS;
        private string nazwa;
        private List<Osoba> lista_klientow;

        public Firma(string KRS, string nazwa_firmy)
        {
            if (BiletSystem.CzyNumer(KRS)) //Sprawdzenie poprawności numeru KRS
            {
                nr_KRS = KRS;
                nazwa = nazwa_firmy;
                lista_klientow = new List<Osoba>();
            }
            else throw new NiepoprawnaInformacjaException("Bledny numer KRS");
        }
        ~Firma()
        {
            if (lista_klientow!=null)lista_klientow.Clear();
            lista_klientow = null;
        }
        public void DodajKlienta(Osoba nowy_klient)
        {
            if (!lista_klientow.Contains(nowy_klient)) 
                lista_klientow.Add(nowy_klient);
        }
        public void UsunKlienta(Osoba dany_klient)
        {
            if (lista_klientow.Contains(dany_klient)) 
                lista_klientow.Remove(dany_klient);
        }
        public override bool CzyZawieraZnaki(string tekst)
        {
            if (nazwa.Contains(tekst)
                || nr_KRS.Contains(tekst)) return true;
            else return false;
        }
        public override bool CzyTenSamUnikalnyNr(string nr)
        {
            if (nr_KRS.Equals(nr)) return true;
            else return false;
        }
        public bool CzyTaSamaNazwaFirmy(string nr)
        {
            if (nazwa.Equals(nr)) return true;
            else return false;
        }
        public override string ToString()
        {
            return nazwa;
        }
        public static bool operator == (Firma a, Firma b)
        {
            if (a.nr_KRS == b.nr_KRS || a.nazwa == b.nazwa) return true;
            else return false;
        }
        public static bool operator !=(Firma a, Firma b)
        {
            if (a.nr_KRS != b.nr_KRS && a.nazwa != b.nazwa) return true;
            else return false;
        }
    }
}