using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class PocetniRaspored_MinimalniTroskovi
    {
        int[] kolicineIzvora = new int[11];
        int[] kolicineOdredista = new int[11];
        int sumaKolicine;
        List<int> slobodniRedovi = new List<int>();
        List<int> slobodniStupci = new List<int>();

        public string RjesiRasporedivanje()
        {
            //ResetirajKolicine();
            DodajKolicine();
            sumaKolicine = 0;
            int kolicina;
            

            do
            {

            } while (sumaKolicine < UpraviteljTablice.tablicaTransporta.SumaKolicine);

            return IzracunajPocetniZ();
        }

        private void DodajKolicine()
        {
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                kolicineIzvora[i] = UpraviteljTablice.tablicaTransporta.KapacitetiIzvora[i];
            }
            for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
            {
                kolicineOdredista[i] = UpraviteljTablice.tablicaTransporta.PotrebeOdredista[i];
            }
        }

/*        private void ResetirajKolicine()
        {
            Array.Clear(kolicineIzvora, 0, kolicineIzvora.Length);
            Array.Clear(kolicineOdredista, 0, kolicineOdredista.Length);
        }*/

        private string IzracunajPocetniZ()
        {
            string pocetniZ = "Z = ";

            return pocetniZ;
        }
    }
}
