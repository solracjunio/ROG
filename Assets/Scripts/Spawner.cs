using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Prefab;
    public GameObject Player;
    public Vector2 SpawnRange;
    public float Delay = 1f;
    private float currentAngle;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= Delay)
        {
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
        Instantiate(Prefab, spawnPosition, Quaternion.identity, transform).GetComponent<Enemy>().Player = Player.GetComponent<Player>();
    }
}
