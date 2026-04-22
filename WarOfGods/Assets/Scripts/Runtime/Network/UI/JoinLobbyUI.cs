using Mirror;
using TMPro;
using UnityEngine;

namespace Network.UI
{
    public class JoinLobbyUI : MonoBehaviour
    {
        [SerializeField] private NetworkManager _netManager;
        public TMP_InputField ipInput;
        public TMP_InputField portInput;

        public void Start()
        {
            _netManager = FindAnyObjectByType<NetworkManager>();
            if (_netManager == null) this.enabled = false;
        }

        public void JoinButton()
        {
            UpdateNetwork();
            _netManager.StartClient();
        }

        private void UpdateNetwork()
        {
            // set the IP
            _netManager.networkAddress = ipInput.text;

            // set the port if transport supports it
            if (Transport.active is PortTransport portTransport)
            {
                if (ushort.TryParse(portInput.text, out ushort port))
                {
                    portTransport.Port = port;
                }
                else
                {
                    Debug.LogWarning("Invalid port entered. Using default.");
                }
            }
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
