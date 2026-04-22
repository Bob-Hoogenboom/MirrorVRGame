using Mirror;
using TMPro;
using UnityEngine;

namespace Network.UI
{
    public class ConnectionTracker : MonoBehaviour
    {
        [SerializeField] private TMP_Text loadingText;
        [SerializeField] private float timeout = 10f;

        private float connectTimer;
        private bool tryingToConnect;


        public void StartConnecting()
        {
            tryingToConnect = true;
            connectTimer = 0f;
        }

        void Update()
        {
            if (tryingToConnect && NetworkClient.isConnecting)
            {
                connectTimer += Time.deltaTime;

                if (connectTimer > timeout)
                {
                    Debug.Log("Connection timed out");
                    tryingToConnect = false;

                    NetworkClient.Disconnect();
                }
            }
        }

        void OnEnable()
        {
            NetworkClient.OnConnectedEvent += OnConnected;
            NetworkClient.OnDisconnectedEvent += OnDisconnected;
        }

        void OnDisable()
        {
            NetworkClient.OnConnectedEvent -= OnConnected;
            NetworkClient.OnDisconnectedEvent -= OnDisconnected;
        }

        void OnConnected()
        {
            tryingToConnect = false;
            Debug.Log("Connected");
            loadingText.color = Color.green;
            loadingText.text = "Connected!";


        }

        void OnDisconnected()
        {
            if (tryingToConnect)
            {
                Debug.Log("Failed to connect (timeout or refused)");
                loadingText.color = Color.red;
                loadingText.text = "Connection Timed out";
            }
            else
            {
                Debug.Log("Disconnected after being connected");
                loadingText.color = Color.orange;
                loadingText.text = "Connection was made but ultimately lost again";
            }

            tryingToConnect = false;
        }
    }
}

