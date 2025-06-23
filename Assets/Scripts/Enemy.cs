using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.AddHealth(-10f); // Example damage value
                print("Enemy hit the player, health reduced.");
            }
        }
    }
}
