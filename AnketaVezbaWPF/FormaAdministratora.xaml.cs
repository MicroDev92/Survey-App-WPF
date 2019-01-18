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
    /// Interaction logic for FormaAdministratora.xaml
    /// </summary>
    public partial class FormaAdministratora : Window
    {
        private string selektovanElement = "anketa";
        private int idKorisnika = 0;

        public FormaAdministratora()
        {
            InitializeComponent();
            radioButtonAnkete.IsChecked = true;
            radioButtonKorisnici.IsChecked = false;

            dataGrid.ItemsSource = ListePodataka.ListaAnketa;

        }

        private void radioButtonAnkete_Checked(object sender, RoutedEventArgs e)
        {
            selektovanElement = "anketa";
            dataGrid.ItemsSource = ListePodataka.ListaAnketa;
            dataGrid.Items.Refresh();
        }

        private void radioButtonKorisnici_Checked(object sender, RoutedEventArgs e)
        {
            selektovanElement = "osoba";
            dataGrid.ItemsSource = ListePodataka.ListaOsoba;
            dataGrid.Items.Refresh();
        }

        private void UpisBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selektovanElement == "anketa") {
                FormaUpisAnkete forma = new FormaUpisAnkete("upis", null);
                forma.Owner = this;//kad se pokrene forma, izmene se direktno proslede na ovu formu koja je poziva
                forma.ShowDialog();
                dataGrid.Items.Refresh();
            }

            if (selektovanElement == "osoba")
            {
                FormaUpisKorisnika forma = new FormaUpisKorisnika(TipoviKorisnika.administrator, "upis", 0);
                forma.Owner = this;
                forma.cbPristup.IsChecked = true;
                forma.cbPristup.IsEnabled = false;
                forma.cbTipKorisnika.SelectedIndex = 1;
                forma.cbTipKorisnika.IsEnabled = false;
                
                forma.ShowDialog();
                dataGrid.Items.Refresh();
            }
        }

       

        private void IzmenaBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (selektovanElement == "anketa")
            {
                if (ListePodataka.ListaAnketa.Count > 0 && dataGrid.SelectedIndex != -1) {

                    Anketa selektovanaAnketa = (Anketa)dataGrid.SelectedItem;

                    FormaUpisAnkete forma = new FormaUpisAnkete("izmena", selektovanaAnketa);
                    forma.Owner = this;
                    forma.ShowDialog();

                    dataGrid.Items.Refresh();

                }        
            }
            if (selektovanElement == "osoba")
            {
                if (ListePodataka.ListaOsoba.Count > 0 && dataGrid.SelectedIndex != -1)
                {
                    Osoba selektovanaOsoba = (Osoba)dataGrid.SelectedItem;

                    idKorisnika = selektovanaOsoba.OsobaID;
                    TipoviKorisnika tip = selektovanaOsoba.TipKorisnika;


                    FormaUpisKorisnika formaUpisKorisnika = new FormaUpisKorisnika(tip, "izmena", idKorisnika);
                    formaUpisKorisnika.tbKorisnickoIme.Text = selektovanaOsoba.KorisnickoIme;
                    formaUpisKorisnika.tbSifraKorisnika.Text = selektovanaOsoba.Sifra;
                    formaUpisKorisnika.cbPristup.IsChecked = selektovanaOsoba.Pristup;
                    formaUpisKorisnika.cbTipKorisnika.Text = selektovanaOsoba.TipKorisnika.ToString();
                    formaUpisKorisnika.btnRegistrujSe.Content = "Izmeni";

                    if (tip == TipoviKorisnika.administrator)
                    {
                        formaUpisKorisnika.tbKorisnickoIme.IsReadOnly = true;
                        formaUpisKorisnika.tbSifraKorisnika.IsReadOnly = true;
                        formaUpisKorisnika.cbPristup.IsEnabled = false;
                        formaUpisKorisnika.cbTipKorisnika.IsEnabled = false;
                        formaUpisKorisnika.btnRegistrujSe.IsEnabled = false;
                    }

                    formaUpisKorisnika.Owner = this;    
                    formaUpisKorisnika.ShowDialog();
                    

                    dataGrid.Items.Refresh();
                }
            }

        }

        private void BrisanjeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selektovanElement == "anketa")
            {
                if (ListePodataka.ListaAnketa.Count > 0 && dataGrid.SelectedIndex != -1)
                {

                    Anketa selektovanaAnketa = (Anketa)dataGrid.SelectedItem;

                    for (int i = 0; i < ListePodataka.ListaAnketa.Count; i++)
                    {
                        if (ListePodataka.ListaAnketa.ElementAt(i).AnketaID == selektovanaAnketa.AnketaID)
                        {
                            ListePodataka.ListaAnketa.RemoveAt(i);
                        }
                    }

                    UpisUBazu.brisiRedTabele(selektovanaAnketa.AnketaID, "Anketa");

                    dataGrid.Items.Refresh();

                }

            }

            if (selektovanElement == "osoba")
            {
                if (ListePodataka.ListaOsoba.Count > 0 && dataGrid.SelectedIndex != -1)
                {
                    Osoba selektovanaOsoba = (Osoba)dataGrid.SelectedItem;

                    for (int i = 0; i < ListePodataka.ListaOsoba.Count; i++)
                    {
                        if (ListePodataka.ListaOsoba.ElementAt(i).OsobaID == selektovanaOsoba.OsobaID)
                        {
                            ListePodataka.ListaOsoba.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }
}
