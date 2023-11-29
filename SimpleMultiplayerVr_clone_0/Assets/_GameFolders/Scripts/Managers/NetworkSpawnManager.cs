using Unity.Netcode;
using UnityEngine;

namespace SimpleMultiplayerVr.Managers
{
    public class NetworkSpawnManager : NetworkBehaviour
    {
        [SerializeField] Transform _player;
        [SerializeField] Transform[] _spawnPositions;
        [SerializeField] NetworkVariable<int> _networkIndex = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);

        public void SetPlayerPosition()
        {
            int index = _networkIndex.Value;
            _player.position = _spawnPositions[index].position;
            _player.rotation = _spawnPositions[index].rotation;
        }

        void Start()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += HandleOnClientConnectedCallback;
            NetworkManager.Singleton.OnServerStarted += HandleOnServerStarted;
        }

        void HandleOnServerStarted()
        {
            if (IsServer)
            {
                _networkIndex.Value++;
                if (_networkIndex.Value == _spawnPositions.Length)
                {
                    _networkIndex.Value = 0;
                }
            }
        }

        void HandleOnClientConnectedCallback(ulong clientId)
        {
            if (clientId == NetworkManager.Singleton.LocalClient.ClientId)
            {
                SetPlayerPosition();
            }
            
            if (IsServer)
            {
                _networkIndex.Value++;
                if (_networkIndex.Value == _spawnPositions.Length)
                {
                    _networkIndex.Value = 0;
                }
            }
        }
    }    
}

