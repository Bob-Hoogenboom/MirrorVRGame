using Mirror;
using UnityEngine;

namespace Network.UI
{
    public class HostMenuUI : MonoBehaviour
    {
        [SerializeField] private NetworkManager netManager;

        public void Start()
        {
            netManager = FindAnyObjectByType<NetworkManager>();
            if (netManager == null) this.enabled = false;
        }

        public void ServerAndClientButton()
        {
            netManager.StartHost();
        }

        public void ServerOnlyButton()
        {
            netManager.StartServer();
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
