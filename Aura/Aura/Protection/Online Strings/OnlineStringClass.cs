using System.Net;
using System.Reflection;

namespace Aura.Protection.StringOnline
{
    internal class OnlineString
    {
        public static string Decoder(string encrypted)
        {
            if (Assembly.GetExecutingAssembly() != Assembly.GetCallingAssembly()) return "Aura.png";
            var webClient = new WebClient();
            return webClient.DownloadString($"https://liria.club/encryption/Decoder.php?string={encrypted}");
        }
    }
}
