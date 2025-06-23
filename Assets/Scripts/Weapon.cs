using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Spawner Spawner;
    public Projectile ProjectilePrefab;
    public float FireRate;
    private Transform ownerTransform;
    private float timer;

    private void Awake()
    {
        ownerTransform = transform.parent == null ? transform : transform.parent;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / FireRate)
        {
            ShootAtClosestEnemy();
            timer = 0f;
        }
    }

    private void ShootAtClosestEnemy()
    {
        var closest = GetClosest(transform.position);
        if (closest != null)
        {
            var direction = closest.transform.position - ownerTransform.position;
            SpawnProjectile(direction.normalized);
        }
    }

    private void SpawnProjectile(Vector3 directionNormalized)
    {
        var spawnPoint = ownerTransform.position + directionNormalized * 0.5f;
        spawnPoint.y = 1f;
        var projectile = Instantiate(ProjectilePrefab, spawnPoint, Quaternion.identity);
        projectile.Init(directionNormalized);
    }

    private Enemy GetClosest(Vector3 position)
    {
        if (Spawner.SpawnedEnemies.Count == 0) return null;
        var closest = Spawner.SpawnedEnemies.OrderBy(enemy => (position - enemy.transform.position).sqrMagnitude).First();
        return closest.GetComponent<Enemy>();
    }
}