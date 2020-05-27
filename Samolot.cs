using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilety
{
    public abstract class Samolot
    {
        protected int zasieg;
        protected int liczba_miejsc;
        private const int predkosc = 1000;
        public Samolot(int _zasieg, int _liczba_miejsc)
        {
            zasieg = _zasieg;
            liczba_miejsc = _liczba_miejsc;
        }
    }
}