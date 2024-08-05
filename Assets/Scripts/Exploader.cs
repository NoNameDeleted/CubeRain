using UnityEngine;

public class Exploader : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _force = 100f;
    [SerializeField] private float _upwardsModifier = 3f;

    public void Exploid()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_force, transform.position, _radius, _upwardsModifier);
        }
    }
}
