using DG.Tweening;
using System;
using System.Drawing;
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
    }
    private void OnDisable() => OnDeactive?.Invoke(this);
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
    }

    public void AnimateJump(Vector3 posInit, Vector3 targetPos, float time, Action actionEnd = null)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(Vector3.zero, 0)).SetEase(Ease.InBack);
        sequence.Append(transform.DOMove(posInit, 0)).SetEase(Ease.InBack);
        sequence.Append(transform.DOScale(Vector3.one, 0.1f)).SetEase(Ease.InBack);
        sequence.Append(rb.DOJump(targetPos, 4, 1, time *1.1f).OnComplete(() => actionEnd?.Invoke()));
    }
}