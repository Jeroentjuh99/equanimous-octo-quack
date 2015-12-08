using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabHUELess2
{
    class Lamp
    {
        int id { get;}
        int hue { get; }
        int sat { get; }
        int bri { get; }
        bool on { get; }
        string name;
        public Lamp(int id, int hue, int sat, int bri, bool on, string name)
        {
            this.id = id;
            this.hue = hue;
            this.sat = sat;
            this.bri = bri;
            this.on = on;
            this.name = name;
        }

    }
}
