using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _minLiftime = 2;
    [SerializeField] private int _maxLiftime = 5;

    private Renderer _renderer;
    private bool _isWaitToBeDestroyed = false;

    public event Action<Cube> Waited;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private IEnumerator ReleaseWithDelay()
    {
        int delay = Random.Range(_minLiftime, _maxLiftime + 1);
        yield return new WaitForSeconds(delay);
        _isWaitToBeDestroyed = false;
        Waited?.Invoke(this);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<Platform>(out Platform ground))
        {
            if (_isWaitToBeDestroyed) 
                return;

            _isWaitToBeDestroyed = true;
            _renderer.material.color = Random.ColorHSV();
            StartCoroutine(ReleaseWithDelay());
        }
    }
}
