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

namespace Transportium
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TextBox> txtBoxeviCelija = new List<TextBox>();
        private int _brojRedova;
        private int _brojStupaca;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerirajTablicu()
        {
            DodajOznakeStupaca();
            DodajOznakeRedova();
            DodajTextBoxeve();
        }

        private void DodajTextBoxeve()
        {
            int tabIndex = 1;
            for (int i = 1; i <= _brojRedova + 1; i++)
            {
                for (int j = 1; j <= _brojStupaca + 1; j++)
                {
                    if(i != _brojRedova + 1 || j != _brojStupaca + 1)
                    {
                        Grid celija = new Grid();
                        ContentControl container = new ContentControl();
                        TextBox textBox = new TextBox();

                        textBox.Name = "C" + i.ToString() + j.ToString();
                        textBox.Margin = new Thickness(3);
                        KeyboardNavigation.SetTabIndex(textBox, tabIndex);

                        container.Content = textBox;
                        celija.Children.Add(container);
                        gridTablicaProblema.Children.Add(celija);
                        Grid.SetColumn(celija, j);
                        Grid.SetRow(celija, i);

                        txtBoxeviCelija.Add(textBox);
                        tabIndex++;
                    }
                }
            }
        }

        private void DodajOznakeRedova()
        {
            for (int i = 1; i <= _brojRedova + 1; i++)
            {
                Grid celija = new Grid();
                ContentControl container = new ContentControl();
                Label oznaka = new Label();

                if (i != _brojRedova + 1)
                {
                    oznaka.Content = "I" + i.ToString();
                }
                else
                {
                    oznaka.Content = "bj";
                }

                oznaka = OblikujLabel(oznaka);

                container.Content = oznaka;
                celija.Children.Add(container);
                gridTablicaProblema.Children.Add(celija);
                Grid.SetColumn(celija, 0);
                Grid.SetRow(celija, i);
            }
        }

        private void DodajOznakeStupaca()
        {

            for (int i = 1; i <= _brojStupaca + 1; i++)
            {
                Grid celija = new Grid();
                ContentControl container = new ContentControl();
                Label oznaka = new Label();
                if (i != _brojStupaca + 1)
                {
                    oznaka.Content = "O" + i.ToString();
                }
                else
                {
                    oznaka.Content = "ai";
                }
                oznaka = OblikujLabel(oznaka);
                container.Content = oznaka;
                celija.Children.Add(container);
                gridTablicaProblema.Children.Add(celija);
                Grid.SetRow(celija, 0);
                Grid.SetColumn(celija, i);
            }
        }

        private Label OblikujLabel(Label oznaka)
        {
            oznaka.FontSize = 15;
            oznaka.FontWeight = FontWeights.Bold;
            oznaka.VerticalContentAlignment = VerticalAlignment.Center;
            oznaka.HorizontalContentAlignment = HorizontalAlignment.Center;
            return oznaka;
        }

        private void OcistiGrid()
        {
            gridTablicaProblema.Children.Clear();
            txtBoxeviCelija.Clear();
        }

        private void btnGenerirajTablicu_Click(object sender, RoutedEventArgs e)
        {
            if (ProvjeriUnosRedovaStupaca())
            {
                OcistiGrid();
                OcistiVarijable();
                _brojRedova = Int32.Parse(txtBrojRedova.Text);
                _brojStupaca = Int32.Parse(txtBrojStupaca.Text);
                GenerirajTablicu();
                cmbMetodaPocetnogRasporeda.IsEnabled = true;
                btnPocetniRaspored.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Broj redova i broj stupaca moraju biti cjelobrojne vrijednosti od 2 do 10.", "Provjerite podatke!");
            }
        }

        private void OcistiVarijable()
        {
            Array.Clear(potrebeOdredista, 0, potrebeOdredista.Length);
            Array.Clear(kapacitetiIzvora, 0, kapacitetiIzvora.Length);
            for (int i = 0; i < troskoviTransporta.Length; i++)
            {
                troskoviTransporta[i] = new int[11];
            }
        }

        private bool ProvjeriUnosRedovaStupaca()
        {
            bool ispravno = false;
            int nRedova;
            int nStupaca;

            if (int.TryParse(txtBrojRedova.Text, out nRedova) && int.TryParse(txtBrojStupaca.Text, out nStupaca)
                && nRedova > 1 && nRedova <= 10 && nStupaca > 1 && nStupaca <= 10)
            {
                ispravno = true;
            }

            return ispravno;
        }

        int[] potrebeOdredista = new int[11];
        int[] kapacitetiIzvora = new int[11];
        int[][] troskoviTransporta = new int[11][];
        private void btnPocetniRaspored_Click(object sender, RoutedEventArgs e)
        {
            if (UpraviteljTablice.ucitanaTablica)
            {
                UpraviteljTablice.OcistiTablicu();
                MakniIzracunatiTeret();
            }

            if (ProvjeriVrijednostiTabliceTransporta())
            {
                PopuniKapaciteteIzvora();
                PopuniPotrebeOdredista();
                PopuniTroskoveTransporta();
                UpraviteljTablice.DefinirajRedoveIStupce(_brojRedova, _brojStupaca);
                UpraviteljTablice.UcitajPodatke(kapacitetiIzvora, potrebeOdredista, troskoviTransporta);

                if (UpraviteljTablice.ProvjeriKapaciteteIPotrebe())
                {
                    var metodaPocetnogRasporeda = (cmbMetodaPocetnogRasporeda.SelectedItem as ComboBoxItem).Content.ToString();
                    if (metodaPocetnogRasporeda == "Sjeverozapadni kut")
                    {
                        lblRjesenje.Content = UpraviteljTablice.Rasporedi_SZKut();
                        IspisiRezultatRasporedivanja();
                    }
                    if (metodaPocetnogRasporeda == "Minimalni troškovi")
                    {
                        lblRjesenje.Content = UpraviteljTablice.Rasporedi_MinCost();
                        IspisiRezultatRasporedivanja();
                    }
                    if (metodaPocetnogRasporeda == "Vogel aproksimacija")
                    {
                        lblRjesenje.Content = UpraviteljTablice.Rasporedi_Vogel();
                        IspisiRezultatRasporedivanja();
                    }
                    btnSljedecaIteracija.IsEnabled = true;
                    cmbMetodaOptimizacije.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Provjerite unesene vrijednosti kapaciteta izvora i potreba odredista.", "Transportni problem nije zatvoren!");
                }
            }
            else
            {
                MessageBox.Show("Provjerite unesene vrijednosti u tablici.", "Neispravni podaci tablice!");
            }
        }

        private void MakniIzracunatiTeret()
        {
            for (int i = 1; i <= _brojRedova; i++)
            {
                for (int j = 1; j <= _brojStupaca; j++)
                {
                    TextBox celija = txtBoxeviCelija.Find(x => x.Name == "C" + i + j);
                    celija.Text = celija.Text.Split('/')[0];
                }
            }
        }

        private void IspisiRezultatRasporedivanja()
        {
            for (int i = 1; i <= _brojRedova; i++)
            {
                for (int j = 1; j <= _brojStupaca; j++)
                {
                    int teret = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].KolicinaTereta;
                    TextBox celija = txtBoxeviCelija.Find(x => x.Name == "C" + i + j);
                    celija.Text += "/";
                    if (teret != 0) celija.Text += teret.ToString();
                }
            }
        }

        private void PopuniPotrebeOdredista()
        {
            for (int i = 1; i <= _brojStupaca; i++)
            {
                TextBox celija = txtBoxeviCelija.Find(x => x.Name == "C" + (_brojRedova+1).ToString() + i);
                potrebeOdredista[i] = Int32.Parse(celija.Text);
            }
        }

        private void PopuniKapaciteteIzvora()
        {
            for (int i = 1; i <= _brojRedova; i++)
            {
                TextBox celija = txtBoxeviCelija.Find(x => x.Name == "C" + i + (_brojStupaca+1).ToString());
                kapacitetiIzvora[i] = Int32.Parse(celija.Text);
            }
        } 

        private void PopuniTroskoveTransporta()
        {
            InicijalizirajArrayTroskovaTransporta();
            for (int i = 1; i <= _brojRedova; i++)
            {
                for (int j = 1; j <= _brojStupaca; j++)
                {
                    TextBox celija = txtBoxeviCelija.Find(x => x.Name == "C" + i + j);
                    troskoviTransporta[i][j] = Int32.Parse(celija.Text);
                }
            }
        }

        private void InicijalizirajArrayTroskovaTransporta()
        {
            for (int i = 0; i < troskoviTransporta.Length; i++)
            {
                troskoviTransporta[i] = new int[11];
            }
        }

        private bool ProvjeriVrijednostiTabliceTransporta()
        {
            bool ispravno = true;

            foreach (var celija in txtBoxeviCelija)
            {
                int trosak;
                if (!Int32.TryParse(celija.Text, out trosak) || trosak <= 0)
                {
                    ispravno = false;
                }
            }

            return ispravno;
        }

        private void btnSljedecaIteracija_Click(object sender, RoutedEventArgs e)
        {
            if (ProvjeriRangSustava())
            {
                var metodaOptimizacije = (cmbMetodaOptimizacije.SelectedItem as ComboBoxItem).Content.ToString();
                if (metodaOptimizacije == "Stepping Stone metoda")
                {
                    lblRjesenje.Content = UpraviteljTablice.SteppingStoneIducaIteracija();
                    if ((string)lblRjesenje.Content == UpraviteljTablice.SteppingStoneIducaIteracija())
                    {
                        MessageBox.Show("Postignuto je optimalno rjesenje.");
                        cmbMetodaOptimizacije.IsEnabled = false;
                        btnSljedecaIteracija.IsEnabled = false;
                    }
                }
                if (metodaOptimizacije == "MODI metoda")
                {

                }
            }
            else
            {
                MessageBox.Show("Rjesavanje degeneracije nije jos implementirano.", "Degeneracija!");
            }
        }

        private bool ProvjeriRangSustava()
        {
            int brojZauzetihCelija = 0;
            for (int i = 1; i <= _brojRedova; i++)
            {
                for (int j = 1; j <= _brojStupaca; j++)
                {
                    if(UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].Zauzeto) brojZauzetihCelija++;
                }
            }
            return brojZauzetihCelija == _brojRedova + _brojStupaca - 1;
        }
    }
}
