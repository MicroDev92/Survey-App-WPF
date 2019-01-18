using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnketaVezbaWPF.Model
{
    public class Odgovor
    {

        public int OdgovorID { get; set; }
        public int PitanjeID { get; set; }
        public string TekstOdg { get; set; }

        public Odgovor(int odgovorID, int pitanjeID, string tekstOdg)
        {
            OdgovorID = odgovorID;
            PitanjeID = pitanjeID;
            TekstOdg = tekstOdg;
        }
    }
}
