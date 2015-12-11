namespace FabHUELess2
{
    public class state
    {
        public bool on { get; set; }
        public double bri { get; set; }
        public double hue { get; set; }
        public double sat { get; set; }
        public state(int bri,int hue, int sat, bool on)
        {
            this.bri = bri;
            this.hue = hue;
            this.sat = sat;
            this.on = on;
        }
    }
}