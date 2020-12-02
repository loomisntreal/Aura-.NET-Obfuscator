using System.Net;
using System.Reflection;

namespace Aura.Protection.StringOnline
{
    internal class OnlineString
    {
        public static string Decoder(string encrypted)
        {
            if (Assembly.GetExecutingAssembly() != Assembly.GetCallingAssembly()) return "Aura.png";
            return "null";
        }
    }
}
