using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bilety
{
    class DuplikatNumeruException : ApplicationException
    {
        public DuplikatNumeruException(string text) : base(text) { }
    }
    class NiepoprawnaInformacjaException : ApplicationException
    {
        public NiepoprawnaInformacjaException(string text) : base(text) { }
    }
    class NiepoprawnyNumerException : ApplicationException
    {
        public NiepoprawnyNumerException(string text) : base(text) { }
    }
    class BrakObiektuDoDodaniaException : ApplicationException
    {
        public BrakObiektuDoDodaniaException(string text) : base(text) { }
    }
}
