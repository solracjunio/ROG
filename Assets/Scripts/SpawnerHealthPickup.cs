using System.Collections;
using UnityEngine;

public class SpawnerHealthPickup : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject Player;
    public Vector2 SpawnRange;
    public float SpawnInterval = 1f;
    public float InitialDelay = 1f;
    public int Amount = 1;
    private float currentAngle;
    private float timer;
    private bool isActive;

    private IEnumerator Start()
    {
        timer = SpawnInterval;
        yield return new WaitForSeconds(InitialDelay);
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        if (timer >= SpawnInterval)
        {
            for (int i = 0; i < Amount; i++)
                Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        currentAngle += 180f + Random.Range(-45f, 45f);
        var angleInRadians = currentAngle * Mathf.Deg2Rad;
        var range = Random.Range(SpawnRange.x, SpawnRange.y);
        var relativePosition = new Vector3(Mathf.Cos(angleInRadians) * range, 0f, Mathf.Sin(angleInRadians) * range);
        var spawnPosition = Player.transform.position + relativePosition;
        Instantiate(Prefab, spawnPosition, Quaternion.identity, transform);
    }
}
