using SimpleMultiplayerVr.Handlers;
using Unity.Netcode;
using UnityEngine;

namespace SimpleMultiplayerVr.Controllers
{
    public class NetworkPlayerController : NetworkBehaviour
    {
        [SerializeField] Transform _root;
        [SerializeField] Transform _head;
        [SerializeField] Transform _rightHand;
        [SerializeField] Transform _leftHand;

        void Update()
        {
            if (!IsOwner) return;
            
            _root.position = VrRigReferenceHandler.Instance.Root.position;
            _root.rotation = VrRigReferenceHandler.Instance.Root.rotation;
            
            _head.position = VrRigReferenceHandler.Instance.Head.position;
            _head.rotation = VrRigReferenceHandler.Instance.Head.rotation;
            
            _rightHand.position = VrRigReferenceHandler.Instance.RightHand.position;
            _rightHand.rotation = VrRigReferenceHandler.Instance.RightHand.rotation;
            
            _leftHand.position = VrRigReferenceHandler.Instance.LeftHand.position;
            _leftHand.rotation = VrRigReferenceHandler.Instance.LeftHand.rotation;
        }
    }
}