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
            //DodajOznakeRedova(brojRedova);
            //DodajTextBoxeve(brojRedova, brojStupaca);
        }

        private void DodajTextBoxeve(int brojRedova, int brojStupaca)
        {
            throw new NotImplementedException();
        }

        private void DodajOznakeRedova(int brojRedova)
        {
            throw new NotImplementedException();
        }

        private void DodajOznakeStupaca(int brojStupaca)
        {

            for (int i = 1; i <= brojStupaca + 1; i++)
            {
                Label oznaka = new Label();
                oznaka.Content = "O" + i.ToString();
                gridTablicaProblema.Children.Add(oznaka);
                Grid.SetRow(oznaka, 0);
                Grid.SetColumn(oznaka, i);
            }
        }

        private void btnGenerirajTablicu_Click(object sender, RoutedEventArgs e)
        {
            int nRedova = Int32.Parse(txtBrojRedova.Text);
            int nStupaca = Int32.Parse(txtBrojStupaca.Text);
            GenerirajTablicu(nRedova,nStupaca);
        }
    }
}
