using DG.Tweening;
using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;
    [field:SerializeField] public ResourceSheet Sheet { get; private set; }
    public event Action<ResourceHandler> OnDeactive;
    Rigidbody rb;
    public bool IsThrown { get; set; } = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(Sheet == null)
            Sheet = ResourcesRepository.Instance.GetResourcesRandom();
        AnimateInstanciate();
    }
    private void OnDisable() => OnDeactive?.Invoke(this);
    private void OnEnable()
    {
        transform.localScale = Vector3.one;
        rb.linearVelocity = Vector3.zero;
    }

    public void AnimateJump(Transform positionWagon) => rb.DOJump(positionWagon.position, 1, 1, 0.5f);
    public void AnimateInstanciate() => rb.AddForce((Vector3.up * 3) + (Vector3.right * 2), ForceMode.Impulse);

    public void Throw(Vector3 posInit,Vector3 targetPos, float time, Action actionEnd = null)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(Vector3.zero, 0.1f)).SetEase(Ease.InBack);
        sequence.Append(transform.DOMove(posInit, 0)).SetEase(Ease.InBack);
        sequence.Append(rb.DOJump(targetPos, 5, 1, time * 3).OnComplete(() => actionEnd?.Invoke()));
    }
}