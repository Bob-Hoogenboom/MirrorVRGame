using UnityEngine;
using Mirror;

namespace Network
{
    public class BattleNetworkManager : NetworkManager
    {
        NetworkConnectionToClient currentJoinedPlayer;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);

            currentJoinedPlayer = conn;

            conn.identity.GetComponent<NetworkPlayer>().OnInitializeOnServer(OnPlayerInitialization);
        }

        private void OnPlayerInitialization()
        {
            currentJoinedPlayer.identity.GetComponent<NetworkPlayer>().LoadPlayerToAllClients();
        }
    }
}
