using AnketaVezbaWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnketaVezbaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //int osobaID, string korisnickoIme, string sifra, TipoviKorisnika tipKorisnika, bool pristup
            //ListePodataka.ListaOsoba.Add(new Osoba(ListePodataka.generisiOsobaID(), "polladmin@cloudhorizon.com", "polladmin", TipoviKorisnika.administrator, true));
            /*
            ListePodataka.ListaOsoba.Add(new Osoba(ListePodataka.generisiOsobaID(), "admin", "admin", TipoviKorisnika.administrator, new DateTime(), true));
            ListePodataka.ListaOsoba.Add(new Osoba(ListePodataka.generisiOsobaID(), "", "", TipoviKorisnika.administrator, new DateTime(), true));
            ListePodataka.ListaOsoba.Add(new Osoba(ListePodataka.generisiOsobaID(), "Korisnik", "korisnik", TipoviKorisnika.korisnik, new DateTime(), true));
            */
            //public Anketa(int anketaID, string naslovAnkete, bool aktivnost, bool javnost)

            ListePodataka.ListaOsoba = UpisUBazu.ucitajOsobe();
            //ListePodataka.ListaAnketa = UpisUBazu.UcitajAnkete();
            ListePodataka.ListaAnketa = UpisUBazu.DodajObjekteUListe();


            /*
            ListePodataka.ListaAnketa.Add(new Anketa(ListePodataka.generisiAnketaID(), "Anketa1", true, true));
            ListePodataka.ListaAnketa.Add(new Anketa(ListePodataka.generisiAnketaID(), "Anketa2", true, true));
            */

            //  public Odgovor(int odgovorID, string tekstOdg)
            // public Pitanje(int pitanjeID, string tekstPitanja)  pitanje ima kolekciju odgovora, anketa kolekciju pitanja


            /*
            Pitanje pitanje1 = new Pitanje(ListePodataka.generisiPitanjeID(ListePodataka.ListaAnketa.ElementAt(0)), "glavni grad Srbije je?");
            ListePodataka.ListaAnketa.ElementAt(0).ListaPitanja.Add(pitanje1);
            Pitanje pitanje2 = new Pitanje(ListePodataka.generisiPitanjeID(ListePodataka.ListaAnketa.ElementAt(0)), "glavni grad Nemacke je?");
            ListePodataka.ListaAnketa.ElementAt(0).ListaPitanja.Add(pitanje2);
            Pitanje pitanje3 = new Pitanje(ListePodataka.generisiPitanjeID(ListePodataka.ListaAnketa.ElementAt(0)), "glavni grad Francuske je?");
            ListePodataka.ListaAnketa.ElementAt(0).ListaPitanja.Add(pitanje3);

            Pitanje pitanje4 = new Pitanje(ListePodataka.generisiPitanjeID(ListePodataka.ListaAnketa.ElementAt(1)), "Da li znate C# programski jezik?");
            ListePodataka.ListaAnketa.ElementAt(1).ListaPitanja.Add(pitanje4);
            Pitanje pitanje5 = new Pitanje(ListePodataka.generisiPitanjeID(ListePodataka.ListaAnketa.ElementAt(1)), "Da li znate Java programski jezik?");
            ListePodataka.ListaAnketa.ElementAt(1).ListaPitanja.Add(pitanje5);
            Pitanje pitanje6 = new Pitanje(ListePodataka.generisiPitanjeID(ListePodataka.ListaAnketa.ElementAt(1)), "Da li znate Python programski jezik?");
            ListePodataka.ListaAnketa.ElementAt(1).ListaPitanja.Add(pitanje6);

            Odgovor odgovor1 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje1), "Beograd");
            pitanje1.ListaOdgovora.Add(odgovor1);
            Odgovor odgovor2 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje1), "Nis");
            pitanje1.ListaOdgovora.Add(odgovor2);

            Odgovor odgovor3 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje2), "Berlin");
            pitanje2.ListaOdgovora.Add(odgovor3);
            Odgovor odgovor4 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje2), "Frankfurt");
            pitanje2.ListaOdgovora.Add(odgovor4);

            Odgovor odgovor5 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje3), "Lion");
            pitanje3.ListaOdgovora.Add(odgovor5);
            Odgovor odgovor6 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje3), "Pariz");
            pitanje3.ListaOdgovora.Add(odgovor6);

            Odgovor odgovor7 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje4), "Da");
            pitanje4.ListaOdgovora.Add(odgovor7);
            Odgovor odgovor8 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje4), "Ne");
            pitanje4.ListaOdgovora.Add(odgovor8);

            Odgovor odgovor9 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje5), "Da");
            pitanje5.ListaOdgovora.Add(odgovor9);
            Odgovor odgovor10 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje5), "Ne");
            pitanje5.ListaOdgovora.Add(odgovor10);

            Odgovor odgovor11 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje6), "Da");
            pitanje6.ListaOdgovora.Add(odgovor11);
            Odgovor odgovor12 = new Odgovor(ListePodataka.generisiOdgovorID(pitanje6), "Ne");
            pitanje6.ListaOdgovora.Add(odgovor12);
            */


        }

        private void btnRegistrujSe_Click(object sender, RoutedEventArgs e)
        {
            string korisnickoIme = tbKorisnickoIme.Text;
            string lozinka = pbLozinka.Password.ToString();

            bool postoji = false;
            foreach (Osoba o in ListePodataka.ListaOsoba) {
                if (o.KorisnickoIme == korisnickoIme && tbKorisnickoIme.Text != "")
                {
                    postoji = true;
                    MessageBox.Show("Postoji osoba korisnickog imena: " + korisnickoIme);
                }

            }

            if (!postoji && korisnickoIme != "" && lozinka != "")
            {
                Osoba os = new Osoba(ListePodataka.generisiOsobaID(), korisnickoIme, lozinka, TipoviKorisnika.korisnik, new DateTime(), true);
                ListePodataka.ListaOsoba.Add(os);

                UpisUBazu.upisOsobe(os);
            }
            else if(korisnickoIme == "" && lozinka == "")
            {
                MessageBox.Show("Morate uneti korisnicko ime i sifru!");
            }
        }

        private void btnPrijaviSe_Click(object sender, RoutedEventArgs e)
        {
            string korisnickoIme = tbKorisnickoIme.Text;
            string lozinka = pbLozinka.Password;

            foreach (Osoba o in ListePodataka.ListaOsoba)
            {
                if (o.KorisnickoIme==korisnickoIme && o.Sifra == lozinka)
                {
                    if (o.TipKorisnika==TipoviKorisnika.administrator)
                    {
                        FormaAdministratora formaA = new FormaAdministratora();
                        formaA.Show();
                    }

                    if (o.TipKorisnika==TipoviKorisnika.korisnik)
                    {
                        FormaKorisnika formaK = new FormaKorisnika(o);
                        formaK.Show();
                    }
                }
            }
        }
    }
}
