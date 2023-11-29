using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace SimpleMultiplayerVr.Handlers
{
    public class NetworkRandomSetColorHandler : NetworkBehaviour
    {
        [SerializeField] List<Renderer> _renderers;
        [SerializeField] NetworkVariable<Color> _networkColor = new NetworkVariable<Color>(Color.blue,NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        public void SetRendererColor(Color color)
        {
            foreach (var renderer in _renderers)
            {
                renderer.material.color = color;
            }
        }

        public override void OnNetworkSpawn()
        {
            _networkColor.OnValueChanged += HandleOnColorChanged;
            
            if (IsOwner) _networkColor.Value = Random.ColorHSV();

            SetRendererColor(_networkColor.Value);
        }

        public override void OnNetworkDespawn()
        {
            _networkColor.OnValueChanged -= HandleOnColorChanged;
        }

        void Update()
        {
            
        }
        
        void HandleOnColorChanged(Color previousValue, Color newValue)
        {
            SetRendererColor(newValue);
        }
    }
}