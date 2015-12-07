using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;

namespace FabHUELess
{
    class EventHandlers
    {
        static int HueVal;
        static int SatVal;
        static int BriVal;
        static Boolean on = false;
        public static Color setHueVal(int hue)
        {
            HueVal = hue;
            return HsvToRgb((double)HueVal, (double)SatVal, (double)BriVal);
        }
       
        public static Color setSatVal(int sat)
        {
            SatVal = sat;
            return HsvToRgb((double)HueVal, (double)SatVal, (double)BriVal);
        }
        
        public static Color setBriVal(int bri)
        {
            BriVal = bri;
            return HsvToRgb((double)HueVal, (double)SatVal, (double)BriVal);
        }
        public static void setOnAndOfHandler()
        {
            on = !on;
            SendAndReceive.setOnAndOf(on, 1);
        }
        public static  void SetLampHandler()
        {

            
            SendAndReceive.setLamp(HueVal, SatVal, BriVal, 1);
            
        }
        public static Color HsvToRgb(double hue, double sat, double val)
        {

            double h = ((double)hue * 360.0f) / 65535.0f;
            double s = (double)sat / 255.0f;
            double v = (double)val / 255.0f;

            int hi = (int)Math.Floor(h / 60.0) % 6;
            double f = (h / 60.0) - Math.Floor(h / 60.0);

            double p = v * (1.0 - s);
            double q = v * (1.0 - (f * s));
            double t = v * (1.0 - ((1.0 - f) * s));

            Color ret;

            switch (hi)
            {
                case 0:
                    ret = GetRgb(v, t, p);
                    break;
                case 1:
                    ret = GetRgb(q, v, p);
                    break;
                case 2:
                    ret = GetRgb(p, v, t);
                    break;
                case 3:
                    ret = GetRgb(p, q, v);
                    break;
                case 4:
                    ret = GetRgb(t, p, v);
                    break;
                case 5:
                    ret = GetRgb(v, p, q);
                    break;
                default:
                    ret = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
                    break;
            }
            return ret;
        }
        public static Color GetRgb(double r, double g, double b)
        {
            return Color.FromArgb(255, (byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }
        public static void ConnectToBridge()
        {
            SendAndReceive.ConnectBridge(SendAndReceive.ip);
            Checklamps();
           
        }
        public static void Checklamps()
        {
            for (int i = 1; i < 10; i++)
            {
                SendAndReceive.GetData(i);
            }
            
            
        }

        
    }
}
