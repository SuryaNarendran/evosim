using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : Singleton<Boundaries>
{
    public Bounds bounds;

    public static bool Contains(Vector2 point)
    {
        return Instance.bounds.Contains(point);
    }

    public static Vector2 GetPointInBounds()
    {
        Bounds b = Instance.bounds;
        return new Vector2(Random.Range(b.min.x, b.max.x), Random.Range(b.min.y, b.max.y));
    }
}
