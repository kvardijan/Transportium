using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class Optimizacija_SteppingStone : Optimizacija
    {
        public void IzvediIteraciju()
        {
            if (!UpraviteljTablice.ProvjeriRangSustava())
            {
                UpraviteljPostupka.DodajPostupak("Rijesenje je degenerirano. Potrebno je riješiti degeneraciju.");
                Degeneracija degeneracija = new Degeneracija();
                degeneracija.RijesiDegeneraciju();
            }
            IzracunajRelativneTroskove();
            if (ProvjeriOptimalnostRijesenja())
            {
                optimalnoRijesenje = true;
                UpraviteljPostupka.DodajPostupak("Rijesenje je optimalno!");
                return;
            }
            PretovariTeret();
            OcistiRelativneTroskove();
            UpraviteljPostupka.DodajPostupak("Kraj iteracije. --------");
        }

        private void IzracunajRelativneTroskove()
        {
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                for (int j = 1; j <= UpraviteljTablice.brojStupaca; j++)
                {
                    if (!UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].Zauzeto)
                    {
                        List<Celija> zatvoreniPut = PronadiZatvoreniPut(UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j], new List<Celija>(), true);
                        IspisiZatvoreniPut(zatvoreniPut);
                        int relTrosak = IzracunajRelativniTrosak(zatvoreniPut);
                        UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].RelativniTrosakPrijevoza = relTrosak;
                        UpraviteljPostupka.DodajPostupak("Relativni trosak prijevoza za (" + i + ", " + j + "): " + relTrosak);
                    }
                }
            }
        }

        private int IzracunajRelativniTrosak(List<Celija> zatvoreniPut)
        {
            int relativniTrosak = 0;
            bool minus = true;
            foreach (var celija in zatvoreniPut)
            {
                if (minus) relativniTrosak -= celija.TrosakPrijevoza;
                else relativniTrosak += celija.TrosakPrijevoza;
                minus = !minus;
            }
            return relativniTrosak;
        }


    }
}
