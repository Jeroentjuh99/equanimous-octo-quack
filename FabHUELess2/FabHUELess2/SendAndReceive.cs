using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft;
using Newtonsoft.Json;

namespace FabHUELess2
{
    class SendAndReceive
    {
        private string ip;
        private string port;
        private string username;
        private Eventhandlers eventH;
        public SendAndReceive(Eventhandlers eventH)
        {
            this.eventH = eventH;
        }

        public async void setOnAndOf(Boolean on, int id)
        {
            var response = await LightOnTask(on, id);
            if (string.IsNullOrEmpty(response))
                await new MessageDialog("Error while setting light properties. ….").ShowAsync();
        }
        public async Task<string> LightOnTask(Boolean on, int Id)
        {
                string url = "http://" + ip + ":" + port + "/" + "api/" + username + "/" + "lights" + "/" + Id + "/" + "state";
                HttpContent content = new StringContent
                          ($"{{ \"on\": {on} }}",
                            Encoding.UTF8,
                            "application/json");
            using (HttpClient hc = new HttpClient())
                {
                    var response = await hc.PutAsync(url, content);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            
        }

        public async void setLamp(int hue, int sat, int bri, int id)
        {
            var response = await LightSetTask(hue, sat, bri, id);
            if (string.IsNullOrEmpty(response))
                await new MessageDialog("Error while setting light properties. ….").ShowAsync();

        }
        public async Task<string> LightSetTask(int hue, int sat, int bri, int Id)
        {
            string url = "http://" + ip + ":" + port + "/" + "api/" + username + "/" + "lights" + "/" + Id + "/" + "state";
            HttpContent content = new StringContent
                      ($"{{ \"bri\": {bri} , \"hue\": {hue} , \"sat\": {sat}}}",
                        Encoding.UTF8,
                        "application/json");
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.PutAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }

        }
        public async void GetData(int id)
        {
            var response = await GetTask(id);
            if (string.IsNullOrEmpty(response))
                await new MessageDialog("Error while setting light properties. ….").ShowAsync();
        }
        public async Task<string> GetTask(int Id)
        {
            string url = "http://" + ip + ":" + port + "/" + "api/" + username + "/" + "lights" + "/" + Id + "/" + "state";
           
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();
            }

        }
        public async void ConnectBridge(string usernameN, string port, string ip)
        {
            var response = await ConnectTask(usernameN, port, ip);
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
        public async Task<string> ConnectTask(string usernameN, string port, string ip)
        {
            this.port = port;
            this.ip = ip;
            string url = "http://" + ip + ":" + port + "/" + "api/";
            HttpContent content = new StringContent
                      ($"{{ \"username\": {usernameN} }}",
                        Encoding.UTF8,
                        "application/json");
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            
        }
        
        
    }
    
}
