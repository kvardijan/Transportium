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
            //izracunaj dualne varijable (pozovi funkciju)
            IzracunajDualneVarijable();
        }

        //TODO: Funkcija za računanje dualnih varijabli
        public void IzracunajDualneVarijable()
        {
            int indexTrenutnogReda = 1;
            int indexTrenutnogStupca = 1;
            UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[indexTrenutnogReda] = 0;
            List<int> obradeniRedovi = new List<int> { 1 };
            List<int> obradeniStupci = new List<int>();
            bool obradujemRed = true;

            do
            {
                if (obradujemRed && obradeniStupci.Count < UpraviteljTablice.brojStupaca)
                {
                    for (int i = 1; i <= UpraviteljTablice.brojStupaca; i++)
                    {
                        if (obradeniStupci.Contains(i)) continue;
                        Celija trenutnaCelija = UpraviteljTablice.tablicaTransporta.TablicaCelija[indexTrenutnogReda][i];
                        if (trenutnaCelija.Zauzeto)
                        {
                            UpraviteljTablice.tablicaTransporta.DualneVarijableOdredista[i] =
                                trenutnaCelija.TrosakPrijevoza - UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[indexTrenutnogReda];
                            obradeniStupci.Add(i);
                        }
                    }
                    if(indexTrenutnogReda < UpraviteljTablice.brojRedova) indexTrenutnogReda++;
                }
                if(!obradujemRed && obradeniRedovi.Count < UpraviteljTablice.brojRedova)
                {
                    for (int i = 1; i <= UpraviteljTablice.brojRedova; i++)
                    {
                        if (obradeniRedovi.Contains(i)) continue;
                        Celija trenutnaCelija = UpraviteljTablice.tablicaTransporta.TablicaCelija[i][indexTrenutnogStupca];
                        if (trenutnaCelija.Zauzeto)
                        {
                            UpraviteljTablice.tablicaTransporta.DualneVarijableIshodista[i] =
                                trenutnaCelija.TrosakPrijevoza - UpraviteljTablice.tablicaTransporta.DualneVarijableOdredista[indexTrenutnogStupca];
                            obradeniRedovi.Add(i);
                        }
                    }
                    if (indexTrenutnogStupca < UpraviteljTablice.brojStupaca) indexTrenutnogStupca++;
                }
                obradujemRed = !obradujemRed;
            } while (obradeniRedovi.Count < UpraviteljTablice.brojRedova 
            || obradeniStupci.Count < UpraviteljTablice.brojStupaca);
        }
    }
}
