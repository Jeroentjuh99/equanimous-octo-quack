﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Newtonsoft;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace FabHUELess2
{
    public class SendAndReceive
    {
        public Boolean on;
        public string ip
        {
            get; set;
        } = "145.48.205.190";
        public string port
        {
            get; set;
        } = "80";
        private ObservableCollection<Lamp> lamplist = new ObservableCollection<Lamp>();
        private string username;
        private Eventhandlers eventH;
        public SendAndReceive(Eventhandlers eventH)
        {
            this.eventH = eventH;
        }
        public void setusername(string username)
        {
            this.username = username;
        }
        public async void setOnAndOf(Boolean on, int id)
        {
            var response = await LightOnTask(on, id);
            if (string.IsNullOrEmpty(response))
                await new MessageDialog("Error while setting light properties. ….").ShowAsync();
        }
        public async Task<string> LightOnTask(Boolean on, int Id)
        {
            this.on = on;
                string url = "http://" + ip + ":" + port + "/" + "api/" + username + "/" + "lights" + "/" + Id + "/" + "state";
            HttpContent content;
            if (on == true)
            {
                content = new StringContent
                              ("{\"on\": true }",
                                Encoding.UTF8,
                                "application/json");
            }
            else {
                content = new StringContent
                              ("{\"on\": false}",
                                Encoding.UTF8,
                                "application/json");
            }
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
            string url = "http://" + ip + ":" + port + "/" + "api/"+username+"/" + "lights" + "/" + Id + "/" + "state";
           
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                return await response.Content.ReadAsStringAsync();
            }

        }
        public async Task<int> GetAllData()
        {
            List<char> y = "y".ToList();
            char x = y[0];
            int z = 0;
            var response = await GetAllTask(0);
            for (int i = 0; i < 12; i++)
            {
                if (response.ToString().Contains(i.ToString()))
                {
                    z++;
                }
            }

            {
                for (int c = 1; c < z; c++)
                {
                   
                    try
                    {
                        var response2 = await GetAllTask(c);
                        
                        Lamp lamp = JsonConvert.DeserializeObject<Lamp>(response2);
                        lamp.id = c;
                        lamplist.Add(lamp);

                        if (string.IsNullOrEmpty(response))
                            await new MessageDialog("Error while setting light properties. ….").ShowAsync();
                    }
                    catch
                    {
                        
                    }
                }

                eventH.setList(lamplist);
                return 10;
            }
        }
        public async Task<string> GetAllTask(int id)
        {
            string url;
            if (id == 0)
            {
                url = "http://" + ip + ":" + port + "/" + "api/"+username+"/lights";
            }
            else
            {
              url = "http://" + ip + ":" + port + "/" + "api/" + username + "/" + "lights/" + id+ "/";
            }

             
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.GetAsync(url);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }

        }
        public async Task<int> ConnectBridge(string usernameN, string port, string ip)
        {
            
            Windows.Storage.StorageFolder storageFolder =
    Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile usernameFile =
                await storageFolder.GetFileAsync("username.txt");
            if (usernameFile.ToString() != "")
            {
                
                if (username == null)
                {
                    var response = await ConnectTask(usernameN, port, ip);
                    List<char> list = response.Skip(25).ToList();
                    response = null;
                    foreach (char c in list)
                    {
                        response += c;
                    }
                    response = response.Remove(31);
                    String username1 = response;
                    this.username = username1;
                    await Windows.Storage.FileIO.WriteTextAsync(usernameFile, username1);
                }
            }

            return 10;
           

            


        }
        public async Task<string> ConnectTask(string usernameN, string port, string ip)
        {
            usernameN = "my_hue_app#iphone peter";
            this.port = port;
            this.ip = ip;
            string url = "http://" + ip + ":" + port + "/" + "api/";
            HttpContent content = new StringContent
                      ("{\"devicetype\":\"MijnApp#{" + usernameN + "}\"}",
                        Encoding.UTF8,
                        "application/json");
            using (HttpClient hc = new HttpClient())
            {
                var response = await hc.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            
        }

        public void setIP(string ip, string poort)
        {
            this.ip = ip;
            this.port = poort;
        }
        
    }
    
}
