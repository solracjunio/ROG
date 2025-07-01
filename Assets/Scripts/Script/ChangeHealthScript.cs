using UnityEngine;

public class ChangeHealthScript : MonoBehaviour
{
    public void Change(float amount)
    {
        GameWorld.Instance.MainWorld.Each((ref HealthComponent health) =>
        {
            health.previous = health.current;
            health.current += amount;

            if (health.current > health.max)
            {
                health.current = health.max;
            }
        });
    }
}