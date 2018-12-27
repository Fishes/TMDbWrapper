using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace TmdbWrapper.Utilities
{
    internal class HttpClientFactory
    {
        private static readonly HttpClientFactory Singelton = new HttpClientFactory();

        public static HttpClient GetHttpClient()
        {
            return Singelton.GetAccess();
        }

        private const int RequestWindowInSeconds = -11;
        private const int RequestsInWindow = 35;
        private readonly List<DateTime> previousCalls = new List<DateTime>();
        private readonly object callLock = new object();

        private HttpClient GetAccess()
        {
            lock (callLock)
            {
                bool mayProceed;
                do
                {
                    var limit = DateTime.Now.AddSeconds(RequestWindowInSeconds);
                    previousCalls.RemoveAll(d => d < limit);
                    mayProceed = previousCalls.Count < RequestsInWindow;

                    if (mayProceed)
                    {
                        previousCalls.Add(DateTime.Now);
                    }
                    else
                    {
                        Thread.Sleep(500);
                        Console.WriteLine("Call blocked");
                    }
                } while (!mayProceed);
                Console.WriteLine($"{previousCalls[0].ToLongTimeString()} {DateTime.Now.ToLongTimeString()} {previousCalls.Count} {Thread.CurrentThread.Name}");
            }
            return new HttpClient();
        }
    }
}