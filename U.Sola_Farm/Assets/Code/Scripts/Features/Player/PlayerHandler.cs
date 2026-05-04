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
    [SerializeField] private bool freezeMovement;
    [SerializeField] private Animator _animator;
    private PlayerThrowResources _playerThrowResources;
    private PlayerMovement _movement;
    private bool _isDragging;
    private Rigidbody _rb;

    [Inject] private IInputService _inputService;

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
        var handler = ResourcesPoolManager.Instance.GetResources(_model);
        handler.IsThrown = true;
        handler.transform.position = transform.position + (Vector3.up*3);
        _playerThrowResources.ThrowResources(handler, otherPos, () => ResourcesPoolManager.Instance.ReleaseResource(handler),time,transform);
    } 

    public bool AddResource(ResourceHandler resource)
    {
        bool added = _backPackHandler.AddResource(resource.Sheet.Model.Copy());
        if (!added)
            transform.DOKill();
        else
            ResourcesPoolManager.Instance.ReleaseResource(resource);
        return added;
    }

    private void OnDragStoped() => _isDragging = false;

    private void OnDragStarted() => _isDragging = true;

    public void SetFreezeMovement(bool freeze) => freezeMovement = freeze;

    public void StartGame()
    {
        GameStateContext.ChangeState(GameEventType.GameStarted);
        _animator.SetBool("Started", true);
    }

    private void FixedUpdate()
    {
        if (freezeMovement)
            return;

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
            _animator.SetFloat("Speed", strength);
        }
        else
            _animator.SetFloat("Speed",0);
    }
}



public class PlayerThrowResources
{
    public void ThrowResources(ResourceHandler resource, Transform throwPoint, Action onEnd, float time,Transform initPoint)
    {
        resource.AnimateJump(initPoint.position,throwPoint.position,time, onEnd);
    }
}
