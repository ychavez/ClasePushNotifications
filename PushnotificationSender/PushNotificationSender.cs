using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PushnotificationSender
{
   public class PushNotificationSender
    {
        public bool Successful { get; set; }

        public string Response { get; set; }
        public Exception Error { get; set; }

        public PushNotificationSender SendNotification(string _title, string _message, string _topic)
        {
            PushNotificationSender result = new PushNotificationSender();
            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;
                var requestUri = "https://fcm.googleapis.com/fcm/send";

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAoc7n5ng:APA91bHxf7WX2Rh2mffvb7X73QUcVtK44IH0ccm6Gj2SNNEwxZOOkSPaa2_YxlwyEptOXq4JL_gxP6_5BstdxwLbTDJq5FwTcr6isAZBkwV_wXxGfpybfA82FfHoLn-HsupM9dcpy34F"));
                webRequest.Headers.Add(string.Format("Sender: id={0}", "694961038968"));
                webRequest.ContentType = "application/json";

                var data = new
                {
                    to = "c_qKX_f7TialcqP9jRzbHx:APA91bEnPA1rzx8k8ZxNcJsvtOcSLpsEgW0aBJEvPO4MEqdBf46FuTZj1UZBD8bjmAb7NlJuImJeK42jko77DPPxyrJ5gt70DVE9QJcgN442ZJwL1pgTGKCy7OQpN5KMo_cJ_Kv0UEg4", // Uncoment this if you want to test for single device
                    //to = "/topics/" + _topic,
                    notification = new
                    {
                        title = _title,
                        body = _message,
                        //icon="myicon"
                    },
                    priority = "high"
                };
                //var serializer = new Json
                var json = JsonConvert.SerializeObject(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }
    }
}
