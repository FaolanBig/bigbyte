

namespace bigbyte
{
    internal class GitHubHandler
    {
        //Variablen

        //Constructor: Check Internet Connection
        public GitHubHandler() 
        {
            bool internetAvailable = Network.GetInternetConnectionAvailable();
            if (internetAvailable)
            {
                Console.Error.WriteLine("Connection failed");
            }
            else
            {
                Console.WriteLine("Connection established");
            }
        }

        //GitHub pull Repo

        //GitHub pull single

        //GitHub push
    }
}
