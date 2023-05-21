using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class PocetniRaspored_SjeveroZapadniKut
    {
        int[] kolicineIzvora = new int[11];
        int[] kolicineOdredista = new int[11];
        public void RjesiProblem()
        {
            ResetirajKolicine();
        }

        private void ResetirajKolicine()
        {
            Array.Clear(kolicineIzvora, 0, kolicineIzvora.Length);
            Array.Clear(kolicineOdredista, 0, kolicineOdredista.Length);
        }
    }
}
