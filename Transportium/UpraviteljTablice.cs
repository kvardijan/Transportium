using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public static class UpraviteljTablice
    {
        static TablicaTransporta tablicaTransporta = new TablicaTransporta();
        private static int brojRedova;
        private static int brojStupaca;
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
            for (int i = 0; i < tablicaTransporta.TablicaCelija.Length; i++)
            {
                for (int j = 0; j < tablicaTransporta.TablicaCelija[i].Length; j++)
                {
                    tablicaTransporta.TablicaCelija[i][j] = new Celija();
                }
            }
        }
    }
}
