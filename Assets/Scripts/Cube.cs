using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLiftime = 2;
    [SerializeField] private int _maxLiftime = 5;

    private Renderer _renderer;

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

    public void DeactivateWithDelay(ObjectPool<Cube> pool)
    {
        _isWaitToBeDestroyed = true;
        int time = Random.Range(_minLiftime, _maxLiftime + 1);
        StartCoroutine(ReleaseWithDelay(time, pool));
    }

    public void TrySetColor(Color color)
    {
        if (_isWaitToBeDestroyed == false)
            _renderer.material.color = color;
    }
}
