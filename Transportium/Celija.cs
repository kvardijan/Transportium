﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportium
{
    public class Celija
    {
        public int TrosakPrijevoza { get; set; }
        public int KolicinaTereta { get; set; }
        public int RelativniTrosakPrijevoza { get; set; }
        public bool Zauzeto { get; set; } = false;

        public int Red { get; set; }
        public int Stupac { get; set; }

    }
}
