using Flecs.NET.Core;

public interface IFlecsSystem
{
    void Register(World world);
    void Run();
}