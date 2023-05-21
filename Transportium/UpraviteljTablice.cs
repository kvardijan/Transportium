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

        public static void UcitajPodatke(List<int> kapacitetiIzvora, List<int> potrebeOdredista, List<List<int>> troskoviTransporta)
        {
            UcitajKapaciteteIzvora(kapacitetiIzvora);
            UcitajPotrebeOdredista(potrebeOdredista);
            UcitajTroskoveTransporta(troskoviTransporta);
        }

        private static void UcitajTroskoveTransporta(List<List<int>> troskoviTransporta)
        {
            for (int i = 1; i <= troskoviTransporta.Count; i++)
            {
                for (int j = 1; j <= troskoviTransporta[i].Count; j++)
                {
                    tablicaTransporta.TablicaCelija[i][j].TrosakPrijevoza = troskoviTransporta[i][j];
                }
            }
        }

        private static void UcitajPotrebeOdredista(List<int> potrebeOdredista)
        {
            for (int i = 1; i <= potrebeOdredista.Count; i++)
            {
                tablicaTransporta.PotrebeOdredista[i] = potrebeOdredista[1];
            }
        }

        private static void UcitajKapaciteteIzvora(List<int> kapacitetiIzvora)
        {
            for (int i = 1; i <= kapacitetiIzvora.Count; i++)
            {
                tablicaTransporta.KapacitetiIzvora[i] = kapacitetiIzvora[i];
            }
        }
    }
}
