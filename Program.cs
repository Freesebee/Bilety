using System;
using System.Collections.Generic;
using System.Linq;

namespace Bilety
{
    class Program
    {
        static void Main()
        {
            BiletSystem.WczytajStan();
            BiletSystem.PokazKlientow(BiletSystem.GetKlienci);
            BiletSystem.PokazLotniska();
            BiletSystem.PokazSamoloty();
            BiletSystem.PokazTrasy();
            BiletSystem.PokazLoty();
        }
    }
}
