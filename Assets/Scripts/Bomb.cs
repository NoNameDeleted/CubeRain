using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer), typeof(Exploader))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private int _minLiftime = 2;
    [SerializeField] private int _maxLiftime = 5;

    private Renderer _renderer;
    private Exploader _exploader;

    public event Action<Bomb> Destroyed;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _exploader = GetComponent<Exploader>();
    }

    public void Init()
    {
        int defaultAlpha = 1;
        Color color = _renderer.material.color;
        color.a = defaultAlpha;
        _renderer.material.color = color;
    }

    public IEnumerator ExploidWithDelay()
    {
        float seconds = 0.1f;
        WaitForSeconds wait = new WaitForSeconds(seconds);
        int delay = Random.Range(_minLiftime, _maxLiftime + 1);
        float alphaStep = _renderer.material.color.a * seconds / delay;

        while (_renderer.material.color.a > 0)
        {
            Color color = _renderer.material.color;
            color.a -= alphaStep;
            _renderer.material.color = color;

            yield return wait;
        }

        _exploader.Exploid();
        Destroyed?.Invoke(this);
    }
}
