using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class Optimizacija_SteppingStone
    {
        public void IzvediIteraciju()
        {
            //izracunaj relativne troskove
            IzracunajRelativneTroskove();
            //provjeri optimalnost => ako je optimalno, zavrsi
            if (ProvjeriOptimalnostRijesenja()) return;
            //odredi put za pretovar, odredi kolicinu pretovara pretovari
            PretovariTeret();
            //clear relativne troskove
        }

        private void PretovariTeret()
        {
            throw new NotImplementedException();
        }

        private bool ProvjeriOptimalnostRijesenja()
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

        private List<Celija> PronadiZatvoreniPut(Celija trenutnaCelija, List<Celija> put, bool red)
        {
            put.Add(trenutnaCelija); //dodajem trenutnu celiju u put

            if (red)    //pretrazujem red
            {
                if (trenutnaCelija != put[0]) // ako nije prva iteracija
                {
                    for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++) //pretrazujem cjeli red za pocetnu celiju
                    {
                        if (UpraviteljTablice.tablicaTransporta.TablicaCelija[trenutnaCelija.Red][i] == put[0]) return put; //ako je pronadjena pocetna celija, vrati put
                    }
                }
                for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++) //pretrazujem cjeli red za zauzete celije
                {
                    if (UpraviteljTablice.tablicaTransporta.TablicaCelija[trenutnaCelija.Red][i].Zauzeto) //ako je celija zauzeta
                    {
                        if (!put.Contains(UpraviteljTablice.tablicaTransporta.TablicaCelija[trenutnaCelija.Red][i])) //ako celija nije vec u putu i ako nije trenutna celija
                        {
                            List<Celija> noviPut = PronadiZatvoreniPut(UpraviteljTablice.tablicaTransporta.TablicaCelija[trenutnaCelija.Red][i], put, false); //rekurzivno trazim dalje
                            if (noviPut != null) return noviPut; //ako je pronadjen put, vrati ga
                        }
                    }
                }
            }
            else //pretrazujem stupac
            {
                for (int i = 1; i <= UpraviteljTablice.brojRedova; i++) //pretrazujem cjeli stupac za pocetnu celiju
                {
                    if (UpraviteljTablice.tablicaTransporta.TablicaCelija[i][trenutnaCelija.Stupac] == put[0]) return put; //ako je pronadjena pocetna celija, vrati put
                }
                for (int i = 1; i <= UpraviteljTablice.brojRedova; i++) //pretrazujem cjeli stupac za zauzete celije
                {
                    if (UpraviteljTablice.tablicaTransporta.TablicaCelija[i][trenutnaCelija.Stupac].Zauzeto) //ako je celija zauzeta
                    {
                        if (!put.Contains(UpraviteljTablice.tablicaTransporta.TablicaCelija[i][trenutnaCelija.Stupac])) //ako celija nije vec u putu i ako nije trenutna celija
                        {
                            List<Celija> noviPut = PronadiZatvoreniPut(UpraviteljTablice.tablicaTransporta.TablicaCelija[i][trenutnaCelija.Stupac], put, true); //rekurzivno trazim dalje
                            if (noviPut != null) return noviPut; //ako je pronadjen put, vrati ga
                        }
                    }
                }
            }
            put.Remove(trenutnaCelija); //ako nije pronadjen put, makni trenutnu celiju iz puta
            return null; //ako nije pronadjen put, vrati null
        }
    }
}
