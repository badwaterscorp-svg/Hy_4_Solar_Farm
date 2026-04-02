using UnityEngine;
using UnityEngine.Pool;
using B_Extensions;

public class ResourcePoolService : Singleton<ResourcePoolService>, IResourcePoolService
{
    [SerializeField] private ResourceHandler _prefab;
    [SerializeField] private Transform _parent;
    
    private ObjectPool<ResourceHandler> _pool;

    public int CountActive => _pool.CountActive;
    public int CountInactive => _pool.CountInactive;

    private new void Awake()
    {
        base.Awake();
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
        ResourceHandler handler = UnityEngine.Object.Instantiate(_prefab, _parent);
        handler.gameObject.SetActive(false);
        return handler;
    }

    private void OnGet(ResourceHandler handler)
    {
        handler.gameObject.SetActive(true);
    }

    private void OnRelease(ResourceHandler handler)
    {
        handler.gameObject.SetActive(false);
    }

    private void OnDestroyItem(ResourceHandler handler)
    {
        UnityEngine.Object.Destroy(handler.gameObject);
    }

    public ResourceHandler Get()
    {
        return _pool.Get();
    }

    public void Release(ResourceHandler handler)
    {
        _pool.Release(handler);
    }
}
