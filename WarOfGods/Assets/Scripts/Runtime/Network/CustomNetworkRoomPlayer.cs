using UnityEngine;
using Mirror;

namespace Network
{
    public class CustomNetworkRoomPlayer : NetworkRoomPlayer
    {
        [Header("Refernces")]
        [SerializeField] private GameObject xrRigPrefab;

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

            }
            else
            {
                //you are the host
                //TODO: spawn god preview
                _clientPlayer = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //turn on Hosy UI
            }
        }

        private void OnDestroy()
        {
            Destroy(_clientPlayer);
        }
    }
}
