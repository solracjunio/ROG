using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 2f;
    public Player Player;
    public ParticleSystem DestroyEffect;
    public Spawner Spawner;
    public GameObject ExperiencePickupPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddHealth(-25f);
                StartCoroutine(DestroyBeforeEffect());
            }
        }
    }

    private void Update()
    {
        if (Player.CurrentHealth <= 0f) return;
        var direction = (Player.transform.position - transform.position).normalized;
        transform.position += Speed * Time.deltaTime * direction;
    }

    public IEnumerator DestroyBeforeEffect()
    {
        DestroyEffect.Play();
        yield return new WaitForSeconds(0.25f);
        Spawner.SpawnedEnemies.Remove(gameObject);
        Instantiate(ExperiencePickupPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
