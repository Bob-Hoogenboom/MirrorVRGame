using UnityEngine;
using Mirror;

namespace Network
{
    public class CustomNetworkRoomManager : NetworkRoomManager
    {
        public override void OnRoomClientExit()
        {
            base.OnRoomClientExit();
            //unready all Clients*
        }
    }
}
