using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeDestroyed += SpawnBomb;
    }

    public override Bomb ActionOnCreate(Bomb prefab)
    {
        Bomb bomb = Instantiate(prefab);
        return bomb;
    }

    public override void ActionOnGet(Bomb bomb)
    {
        bomb.gameObject.SetActive(true);
    }

    public void SpawnBomb(Vector3 position)
    {
        Bomb bomb = Pool.Get();
        bomb.transform.position = position;
    }

    public override void ActionOnDestroy(Bomb obj)
    {
        throw new System.NotImplementedException();
    }
}
