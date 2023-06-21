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
                Degeneracija degeneracija = new Degeneracija();
                degeneracija.RijesiDegeneraciju();
            }
            //izracunaj relativne troskove
            IzracunajRelativneTroskove();
            //provjeri optimalnost => ako je optimalno, zavrsi
            if (ProvjeriOptimalnostRijesenja())
            {
                optimalnoRijesenje = true;
                return;
            }
            //odredi put za pretovar, odredi kolicinu pretovara pretovari
            PretovariTeret();
            //clear relativne troskove
            OcistiRelativneTroskove();
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
                        UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].RelativniTrosakPrijevoza = IzracunajRelativniTrosak(zatvoreniPut);
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
