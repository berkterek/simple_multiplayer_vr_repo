using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
#if UNITY_EDITOR
using ParrelSync;
#endif

namespace SimpleMultiplayerVr.Managers
{
    public class AuthenticationManager : MonoBehaviour
    {
        void Awake()
        {
            Login();
        }

        async void Login()
        {
            InitializationOptions initializationOptions = new InitializationOptions();

#if UNITY_EDITOR
            if (ClonesManager.IsClone())
            {
                initializationOptions.SetProfile(ClonesManager.GetArgument());
            }
            else
            {
                initializationOptions.SetProfile("primary");
            }
#endif            
            
            await UnityServices.InitializeAsync(initializationOptions);
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
}