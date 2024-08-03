using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => CreateCube(_prefab),
            actionOnGet: (obj) => GetCube(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => DestroyCube(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void GetCube(Cube cube)
    {
        cube.transform.position = _startPoint.position + Random.insideUnitSphere * 5;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.GetComponent<Renderer>().material.color = Color.red;
        cube.gameObject.SetActive(true);
    }

    private Cube CreateCube(Cube prefab)
    {
        Cube cube = Instantiate(prefab);
        cube.Waited += _pool.Release;
        return cube;
    }

    private void DestroyCube(Cube cube)
    {
        cube.Waited -= _pool.Release;
        Destroy(cube);
    }

    private void GetCube()
    {
        _pool.Get();
    }
}