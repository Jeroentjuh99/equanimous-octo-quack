using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
        Boolean on = true;
        double Id;
        public ObservableCollection<Lamp> collectionlamp { get; set; } = new ObservableCollection<Lamp>();
        public MainPage()
        {
           
            this.InitializeComponent();
            
        }
        public async Task checkUser()
        {
            Windows.Storage.StorageFolder storageFolder =
             Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile usernameFile =
                await storageFolder.GetFileAsync("username.txt");
            string text = await Windows.Storage.FileIO.ReadTextAsync(usernameFile);
            
            if (text != null && !text.Equals(""))
            {
                
                EH.SAR.setusername(text);
                //var responseCheck = await EH.SAR.GetAllTask(0);
            //    if (responseCheck.ToString().Contains("error"))
            //    {
            //        EH.SAR.setusername(null);
            //    }
            //  }
            //else
            //{

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
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
		await storageFolder.CreateFileAsync("username.txt", Windows.Storage.CreationCollisionOption.OpenIfExists);
            await checkUser();
            // hier moet je uit dat textveld waar die acceptbutton in staat even die waarden eruit halen en die variabele in de methode hieronder zetten i.p.v 8000 en 127.0.0.1
            try {
                string[] strings = loginBox.Text.Trim().Split(':');
                await EH.ConnectToBridge("lol", strings[1], strings[0]);
                EH.SAR.ip = strings[0];
                EH.SAR.port = strings[1];
                await EH.getAlldata();
                //collectionlamp = EH.lamps;
                foreach(Lamp l in EH.lamps){
                    collectionlamp.Add(l);
                }
                Flyout f = Resources["Login"] as Flyout;
                f.Hide();
                LoginButton.IsEnabled = false;
            }
            catch (Exception)
            {
                Flyout flyout = new Flyout();
                TextBlock b = new TextBlock();
                b.Text = "Please make sure you entered the adress correctly";
                flyout.Content = b;
                flyout.ShowAt(Elipse);
            }

        }

        private void ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            EH.setVals((int)HUE.Value, (int)SAT.Value, (int)BRI.Value);
            Brush b = new SolidColorBrush(EH.HsvToRgb(HUE.Value, SAT.Value, BRI.Value));
            if (on == true)
            {
                Elipse.Fill = b;
            }
            

        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            EH.SetLampHandler(Id);
            foreach (Lamp l in collectionlamp)
            {
                if (l.id == Id)
                {
                    l.state.hue = HUE.Value;
                    l.state.bri = BRI.Value;
                    l.state.sat = SAT.Value;
                }
            }
        }

        private void ToggleState_Click(object sender, RoutedEventArgs e)
        {
            EH.setOnAndOfHandler(Id, on);
            on = !on;
            if (on == true)
            {
                Brush b = new SolidColorBrush(EH.HsvToRgb(HUE.Value, SAT.Value, BRI.Value));
                Elipse.Fill = b;
            }
            else
            {
                Brush b = new SolidColorBrush(EH.GetRgb(190,190,190));
                Elipse.Fill = b;
            }
        }

        private void LightsBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            Lamp s = (Lamp)e.ClickedItem;
            Id = s.id;
            foreach (Lamp l in collectionlamp)
            {
                if (l.id == Id)
                {
                    CurrentId.Text = l.name;
                    HUE.Value = l.state.hue;
                    BRI.Value = l.state.bri;
                    SAT.Value = l.state.sat;
                    on = l.state.on;
                    if (on == true)
                    {
                        Brush b = new SolidColorBrush(EH.HsvToRgb(HUE.Value, SAT.Value, BRI.Value));
                        Elipse.Fill = b;
                    }
                    else
                    {
                        Brush b = new SolidColorBrush(EH.GetRgb(190, 190, 190));
                        Elipse.Fill = b;
                    }
                }
            }
            Lights.IsPaneOpen = !Lights.IsPaneOpen;


        }

        private void ApplyAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Lamp l in collectionlamp)
            {
                EH.SetLampHandler(l.id);
                l.state.hue = HUE.Value;
                l.state.bri = BRI.Value;
                l.state.sat = SAT.Value;
            }
        }

        
        
    }
}
