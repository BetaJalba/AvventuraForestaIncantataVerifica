using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvventuraForestaIncantataVerifica
{
    public class CDado
    {
        static int step = 6;

        public CDado() 
        {

        }

        public int Lancia() 
        {
            Random random = new Random();
            return random.Next(step) + 1;
        }
    }
}
