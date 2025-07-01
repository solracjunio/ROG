using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Flecs.NET.Core;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public static GameWorld Instance { get; private set; }
    public World MainWorld { get; private set; }

    private List<IFlecsSystem> systems = new();

    private void Awake()
    {
        if (Instance is not null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        MainWorld = World.Create();

        RegisterAllSystems();
    }

    private void Update()
    {
        foreach (var system in systems)
        {
            system.Run();
        }
    }

    private void RegisterAllSystems()
    {
        Assembly assembly = typeof(GameWorld).Assembly;

        if (assembly == null) return;
  
        var systemTypes = assembly
            .GetTypes()
            .Where(t => typeof(IFlecsSystem).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in systemTypes)
        {
            IFlecsSystem instance = (IFlecsSystem)Activator.CreateInstance(type);
            instance.Register(MainWorld);
            systems.Add(instance);
        }
    }
}
