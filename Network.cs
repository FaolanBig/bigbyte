using System.Net.NetworkInformation;


namespace bigbyte
{
    public static class Network
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
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                try
                {
                    using (Ping ping = new Ping())
                    {
                        PingReply reply = ping.Send(urlToTry, 3000);
                        return reply.Status == IPStatus.Success;
                    }
                }
                catch (Exception e2)
                {
                    Console.Error.WriteLine(e2);
                    return false;
                }
            }

        }
        /*public static bool GetInternetConnectionAvailable(string urlToTry = "https://www.google.com", string ipv4ToTry = "8.8.8.8")
        {
            
        }*/
    }
}
