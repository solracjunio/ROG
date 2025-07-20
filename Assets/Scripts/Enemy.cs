using Obvious.Soap;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ScriptableEventInt _onEnemyHitPlayer;
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;

    private void Start()
    {
        _scriptableListEnemy.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _onEnemyHitPlayer.Raise(30);
            Die();
        }
    }

    public void Die()
    {
        _scriptableListEnemy.Remove(this);
        Destroy(gameObject);
    }
}
