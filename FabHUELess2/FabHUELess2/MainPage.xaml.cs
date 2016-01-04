using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FabHUELess2
{

    public sealed partial class MainPage : Page
    {

        private Eventhandlers EH = new Eventhandlers();
        public MainPage()
        {
           
            this.InitializeComponent();
            start();
         }
        public async Task start()
        {
            await EH.ConnectToBridge("lol", "8000", "127.0.0.1");
            EH.getAlldata();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Flyout flyout = Resources["Login"] as Flyout;
            flyout.ShowAt(Elipse);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Brush b = new SolidColorBrush(EH.HsvToRgb(HUE.Value, SAT.Value, BRI.Value));
            Elipse.Fill = b;
        }
    }
}
