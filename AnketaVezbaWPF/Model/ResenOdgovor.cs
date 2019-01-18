using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnketaVezbaWPF.Model
{
    class ResenOdgovor
    {
        public ResenOdgovor(int odgovorID, int pitanjeID, int anketaID)
        {
            OdgovorID = odgovorID;
            PitanjeID = pitanjeID;
            AnketaID = anketaID;
        }

        public int OdgovorID { get; set; }
        public int PitanjeID { get; set; }
        public int AnketaID { get; set; }
    }
}
