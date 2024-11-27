using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvventuraForestaIncantataVerifica
{
    public class CGiocatore
    {
        static int                  count = 1;


        bool                        canThrow;
        int                         mappaIndex,
                                    oldMappaIndex, // risolve un problema molto specifica
                                    pId;
        CCasella[]                  mappa;
        public event EventHandler   OnShouldThrow;
        public event Action<string> OnWin;

        public CGiocatore(CCasella[] mappa) 
        {
            canThrow = true;
            mappaIndex = 0;
            pId = count;
            count++;
            this.mappa = mappa;
            for (int i = 0; i < this.mappa.Length; i++) 
            {
                mappa[i].OnEffetto += (sender, e) =>
                {
                    if (sender is CCasellaRagnatela & e.Casella == mappaIndex && mappaIndex != oldMappaIndex) 
                    {
                        saltaTurno();
                    }
                        
                    else if (sender is CCasellaAlbero && e.Casella == mappaIndex && mappaIndex != oldMappaIndex)
                        ritiraDado();
                };
            }
        }

        public string? Avanza(int n) 
        {
            if (canThrow)
            {
                string bonusString = string.Empty;

                mappaIndex += n;
                if (mappaIndex >= mappa.Length) 
                {
                    OnWin?.Invoke($"Giocatore {pId} ha vinto!");
                    return null;
                }

                if (mappa[mappaIndex].Effetto() > 0)
                    bonusString = $"\nBonus di {mappa[mappaIndex].Effetto()} passi per il giocatore {pId} per essere atterrato sulla casella {mappaIndex}!";
                else if (mappa[mappaIndex].Effetto() < 0)
                    bonusString = $"\nIl giocatore {pId} perde {Math.Abs(mappa[mappaIndex].Effetto())} posizioni per essere atterrato sulla casella {mappaIndex}!";

                mappaIndex += mappa[mappaIndex].Effetto();

                return $"Giocatore {pId} avanza fino alla {mappa[mappaIndex].Nome} in posizione {mappaIndex}. {bonusString}";
            }
            else 
            {
                oldMappaIndex = mappaIndex; // se l'altro giocatore atrriva sulla casella la condizione farebbe saltare al giocatore il turno 2 volte, perciò se il giocatore è già fermo su questa casella allora non deve saltarlo di nuovo
                canThrow = !canThrow;
                return $"Giocatore {pId} non può tirare questo turno.";
            }
        }

        private void saltaTurno() 
        {
            canThrow = false;
        }

        private void ritiraDado() 
        {
            OnShouldThrow?.Invoke(this, EventArgs.Empty);
        }
    }
}
