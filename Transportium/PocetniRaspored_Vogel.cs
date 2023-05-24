using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class PocetniRaspored_Vogel
    {
        int[] kolicineIzvora = new int[11];
        int[] kolicineOdredista = new int[11];
        int sumaKolicine;
        List<int> slobodniRedovi = new List<int>();
        List<int> slobodniStupci = new List<int>();

        public void RjesiRasporedivanje()
        {
            DodajKolicine();
            DodajSlobodneRedoveStupce();
            sumaKolicine = 0;
            int kolicina;
            OdabranoPolje odabranoPolje;

            do
            {
                if (PostojiSamoJedanRedIliStupac())
                {
                    RaspodjeliPreostaliTeret();
                }
                else
                {
                    odabranoPolje = OdaberiPolje();
                }
            } while (sumaKolicine < UpraviteljTablice.tablicaTransporta.SumaKolicine);
        }

        private void RaspodjeliPreostaliTeret()
        {
            throw new NotImplementedException();
        }

        private bool PostojiSamoJedanRedIliStupac()
        {
            return slobodniRedovi.Count == 1 || slobodniStupci.Count == 1;
        }

        private OdabranoPolje OdaberiPolje()
        {
            throw new NotImplementedException();
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

        private void DodajSlobodneRedoveStupce()
        {
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                slobodniRedovi.Add(i);
            }
            for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
            {
                slobodniStupci.Add(i);
            }
        }

        struct OdabranoPolje
        {

        }
    }
}
