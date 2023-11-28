using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class NetworkHandAnimation : NetworkBehaviour
{
    [SerializeField] InputActionReference _gripAction;
    [SerializeField] InputActionReference _pinchAction;
    [SerializeField] Animator _animator;

    private void OnValidate() => GetReference();

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) return;
        
        _gripAction.action.performed += Gripping;
        _gripAction.action.canceled += Gripping;

        _pinchAction.action.performed += Pinching;
        _pinchAction.action.canceled += Pinching;
    }

    public override void OnNetworkDespawn()
    {
        if(!IsOwner) return;
        
        _gripAction.action.performed -= Gripping;
        _gripAction.action.canceled -= Gripping;

        _pinchAction.action.performed -= Pinching;
        _pinchAction.action.canceled -= Pinching;
    }

    private void Gripping(InputAction.CallbackContext obj) => _animator.SetFloat("Grip", obj.ReadValue<float>());

    private void Pinching(InputAction.CallbackContext obj) => _animator.SetFloat("Pinch", obj.ReadValue<float>());

    private void GetReference()
    {
        if (_animator == null) _animator = GetComponent<Animator>();
    }
}