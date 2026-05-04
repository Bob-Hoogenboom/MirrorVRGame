using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Network.UI
{
    public class JoinLobbyUI : NetworkBehaviour
    {
        [SerializeField] private TMP_Text readyFeedbackText;

        private NetworkManager _netManager;
        private NetworkRoomPlayer _clientRoomPlayer;

        private bool _readyToBegin = false;

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

        public void ToggleReadyPlayer()
        {
            //toggle bool
            _readyToBegin = !_readyToBegin;

            // CmdChangeReadyState is build in NetworkRoomPlayer
            _clientRoomPlayer.CmdChangeReadyState(_readyToBegin);

            //Upadte Text
            UpdateReadyPlayerText();
        }
        private void UpdateReadyPlayerText()
        {
            readyFeedbackText.text = _readyToBegin ? "Not Ready" : "Ready";
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

        public void SetRoomPlayer(NetworkRoomPlayer roomPlayer)
        {
            _clientRoomPlayer = roomPlayer;
        }
    }
}
