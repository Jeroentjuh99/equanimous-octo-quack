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
        
        public static void setOnAndOf(Boolean on)
        {
           
        }
        //private async Task LightOn()
        //{
        //var response = await LightOnTask();
        //if (string.IsNullOrEmpty(response))
        //    await new MessageDialog("Error while setting light properties. ….").ShowAsync();
        //}

        //private async task<string> lightontask()
        //{
        //    var cts = new cancellationtokensource();
        //    cts.cancelafter(5000);

        //    try
        //    {
        //        httpclient client = new httpclient();
        //        httpstringcontent content
        //            = new httpstringcontent
        //                  ($"{{ \"on\": {light.ison.tostring().tolower()} }}",
        //                    windows.storage.streams.unicodeencoding.utf8,
        //                    "application/json");

        //        string ip, username;
        //        int port;
        //        mainpage.retrievesettings(out ip, out port, out username);

        //        uri urilampstate = new uri($"http://{ip}:{port}/api/{username}/lights/{light.id}/state");
        //        var response = await client.putasync(urilampstate, content).astask(cts.token);

        //        if (!response.issuccessstatuscode)
        //        {
        //            return string.empty;
        //        }

        //        string jsonresponse = await response.content.readasstringasync();

        //        system.diagnostics.debug.writeline(jsonresponse);

        //        return jsonresponse;
        //    }
        //    catch (exception ex)
        //    {
        //        system.diagnostics.debug.writeline(ex.message);
        //        return string.empty;
        //    }
        //}

        public static void setLamp(int hue, int sat, int bri)
        {


        }



    }
}
