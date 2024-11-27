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
                                    pId,
                                    effetto;
        CCasella[]                  mappa;
        public event EventHandler   OnShouldThrow;
        public event Action<string> OnWin;

        public CGiocatore(CCasella[] mappa) 
        {
            canThrow = true;
            mappaIndex = 0;
            pId = count;
            count++;
            effetto = -1;
            this.mappa = mappa;
            for (int i = 0; i < this.mappa.Length; i++) 
            {
                mappa[i].OnEffetto += (sender, e) =>
                {
                    if (sender is CCasellaRagnatela & e.Casella == mappaIndex && mappaIndex != oldMappaIndex) 
                    {
                        effetto = 0;
                        saltaTurno();
                    }
                        
                    else if (sender is CCasellaAlbero && e.Casella == mappaIndex && mappaIndex != oldMappaIndex) 
                    {
                        ritiraDado();
                        effetto = 1;
                    }
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

                int extra = mappa[mappaIndex].Effetto(); // lo richiama solo una volta

                if (extra > 0)
                    bonusString = $"\nBonus di {extra} passi per il giocatore {pId} per essere atterrato sulla casella {mappaIndex}!";
                else if (extra < 0)
                    bonusString = $"\nIl giocatore {pId} perde {Math.Abs(extra)} posizioni per essere atterrato sulla casella {mappaIndex}!";
                else if (effetto == 0)
                    bonusString = $"\nIl giocatore {pId} non potrà tirare il prossimo turno!";
                else if (effetto == 1)
                    bonusString = $"\nIl giocatore {pId} ha diritto a lanciare di nuovo il dado!";
                
                if (effetto != -1)
                    effetto = -1;

                mappaIndex += extra;

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
