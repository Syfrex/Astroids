using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    public static Bounds WorldBounds;
    public static List<IWorldBoundObject> WorldBoundObjects = new List<IWorldBoundObject>();
    public static void AddWorldBoundObject(IWorldBoundObject anWorldBoundObject)
    {
        WorldBoundObjects.Add(anWorldBoundObject);
    }
}
