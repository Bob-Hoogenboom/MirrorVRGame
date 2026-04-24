using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Network.UI
{
    public class JoinLobbyUI : MonoBehaviour
    {
        [SerializeField] private NetworkManager _netManager;
        public TMP_InputField ipInput;
        public TMP_InputField portInput;

        [SerializeField] private GameObject xrRig;

        private void OnEnable() => NetworkClient.OnConnectedEvent += OnConnected;
        private void OnDisable() => NetworkClient.OnConnectedEvent -= OnConnected;


        public void Start()
        {
            _netManager = FindAnyObjectByType<NetworkManager>();
            if (_netManager == null) this.enabled = false;
        }

        private void OnConnected()
        {
            //TODO: ensure that the new rig that is spawned gets all refernces correctly
            Destroy(xrRig);
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
