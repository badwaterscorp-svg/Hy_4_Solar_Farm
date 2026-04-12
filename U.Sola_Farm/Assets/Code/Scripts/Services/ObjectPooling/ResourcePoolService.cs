using UnityEngine;
using UnityEngine.Pool;
using B_Extensions;

public class ResourcePoolService : MonoBehaviour, IResourcePoolService
{
    [SerializeField] private ResourceSheet _prototype;
    [SerializeField] private Transform _parent;
    
    private ObjectPool<ResourceHandler> _pool;

    public int CountActive => _pool.CountActive;
    public int CountInactive => _pool.CountInactive;

    private void Awake()
    {
        _pool = new ObjectPool<ResourceHandler>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,
            defaultCapacity: 10,
            maxSize: 100
        );
        
    }

    private ResourceHandler CreateItem()
    {
        var path = System.IO.Path.Combine("Prototypes",_prototype.Path);
        var res = Resources.Load<ResourceHandler>(path);
        ResourceHandler handler = Instantiate(res, _parent);
        handler.gameObject.SetActive(false);
        //Resources.UnloadAsset(res);
        return handler;
    }

    private void OnGet(ResourceHandler handler) => handler.gameObject.SetActive(true);

    private void OnRelease(ResourceHandler handler) => handler.gameObject.SetActive(false);

    private void OnDestroyItem(ResourceHandler handler) => Destroy(handler.gameObject);

    public ResourceHandler Get() => _pool.Get();

    public void Release(ResourceHandler handler) => _pool.Release(handler);
}