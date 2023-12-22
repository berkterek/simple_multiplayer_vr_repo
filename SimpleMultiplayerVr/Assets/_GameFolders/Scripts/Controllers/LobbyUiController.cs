using Cysharp.Threading.Tasks;
using SimpleMultiplayerVr.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleMultiplayerVr.Controllers
{
    public class LobbyUiController : MonoBehaviour
    {
        [SerializeField] Button _quickJoinButton;
        [SerializeField] Button _createLobbyButton;
        [SerializeField] Button _joinLobbyButton;
        [SerializeField] GameObject[] _panels;

        async void OnEnable()
        {
            AuthenticationManager.SignedIn += HandleOnSignedIn; 
            _createLobbyButton.onClick.AddListener(() => UiEnable(2));
            _joinLobbyButton.onClick.AddListener(() => UiEnable(3));
            await UniTask.WaitUntil(() => LobbyManager.Instance != null);
            _quickJoinButton.onClick.AddListener(() => LobbyManager.Instance.QuickJoinLobbyAsync());
        }

        void OnDisable()
        {
            AuthenticationManager.SignedIn -= HandleOnSignedIn;
        }
        
        void HandleOnSignedIn()
        {
            UiEnable(1);
        }

        void UiEnable(int index)
        {
            int length = _panels.Length;
            for (int i = 0; i < length; i++)
            {
                _panels[i].SetActive(i == index);
            }
        }
    }
}

