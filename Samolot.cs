using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilety
{
    public abstract class Samolot
    {
        public int zasieg {get; private set; }
        public int liczba_miejsc {get; private set; }
        public int predkosc {get; private set;}

        public Samolot(int _zasieg, int _liczba_miejsc)
        {
            zasieg = _zasieg;
            liczba_miejsc = _liczba_miejsc;
            predkosc = 800; //km na godzine
        }
    }
    public class Boeing : Samolot
    {
        public Boeing() : base(5000, 200) { }
        public override string ToString()
        {
            return "\nBoeing\nZasieg: 2000\nLiczba miejsc: 200";
        }
    }
    public class Airbus : Samolot
    {
        public Airbus() : base(1200, 100) { }
        public override string ToString()
        {
            return "\nAirbus\nZasieg: 1200\nLiczba miejsc: 100";
        }
    }
    public class Bombardier : Samolot
    {
        public Bombardier() : base(500, 50) { }
        public override string ToString()
        {
            return "\nBombardier\nZasieg: 500\nLiczba miejsc: 50";
        }
    }
}