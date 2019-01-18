using AnketaVezbaWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FormaKorisnika.xaml
    /// </summary>
    public partial class FormaKorisnika : Window
    {
        Osoba osoba = null;
        Anketa selektovanaAnketa = null;
        Pitanje selektovanoPitanje = null;
        ObservableCollection<ResenOdgovor> listaResenihOdgovara = new ObservableCollection<ResenOdgovor>();
        public FormaKorisnika(Osoba osoba)
        {
            InitializeComponent();

            this.osoba = osoba;

            resetujCbSifreAnketa();

            
        }

        private void resetujCbSifreAnketa() {

            cbAnkete.Items.Clear();
            lblNaslovAnkete.Content = "Naslov ankete:";
            dgPitanjaAnkete.ItemsSource = null;
            dgOdgovoriPitanja.ItemsSource = null;
            //izlista sve ankete
            foreach (Anketa anketa in ListePodataka.ListaAnketa)
            {
                bool postoji = false;//u svakoj iteraciji setuje na false radi provere sledece ankete da li se za korisnika nalazi u listi resenih anketa
                //izlista sve resene ankete i proveri da li je prijavljena osoba vec resavala anketu tekuce iteracije spoljasnje foreach petlje
                foreach (ResenaAnketa resenaAnketa in ListePodataka.ListaResenihAnketa)
                {
                    //ako je korisnik vec resavao anketu u listi resenih anketa je pronadjen objekat koji sadrzi objekat korisnika i objekat te ankete
                    if (resenaAnketa.osoba.OsobaID == osoba.OsobaID && resenaAnketa.anketa.AnketaID == anketa.AnketaID)
                        postoji = true;
                }
                //sifra ankete se dodaje u combo box samo ako je korisnik jos nije resavao
                if (!postoji)
                    cbAnkete.Items.Add(anketa.AnketaID);

                cbAnkete.SelectedIndex = 0;

            }
        }

        private void cbAnkete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbAnkete.Items.Count > 0)
            {
                int idAnkete = (int)cbAnkete.SelectedItem;
                foreach (Anketa anketa in ListePodataka.ListaAnketa)
                {
                    if (idAnkete == anketa.AnketaID)
                    {
                        lblNaslovAnkete.Content = "Naslov Ankete: " + anketa.NaslovAnkete;
                        this.selektovanaAnketa = anketa;// this znaci da se pristupa promenljivoj na nivou klase, zato sto su istog naziva

                        dgPitanjaAnkete.ItemsSource = anketa.ListaPitanja;
                        dgPitanjaAnkete.Items.Refresh();
                    }
                }
            }
        }

        private void dgPitanjaAnkete_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgPitanjaAnkete.SelectedIndex != -1)
            {
                Pitanje pitanje = (Pitanje)dgPitanjaAnkete.SelectedItem;
                selektovanoPitanje = pitanje;


                dgOdgovoriPitanja.ItemsSource = pitanje.ListaOdgovora;
                dgOdgovoriPitanja.Items.Refresh();
            }
        }

        private void btnPotvrdiOdgovor_Click(object sender, RoutedEventArgs e)
        {
            if (dgOdgovoriPitanja.SelectedIndex != -1)
            {
                Odgovor odg = (Odgovor)dgOdgovoriPitanja.SelectedItem;

                if (MessageBox.Show("Da li ste sigurni?", "Potvrdi", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    int odgID = odg.OdgovorID;
                    int pitID = selektovanoPitanje.PitanjeID;
                    int ankID = selektovanaAnketa.AnketaID;
                    ResenOdgovor resOdg = new ResenOdgovor(odgID, pitID, ankID);
                    listaResenihOdgovara.Add(resOdg);

                }
            }

        }

        private void btnUpisAnkete_Click(object sender, RoutedEventArgs e)
        {
            ResenaAnketa resAnk = new ResenaAnketa(osoba, selektovanaAnketa);
            resAnk.ListaResenihOdgovora = listaResenihOdgovara;
            ListePodataka.ListaResenihAnketa.Add(resAnk);

            listaResenihOdgovara = new ObservableCollection<ResenOdgovor>();

            resetujCbSifreAnketa();
        }

        private void btnPrikaziReseneAnkete_Click(object sender, RoutedEventArgs e)
        {
            PrikazResenihAnketa prikazResAnk = new PrikazResenihAnketa(osoba);
            prikazResAnk.ShowDialog();


        }

    }
}
