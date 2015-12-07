using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace FabHUELess
{
    class SendAndReceive
    {
        static string ip = "145.48.230.224";
        public static string username = "52f50a9999a85250e53c543986bff72";
        static int port = 8000;
        public static async void setOnAndOf(Boolean on, int id)
        {
            var response = await LightOnTask(on, id);
            if (string.IsNullOrEmpty(response))
                await new MessageDialog("Error while setting light properties. ….").ShowAsync();
        }

        private static async Task<string> LightOnTask(Boolean on, int Id)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpClient client = new HttpClient();
                HttpStringContent content
                    = new HttpStringContent
                          ($"{{ \"on\": {on} }}",
                            Windows.Storage.Streams.UnicodeEncoding.Utf8,
                            "application/json");

                

                Uri uriLampState = new Uri($"http://{ip}:{port}/api/{username}/lights/{Id}/state");
                var response = await client.PutAsync(uriLampState, content).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine(jsonResponse);

                return jsonResponse;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        private static async Task<string> LightColorTask(int hue, int sat, int bri, int Id)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpClient client = new HttpClient();
                HttpStringContent content
                    = new HttpStringContent
                          ($"{{ \"bri\": {bri} , \"hue\": {hue} , \"sat\": {sat}}}",
                            Windows.Storage.Streams.UnicodeEncoding.Utf8,
                            "application/json");
                //MainPage.RetrieveSettings(out ip, out port, out username);

                Uri uriLampState = new Uri($"http://{ip}:{port}/api/{username}/lights/{Id}/state");
                var response = await client.PutAsync(uriLampState, content).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine(jsonResponse);

                return jsonResponse;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        public static async void setLamp(int hue, int sat, int bri, int id)
        {
            var response = await LightColorTask(hue,sat,bri,id);
            if (string.IsNullOrEmpty(response))
                await new MessageDialog("Error while setting light properties. ….").ShowAsync();

        }

        public static async void ConnectBridge(string ip)
        {
            var response = await ConnectTask(ip);
            List<char> list = response.Skip(25).ToList();
            response = null;
            foreach (char c in list)
            {
                response += c;
            }
            response = response.Remove(31);
            username = response;
            
            if (string.IsNullOrEmpty(response))
                await new MessageDialog("Error while setting light properties. ….").ShowAsync();
            
           
        }
        private static async Task<string> ConnectTask(String ip)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpClient client = new HttpClient();
                HttpStringContent content
                    = new HttpStringContent
                          ($"{{ \"username\": App }}",
                            Windows.Storage.Streams.UnicodeEncoding.Utf8,
                            "application/json");
                //MainPage.RetrieveSettings(out ip, out port, out username);

                Uri uriApiState = new Uri($"http://{ip}:{port}/api");
                var response = await client.PostAsync(uriApiState, content).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine(jsonResponse);

                return jsonResponse;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }
}
