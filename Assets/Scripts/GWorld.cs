using System.Runtime.CompilerServices;
using UnityEngine;

public sealed class GWorld
{
    private static readonly GWorld instance = new GWorld();
    private static WorldState world;

    static GWorld()
    {
        world = new WorldState();
    }

    private GWorld()
    {
    }

    public static GWorld Instance
    {
        get { return instance};
    }

    public WorldState GetWorld()
    {
        return world;
    }
}
