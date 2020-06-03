﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilety
{
    public class Lotnisko
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Miasto { get; private set; }
        public string Kraj { get; private set; }

        
        public Lotnisko(int _x, int _y, string _kraj, string _miasto)
        {
            X = _x;
            Y = _y;
            Kraj = _kraj;
            Miasto = _miasto;
            Console.WriteLine($"Utworzono lotnisko {_kraj} {_miasto} ({_x},{_y})");
        }
        public override string ToString()
        {
            return Kraj +
                "\t" + Miasto +
                $"\t({X},{Y})";
        }
        public string DaneDoZapisu()
        {
            return $"{X},{Y},{Kraj},{Miasto}";
        }
    }
}