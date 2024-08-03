using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private float _repeatRate = 1f;

    public event Action<Vector3> CubeDestroyed;

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void GetCube()
    {
        Pool.Get();
    }

    public override void ActionOnGet(Cube cube)
    {
        cube.transform.position = _startPoint.transform.position + Random.insideUnitSphere * 5;
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        cube.GetComponent<Renderer>().material.color = Color.red;
        cube.gameObject.SetActive(true);
    }

    public override Cube ActionOnCreate(Cube prefab)
    {
        Cube cube = Instantiate(prefab);
        cube.Waited += OnCubeWaited;
        return cube;
    }

    private void OnCubeWaited(Cube cube)
    {
        Pool.Release(cube);
        CubeDestroyed?.Invoke(cube.transform.position);
    }

    public override void ActionOnDestroy(Cube cube)
    {
        cube.Waited -= OnCubeWaited;
    }
}