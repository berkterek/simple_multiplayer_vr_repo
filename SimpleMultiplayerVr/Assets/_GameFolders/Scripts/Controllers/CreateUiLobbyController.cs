using SimpleMultiplayerVr.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleMultiplayerVr.Controllers
{
    public class CreateUiLobbyController : MonoBehaviour
    {
        [SerializeField] TMP_InputField _lobbyNameInputField;
        [SerializeField] Slider _maxPlayerSlider;
        [SerializeField] Button _createButton;

        void OnEnable()
        {
            _createButton.onClick.AddListener(HandleOnButtonClicked);
        }

        void OnDisable()
        {
            _createButton.onClick.RemoveListener(HandleOnButtonClicked);
        }
        
        void HandleOnButtonClicked()
        {
            LobbyData lobbyData = new LobbyData()
            {
                LobbyName = _lobbyNameInputField.text,
                MaxPlayer = (int)_maxPlayerSlider.value
            };
            
            LobbyManager.Instance.CreateLobbyAsync(lobbyData);
        }
    }    
}

