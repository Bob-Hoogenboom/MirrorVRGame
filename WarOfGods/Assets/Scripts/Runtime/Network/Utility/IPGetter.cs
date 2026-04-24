using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Network.Utility
{
    public class IPGetter : MonoBehaviour
    {
        private string _localIP;

        private void Start()
        {
            _localIP = GetLocalIP();
        }

        public string GetLocalIP()
        {
            string localIP = "Not found";

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }

            return localIP;
        }

        private void OnGUI()
        {
            GUI.Box(new Rect(0, 0, 100, 50), $"Local IP is:\n{_localIP}");
        }
    }
}