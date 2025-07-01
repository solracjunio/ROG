using Flecs.NET.Core;
using TMPro;
using UnityEngine.UI;

public class BindImageHealthSystem : IFlecsSystem
{
    public Routine routine;

    public void Register(World world)
    {
        routine = world.Routine<Image>()
            .Each((Entity entity, ref Image imageComponent) =>
            {
                var image = imageComponent;
                world.Query<HealthComponent>()
                    .Each((ref HealthComponent health) =>
                    {
                        image.fillAmount = health.current / health.max;
                    });
            });
    }

    public void Run()
    {
        routine.Run();
    }
}