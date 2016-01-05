using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Button> ButtonsList { get; set; } = new ObservableCollection<Button>();
        public MainPage()
        {
           
            this.InitializeComponent();
            checkUser();
        }
        public async Task checkUser()
        {
            Windows.Storage.StorageFolder storageFolder =
             Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile usernameFile =
                await storageFolder.GetFileAsync("username.txt");
            string text = await Windows.Storage.FileIO.ReadTextAsync(usernameFile);
            if (text != null)
            {
                
                EH.SAR.setusername(text);
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(LoginButton)) { 
            Flyout flyout = Resources["Login"] as Flyout;
            flyout.ShowAt(Elipse);
            }
            else
            {
                Lights.IsPaneOpen = !Lights.IsPaneOpen;
            }
        }

        private async void Accept_Click(object sender, RoutedEventArgs e)
        {
           

            Windows.Storage.StorageFolder storageFolder =
          Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile usernameFile =
                await storageFolder.CreateFileAsync("username.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);
            // hier moet je uit dat textveld waar die acceptbutton in staat even die waarden eruit halen en die variabele in de methode hieronder zetten i.p.v 8000 en 127.0.0.1
            await EH.ConnectToBridge("lol", "8000", "127.0.0.1");
            await EH.getAlldata();
        }

        private void ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            EH.setVals((int)HUE.Value, (int)SAT.Value, (int)BRI.Value);
            Brush b = new SolidColorBrush(EH.HsvToRgb(HUE.Value, SAT.Value, BRI.Value));
            Elipse.Fill = b;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            EH.SetLampHandler();
        }

        private void ToggleState_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
