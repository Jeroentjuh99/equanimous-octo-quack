using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace FabHUELess2
{

    public class Eventhandlers
    {
        public SendAndReceive SAR;
        public ObservableCollection<Lamp> lamps { get; set; } = new ObservableCollection<Lamp>();
        private int HueVal { get; set; }
        private int SatVal { get; set; }
        private int BriVal { get; set; }
        public Eventhandlers()
        {
            SAR = new SendAndReceive(this);
        }

        //public Color setHueVal(int hue)
        //{
        //    HueVal = hue;
        //    return HsvToRgb((double)HueVal, (double)SatVal, (double)BriVal);
        //}

        //public Color setSatVal(int sat)
        //{
        //    SatVal = sat;
        //    return HsvToRgb((double)HueVal, (double)SatVal, (double)BriVal);
        //}

        //public Color setBriVal(int bri)
        //{
        //    BriVal = bri;
        //    return HsvToRgb((double)HueVal, (double)SatVal, (double)BriVal);
        //}
        public void setVals(int hue, int sat, int bri)
        {
            BriVal = bri;
            SatVal = sat;
            HueVal = hue;
        }
        public void setOnAndOfHandler(double id, Boolean on)
        {
            on = !on;
            SAR.setOnAndOf(on, (int)id);
        }
        public void SetLampHandler(double id)
        {
            SAR.setLamp(HueVal, SatVal, BriVal, (int)id);
        }
        public Color HsvToRgb(double hue, double sat, double val)
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
        public Color GetRgb(double r, double g, double b)
        {
            return Color.FromArgb(255, (byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }
        public async Task<int> ConnectToBridge(string usernameN, string port, string ip)
        { 
          var response = await SAR.ConnectBridge(usernameN,port,ip);
            return 10;
        }
        public void addLamp(int id, int hue, int sat, int bri, bool on, string name)
        {
            new Lamp(id, hue, sat, bri, on, name);
        }
        public async Task<int> getAlldata() {

            await SAR.GetAllData();
            return 10;
        }
        public void setList(ObservableCollection<Lamp> a)
        {
            lamps = a;
            
            //await new MessageDialog(a[1].name.ToString()).ShowAsync();
        }
        public SendAndReceive GetSAR()
        {
            return SAR;
        }
    }
}
