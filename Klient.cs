using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bilety
{
    public abstract class Klient
    {
        public virtual bool CzyZawieraZnaki(string tekst) { return false; }
        public virtual bool CzyTenSamUnikalnyNr(string nr) { return false; }
    }
}