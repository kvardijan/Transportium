using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class TablicaTransporta
    {
        public int[] KapacitetiIzvora { get; set; } = new int[11];
        public int[] PotrebeOdredista { get; set; } = new int[11];
        public int SumaKolicine { get; set; }
        public Celija[][] TablicaCelija { get; set; } = new Celija[11][];

        public TablicaTransporta()
        {
            for (int i = 0; i < TablicaCelija.Length; i++)
            {
                TablicaCelija[i] = new Celija[11];
                for (int j = 0; j < TablicaCelija[i].Length; j++)
                {
                    TablicaCelija[i][j] = new Celija();
                }
            }
        }
    }
}
