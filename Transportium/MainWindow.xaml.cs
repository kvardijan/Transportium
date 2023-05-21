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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerirajTablicu(int brojRedova, int brojStupaca)
        {
            DodajOznakeStupaca(brojStupaca);
            DodajOznakeRedova(brojRedova);
            DodajTextBoxeve(brojRedova, brojStupaca);
        }

        private void DodajTextBoxeve(int brojRedova, int brojStupaca)
        {
            for (int i = 1; i <= brojRedova + 1; i++)
            {
                for (int j = 1; j <= brojStupaca + 1; j++)
                {
                    if(i != brojRedova + 1 || j != brojStupaca + 1)
                    {
                        Grid celija = new Grid();
                        ContentControl container = new ContentControl();
                        TextBox textBox = new TextBox();

                        textBox.Name = "C" + i.ToString() + j.ToString();
                        textBox.Margin = new Thickness(3);

                        container.Content = textBox;
                        celija.Children.Add(container);
                        gridTablicaProblema.Children.Add(celija);
                        Grid.SetColumn(celija, j);
                        Grid.SetRow(celija, i);

                        txtBoxeviCelija.Add(textBox);
                    }
                }
            }
        }

        private void DodajOznakeRedova(int brojRedova)
        {
            for (int i = 1; i <= brojRedova + 1; i++)
            {
                Grid celija = new Grid();
                ContentControl container = new ContentControl();
                Label oznaka = new Label();

                if (i != brojRedova + 1)
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

        private void DodajOznakeStupaca(int brojStupaca)
        {

            for (int i = 1; i <= brojStupaca + 1; i++)
            {
                Grid celija = new Grid();
                ContentControl container = new ContentControl();
                Label oznaka = new Label();
                if (i != brojStupaca + 1)
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
                int nRedova = Int32.Parse(txtBrojRedova.Text);
                int nStupaca = Int32.Parse(txtBrojStupaca.Text);
                GenerirajTablicu(nRedova, nStupaca);
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
            int nRedova = Int32.Parse(txtBrojRedova.Text);
            int nStupaca = Int32.Parse(txtBrojStupaca.Text);
            if (ProvjeriVrijednostiTabliceTransporta())
            {
                PopuniKapaciteteIzvora();
                PopuniPotrebeOdredista();
                PopuniTroskoveTransporta();

                UpraviteljTablice.DefinirajRedoveIStupce(nRedova, nStupaca);
                UpraviteljTablice.UcitajPodatke(kapacitetiIzvora, potrebeOdredista, troskoviTransporta);
                MessageBox.Show("uspe");
            }
            else
            {
                MessageBox.Show("Provjerite unesene vrijednosti u tablici.", "Neispravni podaci tablice!");
            }
        }

        private void PopuniPotrebeOdredista()
        {
            int nRedova = Int32.Parse(txtBrojRedova.Text);
            int nStupaca = Int32.Parse(txtBrojStupaca.Text);

            for (int i = 1; i <= nStupaca; i++)
            {
                TextBox celija = txtBoxeviCelija.Find(x => x.Name == "C" + (nRedova+1).ToString() + i);
                potrebeOdredista[i] = Int32.Parse(celija.Text);
            }
        }

        private void PopuniKapaciteteIzvora()
        {
            int nRedova = Int32.Parse(txtBrojRedova.Text);
            int nStupaca = Int32.Parse(txtBrojStupaca.Text);

            for (int i = 1; i <= nRedova; i++)
            {
                TextBox celija = txtBoxeviCelija.Find(x => x.Name == "C" + i + (nStupaca+1).ToString());
                kapacitetiIzvora[i] = Int32.Parse(celija.Text);
            }
        } 

        private void PopuniTroskoveTransporta()
        {
            int nRedova = Int32.Parse(txtBrojRedova.Text);
            int nStupaca = Int32.Parse(txtBrojStupaca.Text);
            InicijalizirajArrayTroskovaTransporta();
            for (int i = 1; i <= nRedova; i++)
            {
                for (int j = 1; j <= nStupaca; j++)
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
    }
}
