using System;
using UnityEngine;
using Zenject;

public class PlayerHandler : MonoBehaviour
{
    [SerializeField] private PlayerModel _model;
    [SerializeField] private CollisionDetector _collisionDetector;
    
    private IInputService _inputService;
    private IResourceInventoryService _inventoryService;
    private PlayerMovement _movement;
    private bool _isDragging;

    [Inject]
    public void Initialize(IInputService inputService, IResourceInventoryService inventoryService)
    {
        _inputService = inputService;
        _inventoryService = inventoryService;
        _movement = new PlayerMovement(_model, transform);
        _inputService.OnDragStarted += OnDragStarted;
        _inputService.OnDragStoped += OnDragStoped;
        _collisionDetector.OnCollisionEntered += OnResourceCollision;
    }

    private void OnDisable()
    {
        _inputService.OnDragStarted -= OnDragStarted;
        _inputService.OnDragStoped -= OnDragStoped;
        if (_collisionDetector != null)
            _collisionDetector.OnCollisionEntered -= OnResourceCollision;
    }

    private void OnResourceCollision(Collision other)
    {
        ResourceHandler resource = other.gameObject.GetComponent<ResourceHandler>();
        if (resource != null)
        {
            _inventoryService.AddResource(resource.Sheet.GetModelCopy());
        }
    }

    private void OnDragStoped()
    {
        _isDragging = false;
    }

    private void OnDragStarted() => _isDragging = true;

    private void Update()
    {
        if (_isDragging)
        {
            Vector2 currentPos = _inputService.PointPosition;
            Vector2 dragDir = currentPos - _inputService.DragStartPosition;
            float strength = dragDir.magnitude;
            
            if (dragDir.sqrMagnitude > 0.01f)
            {
                Vector3 lookDirection = new Vector3(dragDir.x, 0, dragDir.y);
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = targetRotation;
            }
            
            _movement.Move(dragDir, strength);
        }
    }
}
