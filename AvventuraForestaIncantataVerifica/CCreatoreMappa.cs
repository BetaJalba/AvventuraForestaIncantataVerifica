using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvventuraForestaIncantataVerifica
{
    public class CCreatoreMappa
    {
        CCasella[]  mappa;

        public CCasella[] Mappa 
        {
            get { return mappa; } 
        }

        public CCreatoreMappa() 
        {
            mappa = new CCasella[50];
            fillMappa();
        }

        void fillMappa() 
        {
            for (int i = 0; i < mappa.Length; i++) 
            {
                if (i > 7 && i - 1 % 7 == 0)
                    mappa[i] = new CCasellaMagica();
                else if (i == 12)
                    mappa[i] = new CCasellaRagnatela();
                else if (i == 26)
                    mappa[i] = new CCasellaPalude();
                else if (i == 15 || i == 34)
                    mappa[i] = new CCasellaAmico();
                else if (i == 18)
                    mappa[i] = new CCasellaAlbero();
                else
                    mappa[i] = new CCasella();
            }
        }
    }
}
