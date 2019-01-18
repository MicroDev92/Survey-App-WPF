using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnketaVezbaWPF.Model
{
    public class Pitanje
    {
        public Pitanje(int pitanjeID, int anketaID, string tekstPitanja)
        {
            PitanjeID = pitanjeID;
            AnketaID = anketaID;
            TekstPitanja = tekstPitanja;
            ListaOdgovora = new ObservableCollection<Odgovor>();
        }

        public int PitanjeID { get; set; }
        public int AnketaID { get; set; }
        public string TekstPitanja { get; set; }
        public ObservableCollection<Odgovor> ListaOdgovora { get; set; }

    }
}
