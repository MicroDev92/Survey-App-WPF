using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnketaVezbaWPF.Model
{
    public class Anketa
    {
        public Anketa(int anketaID, string naslovAnkete, bool aktivnost, bool javnost)
        {
            AnketaID = anketaID;
            NaslovAnkete = naslovAnkete;
            ListaPitanja = new ObservableCollection<Pitanje>(); //instancirana lista
            Aktivnost = aktivnost;
            Javnost = javnost;
        }

        public int AnketaID { get; set; }
        public string NaslovAnkete { get; set; }
        public ObservableCollection<Pitanje> ListaPitanja { get; set; }//definisana lista
        public bool Aktivnost { get; set; }
        public bool Javnost { get; set; }
    }
}
