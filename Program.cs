using System;
using System.Collections.Generic;
using System.Linq;

namespace Bilety
{
    class Program
    {
        static void Main()
        {
            BiletSystem.DodajLotnisko(1,2,"asd","asd");
            BiletSystem.UsunLotnisko(1);
            Console.ReadKey();
        }
    }
}
