using Obvious.Soap;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Vector3Variable _playerPosition;
    [SerializeField] private Vector2 _spawnRange;
    [SerializeField] private float _delay = 1f;
    private float _currentAngle;
    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _delay)
        {
            Spawn();
            _timer = 0f;
        }
    }

    private void Spawn()
    {
        _currentAngle += 180f + Random.Range(-45, 45);
        var angleInRad = _currentAngle * Mathf.Deg2Rad;
        var range = Random.Range(_spawnRange.x, _spawnRange.y);
        var relativePosition = new Vector3(Mathf.Cos(angleInRad) * range, 0, Mathf.Sin(angleInRad) * range);
        var spawnPosition = _playerPosition.Value + relativePosition;
        Instantiate(_prefab, spawnPosition, Quaternion.identity, transform);
    }
}
