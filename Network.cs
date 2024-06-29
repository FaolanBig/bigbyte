/*
    bitbyte serves as a package manager.
    It serves as a management tool for downloading, installing, updating, upgrading, removing and running various software aplications from the command line.

    Copyright (C) 2024  Carl Öttinger (Carl Oettinger)

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as published
    by the Free Software Foundation, either version 3 of the License, or
    any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.

    You can contact me in the following ways:
        EMail: oettinger.carl@web.de, big-programming@web.de
 */


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
