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
            if (slobodniRedovi.Count == 1)
            {
                int indexReda = slobodniRedovi.First();
                foreach (var indexStupca in slobodniStupci)
                {
                    StaviKolicinuNaRelaciju(indexReda, indexStupca);
                }
            }
            else
            {
                int indexStupca = slobodniStupci.First();
                foreach (var indexReda in slobodniRedovi)
                {
                    StaviKolicinuNaRelaciju(indexReda, indexStupca);
                }
            }
        }

        private void StaviKolicinuNaRelaciju(int indexReda, int indexStupca)
        {
            int kolicina = IzracunajKolicinuTeretaZaStaviti(indexReda, indexStupca);
            UpraviteljTablice.tablicaTransporta.TablicaCelija[indexReda][indexStupca]
                .KolicinaTereta = kolicina;
            UpraviteljTablice.tablicaTransporta.TablicaCelija[indexReda][indexStupca].Zauzeto = true;
            kolicineIzvora[indexReda] -= kolicina;
            kolicineOdredista[indexStupca] -= kolicina;
            sumaKolicine += kolicina;

            if (kolicineIzvora[indexReda] == 0) slobodniRedovi.Remove(indexReda); //ovo bude mozda problem jer removeam element iz foreach petlje gore
            if (kolicineOdredista[indexStupca] == 0) slobodniStupci.Remove(indexStupca);
        }

        private int IzracunajKolicinuTeretaZaStaviti(int indexReda, int indexStupca)
        {
            int kolicina;
            if (kolicineIzvora[indexReda] > kolicineOdredista[indexStupca])
            {
                kolicina = kolicineOdredista[indexStupca];
            }
            else
            {
                kolicina = kolicineIzvora[indexReda];
            }
            return kolicina;
        }

        private bool PostojiSamoJedanRedIliStupac()
        {
            return slobodniRedovi.Count == 1 || slobodniStupci.Count == 1;
        }

        private OdabranoPolje OdaberiPolje()
        {
            OdabranoPolje odabranoPolje = new OdabranoPolje
            {
                vogelIndex = int.MinValue
            };
            odabranoPolje = ProdiKrozRedove(odabranoPolje);

            return odabranoPolje;
        }

        private OdabranoPolje ProdiKrozRedove(OdabranoPolje odabranoPolje)
        {
            int brojacR = 1;
            for (int i = slobodniRedovi.First(); brojacR <= slobodniRedovi.Count;)
            {
                int brojacS = 1;
                List<int> troskovi = new List<int>();
                int indexOdabranogStupca = 0;
                int najmanjiTrosak = int.MaxValue;

                for (int j = slobodniStupci.First(); brojacS <= slobodniStupci.Count;)
                {
                    int trosakPrijevoza = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].TrosakPrijevoza;
                    troskovi.Add(trosakPrijevoza);
                    if (trosakPrijevoza < najmanjiTrosak)
                    {
                        indexOdabranogStupca = j;
                    }
                    else if (trosakPrijevoza == najmanjiTrosak 
                        && IzracunajKolicinuTeretaZaStaviti(i, j) > odabranoPolje.maxTeret)
                    {
                        indexOdabranogStupca = j;
                    }
                    brojacS++;
                    if (brojacS <= slobodniStupci.Count) j = slobodniStupci[brojacS - 1];
                }

                troskovi.Sort();
                int rIndex = troskovi[1] - troskovi[0];
                if (rIndex > odabranoPolje.vogelIndex)
                {
                    odabranoPolje.vogelIndex = rIndex;
                    odabranoPolje.maxTeret = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][indexOdabranogStupca].KolicinaTereta;
                    odabranoPolje.indexReda = i;
                    odabranoPolje.indexStupca = indexOdabranogStupca;
                }

                brojacR++;
                if (brojacR <= slobodniRedovi.Count) i = slobodniRedovi[brojacR - 1];
            }

            return odabranoPolje;
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
            public int vogelIndex;
            public int indexReda;
            public int indexStupca;
            public int maxTeret;
        }
    }
}
