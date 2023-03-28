using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionHit
{
    public CollisionHit(float _distance, Vector3 _normal, Vector3 _point) { distance = _distance; normal = _normal; point = _point; }

    public float distance = 0.0f;
    public Vector3 normal = Vector2.zero;
    public Vector3 point = Vector2.zero;
}

public class Collision
{
    public static CollisionHit CheckHit(Vector3 pos, Vector3 dir, float distance, params string[] sMask)
    {
        int mask = 0;
        for (int i = 0; i < sMask.Length; i++)
        {
            mask |= LayerMask.GetMask(sMask[i]);
        }

        RaycastHit hit;

        Physics.Raycast(pos, dir, out hit, distance, mask);

        if (hit.collider != null)
            return new CollisionHit(hit.distance, hit.normal, hit.point);
        else
            return null;
    }
}
