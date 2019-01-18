using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnketaVezbaWPF.Model
{
    public enum TipoviKorisnika { korisnik, administrator}

    public class Osoba
    {
        public Osoba(int osobaID, string korisnickoIme, string sifra, TipoviKorisnika tipKorisnika, DateTime datumRegistracije, bool pristup)
        {
            OsobaID = osobaID;
            KorisnickoIme = korisnickoIme;
            Sifra = sifra;
            TipKorisnika = tipKorisnika;
            DatumRegistracije = datumRegistracije;
            Pristup = pristup;
        }

        public int OsobaID { get; set; }
        public string KorisnickoIme { get; set; }
        public string Sifra { get; set; }
        public TipoviKorisnika TipKorisnika { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public bool Pristup { get; set; }

        


    }
}
