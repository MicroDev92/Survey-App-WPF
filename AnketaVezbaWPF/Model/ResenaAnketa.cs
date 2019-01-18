using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnketaVezbaWPF.Model
{
    class ResenaAnketa
    {
        public Osoba osoba { get; set; }
        public Anketa anketa { get; set; }
        public ObservableCollection<ResenOdgovor> ListaResenihOdgovora { get; set; }

        public ResenaAnketa(Osoba osoba, Anketa anketa)
        {
            this.osoba = osoba;
            this.anketa = anketa;
            ListaResenihOdgovora = new ObservableCollection<ResenOdgovor>();
        }

        public string PrikazPodataka()
        {
            string prikaz = "  Ime: " + osoba.KorisnickoIme + "\n" +
                 "  Naziv ankete: " + anketa.AnketaID +"\nLista resenih odgovora:\n--------------\n";

            foreach (ResenOdgovor resOdg in ListaResenihOdgovora)
            {
                foreach (Anketa a in ListePodataka.ListaAnketa) {
                    if (a.AnketaID == resOdg.AnketaID) {

                        foreach (Pitanje pit in a.ListaPitanja) {

                            if (pit.PitanjeID == resOdg.PitanjeID)
                            {
                                prikaz += "Tekst pitanja: " + pit.TekstPitanja;
                                foreach (Odgovor odg in pit.ListaOdgovora)
                                {
                                    if (odg.OdgovorID == resOdg.OdgovorID)
                                    {
                                        prikaz += "    Tekst odgovora: " + odg.TekstOdg + "\n";
                                    }
                                }
                            }

                        }
                    }
                }
            }




            return prikaz;
        }
    }
}
