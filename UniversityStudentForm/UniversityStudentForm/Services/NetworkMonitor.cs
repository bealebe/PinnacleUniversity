using System;
using System.Net.NetworkInformation;

namespace PinnacleUniversity
{
    public class NetworkMonitor
    {
        /// <summary>
        /// Checks to see if it can ping Google.
        /// </summary>
        /// <returns></returns>
        public static bool CanPingOutsideNetwork()
        {
            const int timeout = 1000;
            const string host = "google.com";

            var ping = new Ping();
            var buffer = new byte[32];
            var pingOptions = new PingOptions();

            try
            {
                var reply = ping.Send(host, timeout, buffer, pingOptions);
                return (reply != null && reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}