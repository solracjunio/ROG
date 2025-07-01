using UnityEngine;

public class Player : MonoBehaviour
{
    public float currentHealth;

    void Start()
    {
        GameWorld.Instance.MainWorld.Entity("Player")
            .Set(new InputComponent() { value = Vector3.zero })
            .Set(new SpeedComponent() { value = 5f })
            .Set(transform)
            .Set(new HealthComponent() { current = 100f , max = 100f, previous = 100f });
    }

    void Update()
    {
        currentHealth = GameWorld.Instance.MainWorld
            .Entity("Player")
            .Get<HealthComponent>().current;
    }
}