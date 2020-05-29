using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Bilety
{
    public class Lot
    {
        private Samolot samolot;
        private DateTime czas_wylotu;
        private List<Bilet> bilety;

        public Lot(DateTime czaswylotu)
        {
            czas_wylotu = czaswylotu;
            samolot = DobierzSamolot();
        }
        ~Lot()
        {
            bilety.Clear();
            bilety = null;
        }
        public override string ToString()
        {
            throw new System.NotImplementedException();
        }
        private Samolot DobierzSamolot()
        {
            throw new System.NotImplementedException();
        }

        public int PoliczWolneMiejsca()
        {
            throw new System.NotImplementedException();
        }
    }
}