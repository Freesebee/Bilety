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
            nr_KRS = KRS;
            nazwa = nazwa_firmy;
            lista_klientow = null;
        }
        ~Firma()
        {
            lista_klientow.Clear();
            lista_klientow = null;
        }
        public void DodajKlienta(Osoba nowy_klient)
        {
            if (!lista_klientow.Contains(nowy_klient)) lista_klientow.Add(nowy_klient);
        }
        public void UsunKlienta(Osoba dany_klient)
        {
            if (lista_klientow.Contains(dany_klient)) lista_klientow.Remove(dany_klient);
        }
        public override string ToString()
        {
            return nazwa;
        }
    }
}