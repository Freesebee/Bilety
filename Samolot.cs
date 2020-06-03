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
        public bool CzyWolny {get; private set; }

        public Samolot(int _zasieg, int _liczba_miejsc)
        {
            zasieg = _zasieg;
            liczba_miejsc = _liczba_miejsc;
            predkosc = 800; //km na godzine
            CzyWolny = true;
        }
        public void Zajety()
        {
            CzyWolny = false;
        }
    }
    public class Boeing : Samolot
    {
        public Boeing() : base(5000, 200) { }
        public override string ToString()
        {
            if (CzyWolny)
                return $"|Boeing|\t|Zasieg: {zasieg}km|\t|Liczba miejsc: {liczba_miejsc}|\t|Wolny|";
            else
                return $"|Boeing|\t|Zasieg: {zasieg}km|\t|Liczba miejsc: {liczba_miejsc}|\t|Na trasie|";
        }
    }
    public class Airbus : Samolot
    {
        public Airbus() : base(1200, 100) { }
        public override string ToString()
        {
            if (CzyWolny)
                return $"|Airbus|\t|Zasieg: {zasieg}km|\t|Liczba miejsc: {liczba_miejsc}|\t|Wolny|";
            else
                return $"|Airbus|\t|Zasieg: {zasieg}km|\t|Liczba miejsc: {liczba_miejsc}|\t|Na trasie|";
        }
    }
    public class Bombardier : Samolot
    {
        public Bombardier() : base(500, 50) { }
        public override string ToString()
        {
            if (CzyWolny)
                return $"|Bombardier|\t|Zasieg: {zasieg}km|\t|Liczba miejsc: {liczba_miejsc}|\t|Wolny|";
            else
                return $"|Bombardier|\t|Zasieg: {zasieg}km|\t|Liczba miejsc: {liczba_miejsc}|\t|Na trasie|";
        }
    }
}