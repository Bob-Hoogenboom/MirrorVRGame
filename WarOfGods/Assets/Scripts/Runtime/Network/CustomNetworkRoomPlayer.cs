using UnityEngine;
using Mirror;

namespace Network
{
    public class CustomNetworkRoomPlayer : NetworkRoomPlayer
    {
        [SerializeField] private GameObject xrRigPrefab;

        private GameObject _clientPlayer;

        private void Start()
        {
            DontDestroyOnLoad(this);
            if (!isLocalPlayer) return;

            if (isClientOnly)
            {
                //you are the client
                _clientPlayer = Instantiate(xrRigPrefab);
            }
            else
            {
                //you are the host
                //TODO: spawn god preview
                _clientPlayer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            }
        }

        private void OnDestroy()
        {
            Destroy(_clientPlayer);
        }
    }
}
