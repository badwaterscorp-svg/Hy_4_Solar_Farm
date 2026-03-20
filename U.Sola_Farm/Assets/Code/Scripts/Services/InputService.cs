using UnityEngine;
using UnityEngine.InputSystem;

public class InputService : MonoBehaviour, IInputService
{
    [SerializeField] private InputActionAsset _inputAsset;
    
    private InputAction _moveAction;
    
    public Vector2 MoveInput => _moveAction?.ReadValue<Vector2>() ?? Vector2.zero;

    private void Awake()
    {
        var playerMap = _inputAsset.FindActionMap("Player");
        _moveAction = playerMap.FindAction("Move");
    }

    public void Enable()
    {
        _inputAsset.Enable();
    }

    public void Disable()
    {
        _inputAsset.Disable();
    }
}
