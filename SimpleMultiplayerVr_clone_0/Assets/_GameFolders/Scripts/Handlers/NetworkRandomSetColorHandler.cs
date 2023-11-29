using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

namespace SimpleMultiplayerVr.Handlers
{
    public class NetworkRandomSetColorHandler : NetworkBehaviour
    {
        [SerializeField] List<Renderer> _renderers;
        [SerializeField] NetworkVariable<Color> _networkColor = new NetworkVariable<Color>(Color.blue,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        void SetRendererColor(Color color)
        {
            foreach (var renderer in _renderers)
            {
                renderer.material.color = color;
            }
        }

        public override void OnNetworkSpawn()
        {
            _networkColor.OnValueChanged += HandleOnColorChanged;
            
            if (IsOwner) _networkColor.Value = Random.ColorHSV(0f,1f,1f,1f);

            SetRendererColor(_networkColor.Value);
        }

        public override void OnNetworkDespawn()
        {
            _networkColor.OnValueChanged -= HandleOnColorChanged;
        }

        void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                UpdateColorServerRpc();
            }
        }
        
        void HandleOnColorChanged(Color previousValue, Color newValue)
        {
            SetRendererColor(newValue);
        }

        [ClientRpc]
        void UpdateColorClientRpc()
        {
            if (IsOwner) _networkColor.Value = Random.ColorHSV(0f,1f,1f,1f);
        }

        [ServerRpc(RequireOwnership = false)]
        void UpdateColorServerRpc()
        {
            UpdateColorClientRpc();
        }
    }
}