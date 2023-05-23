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
            DodajSlobodneRedoveStupce();
            sumaKolicine = 0;
            int kolicina;
            PoljeNajmanjegTroska poljeNajmanjegTroska;

            do
            {
                poljeNajmanjegTroska = OdrediPoljeNajmanjegTroska();
                //vidi je li kolicina stupca ili reda manja i stavi tolko
                //oduzmi od kolicine
                //makni red ili stupac iz liste slobodnih

            } while (sumaKolicine < UpraviteljTablice.tablicaTransporta.SumaKolicine);

            return IzracunajPocetniZ();
        }

        private PoljeNajmanjegTroska OdrediPoljeNajmanjegTroska()
        {
            PoljeNajmanjegTroska poljeNajmanjegTroska = new PoljeNajmanjegTroska
            {
                trosak = int.MaxValue,
                maxTeret = int.MinValue
            };
            int brojacR = 1;

            for (int i = slobodniRedovi.First(); brojacR <= slobodniRedovi.Count; i = slobodniRedovi[brojacR])
            {
                int brojacS = 1;
                for (int j = slobodniStupci.First(); brojacS <= slobodniStupci.Count; j = slobodniStupci[brojacS])
                {
                    Celija celija = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j];
                    if (celija.TrosakPrijevoza < poljeNajmanjegTroska.trosak)
                    {
                        poljeNajmanjegTroska.trosak = celija.TrosakPrijevoza;
                        poljeNajmanjegTroska.indexReda = i;
                        poljeNajmanjegTroska.indexStupca = j;
                        poljeNajmanjegTroska.maxTeret = IzracunajKolicinuTeretaZaStaviti(i, j);
                    }
                    else if (celija.TrosakPrijevoza == poljeNajmanjegTroska.trosak)
                    {
                        int maxTeretCelije = IzracunajKolicinuTeretaZaStaviti(i, j);
                        if (maxTeretCelije > poljeNajmanjegTroska.maxTeret)
                        {
                            poljeNajmanjegTroska.trosak = celija.TrosakPrijevoza;
                            poljeNajmanjegTroska.indexReda = i;
                            poljeNajmanjegTroska.indexStupca = j;
                            poljeNajmanjegTroska.maxTeret = maxTeretCelije;
                        }
                    }
                    brojacS++;
                }
                brojacR++;
            }

            return poljeNajmanjegTroska;
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

        private struct PoljeNajmanjegTroska
        {
            public int trosak;
            public int maxTeret;
            public int indexReda;
            public int indexStupca;
        }
    }
}
