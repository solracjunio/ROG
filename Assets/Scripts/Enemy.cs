using Obvious.Soap;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableEventInt _onEnemyHitPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _onEnemyHitPlayer.Raise(30);
        }
    }
}
