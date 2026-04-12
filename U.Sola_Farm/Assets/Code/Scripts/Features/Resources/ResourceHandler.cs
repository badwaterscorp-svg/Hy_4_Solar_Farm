using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;
    [field:SerializeField] public ResourceSheet Sheet { get; private set; }
    public event System.Action<ResourceHandler> OnDeactive;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if(Sheet == null)
            Sheet = ResourcesRepository.Instance.GetResourcesRandom();
        AnimateInstanciate();
    }

    private void OnDisable() => OnDeactive?.Invoke(this);

    public void AnimateJump(Transform positionWagon) => transform.DOJump(positionWagon.position, 1, 1, 0.5f);

    public void AnimateInstanciate()
    {
        rb.AddForce((Vector3.up * 3) + (Vector3.right * 2), ForceMode.Impulse);
    }
}