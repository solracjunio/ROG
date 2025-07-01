using Flecs.NET.Core;
using TMPro;

public class BindTextHealthSystem : IFlecsSystem
{
    public Routine routine;

    public void Register(World world)
    {
        routine = world.Routine<TextMeshProUGUI>()
            .Each((Entity entity, ref TextMeshProUGUI textComponent) =>
            {
                var text = textComponent;
                world.Query<HealthComponent>()
                    .Each((ref HealthComponent health) =>
                    {
                        text.SetText($"{health.current}");
                    });
            });
    }

    public void Run()
    {
        routine.Run();
    }
}