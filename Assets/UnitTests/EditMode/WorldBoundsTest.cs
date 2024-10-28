using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WorldBoundsTest
{
    [Test]
    public void WithinMaxXBounds()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        GameObject world = new GameObject();
        Global.WorldBounds.SetMinMax(new Vector3(-10, -10, 0), new Vector3(10, 10, 0));
        WorldBounds worldBounds = world.AddComponent<WorldBounds>();

        worldBounds.SetToWorldBounds();
        Global.AddWorldBoundObject(player);
        player.SetPosition(new Vector3(11, -10, 0));
        worldBounds.SweepWorldObjects();

        Assert.IsTrue(player.GetPosition().x < Global.WorldBounds.max.x);
    }
    [Test]
    public void WithinMinXBounds()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        GameObject world = new GameObject();
        Global.WorldBounds.SetMinMax(new Vector3(-10, -10, 0), new Vector3(10, 10, 0));
        WorldBounds worldBounds = world.AddComponent<WorldBounds>();

        worldBounds.SetToWorldBounds();
        world.AddComponent<WorldBounds>();
        Global.AddWorldBoundObject(player);
        player.SetPosition(new Vector3(-11, -10, 0));
        worldBounds.SweepWorldObjects();

        Assert.IsTrue(player.GetPosition().x > Global.WorldBounds.min.x);
    }
    [Test]
    public void WithinMaxYBounds()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        Global.WorldBounds.SetMinMax(new Vector3(-10, -10, 0), new Vector3(10, 10, 0));
        GameObject world = new GameObject();
        WorldBounds worldBounds = world.AddComponent<WorldBounds>();

        worldBounds.SetToWorldBounds();
        Global.AddWorldBoundObject(player);
        player.SetPosition(new Vector3(-10, 11, 0));
        worldBounds.SweepWorldObjects();

        Assert.IsTrue(player.GetPosition().y < Global.WorldBounds.max.y);
    }
    [Test]
    public void WithinMinYBounds()
    {
        GameObject me = new GameObject();
        Player player = me.AddComponent<Player>();
        GameObject world = new GameObject();
        Global.WorldBounds.SetMinMax(new Vector3(-10, -10, 0), new Vector3(10, 10, 0));
        WorldBounds worldBounds = world.AddComponent<WorldBounds>();

        worldBounds.SetToWorldBounds();
        world.AddComponent<WorldBounds>();
        Global.AddWorldBoundObject(player);
        player.SetPosition(new Vector3(-10, -11, 0));
        worldBounds.SweepWorldObjects();

        Assert.IsTrue(player.GetPosition().y > Global.WorldBounds.min.y);
    }

}
