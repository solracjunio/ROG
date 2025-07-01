using Flecs.NET.Core;
using UnityEngine;

public class MovementSystem : IFlecsSystem
{
    public Routine routine;

    public void Register(World world)
    {
        routine = world.Routine<InputComponent, SpeedComponent, Transform>()
            .Each((Entity entity, ref InputComponent input, ref SpeedComponent speed, ref Transform transform) =>
            {
                transform.position += speed.value * Time.deltaTime * input.value;
            });
    }

    public void Run()
    {
        routine.Run();
    }
}