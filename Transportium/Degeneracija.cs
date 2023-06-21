using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class Degeneracija
    {
        public void RijesiDegeneraciju()
        {
            //TODO: tu mozda provjeru ranga da znamo kolka je razlika da znamo kolko fiktivnih relacija napraviti
            Celija degeneriranaRelacija = ProvjeriZavisnostiRelacija();
            Celija relacijaRjesavanjaDegeneracije = DohvatiPotencijanuRelacijuRjesenjaDegenaracije(degeneriranaRelacija);
            StvoriRelacijuSFiktivnimTeretom(relacijaRjesavanjaDegeneracije);
        }

        private void StvoriRelacijuSFiktivnimTeretom(Celija relacijaRjesavanjaDegeneracije)
        {
            relacijaRjesavanjaDegeneracije.Zauzeto = true;
            relacijaRjesavanjaDegeneracije.KolicinaTereta = 0;
        }

        private Celija ProvjeriZavisnostiRelacija()
        {
            Celija degeneriranaRelacija = null;
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                for (int j = 1; j <= UpraviteljTablice.brojStupaca; j++)
                {
                    Celija celija = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][j];
                    if (celija.Zauzeto)
                    {
                        int brojPovezanihRelacija = Math.Max(IzbrojiZauzeteCelijeReda(i), IzbrojiZauzeteCelijeStupca(j));
                        if (brojPovezanihRelacija == 1) degeneriranaRelacija = celija;
                    }
                }
            }
            return degeneriranaRelacija;
        }

        //vraca nezauzetu celiju koja je u istom redu ili stupcu kao degenerirana relacija i ima najmanju trosak prijevoza
        private Celija DohvatiPotencijanuRelacijuRjesenjaDegenaracije(Celija degeneriranaRelacija)
        {
            Celija odabranaRelacija = new Celija
            {
                TrosakPrijevoza = int.MaxValue
            };

            for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
            {
                Celija celija = UpraviteljTablice.tablicaTransporta.TablicaCelija[degeneriranaRelacija.Red][i];
                if (!celija.Zauzeto && celija.TrosakPrijevoza < odabranaRelacija.TrosakPrijevoza) odabranaRelacija = celija;
            }
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                Celija celija = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][degeneriranaRelacija.Stupac];
                if (!celija.Zauzeto && celija.TrosakPrijevoza < odabranaRelacija.TrosakPrijevoza) odabranaRelacija = celija;
            }

            return odabranaRelacija;
        }

        private int IzbrojiZauzeteCelijeStupca(int indexStupca)
        {
            int brojZauzetihCelija = 0;
            for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
            {
                if (UpraviteljTablice.tablicaTransporta.TablicaCelija[i][indexStupca].Zauzeto) brojZauzetihCelija++;
            }
            return brojZauzetihCelija;
        }

        private int IzbrojiZauzeteCelijeReda(int indexReda)
        {
            int brojZauzetihCelija = 0;
            for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
            {
                if (UpraviteljTablice.tablicaTransporta.TablicaCelija[indexReda][i].Zauzeto) brojZauzetihCelija++;
            }
            return brojZauzetihCelija;
        }
    }
}
