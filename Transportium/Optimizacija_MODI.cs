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
                UpraviteljPostupka.DodajPostupak("Rijesenje je degenerirano. Potrebno je riješiti degeneraciju.");
                Degeneracija degeneracija = new Degeneracija();
                degeneracija.RijesiDegeneraciju();
            }
            IzracunajDualneVarijable();
            IspisiDualneVarijable();
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

        private void IspisiDualneVarijable()
        {
            UpraviteljPostupka.DodajPostupak("Dualne varijable izvorišta: ");
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                UpraviteljPostupka.DodajPostupak("u" + i + " = " + UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[i]);
            }
            UpraviteljPostupka.DodajPostupak("Dualne varijable odredišta: ");
            for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
            {
                UpraviteljPostupka.DodajPostupak("v" + i + " = " + UpraviteljTablice.tablicaTransporta.DualneVarijableOdredista[i]);
            }
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
                        UpraviteljPostupka.DodajPostupak("C" + i + j + "* = (" + UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[i] + 
                            " + " + UpraviteljTablice.tablicaTransporta.DualneVarijableOdredista[j] + ") - " + celija.TrosakPrijevoza + 
                            " = " + celija.RelativniTrosakPrijevoza);
                    }
                }
            }
        }

        public void IzracunajDualneVarijable()
        {
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
                }
                obradujemRed = !obradujemRed;
            } while (obradeniRedovi.Count < UpraviteljTablice.brojRedova 
            || obradeniStupci.Count < UpraviteljTablice.brojStupaca);
        }
    }
}
