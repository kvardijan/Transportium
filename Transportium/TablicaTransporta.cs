using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class TablicaTransporta
    {
        public List<int> KapacitetiIzvora { get; set; }
        public List<int> PotrebeOdredista { get; set; }
        public List<List<Celija>> TablicaCelija { get; set; }
    }
}
