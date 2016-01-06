using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabHUELess2
{
    public class Lamp
    {
        public string name = null;
        public state state = new state(0,0,0,false);
        public double id { get; set; } = 0;
        public Lamp(int id, int hue, int sat, int bri, bool on, string name)
        {
            this.id = id;

            //this.hue = hue;
            ///this.sat = sat;
            //this.bri = bri;
            //this.on = on;
            this.name = name;
        }
        public override String ToString()
        {
            string s =  name.ToString() +" " + state.bri.ToString();

            return s;
        }
    }
}
