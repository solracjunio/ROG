using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
