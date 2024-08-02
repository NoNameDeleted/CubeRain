using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLiftime = 2;
    [SerializeField] private int _maxLiftime = 5;

    private Renderer _renderer;
    private Spawner _spawner;

    private bool _isWaitToBeDestroyed = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private IEnumerator ReleaseWithDelay(int delay, ObjectPool<Cube> pool)
    {
        yield return new WaitForSeconds(delay);
        _isWaitToBeDestroyed = false;
        pool.Release(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<Ground>(out Ground ground))
        {
            if (_isWaitToBeDestroyed) return;
            _isWaitToBeDestroyed = true;
            _renderer.material.color = Random.ColorHSV();
            _spawner.DeactivateCube(this);
        }
    }

    public void Init(Spawner spawner)
    {
        _spawner = spawner;
    }

    public void DeactivateWithDelay(ObjectPool<Cube> pool)
    {
        int time = Random.Range(_minLiftime, _maxLiftime + 1);
        StartCoroutine(ReleaseWithDelay(time, pool));
    }
}