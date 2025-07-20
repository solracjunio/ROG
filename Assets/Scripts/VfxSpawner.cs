using System;
using UnityEngine;

public class VfxSpawner : MonoBehaviour
{
    [SerializeField] private ScriptableListEnemy _scriptableListEnemy;
    [SerializeField] private GameObject _spawnVfxPrefab;
    [SerializeField] private GameObject _destroyVfxPrefab;
    [SerializeField] private GameObject _expPickupPrefab;

    private void Awake()
    {
        _scriptableListEnemy.OnItemAdded += OnEnemySpawned;
        _scriptableListEnemy.OnItemRemoved += OnEnemyDestroyed;
    }

    private void OnDestroy()
    {
        _scriptableListEnemy.OnItemAdded -= OnEnemySpawned;
        _scriptableListEnemy.OnItemRemoved -= OnEnemyDestroyed;
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        Instantiate(_spawnVfxPrefab, enemy.transform.position, Quaternion.identity);
    }

    private void OnEnemyDestroyed(Enemy enemy)
    {
        Instantiate(_destroyVfxPrefab, enemy.transform.position, Quaternion.identity);
        Instantiate(_expPickupPrefab, enemy.transform.position, Quaternion.identity);
    }
}