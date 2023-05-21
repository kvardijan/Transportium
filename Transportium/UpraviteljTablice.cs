using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public static class UpraviteljTablice
    {
        static TablicaTransporta tablicaTransporta;

        public static void UcitajPodatke(List<int> kapacitetiIzvora, List<int> potrebeOdredista, List<List<int>> troskoviTransporta)
        {
            UcitajKapaciteteIzvora(kapacitetiIzvora);
            UcitajPotrebeOdredista(potrebeOdredista);
            UcitajTroskoveTransporta(troskoviTransporta);
        }

        private static void UcitajTroskoveTransporta(List<List<int>> troskoviTransporta)
        {
            throw new NotImplementedException();
        }

        private static void UcitajPotrebeOdredista(List<int> potrebeOdredista)
        {
            throw new NotImplementedException();
        }

        private static void UcitajKapaciteteIzvora(List<int> kapacitetiIzvora)
        {
            throw new NotImplementedException();
        }
    }
}
