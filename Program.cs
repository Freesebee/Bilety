using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilety
{
    class Program
    {
        static void Main(string[] args)
        {
            Osoba k = new Osoba("K","S","123");
            Lot L = new Lot();
            Bilet n = new Bilet(k, L, k);
        }
    }
}
