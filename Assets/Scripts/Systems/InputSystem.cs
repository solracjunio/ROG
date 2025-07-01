using Flecs.NET.Core;
using UnityEngine;

public class InputSystem : IFlecsSystem
{
    public Routine routine;

    public void Register(World world)
    {
        routine = world.Routine<InputComponent>()
            .Each((Entity entity, ref InputComponent inputComponent) =>
            {
                inputComponent.value = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            });
    }

    public void Run()
    {
        routine.Run();
    }
}