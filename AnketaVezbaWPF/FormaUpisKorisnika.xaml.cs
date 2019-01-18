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
using System.Windows.Shapes;

namespace AnketaVezbaWPF
{
    /// <summary>
    /// Interaction logic for FormaUpisKorisnika.xaml
    /// </summary>
    public partial class FormaUpisKorisnika : Window
    {
        TipoviKorisnika tipKor = TipoviKorisnika.korisnik;
        string upisIzmena = "upis";
        int idKorisnika = 0;
        
        public FormaUpisKorisnika(TipoviKorisnika tipKor, string upisIzmena, int idKorisnika)
        {
            InitializeComponent();

            cbTipKorisnika.ItemsSource = Enum.GetValues(typeof(TipoviKorisnika)).Cast<TipoviKorisnika>();

            this.tipKor = tipKor;
            this.upisIzmena = upisIzmena;
            this.idKorisnika = idKorisnika;


            if (tipKor == TipoviKorisnika.administrator)
                btnRegistrujSe.Content = "Registruj";

             
        }

        private void btnZatvori_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRegistrujSe_Click(object sender, RoutedEventArgs e)
        {

            string korisnickoIme = tbKorisnickoIme.Text;
            string sifra = tbSifraKorisnika.Text;
            tipKor = (TipoviKorisnika)cbTipKorisnika.SelectedIndex;

            if (upisIzmena == "upis")
            {
                //int osobaID, string korisnickoIme, string sifra, TipoviKorisnika tipKorisnika, DateTime datumRegistracije, bool pristup

                idKorisnika = ListePodataka.generisiOsobaID();

                Osoba osoba = new Osoba(idKorisnika, korisnickoIme, sifra, tipKor, new DateTime(), true);
                ListePodataka.ListaOsoba.Add(osoba);
                UpisUBazu.upisOsobe(osoba);
            }
            else if (upisIzmena == "izmena") {

                foreach (Osoba osoba in ListePodataka.ListaOsoba)
                {
                    if (idKorisnika == osoba.OsobaID)
                    {
                        if (cbPristup.IsChecked == true)
                        {
                            osoba.Pristup = true;
                        }
                        else
                        {
                            osoba.Pristup = false;
                        }

                        osoba.KorisnickoIme = korisnickoIme;
                        osoba.Sifra = sifra;
                        osoba.TipKorisnika = tipKor;

                        UpisUBazu.izmeniPodatkeOOsobi(osoba);
                    }
                }

            }
            this.Close();
        }
    }
}
