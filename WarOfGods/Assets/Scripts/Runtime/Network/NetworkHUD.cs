using Mirror;
using TMPro;
using UnityEngine;


namespace Network
{
    public class NetworkHUD : MonoBehaviour
    {
        [SerializeField] private NetworkManager _netManager;
        public TMP_InputField ipInput;
        public TMP_InputField portInput;

        public void Start()
        {
            _netManager = FindAnyObjectByType<NetworkManager>();
            if (_netManager == null) this.enabled = false;
        }
        public void HostButton()
        {
            //#TODO does the host need to fill in their own IP and port? or should this be done automatic?
            _netManager.StartHost();
        }

        public void JoinButton()
        {
            UpdateNetwork();
            _netManager.StartClient();
        }

        [Tooltip("Sends the client out of the room but if the host invokes this the room gets terminated")]
        public void ReturnToMainMenu()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                //you are the host
                _netManager.StopHost();
            }
            else
            {
                _netManager.StopClient();
            }
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
    }
}
