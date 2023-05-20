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
        }




        private void btnGenerirajTablicu_Click(object sender, RoutedEventArgs e)
        {
            OcistiGrid();
            int nRedova = Int32.Parse(txtBrojRedova.Text);
            int nStupaca = Int32.Parse(txtBrojStupaca.Text);
            GenerirajTablicu(nRedova,nStupaca);
        }
    }
}
