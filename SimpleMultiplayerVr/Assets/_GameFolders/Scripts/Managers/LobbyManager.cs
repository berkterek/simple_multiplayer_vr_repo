using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;

namespace SimpleMultiplayerVr.Managers
{
    public class LobbyManager : MonoBehaviour
    {
        const string LOBBY_DATA_KEY = "JoinCodeKey";
        
        public static LobbyManager Instance { get; private set; }

        void Awake()
        {
            Instance = this;
        }

        public async void CreateLobbyAsync(LobbyData lobbyData)
        {
            try
            {
                CreateLobbyOptions createLobbyOptions = new CreateLobbyOptions
                {
                    IsPrivate = false,
                    Data = new Dictionary<string, DataObject>()
                };

                string joinCode = await RelayManager.Instance.CreateRelayGameAsync(lobbyData.MaxPlayer);
                Debug.Log($"<color=red>{joinCode}</color>");
                DataObject dataObject = new DataObject(DataObject.VisibilityOptions.Public, joinCode);
                createLobbyOptions.Data.Add(joinCode, dataObject);
                
                await Lobbies.Instance.CreateLobbyAsync(lobbyData.LobbyName, lobbyData.MaxPlayer, createLobbyOptions);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{e}: {e.Message}");
            }
        }
        
        public async void QuickJoinLobbyAsync()
        {
            try
            {
                Lobby lobby = await Lobbies.Instance.QuickJoinLobbyAsync();
                string relayJoinCode = lobby.Data[LOBBY_DATA_KEY].Value;
                Debug.Log($"<color=red>{relayJoinCode}</color>");
                
                RelayManager.Instance.JoinRelayGame(relayJoinCode);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{e}: {e.Message}");
            }
        }
        
        public async void JoinLobbyAsync(string lobbyId)
        {
            try
            {
                Lobby lobby = await Lobbies.Instance.JoinLobbyByIdAsync(lobbyId);
                string relayJoinCode = lobby.Data[LOBBY_DATA_KEY].Value;
                
                RelayManager.Instance.JoinRelayGame(relayJoinCode);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"{e}: {e.Message}");
            }
        }
    }

    public struct LobbyData
    {
        public string LobbyName;
        public int MaxPlayer;
    }
}