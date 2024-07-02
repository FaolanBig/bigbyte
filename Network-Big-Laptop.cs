using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;


namespace bigbyte
{
    public static class NetworkManager
    {
        public static bool GetInternetConnectionAvailable(string ipv4ToTry = "8.8.8.8", string urlToTry = "https://www.google.com")
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(ipv4ToTry, 3000); // Google's public DNS server
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }

        }
        /*public static bool GetInternetConnectionAvailable(string urlToTry = "https://www.google.com", string ipv4ToTry = "8.8.8.8")
        {
            
        }*/
    }
}
