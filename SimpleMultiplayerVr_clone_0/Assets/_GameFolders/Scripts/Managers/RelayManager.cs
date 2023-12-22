using System;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

namespace SimpleMultiplayerVr.Managers
{
    public class RelayManager : MonoBehaviour
    {
        [SerializeField] UnityTransport _unityTransport;

        public static RelayManager Instance { get; private set; }

        void OnValidate()
        {
            if (_unityTransport == null) _unityTransport = GetComponent<UnityTransport>();
        }

        void Awake()
        {
            Instance = this;
        }

        public void CreateRelayGame(int maxPlayer)
        {
            TryCatchMethod(async () =>
            {
                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayer);
                string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                Debug.Log("The join code => " + joinCode);

                _unityTransport.SetHostRelayData
                (
                    allocation.RelayServer.IpV4,
                    (ushort)allocation.RelayServer.Port,
                    allocation.AllocationIdBytes,
                    allocation.Key,
                    allocation.ConnectionData
                );

                NetworkManager.Singleton.StartHost();
            });
        }

        public async UniTask<string> CreateRelayGameAsync(int maxPlayer)
        {
            try
            {
                Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayer);
                string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                Debug.Log("The join code => " + joinCode);

                _unityTransport.SetHostRelayData
                (
                    allocation.RelayServer.IpV4,
                    (ushort)allocation.RelayServer.Port,
                    allocation.AllocationIdBytes,
                    allocation.Key,
                    allocation.ConnectionData
                );

                NetworkManager.Singleton.StartHost();
                return joinCode;
            }
            catch (Exception e)
            {
                Debug.LogError($"{e}: {e.Message}");
            }

            return null;
        }

        public void JoinRelayGame(string joinCode)
        {
            TryCatchMethod(async () =>
            {
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);
                _unityTransport.SetClientRelayData
                (
                    joinAllocation.RelayServer.IpV4,
                    (ushort)joinAllocation.RelayServer.Port,
                    joinAllocation.AllocationIdBytes,
                    joinAllocation.Key,
                    joinAllocation.ConnectionData,
                    joinAllocation.HostConnectionData
                );
                
                NetworkManager.Singleton.StartClient();
            });
        }

        private void TryCatchMethod(System.Action insideTryCallback, System.Action insideCatchCallback = null)
        {
            try
            {
                insideTryCallback?.Invoke();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
                Debug.LogError(e.Message);
                insideCatchCallback?.Invoke();
            }
        }
    }
}