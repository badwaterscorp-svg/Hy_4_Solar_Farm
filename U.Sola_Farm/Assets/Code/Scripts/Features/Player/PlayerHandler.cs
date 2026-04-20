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

    private PlayerThrowResources _playerThrowResources;
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
        _playerThrowResources = new PlayerThrowResources();
    }

    private void OnEnable()
    {
        _inputService.OnDragStarted += OnDragStarted;
        _inputService.OnDragStoped += OnDragStoped;
        _triggerDetector.OnTriggerEntered += OnTriggerEntered;
    }


    private void OnDisable()
    {
        _inputService.OnDragStarted -= OnDragStarted;
        _inputService.OnDragStoped -= OnDragStoped;
        _triggerDetector.OnTriggerEntered -= OnTriggerEntered;
    }

    private void OnTriggerEntered(Transform other)
    {
        if (_backPackHandler.IsBackPackFull())
            return;

        if (other.TryGetComponent<ResourceHandler>(out var ss))
        { 
            if(!ss.IsThrown)
                other.DOMove(transform.position, 0.3f).OnComplete(() => AddResource(ss));
        }
    }
    public BackPackHandler AccessBackPackHandler() => _backPackHandler;
    public void ThrowResource(ResourceModel _model, Transform otherPos,float time) 
    {
        var handler = GetResources(_model);
        handler.IsThrown = true;
        handler.transform.position = transform.position + (Vector3.up*3);
        _playerThrowResources.ThrowResources(handler, otherPos, () => ReleaseResource(handler),time,transform);
    } 

    public bool AddResource(ResourceHandler resource)
    {
        bool added = _backPackHandler.AddResource(resource.Sheet.Model.Copy());
        if (!added)
            transform.DOKill();
        else
            ReleaseResource(resource);
        return added;
    }

    #region TODO
    // TODO decirle a la IA que lo aisle en un script diferente

    private ResourceHandler GetResources(ResourceModel model) 
    {
        if (model.Name.Equals("Water"))
            return _poolWater.Get();
        else if (model.Name.Equals("Solar Energy"))
            return _poolEnergy.Get();
        return null;
    }

    private void ReleaseResource(ResourceHandler resource)
    {
        if (resource.Sheet.Model.Name.Equals("Water"))
            _poolWater.Release(resource);
        else if (resource.Sheet.Model.Name.Equals("Solar Energy"))
            _poolEnergy.Release(resource);
    }
    #endregion

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



public class PlayerThrowResources
{
    public void ThrowResources(ResourceHandler resource, Transform throwPoint, Action onEnd, float time,Transform initPoint)
    {
        resource.AnimateJump(initPoint.position,throwPoint.position,time, onEnd);
    }
}
