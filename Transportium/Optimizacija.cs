using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public abstract class Optimizacija
    {
        public bool optimalnoRijesenje = false;

        protected void OcistiRelativneTroskove()
        {
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                for (int j = 1; j <= UpraviteljTablice.brojStupaca; j++)
                {
                    if (UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].RelativniTrosakPrijevoza > 0)
                        UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].RelativniTrosakPrijevoza = 0;
                }
            }
        }

        protected bool ProvjeriOptimalnostRijesenja()
        {
            bool optimalno = true;
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                for (int j = 1; j <= UpraviteljTablice.brojStupaca; j++)
                {
                    if (UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j].RelativniTrosakPrijevoza > 0)
                    {
                        optimalno = false;
                        break;
                    }
                }
            }
            return optimalno;
        }

        protected void PretovariTeret()
        {
            StozerniElement stozerniElement = new StozerniElement
            {
                relativniTrosak = Int32.MinValue,
                teretPretovara = Int32.MinValue
            };

            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                for (int j = 1; j <= UpraviteljTablice.brojStupaca; j++)
                {
                    Celija trenutnaCelija = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j];

                    if (trenutnaCelija.RelativniTrosakPrijevoza > 0
                        && trenutnaCelija.RelativniTrosakPrijevoza >= stozerniElement.relativniTrosak)
                    {
                        List<Celija> putPretovara = PronadiZatvoreniPut(trenutnaCelija, new List<Celija>(), true);
                        int kolicinaPretovara = OdrediKolicinuZaPretovariti(putPretovara);
                        if (trenutnaCelija.RelativniTrosakPrijevoza > stozerniElement.relativniTrosak
                            || (trenutnaCelija.RelativniTrosakPrijevoza == stozerniElement.relativniTrosak
                            && kolicinaPretovara > stozerniElement.teretPretovara))
                        {
                            stozerniElement.relativniTrosak = trenutnaCelija.RelativniTrosakPrijevoza;
                            stozerniElement.teretPretovara = kolicinaPretovara;
                            stozerniElement.putPretovara = putPretovara;
                        }
                    }
                }
            }
            UpraviteljPostupka.DodajPostupak("Pretovar tereta:");
            IspisiZatvoreniPut(stozerniElement.putPretovara);
            UpraviteljPostupka.DodajPostupak("Količina pretovara je " + stozerniElement.teretPretovara);
            RaspodjeliTeretNaNovuRelaciju(stozerniElement.teretPretovara, stozerniElement.putPretovara);
        }

        protected List<Celija> PronadiZatvoreniPut(Celija trenutnaCelija, List<Celija> put, bool red)
        {
            put.Add(trenutnaCelija);

            if (red)
            {
                if (trenutnaCelija != put[0])
                {
                    for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
                    {
                        if (UpraviteljTablice.tablicaTransporta.TablicaCelija[trenutnaCelija.Red][i] == put[0]) return put;
                    }
                }
                for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
                {
                    if (UpraviteljTablice.tablicaTransporta.TablicaCelija[trenutnaCelija.Red][i].Zauzeto)
                    {
                        if (!put.Contains(UpraviteljTablice.tablicaTransporta.TablicaCelija[trenutnaCelija.Red][i]))
                        {
                            List<Celija> noviPut = PronadiZatvoreniPut(UpraviteljTablice.tablicaTransporta.TablicaCelija[trenutnaCelija.Red][i], put, false);
                            if (noviPut != null) return noviPut;
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
                {
                    if (UpraviteljTablice.tablicaTransporta.TablicaCelija[i][trenutnaCelija.Stupac] == put[0]) return put;
                }
                for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
                {
                    if (UpraviteljTablice.tablicaTransporta.TablicaCelija[i][trenutnaCelija.Stupac].Zauzeto)
                    {
                        if (!put.Contains(UpraviteljTablice.tablicaTransporta.TablicaCelija[i][trenutnaCelija.Stupac]))
                        {
                            List<Celija> noviPut = PronadiZatvoreniPut(UpraviteljTablice.tablicaTransporta.TablicaCelija[i][trenutnaCelija.Stupac], put, true);
                            if (noviPut != null) return noviPut;
                        }
                    }
                }
            }
            put.Remove(trenutnaCelija);
            return null;
        }

        protected void IspisiZatvoreniPut(List<Celija> zatvoreniPut)
        {
            UpraviteljPostupka.DodajPostupak("Zatvoreni put za (" + zatvoreniPut[0].Red + ", " + zatvoreniPut[0].Stupac + "): ");
            string put = "";
            foreach (var celija in zatvoreniPut)
            {
                put += "(" + celija.Red + ", " + celija.Stupac + ") ";
            }
            UpraviteljPostupka.DodajPostupak(put);
        }

        private int OdrediKolicinuZaPretovariti(List<Celija> putPretovara)
        {
            int kolicinaPretovara = Int32.MaxValue;
            bool minus = false;
            foreach (var celija in putPretovara)
            {
                if (minus && celija.KolicinaTereta < kolicinaPretovara) kolicinaPretovara = celija.KolicinaTereta;
                minus = !minus;
            }
            return kolicinaPretovara;
        }

        private void RaspodjeliTeretNaNovuRelaciju(int kolicinaPretovara, List<Celija> putPretovara)
        {
            putPretovara[0].KolicinaTereta = kolicinaPretovara;
            putPretovara[0].Zauzeto = true;
            putPretovara[0].RelativniTrosakPrijevoza = 0;
            bool minus = true;

            for (int i = 1; i < putPretovara.Count; i++)
            {
                if (minus) putPretovara[i].KolicinaTereta -= kolicinaPretovara;
                else putPretovara[i].KolicinaTereta += kolicinaPretovara;
                if (putPretovara[i].KolicinaTereta == 0) putPretovara[i].Zauzeto = false;
                minus = !minus;
            }
        }

        protected struct StozerniElement
        {
            public int relativniTrosak;
            public int teretPretovara;
            public List<Celija> putPretovara;
        }
    }
}
