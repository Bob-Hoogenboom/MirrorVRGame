using UnityEngine;
using Mirror;

namespace Network
{
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private GameObject[] avatars;
        [SerializeField] private int myAvatarIndex;

        System.Action initialization;

        private void Start()
        {
            if (isLocalPlayer)
            {
                CmdPlayerInitializationOnServer(myAvatarIndex);
            }
        }

        [Command]
        public void CmdPlayerInitializationOnServer(int avatarIndex)
        {
            myAvatarIndex = avatarIndex;
            initialization?.Invoke();
        }

        public void OnInitializeOnServer(System.Action serverInitialization)
        {
            initialization = serverInitialization;
        }

        public void LoadPlayerToAllClients()
        {
            RPCLoadPlayerToAllClients(myAvatarIndex);
        }

        [ClientRpc]
        public void RPCLoadPlayerToAllClients(int avatarIndex)
        {
            InitializePlayer(avatarIndex);
        }


        void InitializePlayer (int avatarIndex)
        {
            GameObject plyr = Instantiate(avatars[avatarIndex], transform);
            plyr.transform.localPosition = plyr.transform.localEulerAngles =Vector3.zero;
        }
    }
}
