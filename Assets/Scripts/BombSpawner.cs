using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.CubeDestroyed += SpawnBomb;
    }

    public void SpawnBomb(Vector3 position)
    {
        UpdateActiveCounter();
        Bomb bomb = Pool.Get();
        bomb.transform.position = position;
        bomb.Init();
        StartCoroutine(bomb.ExploidWithDelay());
    }

    public override Bomb ActionOnCreate(Bomb prefab)
    {
        UpdateCreatedCounter();
        Bomb bomb = Instantiate(prefab);
        bomb.Destroyed += Pool.Release;
        return bomb;
    }

    public override void ActionOnGet(Bomb bomb)
    {
        UpdateSpawnedCounter();
        bomb.gameObject.SetActive(true);
    }

    public override void ActionOnDestroy(Bomb obj)
    {
        throw new System.NotImplementedException();
    }
}
