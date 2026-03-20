using B_Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragInputService : Singleton<DragInputService>, IInputService
{
    [SerializeField] private InputActionAsset _inputAsset;
    
    private InputAction _moveAction;
    
    public Vector2 MoveInput => _moveAction?.ReadValue<Vector2>() ?? Vector2.zero;

    private new void Awake()
    {
        base.Awake();
        var playerMap = _inputAsset.FindActionMap("UI");
        _moveAction = playerMap.FindAction("Point");
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
