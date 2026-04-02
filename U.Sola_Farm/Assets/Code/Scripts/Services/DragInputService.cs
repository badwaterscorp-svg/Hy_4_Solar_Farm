using B_Extensions;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

public class DragInputService : Singleton<DragInputService>, IInputService
{
    [SerializeField] private InputActionAsset _inputAsset;
    
    private InputAction _clickAction;
    private InputAction _pointAction;
    
    private Vector2 _dragStartPosition;
    private bool _isDragging;

    public bool IsDragging => _isDragging;
    public event Action OnDragStarted;
    public event Action OnDragStoped;
    public Vector2 PointPosition => _pointAction.ReadValue<Vector2>();
    public Vector2 DragStartPosition => _dragStartPosition;

    InputActionMap uiMap;
    private new void Awake()
    {
        base.Awake();
        uiMap = _inputAsset.FindActionMap("UI");
        _pointAction = uiMap.FindAction("Point");
        
    }

    private void OnEnable() 
    {
        _clickAction = uiMap.FindAction("Click");
        _clickAction.started += OnClickPerformed;
        _clickAction.performed += OnClickPerformed;
        Enable();
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            _dragStartPosition = PointPosition;
            _isDragging = true;
            OnDragStarted?.Invoke();
        }
        else
        {
            _isDragging = false;
            OnDragStoped?.Invoke();
        }
    }



    public void Enable() => _inputAsset.Enable();

    public void Disable() => _inputAsset.Disable();

    private void OnDisable()
    {
        _clickAction.performed -= OnClickPerformed;
        _clickAction.started += OnClickPerformed;
        Disable();
    }
}


public class DragReadUIService : MonoBehaviour
{
    [SerializeField] private Image _initPosImage;
    [SerializeField] private Image _currentPosImage;

    [Inject] private IInputService _inputService;
    private void Update()
    {
        if (_inputService.IsDragging)
        {
            Vector2 currentPos = _inputService.PointPosition;
            Vector2 dragDir = currentPos - _inputService.DragStartPosition;
            float strength = dragDir.magnitude;
            print($"Dragging: Direction={dragDir}, Strength={strength}");
        }
    }
}