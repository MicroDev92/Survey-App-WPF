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
    /// Interaction logic for PrikazResenihAnketa.xaml
    /// </summary>
    public partial class PrikazResenihAnketa : Window
    {
        Osoba osoba = null;
        public PrikazResenihAnketa(Osoba osoba)
        {
            InitializeComponent();

            this.osoba = osoba;

            foreach (ResenaAnketa resAnketa in ListePodataka.ListaResenihAnketa)
            {
                if (resAnketa.osoba.OsobaID == osoba.OsobaID)
                {
                    cbReseneAnketeOsobe.Items.Add(resAnketa.anketa.AnketaID);

                    lblNazivReseneAnkete.Content = "Naslov ankete: " + resAnketa.anketa.NaslovAnkete;
                }
                cbReseneAnketeOsobe.SelectedIndex = 0;
            }

            //dodati u combo box sifre svih anketa koje je osoba resavala


        }

        private void cbReseneAnketeOsobe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int resAnkId = (int)cbReseneAnketeOsobe.SelectedItem;
            foreach (ResenaAnketa resAnketa in ListePodataka.ListaResenihAnketa)
            {
                if (resAnkId == resAnketa.anketa.AnketaID)
                {
                    tbPrikazOdgovora.Text = resAnketa.PrikazPodataka();
                }
            }
        }

        //na odabir opcije combo boxa u textboxu prikazati podatke o selektovanoj resenoj anketi pozivom metode PrikazPodataka()
    }
}
