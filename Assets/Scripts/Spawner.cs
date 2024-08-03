using UnityEngine;
using UnityEngine.Pool;

abstract public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<T> _pool;

    public ObjectPool<T> Pool
    {
        get { return _pool; }
        private set {}
    }

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => ActionOnCreate(_prefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => ActionOnDestroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    abstract public T ActionOnCreate(T prefab);

    abstract public void ActionOnGet(T obj);

    abstract public void ActionOnDestroy(T obj);
}