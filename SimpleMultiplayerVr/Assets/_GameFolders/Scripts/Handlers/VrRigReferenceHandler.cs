using Cysharp.Threading.Tasks;
using UnityEngine;

namespace SimpleMultiplayerVr.Handlers
{
    public class VrRigReferenceHandler : MonoBehaviour
    {
        [SerializeField] Transform _root;
        [SerializeField] Transform _head;
        [SerializeField] Transform _rightHand;
        [SerializeField] Transform _leftHand;

        public static VrRigReferenceHandler Instance { get; private set; }

        public Transform Root => _root;
        public Transform Head => _head;
        public Transform RightHand => _rightHand;
        public Transform LeftHand => _leftHand;

        void Awake()
        {
            Instance = this;
        }

        async void Start()
        {
            await UniTask.Delay(2000);
            
            _rightHand.gameObject.SetActive(true);
            _leftHand.gameObject.SetActive(true);
        }
    }    
}

