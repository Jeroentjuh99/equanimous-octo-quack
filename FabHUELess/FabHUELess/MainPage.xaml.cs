using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace FabHUELess
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EventHandlers.SetLampHandler();
        }

        private void Hue_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(EventHandlers.setHueVal((int)e.NewValue));
            setColor(brush);
        }

        private void Saturation_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            SolidColorBrush brush = new SolidColorBrush(EventHandlers.setSatVal((int)e.NewValue));
            setColor(brush);
        }

        private void Brightness_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

            SolidColorBrush brush = new SolidColorBrush(EventHandlers.setBriVal((int)e.NewValue));
            setColor(brush);
        }

        private void LightSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            EventHandlers.setOnAndOfHandler();
        }
        private void setColor(SolidColorBrush brush)
        {
            Brightness.Foreground = brush;
            Saturation.Foreground = brush;
            Hue.Foreground = brush;
            LightSwitch.Foreground = brush;
            
        }
    }
}
