using DG.Tweening;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private CollisionDetector _collisionDetector;
    public ResourceSheet Sheet { get; private set; }
    public event System.Action<ResourceHandler> OnDeactive;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Sheet = ResourcesRepository.Instance.GetResourcesRandom();
        AnimateInstanciate();
    }

    private void OnEnable()
    {
        //_collisionDetector.OnCollisionEntered += OnResourceCollision;
    }

    private void OnDisable()
    {
        OnDeactive?.Invoke(this);
        //_collisionDetector.OnCollisionEntered -= OnResourceCollision;
    }

    private void OnResourceCollision(Collision other)
    {
        PlayerHandler player = other.gameObject.GetComponent<PlayerHandler>();
        if (player != null)
        {
            bool accept = player.AddResource(this);
            print("Resource accepted: " + accept);
        }
        else
            print("No Collided with: " + other.gameObject.name);
    }


    public void AnimateJump(Transform positionWagon) => transform.DOJump(positionWagon.position, 1, 1, 0.5f);

    public void AnimateInstanciate()
    {
        //transform.position = transform.position + Vector3.up;
        rb.AddForce((Vector3.up * 3) + (Vector3.right * 2), ForceMode.Impulse);
    }
}
