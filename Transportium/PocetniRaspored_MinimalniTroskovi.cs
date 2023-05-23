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

        public void RjesiRasporedivanje()
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
                int red = poljeNajmanjegTroska.indexReda;
                int stupac = poljeNajmanjegTroska.indexStupca;
                kolicina = poljeNajmanjegTroska.maxTeret;

                UpraviteljTablice.tablicaTransporta.TablicaCelija[red][stupac].KolicinaTereta = kolicina;
                UpraviteljTablice.tablicaTransporta.TablicaCelija[red][stupac].Zauzeto = true;

                kolicineIzvora[red] -= kolicina;
                kolicineOdredista[stupac] -= kolicina;
                sumaKolicine += kolicina;

                if (kolicineIzvora[red] == 0) slobodniRedovi.Remove(red);
                if (kolicineOdredista[stupac] == 0) slobodniStupci.Remove(stupac);
            } while (sumaKolicine < UpraviteljTablice.tablicaTransporta.SumaKolicine);
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

        private struct PoljeNajmanjegTroska
        {
            public int trosak;
            public int maxTeret;
            public int indexReda;
            public int indexStupca;
        }
    }
}
