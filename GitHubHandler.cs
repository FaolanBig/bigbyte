

namespace bigbyte
{
    internal class GitHubHandler
    {
        //Variablen

        //Constructor: Check Internet Connection
        public GitHubHandler() 
        {
            bool internetAvailable = Network.CheckInternetConnection().Result;
        }

        //GitHub pull Repo

        //GitHub pull single

        //GitHub push
    }
}
