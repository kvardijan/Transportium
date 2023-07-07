using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Transportium
{
    public static class UpraviteljPostupka
    {
        public static ListBox Postupak { get; set; }

        public static void DodajPostupak(string postupak)
        {
            Postupak.Items.Add(postupak);
        }

        public static void ObrisiPostupak()
        {
            Postupak.Items.Clear();
        }
    }
}
