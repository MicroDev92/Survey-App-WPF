using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnketaVezbaWPF.Model
{
    class ListePodataka
    {

        private static ListePodataka instance;
        public static ListePodataka Instance {
            get {

                if (instance == null)
                    instance = new ListePodataka();

                return instance;
            }
        }

        // ListePodataka.Instance.nekaMetoda(); 

        //static promenljive, jedinstvene na nivou svih klasa
        public static ObservableCollection<Osoba> ListaOsoba = new ObservableCollection<Osoba>();
        public static ObservableCollection<Anketa> ListaAnketa = new ObservableCollection<Anketa>();
        public static ObservableCollection<ResenaAnketa> ListaResenihAnketa = new ObservableCollection<ResenaAnketa>();

        public static int generisiOsobaID()
        {
            int max = 0;
            if (ListaOsoba.Count > 0)
            {//ako ima podataka u listi osoba
                foreach (Osoba o in ListaOsoba)
                { //nakon svih iteracija u promenljivoj max ce biti max OsobaID
                    //3 4 2 1 6   3>0? max=3  4>3? max=4 2>4? 1>4? 6>4? max=6
                    if (o.OsobaID > max)
                        max = o.OsobaID;
                }
            }
            max++; //novi OsobaID
            return max;
        }

        public static int generisiAnketaID()
        {
            int max = 0;
            if (ListaAnketa.Count > 0)
            {
                foreach (Anketa a in ListaAnketa)
                {
                    if (a.AnketaID > max)
                    {
                        max = a.AnketaID;
                    }
                }
            }
            max++;
            return max;
        }
        public static int generisiPitanjeID(Anketa anketa)
        {
            int max = 0;

            foreach (Anketa ank in ListePodataka.ListaAnketa)
            {
                foreach (Pitanje pit in ank.ListaPitanja)
                {
                    if (pit.PitanjeID > max)
                    {
                        max = pit.PitanjeID;
                    }
                }
            }

            /*
            if (anketa.ListaPitanja.Count > 0)
            {
                foreach (Pitanje pit in anketa.ListaPitanja)
                {
                    if (pit.PitanjeID > max)
                    {
                        max = pit.PitanjeID;
                    }
                }
            }
            */
            max++;
            return max;
        }

        public static int generisiOdgovorID(Pitanje pitanje)
        {
            int max = 0;

            foreach (Anketa anketa in ListePodataka.ListaAnketa) {
                foreach (Pitanje pit in anketa.ListaPitanja) {
                    foreach (Odgovor odg in pit.ListaOdgovora) {

                        if (odg.OdgovorID > max)
                            max = odg.OdgovorID;
                    }
                }
            }

            /*
            if (pitanje.ListaOdgovora.Count > 0)
            {
                foreach (Odgovor odg in pitanje.ListaOdgovora)
                {
                    if (odg.OdgovorID > max)
                    {
                        max = odg.OdgovorID;
                    }
                }
            }
            */
            max++;
            return max;
        }

        public static int generisiPitanjeID(int anketaId)
        {
            int max = 0;
            Anketa anketa = null;

            foreach (Anketa a in ListaAnketa) {
                if (a.AnketaID == anketaId)
                    anketa = a;
            }

            if (anketa != null)
            {
                if (anketa.ListaPitanja.Count > 0)
                {
                    foreach (Pitanje pit in anketa.ListaPitanja)
                    {
                        if (pit.PitanjeID > max)
                        {
                            max = pit.PitanjeID;
                        }
                    }
                }
            }
            max++;
            return max;
        }

    }
}
