using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public static class UpraviteljTablice
    {
        public static TablicaTransporta tablicaTransporta = new TablicaTransporta();
        public static int brojRedova;
        public static int brojStupaca;
        public static bool ucitanaTablica = false;

        public static void UcitajPodatke(int[] kapacitetiIzvora, int[] potrebeOdredista, int[][] troskoviTransporta)
        {
            UcitajKapaciteteIzvora(kapacitetiIzvora);
            UcitajPotrebeOdredista(potrebeOdredista);
            UcitajTroskoveTransporta(troskoviTransporta);
            ucitanaTablica=true;
        }

        private static void UcitajTroskoveTransporta(int[][] troskoviTransporta)
        {
            for (int i = 1; i <= brojRedova; i++)
            {
                for (int j = 1; j <= brojStupaca; j++)
                {
                    tablicaTransporta.TablicaCelija[i][j].TrosakPrijevoza = troskoviTransporta[i][j];
                }
            }
        }

        private static void UcitajPotrebeOdredista(int[] potrebeOdredista)
        {
            for (int i = 1; i <= brojStupaca; i++)
            {
                tablicaTransporta.PotrebeOdredista[i] = potrebeOdredista[i];
            }
        }

        private static void UcitajKapaciteteIzvora(int[] kapacitetiIzvora)
        {
            for (int i = 1; i <= brojRedova; i++)
            {
                tablicaTransporta.KapacitetiIzvora[i] = kapacitetiIzvora[i];
            }
        }

        public static void DefinirajRedoveIStupce(int nRedova, int nStupaca)
        {
            brojRedova = nRedova;
            brojStupaca = nStupaca;
        }

        public static void OcistiTablicu()
        {
            Array.Clear(tablicaTransporta.PotrebeOdredista, 0, tablicaTransporta.PotrebeOdredista.Length);
            Array.Clear(tablicaTransporta.KapacitetiIzvora, 0, tablicaTransporta.KapacitetiIzvora.Length);
            tablicaTransporta.SumaKolicine = 0;
            tablicaTransporta.Z = 0;
            for (int i = 0; i < tablicaTransporta.TablicaCelija.Length; i++)
            {
                for (int j = 0; j < tablicaTransporta.TablicaCelija[i].Length; j++)
                {
                    tablicaTransporta.TablicaCelija[i][j] = new Celija();
                }
            }
        }

        public static bool ProvjeriKapaciteteIPotrebe()
        {
            bool ispravno = true;
            int sumKapacitetiIzvora = 0;
            int sumPotrebeOdredista = 0;

            for (int i = 1; i <= brojRedova; i++)
            {
                sumKapacitetiIzvora += tablicaTransporta.KapacitetiIzvora[i];
            }
            for (int i = 1; i <= brojStupaca; i++)
            {
                sumPotrebeOdredista += tablicaTransporta.PotrebeOdredista[i];
            }
            if (sumKapacitetiIzvora != sumPotrebeOdredista) ispravno = false;
            tablicaTransporta.SumaKolicine = sumPotrebeOdredista;
            return ispravno;
        }

        private static string IzracunajZ()
        {
            string Z = "Z = ";
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
                            Z += "+ " + celija.TrosakPrijevoza + "*" + celija.KolicinaTereta + " ";
                        }
                        else
                        {
                            Z += celija.TrosakPrijevoza + "*" + celija.KolicinaTereta + " ";
                            prvi = false;
                        }
                        UpraviteljTablice.tablicaTransporta.Z += celija.TrosakPrijevoza * celija.KolicinaTereta;
                    }
                }
            }
            Z += "= " + UpraviteljTablice.tablicaTransporta.Z;
            return Z;
        }

        public static string Rasporedi_SZKut()
        {
            PocetniRaspored_SjeveroZapadniKut rasporedivac = new PocetniRaspored_SjeveroZapadniKut();
            rasporedivac.RjesiRasporedivanje();
            return IzracunajZ();
        }

        public static string Rasporedi_MinCost()
        {
            PocetniRaspored_MinimalniTroskovi rasporedivac = new PocetniRaspored_MinimalniTroskovi();
            rasporedivac.RjesiRasporedivanje();
            return IzracunajZ();
        }
    }
}
