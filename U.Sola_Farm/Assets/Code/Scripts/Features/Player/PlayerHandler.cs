using B_Extensions;
using DG.Tweening;
using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerHandler : Singleton<PlayerHandler>
{
    [SerializeField] private PlayerModel _model;
    [SerializeField] private BackPackHandler _backPackHandler;
    [SerializeField] private TriggerDetector _triggerDetector;

    private PlayerMovement _movement;
    private bool _isDragging;
    private Rigidbody _rb;

    [Inject] private IInputService _inputService;
    [Inject(Id = "Water")] IResourcePoolService _poolWater;
    [Inject(Id = "SolarEnergy")] IResourcePoolService _poolEnergy;

    private new void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _movement = new PlayerMovement(_model, transform, _rb);
    }

    private void OnEnable()
    {
        _inputService.OnDragStarted += OnDragStarted;
        _inputService.OnDragStoped += OnDragStoped;
        _triggerDetector.OnTriggerEntered += OnTriggerEntered;
    }

    private void OnTriggerEntered(Transform other)
    {
        if (_backPackHandler.IsBackPackFull())
            return;

        if (other.TryGetComponent<ResourceHandler>(out var ss))
            other.DOMove(transform.position, 0.3f).OnComplete(() => AddResource(ss));
    }

    private void OnDisable()
    {
        _inputService.OnDragStarted -= OnDragStarted;
        _inputService.OnDragStoped -= OnDragStoped;
        _triggerDetector.OnTriggerEntered -= OnTriggerEntered;
    }

    public BackPackHandler AccessBackPackHandler() => _backPackHandler;

    public bool AddResource(ResourceHandler resource)
    {
        bool added = _backPackHandler.AddResource(resource.Sheet.Model.Copy());
        if (!added)
            transform.DOKill();
        else
        {
            if(resource.Sheet.Model.Name.Equals("Water"))
                _poolWater.Release(resource);
            else if(resource.Sheet.Model.Name.Equals("Solar Energy"))
                _poolEnergy.Release(resource);
        }
        return added;
    }

    private void OnDragStoped() => _isDragging = false;

    private void OnDragStarted() => _isDragging = true;

    private void FixedUpdate()
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
                _rb.MoveRotation(targetRotation);
            }
            _movement.Move(dragDir, strength);
        }
    }
}
