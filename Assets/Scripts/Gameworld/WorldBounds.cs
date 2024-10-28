using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WorldBounds : MonoBehaviour
{
    private Bounds myBounds;
    void Awake()
    {
        Camera camera = Camera.main;
        float aspectRatio = camera.pixelWidth / (camera.pixelHeight);
        float height = camera.orthographicSize * 2;
        float width = aspectRatio * camera.orthographicSize * 2;
        float minX = camera.transform.position.x - (width / 2);
        float maxX = camera.transform.position.x + (width / 2);
        float minY = camera.transform.position.y - (height/ 2);
        float maxY = camera.transform.position.y + (height/ 2);
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        bounds.SetMinMax(new Vector3(minX,minY,0), new Vector3(maxX,maxY,1));
        myBounds = new Bounds();
        myBounds = bounds;
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
