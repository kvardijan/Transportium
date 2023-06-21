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
            //tu mozda provjeru ranga da znamo kolka je razlika da znamo kolko fiktivnih relacija napraviti
            Celija degeneriranaRelacija = ProvjeriZavisnostiRelacija();
            DohvatiPotencijanuRelacijuRjesenjaDegenaracije(degeneriranaRelacija);
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
                        int brojPovezanihRelacija = IzbrojiZauzeteCelijeReda(i);
                        brojPovezanihRelacija = IzbrojiZauzeteCelijeStupca(j);
                        if (brojPovezanihRelacija == 1) degeneriranaRelacija = celija;
                    }
                }
            }
            return degeneriranaRelacija;
        }

        private void DohvatiPotencijanuRelacijuRjesenjaDegenaracije(Celija degeneriranaRelacija)
        {
            throw new NotImplementedException();
        }

        private int IzbrojiZauzeteCelijeStupca(int indexStupca)
        {
            throw new NotImplementedException();
        }

        private int IzbrojiZauzeteCelijeReda(int indexReda)
        {
            throw new NotImplementedException();
        }
    }
}
