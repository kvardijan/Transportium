using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class Optimizacija_MODI : Optimizacija
    {
        public void IzvediIteraciju()
        {
            if (!UpraviteljTablice.ProvjeriRangSustava())
            {
                Degeneracija degeneracija = new Degeneracija();
                degeneracija.RijesiDegeneraciju();
            }
            //izracunaj dualne varijable (pozovi funkciju)
            IzracunajDualneVarijable();
            //Izracunaj relativne troskove
            IzracunajRelativneTroskove();
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
                    Celija celija = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j];
                    if (!celija.Zauzeto)
                    {
                        celija.RelativniTrosakPrijevoza = UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[i] + 
                            UpraviteljTablice.tablicaTransporta.DualneVarijableOdredista[j] - celija.TrosakPrijevoza;
                    }
                }
            }
        }

        public void IzracunajDualneVarijable()
        {
            //int indexTrenutnogReda = 1;
            //int indexTrenutnogStupca = 1;
            UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[1] = 0;
            List<int> redoviZaProlaz = new List<int> { 1 };
            List<int> stupciZaProlaz = new List<int>();
            List<int> obradeniRedovi = new List<int> { 1 };
            List<int> obradeniStupci = new List<int>();
            bool obradujemRed = true;

            do
            {
                if (obradujemRed && obradeniStupci.Count < UpraviteljTablice.brojStupaca)
                {
                    foreach (var red in redoviZaProlaz)
                    {
                        for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
                        {
                            if (obradeniStupci.Contains(i)) continue;
                            Celija trenutnaCelija = UpraviteljTablice.tablicaTransporta.TablicaCelija[red][i];
                            if (trenutnaCelija.Zauzeto)
                            {
                                UpraviteljTablice.tablicaTransporta.DualneVarijableOdredista[i] =
                                    trenutnaCelija.TrosakPrijevoza - UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[red];
                                obradeniStupci.Add(i);
                                stupciZaProlaz.Add(i);
                            }
                        }
                    }
                    redoviZaProlaz.Clear();
                    //if(indexTrenutnogReda < UpraviteljTablice.brojRedova) indexTrenutnogReda++;
                }
                if(!obradujemRed && obradeniRedovi.Count < UpraviteljTablice.brojRedova)
                {
                    foreach (var stupac in stupciZaProlaz)
                    {
                        for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
                        {
                            if (obradeniRedovi.Contains(i)) continue;
                            Celija trenutnaCelija = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][stupac];
                            if (trenutnaCelija.Zauzeto)
                            {
                                UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[i] =
                                    trenutnaCelija.TrosakPrijevoza - UpraviteljTablice.tablicaTransporta.DualneVarijableOdredista[stupac];
                                obradeniRedovi.Add(i);
                                redoviZaProlaz.Add(i);
                            }
                        }
                    }
                    stupciZaProlaz.Clear();
                    //if (indexTrenutnogStupca < UpraviteljTablice.brojStupaca) indexTrenutnogStupca++;
                }
                obradujemRed = !obradujemRed;
            } while (obradeniRedovi.Count < UpraviteljTablice.brojRedova 
            || obradeniStupci.Count < UpraviteljTablice.brojStupaca);
        }
    }
}
