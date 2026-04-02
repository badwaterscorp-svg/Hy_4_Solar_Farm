using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ResourceHandler : MonoBehaviour
{
    public ResourceSheet Sheet { get; private set; }
    Rigidbody rb;
    public Transform CarryPosition { get; set; }
    bool previouseHasCarry;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Sheet = ResourcesRepository.Instance.GetResourcesRandom();
        AnimateInstanciate();
    }

    private void RenderDeco() 
    {
        //RENDER SPRITES
    }

    private void Update()
    {
        if (previouseHasCarry && CarryPosition == null)
        {
            transform.position = transform.position + Vector3.up*0.5f;
            previouseHasCarry = false;
        }

        if(CarryPosition != null)
        { 
            transform.position = CarryPosition.position;
            previouseHasCarry = true;
        }
    }

    public void AnimateJump(Transform positionWagon) => transform.DOJump(positionWagon.position, 1, 1, 0.5f);

    public void AnimateInstanciate()
    {
        //transform.position = transform.position + Vector3.up;
        rb.AddForce((Vector3.up * 3) + (Vector3.right * Random.Range(-2, 2)), ForceMode.Impulse);
    }
}
