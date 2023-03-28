using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision2DHit
{
    public Collision2DHit(float _distance, Vector2 _normal, Vector2 _point) { distance = _distance; normal = _normal; point = _point; }

    public float distance = 0.0f;
    public Vector2 normal = Vector2.zero;
    public Vector2 point = Vector2.zero;
}

public class Collision2D
{
    public static Collision2DHit CheckHit(Vector3 pos, Vector3 dir, float distance, params string[] sMask)
    {
        int mask = 0;
        for (int i = 0; i < sMask.Length; i++)
        {
            mask |= LayerMask.GetMask(sMask[i]);
        }

        RaycastHit2D hit = Physics2D.Raycast(pos, dir, distance, mask);

        if (hit.collider != null)
            return new Collision2DHit(hit.distance, hit.normal, hit.point);
        else
            return null;
    }
}
