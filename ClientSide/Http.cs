using Bridge.Html5;
using System;
using System.Threading.Tasks;

namespace ClientSide
{
    public static class Http
    {
        public static async Task<string> GetAsync(string url)
        {
            var tcs = new TaskCompletionSource<string>();
            var xmlHttp = new XMLHttpRequest();
            xmlHttp.Open("GET", url, true);
            xmlHttp.SetRequestHeader("Content-Type", "application/json");
            xmlHttp.OnReadyStateChange = () =>
            {
                if (xmlHttp.ReadyState == AjaxReadyState.Done)
                {
                    if (xmlHttp.Status == 200 || xmlHttp.Status == 304)
                    {

                        tcs.SetResult(xmlHttp.ResponseText);
                    }
                    else
                    {
                        tcs.SetException(new Exception(xmlHttp.ResponseText));
                    }
                }

            };

            xmlHttp.Send();
            return await tcs.Task;
        }

         
        public static async Task<string> PostAsync(string url, string content)
        {
            var tcs = new TaskCompletionSource<string>();
            var xmlHttp = new XMLHttpRequest();
            xmlHttp.Open("POST", url, true);
            xmlHttp.SetRequestHeader("Content-Type", "application/json");
            xmlHttp.OnReadyStateChange = () =>
            {
                if (xmlHttp.ReadyState == AjaxReadyState.Done)
                {
                    if (xmlHttp.Status == 200 || xmlHttp.Status == 304)
                    {

                        tcs.SetResult(xmlHttp.ResponseText);
                    }
                    else
                    {
                        tcs.SetException(new Exception(xmlHttp.ResponseText));
                    }
                }

            };

            xmlHttp.Send(content);
            return await tcs.Task;
        }
    }
}