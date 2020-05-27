using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilety
{
    public class Airbus : Samolot
    {
        public Airbus() : base(1200, 100) { }
        public override string ToString()
        {
            return "Airbus";
        }
    }
}