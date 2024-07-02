using System.Net.NetworkInformation;


namespace bigbyte
{
    internal static class NetworkAgent
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
        internal static async Task<bool> CheckInternetConnection(string urlToTry = "https://www.github.com")
        {
            int timeout = 3;
            ToLog.Inf($"checking internet connection to {urlToTry} with timeout: {timeout}s");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(timeout);
                    HttpResponseMessage response = await client.GetAsync(urlToTry);
                    ToLog.Inf("connection established successfully");
                    return response.IsSuccessStatusCode;
                }
            }
            catch
            {
                ToLog.Err("connection failed");
                return false;
            }
        }
    }
}
