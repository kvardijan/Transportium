﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class PocetniRaspored_SjeveroZapadniKut
    {
        int[] kolicineIzvora = new int[11];
        int[] kolicineOdredista = new int[11];
        int sumaKolicine;
        public string RjesiRasporedivanje()
        {
            ResetirajKolicine();
            DodajKolicine();
            sumaKolicine = 0;
            int indexRed = 1;
            int indexStupac = 1;
            int kolicina;

            do
            {
                kolicina = 0;
                if (kolicineIzvora[indexRed] > kolicineOdredista[indexStupac])
                {
                    kolicina = kolicineOdredista[indexStupac];
                }
                else
                {
                    kolicina = kolicineIzvora[indexRed];
                }
                UpraviteljTablice.tablicaTransporta.TablicaCelija[indexRed][indexStupac].KolicinaTereta = kolicina;
                UpraviteljTablice.tablicaTransporta.TablicaCelija[indexRed][indexStupac].Zauzeto = true;

                sumaKolicine += kolicina;
                kolicineIzvora[indexRed] -= kolicina;
                kolicineOdredista[indexStupac] -= kolicina;

                if (kolicineIzvora[indexRed] == 0 && indexRed < UpraviteljTablice.brojRedova) indexRed++;
                if (kolicineOdredista[indexStupac] == 0 && indexStupac < UpraviteljTablice.brojStupaca) indexStupac++;
            } while (sumaKolicine < UpraviteljTablice.tablicaTransporta.SumaKolicine);

            return IzracunajPocetniZ();
        }

        private string IzracunajPocetniZ()
        {
            string pocetniZ = "Z = ";
            bool prvi = true;
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                for (int j = 1; j <= UpraviteljTablice.brojStupaca; j++)
                {
                    Celija celija = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j];
                    if (celija.Zauzeto)
                    {
                        if (!prvi)
                        {
                            pocetniZ += "+ " + celija.TrosakPrijevoza + "*" + celija.KolicinaTereta + " ";
                        }
                        else
                        {
                            pocetniZ += celija.TrosakPrijevoza + "*" + celija.KolicinaTereta + " ";
                            prvi = false;
                        }
                        UpraviteljTablice.tablicaTransporta.Z += celija.TrosakPrijevoza * celija.KolicinaTereta;
                    }
                }
            }
            pocetniZ += "= " + UpraviteljTablice.tablicaTransporta.Z;
            return pocetniZ;
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

        private void ResetirajKolicine()
        {
            Array.Clear(kolicineIzvora, 0, kolicineIzvora.Length);
            Array.Clear(kolicineOdredista, 0, kolicineOdredista.Length);
        }
    }
}
