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
    /// Interaction logic for FormaUpisAnkete.xaml
    /// </summary>
    public partial class FormaUpisAnkete : Window
    {
        //ObservableCollection<Pitanje> ListPitanjaAnkete = new ObservableCollection<Pitanje>();

        public int idAnkete=0;
        public string upisIzmena = "upis";


        private int idPitanjaIzmenaBrisanje = 0;
        private int idOdgovoraIzmenjaBrisanje = 0;

        public FormaUpisAnkete(string upisIzmena, Anketa selektovanaAnketa)
        {
            InitializeComponent();

            this.upisIzmena = upisIzmena;
            if(selektovanaAnketa!=null) //prosledjen je selektovan objekat
                this.idAnkete = selektovanaAnketa.AnketaID;


            if (upisIzmena == "izmena") {
                btnDodajAnketu.Visibility = Visibility.Hidden;
                tbNaslovAnkete.IsEnabled = false;
                tbNaslovAnkete.Text = selektovanaAnketa.NaslovAnkete;
                dataGridPitanja.ItemsSource = selektovanaAnketa.ListaPitanja;//pristupi se listi pitanja u objektu Anketa klase

  
            }
        }

        private void btnDodajAnketu_Click(object sender, RoutedEventArgs e)
        {
            string naslovAnkete = tbNaslovAnkete.Text;
            idAnkete= ListePodataka.generisiAnketaID();

            ListePodataka.ListaAnketa.Add(new Anketa(idAnkete, naslovAnkete, true, true));
            UpisUBazu.upisAnkete(new Anketa(idAnkete, naslovAnkete, true, true));

            lblIdAnkete.Content = "Id ankete: " + idAnkete;
            tbNaslovAnkete.IsEnabled = false;

            //dataGridPitanja.ItemsSource = ListPitanjaAnkete;
            foreach (Anketa anketa in ListePodataka.ListaAnketa)
            {
                if (anketa.AnketaID == idAnkete)
                {
                    dataGridPitanja.ItemsSource = anketa.ListaPitanja;
                }
            }
           

        }

        private void btnDodajPitanje_Click(object sender, RoutedEventArgs e)
        {


            string naslovAnkete = tbNaslovAnkete.Text;
          
            foreach (Anketa anketa in ListePodataka.ListaAnketa)
            {
                if (anketa.AnketaID == idAnkete)
                {
                    //poziva se prvo upis u bazu zato sto metoda generise id na osnovu postojecih id u listi podataka
                    //kad se upise u bazu sledecim pozivom ce opet ucitati postojece iz liste, pa se zato dodavanje u listu poziva posle ove metode
                    UpisUBazu.upisPitanja(new Pitanje(ListePodataka.generisiPitanjeID(anketa), anketa.AnketaID, tbTextPitanja.Text), idAnkete);
                    anketa.ListaPitanja.Add(new Pitanje(ListePodataka.generisiPitanjeID(anketa), anketa.AnketaID, tbTextPitanja.Text));
                   

                }
            }
            tbTextPitanja.Text = "";


        }

        private void btnIzmeniTekstPitanja_Click(object sender, RoutedEventArgs e)
        {
            
            //idPitanjaIzmenaBrisanje prethodno treba selektovati u datagridu
            //idAnkete definisano na nivou klase
            string tekstPitanja = tbIzmenaTekstaPitanja.Text;
            if (idAnkete>0 && tbIzmenaTekstaPitanja.Text !="" && idPitanjaIzmenaBrisanje>0) {

                
                foreach (Anketa a in ListePodataka.ListaAnketa) {
                    if (a.AnketaID == idAnkete) {

                        for (int i = 0; i < a.ListaPitanja.Count; i++) {

                            if (a.ListaPitanja.ElementAt(i).PitanjeID == idPitanjaIzmenaBrisanje) {
                                a.ListaPitanja.ElementAt(i).TekstPitanja = tekstPitanja;
                            }
                        }


                    }
                }

                Pitanje pit = new Pitanje(idPitanjaIzmenaBrisanje, idAnkete, tekstPitanja);
                UpisUBazu.IzmeniPitanje(pit);
                

                /*
                for (int i = 0; i < ListePodataka.ListaAnketa.Count; i++) {
                    if (ListePodataka.ListaAnketa.ElementAt(i).AnketaID == idAnkete) { //Element(i) je objekat klase Anketa u listi ListaAnekta u klasi ListePodataka

                        //pronadjena anketa na osnovu id i sada treba u listi pitanja koja se nalazi u objektu na osnovu id pronaci pitanje za izmenu teksta
                        for (int j = 0; j < ListePodataka.ListaAnketa.ElementAt(i).ListaPitanja.Count; j++)
                        {

                            if (ListePodataka.ListaAnketa.ElementAt(i).ListaPitanja.ElementAt(j).PitanjeID == idPitanjaIzmenaBrisanje)
                            {
                                ListePodataka.ListaAnketa.ElementAt(i).ListaPitanja.ElementAt(j).TekstPitanja = tekstPitanja;
                            }
                        }

                    }
                }*/



                dataGridPitanja.Items.Refresh();
                //idPitanjaOdgovoriIzmenaBrisanje = 0; //setuje se opet na 0 zato da ne dodje do nekog exception-a, npr ako je u medjuvremeno obrisano pitanje koje ima ovaj id
            }
        }

        private void dataGridPitanja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Anketa anketaIzmenaPitanja = null;
            foreach (Anketa anketa in ListePodataka.ListaAnketa) {
                if (idAnkete > 0 && anketa.AnketaID == idAnkete)
                    anketaIzmenaPitanja = anketa;
            }

            if (anketaIzmenaPitanja != null) {//ako je pronadjena anketa na osnovu id, razlikuje se od null
                if (anketaIzmenaPitanja.ListaPitanja.Count > 0 && dataGridPitanja.SelectedIndex != -1) {
                    Pitanje pitanje = (Pitanje)dataGridPitanja.SelectedItem;

                    dataGridOdgovor.ItemsSource = pitanje.ListaOdgovora; //setuje items source 2. data grida, zato sto sadrzi odgovore za to pitanje

                    tbIzmenaTekstaPitanja.Text = pitanje.TekstPitanja;
                    idPitanjaIzmenaBrisanje = pitanje.PitanjeID; //setovana globalna promenljiva kojoj ce se pristupiti prilikom izmene
                }
            }
        }

        private void btnBrisi_Click(object sender, RoutedEventArgs e)
        {


            if (idPitanjaIzmenaBrisanje > 0)
            {
                //broji od 0 do broja elemenata liste koja je staticna i zato joj se pristupa preko naziva klase
                for (int i = 0; i < ListePodataka.ListaAnketa.Count; i++)
                {
                    //elementima liste se pristupa preko indeksa pozicije
                    if (ListePodataka.ListaAnketa.ElementAt(i).AnketaID == idAnkete)
                    { //Element(i) je objekat klase Anketa u listi ListaAnekta u klasi ListePodataka

                        //pronadjena anketa na osnovu id i sada treba u listi pitanja koja se nalazi u objektu na osnovu id pronaci pitanje za izmenu teksta
                        for (int j = 0; j < ListePodataka.ListaAnketa.ElementAt(i).ListaPitanja.Count; j++)
                        {

                            if (ListePodataka.ListaAnketa.ElementAt(i).ListaPitanja.ElementAt(j).PitanjeID == idPitanjaIzmenaBrisanje)
                            {
                                ListePodataka.ListaAnketa.ElementAt(i).ListaPitanja.RemoveAt(j);
                            }
                        }

                    }
                }

                UpisUBazu.brisiRedTabele(idPitanjaIzmenaBrisanje, "Pitanje");
                dataGridOdgovor.ItemsSource = null;

                //UpisUBazu.brisiRedTabele(idPitanjaIzmenaBrisanje, "Pitanje");

                /*
                foreach (Anketa a in ListePodataka.ListaAnketa)
                {
                    if (a.AnketaID == idAnkete)
                    {
                        foreach (Pitanje pit in a.ListaPitanja)
                        {
                            if (pit.PitanjeID == idPitanjaIzmenaBrisanje)
                            {
                                a.ListaPitanja.Remove(pit);
                            }
                        }
                    }
                }
                */
            }
        }

        private void btnDodajOdgovor_Click(object sender, RoutedEventArgs e)
        {
            if (idAnkete > 0 && idPitanjaIzmenaBrisanje > 0)
            {
                foreach (Anketa a in ListePodataka.ListaAnketa)
                {
                    if (a.AnketaID == idAnkete)
                    {
                        foreach (Pitanje pit in a.ListaPitanja)
                        {
                            if (pit.PitanjeID == idPitanjaIzmenaBrisanje)
                            {

                                int noviIdOdg = ListePodataka.generisiOdgovorID(pit);
                                pit.ListaOdgovora.Add(new Odgovor(noviIdOdg, pit.PitanjeID, tbOdgovor.Text));
                                UpisUBazu.upisOdgovora(new Odgovor(noviIdOdg, pit.PitanjeID, tbOdgovor.Text), pit.PitanjeID);
                            }
                        }
                    }
                }  
            }
            tbOdgovor.Clear();
        }

        private void dataGridOdgovor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridOdgovor.SelectedIndex != -1) {
                Odgovor odg = (Odgovor)dataGridOdgovor.SelectedItem;
                idOdgovoraIzmenjaBrisanje = odg.OdgovorID;
                tbTekstOdgovoraIzmenaBrisanje.Text = odg.TekstOdg;

            }
        }

        private void btnIzmeniOdgovor_Click(object sender, RoutedEventArgs e)
        {
            izmeniBrisiOdgovor("izmena");
        }

        private void btnObrisiOdgovor_Click(object sender, RoutedEventArgs e)
        {
            izmeniBrisiOdgovor("brisanje");
        }

        private void izmeniBrisiOdgovor(string izmenaBrisanje) {
            foreach (Anketa anketa in ListePodataka.ListaAnketa)
            {
                if (anketa.AnketaID == idAnkete)
                {
                    foreach (Pitanje pitanje in anketa.ListaPitanja)
                    {
                        if (pitanje.PitanjeID == idPitanjaIzmenaBrisanje)
                        {
                            for (int i = 0; i < pitanje.ListaOdgovora.Count; i++)
                            {

                                if (pitanje.ListaOdgovora.ElementAt(i).OdgovorID == idOdgovoraIzmenjaBrisanje)
                                {
                                    if (izmenaBrisanje == "izmena" && tbTekstOdgovoraIzmenaBrisanje.Text != "")
                                    {
                                        pitanje.ListaOdgovora.ElementAt(i).TekstOdg = tbTekstOdgovoraIzmenaBrisanje.Text;
                                        Odgovor odg = new Odgovor(idOdgovoraIzmenjaBrisanje, idPitanjaIzmenaBrisanje, tbTekstOdgovoraIzmenaBrisanje.Text);
                                        UpisUBazu.IzmeniOdgovor(odg);
                                    }
                                    if (izmenaBrisanje == "brisanje")
                                    {
                                        pitanje.ListaOdgovora.RemoveAt(i);

                                        UpisUBazu.brisiRedTabele(idOdgovoraIzmenjaBrisanje, "Odgovor");
                                    }
                                }
                            }

                            dataGridOdgovor.Items.Refresh();


                        }
                    }
                }
            }
        }
    }
}
