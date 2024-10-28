using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WorldBounds : MonoBehaviour
{
    private Bounds myBounds;
    void Awake()
    {
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        float minX = bounds.min.x - bounds.center.x;
        float maxX = bounds.max.x + bounds.center.x;
        float minY = bounds.min.y - bounds.center.y;
        float maxY = bounds.max.y + bounds.center.y;
        myBounds = new Bounds();
        myBounds.SetMinMax(new Vector3(minX, minY, 0), new Vector3(maxX, maxY, 0));
        Global.WorldBounds = myBounds;
        Global.WorldBoundObjects.Clear();
    }
    public void SetToWorldBounds() //This is for testing
    {
        myBounds = Global.WorldBounds;
    }

    public void SweepWorldObjects()
    {
        foreach (IWorldBoundObject obj in Global.WorldBoundObjects)
        {
            if (!myBounds.Contains(obj.GetPosition()))
            {
                if (obj.GetPosition().x < myBounds.min.x)
                {
                    obj.SetPosition(new Vector3(myBounds.max.x, obj.GetPosition().y, 0));
                }
                if (obj.GetPosition().x > myBounds.max.x)
                {
                    obj.SetPosition(new Vector3(myBounds.min.x, obj.GetPosition().y, 0));
                }
                if (obj.GetPosition().y < myBounds.min.y)
                {
                    obj.SetPosition(new Vector3(obj.GetPosition().x, myBounds.max.y, 0));
                }
                if (obj.GetPosition().y > myBounds.max.y)
                {
                    obj.SetPosition(new Vector3(obj.GetPosition().x, myBounds.min.y, 0));
                }
            }
        }
    }
    private void Update()
    {
        SweepWorldObjects();
    }
}
