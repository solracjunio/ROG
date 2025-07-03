using UnityEngine;
using Flecs.NET.Core;
using NUnit.Framework;

public class ChangeHealthScript : MonoBehaviour
{
    public void Change(float amount)
    {
        GameWorld.Instance.MainWorld.Each((Entity e, ref HealthComponent health) =>
        {
            health.previous = health.current;
            health.current += amount;

            if (health.current > health.max)
            {
                health.current = health.max;
            }

            var diff = health.current - health.previous;
            if (diff > 0)
            {
                GameWorld.Instance.MainWorld.Event<HealedEvent>()
                    .Id<HealthComponent>()
                    .Entity(e)
                    .Emit();
            }
            else if (diff < 0)
            {
                GameWorld.Instance.MainWorld.Event<DamagedEvent>()
                    .Id<HealthComponent>()
                    .Entity(e)
                    .Emit();
            }

            if (health.current <= 0)
            {
                GameWorld.Instance.MainWorld.Event<DeathEvent>()
                    .Id<HealthComponent>()
                    .Entity(e)
                    .Emit();
            }
        });
    }
}