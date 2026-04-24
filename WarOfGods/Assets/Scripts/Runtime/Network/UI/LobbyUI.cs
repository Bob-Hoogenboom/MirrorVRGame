using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace Network.UI
{
    public class LobbyUI : NetworkBehaviour
    {
        private NetworkManager _netManager;

        private void Start()
        {
            _netManager = FindAnyObjectByType<NetworkManager>();
            if (_netManager == null)
            {
                Debug.LogWarning("UI is Disabled because of a missing NetworkManager");
                DisableAllUI();
            }
        }

        [Tooltip("Sends the client out of the room but if the host invokes this the room gets terminated")]
        public void ReturnToMainMenu()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                //you are the Host 
                _netManager.StopHost();
            }
            else if(NetworkServer.active && !NetworkClient.isConnected)
            {
                //you are the Host (Server only)
                _netManager.StopServer();
            }
            else
            {
                //you are just a Client
                _netManager.StopClient();
            }
        }

        public void DisableAllUI()
        {
            Button[] buttons = GetComponentsInChildren<Button>();

            foreach (Button button in buttons)
            {
                button.interactable = false;
            }

            this.enabled = false;
        }
    }
}
