using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace Bilety
{
    public class Lot
    {
        private Bilety.Samolot samolot;
        private DateTime czas_wylotu;
        private Trasa trasa_lotu;
        private List<Bilet> bilety;

        public Lot()
        {
            throw new System.NotImplementedException();
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
        public void DobierzSamolot()
        {
            throw new System.NotImplementedException();
        }

        public int PoliczWolneMiejsca()
        {
            throw new System.NotImplementedException();
        }
    }
}