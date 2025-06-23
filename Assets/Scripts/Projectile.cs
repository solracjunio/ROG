using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed;
    public float Lifetime;

    public void Init(Vector3 direction)
    {
        transform.forward = direction;
        Invoke(nameof(Destroy), Lifetime);
    }

    private void Update()
    {
        transform.position += Speed * Time.deltaTime * transform.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        enemy.StartCoroutine(enemy.DestroyBeforeEffect());
        Destroy(gameObject);
    }
}