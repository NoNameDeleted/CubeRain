using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => ActionOnCreate(_prefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void ActionOnGet(Cube obj)
    {
        obj.transform.position = _startPoint.transform.position + Random.insideUnitSphere * 5;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Renderer>().material.color = Color.red;
        obj.gameObject.SetActive(true);
    }

    private Cube ActionOnCreate(Cube prefab)
    {
        Cube cube = Instantiate(prefab);
        cube.Init(this);
        return cube;
    }

    private void GetCube()
    {
        _pool.Get();
    }

    public void DeactivateCube(Cube cube)
    {
        cube.DeactivateWithDelay(_pool);
    }
}