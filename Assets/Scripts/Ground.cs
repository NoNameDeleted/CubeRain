using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent<Cube>(out Cube cube))
        {
            cube.TrySetColor(Random.ColorHSV());
            _spawner.DeactivateCube(cube);
        }
    }
}
