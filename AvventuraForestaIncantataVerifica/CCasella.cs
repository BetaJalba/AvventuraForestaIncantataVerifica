using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvventuraForestaIncantataVerifica
{
    public class CCasella
    {
        static int count = 0;

        int                                             id;
        string                                          nome;
        public event EventHandler<CasellaEventArgs>     OnEffetto;
        
        public string Nome 
        { 
            get { return nome; } 
        }

        public CCasella() 
        {
            id = count;
            count++;
            nome = "Casella Normale";
        }

        protected CCasella(string nome)
        {
            id = count;
            count++;
            this.nome = nome;
        }

        protected void EventoEffetto() 
        {
            CasellaEventArgs args = new CasellaEventArgs();
            args.Casella = id;
            OnEffetto?.Invoke(this, args);
        }

        public virtual int Effetto() 
        {
            return 0;
        }
    }

    public class CCasellaMagica : CCasella 
    {
        public CCasellaMagica() : base("Casella Sorgente Magica") 
        {

        }

        public override int Effetto() 
        {
            return 3;
        }
    }

    public abstract class CCasellaTrappola : CCasella 
    {
        public CCasellaTrappola(string nome) : base($"Casella Trappola: {nome}") 
        {

        }
    }

    public class CCasellaRagnatela : CCasellaTrappola  
    {
        public CCasellaRagnatela() : base("RagnatelaGigante") 
        {

        }

        public override int Effetto() // if sender is CCasellaRagnatela salta
        {
            EventoEffetto();
            return 0;
        }
    }

    public class CCasellaPalude : CCasellaTrappola 
    {
        public CCasellaPalude() : base("Palude Viscosa") 
        {

        }

        public override int Effetto()
        {
            return -7;
        }
    }

    public class CCasellaAmico : CCasella 
    {
        public CCasellaAmico() : base("Casella Amico della Foresta") 
        {

        }

        public override int Effetto()
        {
            return 5;
        }
    }

    public class CCasellaAlbero : CCasella
    {
        public CCasellaAlbero() : base("Casella Albero del Destino") 
        {

        }

        public override int Effetto() // if sender is CCasellaAlbero ritira
        {
            EventoEffetto();
            return 0;
        }
    }

    public class CasellaEventArgs : EventArgs 
    {
        public int Casella;
    }
}
