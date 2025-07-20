using Obvious.Soap;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private FloatReference _speed;
    [SerializeField] private FloatReference _lifetime;

    public void Init(Vector3 direction)
    {
        transform.forward = direction;
        Invoke(nameof(Destroy), _lifetime);
    }

    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<Enemy>();
        enemy.Die();
        Destroy();
    }

    void Destroy() => Destroy(gameObject);
}
