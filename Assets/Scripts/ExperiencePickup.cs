using UnityEngine;

public class ExperiencePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.GetComponent<Player>().Experience += 10f;
        Destroy(gameObject);
    }
}
