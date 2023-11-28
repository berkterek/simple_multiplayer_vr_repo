using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleMultiplayerVr.Controllers
{
    public class NetworkUiController : MonoBehaviour
    {
        [SerializeField] Button _hostButton;
        [SerializeField] Button _clientButton;
        [SerializeField] Button _serverButton;

        void OnEnable()
        {
            _hostButton.onClick.AddListener(() => NetworkManager.Singleton.StartHost());
            _clientButton.onClick.AddListener(() => NetworkManager.Singleton.StartClient());
            _serverButton.onClick.AddListener(() => NetworkManager.Singleton.StartServer());
        }

        void OnDisable()
        {
            _hostButton.onClick.RemoveListener(() => NetworkManager.Singleton.StartHost());
            _clientButton.onClick.RemoveListener(() => NetworkManager.Singleton.StartClient());
            _serverButton.onClick.RemoveListener(() => NetworkManager.Singleton.StartServer());
        }
    }    
}