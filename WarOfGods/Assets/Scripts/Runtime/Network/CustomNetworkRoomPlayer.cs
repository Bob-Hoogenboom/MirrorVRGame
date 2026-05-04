using UnityEngine;
using Mirror;
using Network.UI;

namespace Network
{
    public class CustomNetworkRoomPlayer : NetworkRoomPlayer
    {
        [Header("Refernces")]
        [SerializeField] private GameObject xrRigPrefab;
        [Space]
        [SerializeField] private JoinLobbyUI xrUI;

        private GameObject _clientPlayer;


        public override void OnStartClient()
        {
            DontDestroyOnLoad(this);
            if (!isLocalPlayer) return;

            if (isClientOnly)
            {
                //you are the client
                _clientPlayer = Instantiate(xrRigPrefab);
                //turn on Client UI
                xrUI = FindFirstObjectByType<JoinLobbyUI>();

                if (xrUI == null) return;
                xrUI.SetRoomPlayer(this);
            }
            else
            {
                //you are the host
                //TODO: spawn god preview
                _clientPlayer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //turn on Host UI
            }
        }

        private void OnDestroy()
        {
            Destroy(_clientPlayer);
        }
    }
}
