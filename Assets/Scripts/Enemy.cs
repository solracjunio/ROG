using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 5f;
    public Player Player;
    public ParticleSystem DestroyEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                DestroyEffect.Play();
                player.AddHealth(-25f);
                StartCoroutine(DestroyBeforeEffect());
            }
        }
    }

    private void Update()
    {
        var direction = (Player.transform.position - transform.position).normalized;
        transform.position += Speed * Time.deltaTime * direction;
    }

    public IEnumerator DestroyBeforeEffect()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
