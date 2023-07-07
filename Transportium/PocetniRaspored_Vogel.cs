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
                    StaviKolicinuNaRelaciju(odabranoPolje.indexReda, odabranoPolje.indexStupca);
                    if (kolicineIzvora[odabranoPolje.indexReda] == 0) slobodniRedovi.Remove(odabranoPolje.indexReda);
                    if (kolicineOdredista[odabranoPolje.indexStupca] == 0) slobodniStupci.Remove(odabranoPolje.indexStupca);
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
            slobodniRedovi.Clear();
            slobodniStupci.Clear();
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
            UpraviteljPostupka.DodajPostupak("Odabrana je relacija (" + indexReda + ", " + indexStupca + ") i stavljena je količina od " + kolicina);
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
            odabranoPolje = ProdiKrozStupce(odabranoPolje);

            return odabranoPolje;
        }

        private OdabranoPolje ProdiKrozStupce(OdabranoPolje odabranoPolje)
        {
            int brojacS = 1;
            for (int i = slobodniStupci.First(); brojacS <= slobodniStupci.Count;)
            {
                int brojacR = 1;
                List<int> troskovi = new List<int>();
                int indexOdabranogReda = 0;
                int najmanjiTrosakStupca = int.MaxValue;
                int teretOdabranogReda = int.MinValue;

                for (int j = slobodniRedovi.First(); brojacR <= slobodniRedovi.Count;)
                {
                    int trosakPrijevoza = UpraviteljTablice.tablicaTransporta.TablicaCelija[j][i].TrosakPrijevoza;
                    troskovi.Add(trosakPrijevoza);
                    if (trosakPrijevoza < najmanjiTrosakStupca)
                    {
                        najmanjiTrosakStupca = trosakPrijevoza;
                        teretOdabranogReda = IzracunajKolicinuTeretaZaStaviti(j, i);
                        indexOdabranogReda = j;
                    }
                    else if (trosakPrijevoza == najmanjiTrosakStupca
                        && IzracunajKolicinuTeretaZaStaviti(j, i) > teretOdabranogReda)
                    {
                        teretOdabranogReda = IzracunajKolicinuTeretaZaStaviti(j, i);
                        indexOdabranogReda = j;
                    }
                    brojacR++;
                    if (brojacR <= slobodniRedovi.Count) j = slobodniRedovi[brojacR - 1];
                }

                troskovi.Sort();
                int rIndex = troskovi[1] - troskovi[0];
                UpraviteljPostupka.DodajPostupak("Indeks za stupac " + brojacS + " je: " + rIndex);
                if (rIndex > odabranoPolje.vogelIndex)
                {
                    odabranoPolje = AzurirajOdabranoPolje(odabranoPolje, rIndex, IzracunajKolicinuTeretaZaStaviti(indexOdabranogReda, i),
                        indexOdabranogReda, i, najmanjiTrosakStupca);
                }
                else if (rIndex == odabranoPolje.vogelIndex)
                {
                    if (najmanjiTrosakStupca < odabranoPolje.trosak)
                    {
                        odabranoPolje = AzurirajOdabranoPolje(odabranoPolje, rIndex, IzracunajKolicinuTeretaZaStaviti(indexOdabranogReda, i),
                        indexOdabranogReda, i, najmanjiTrosakStupca);
                    }
                    else if (najmanjiTrosakStupca == odabranoPolje.trosak && teretOdabranogReda > odabranoPolje.maxTeret)
                    {
                        odabranoPolje = AzurirajOdabranoPolje(odabranoPolje, rIndex, IzracunajKolicinuTeretaZaStaviti(indexOdabranogReda, i),
                        indexOdabranogReda, i, najmanjiTrosakStupca);
                    }
                }

                brojacS++;
                if (brojacS <= slobodniStupci.Count) i = slobodniStupci[brojacS - 1];
            }
            return odabranoPolje;
        }

        private OdabranoPolje AzurirajOdabranoPolje(OdabranoPolje odabranoPolje, int rIndex, int teret, int red, int stupac, int trosak)
        {
            odabranoPolje.vogelIndex = rIndex;
            odabranoPolje.maxTeret = teret;
            odabranoPolje.indexReda = red;
            odabranoPolje.indexStupca = stupac;
            odabranoPolje.trosak = trosak;
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
                int najmanjiTrosakReda = int.MaxValue;
                int teretOdabranogStupca = int.MinValue;

                for (int j = slobodniStupci.First(); brojacS <= slobodniStupci.Count;)
                {
                    int trosakPrijevoza = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].TrosakPrijevoza;
                    troskovi.Add(trosakPrijevoza);
                    if (trosakPrijevoza < najmanjiTrosakReda)
                    {
                        najmanjiTrosakReda = trosakPrijevoza;
                        teretOdabranogStupca = IzracunajKolicinuTeretaZaStaviti(i, j);
                        indexOdabranogStupca = j;
                    }
                    else if (trosakPrijevoza == najmanjiTrosakReda
                        && IzracunajKolicinuTeretaZaStaviti(i, j) > odabranoPolje.maxTeret)
                    {
                        teretOdabranogStupca = IzracunajKolicinuTeretaZaStaviti(i, j);
                        indexOdabranogStupca = j;
                    }

                    brojacS++;
                    if (brojacS <= slobodniStupci.Count) j = slobodniStupci[brojacS - 1];
                }

                troskovi.Sort();
                int rIndex = troskovi[1] - troskovi[0];
                UpraviteljPostupka.DodajPostupak("Indeks za red " + brojacR + " je: " + rIndex);
                if (rIndex > odabranoPolje.vogelIndex)
                {
                    odabranoPolje = AzurirajOdabranoPolje(odabranoPolje, rIndex, IzracunajKolicinuTeretaZaStaviti(i, indexOdabranogStupca),
                        i, indexOdabranogStupca, najmanjiTrosakReda);
                }
                else if (rIndex == odabranoPolje.vogelIndex)
                {
                    if (najmanjiTrosakReda < odabranoPolje.trosak)
                    {
                        odabranoPolje = AzurirajOdabranoPolje(odabranoPolje, rIndex, IzracunajKolicinuTeretaZaStaviti(i, indexOdabranogStupca),
                        i, indexOdabranogStupca, najmanjiTrosakReda);
                    }
                    else if (najmanjiTrosakReda == odabranoPolje.trosak && teretOdabranogStupca > odabranoPolje.maxTeret)
                    {
                        odabranoPolje = AzurirajOdabranoPolje(odabranoPolje, rIndex, IzracunajKolicinuTeretaZaStaviti(i, indexOdabranogStupca),
                        i, indexOdabranogStupca, najmanjiTrosakReda);
                    }
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
            public int trosak;
        }
    }
}
