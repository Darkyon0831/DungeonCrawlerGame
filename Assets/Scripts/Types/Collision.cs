using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionHit
{
    public CollisionHit(float _distance, Vector3 _normal, Vector3 _point, int _layer) { distance = _distance; normal = _normal; point = _point; layer = _layer; }

    public float distance = 0.0f;
    public Vector3 normal = Vector2.zero;
    public Vector3 point = Vector2.zero;
    public int layer = 0;
}

public class Collision
{
    public static bool IsLayer(int mask, string layer)
    {
        return ((1 << mask) & LayerMask.GetMask(layer)) != 0;
    }

    public static CollisionHit CheckHit(Vector3 pos, Vector3 dir, float distance, params string[] sMask)
    {
        int mask = 0;
        for (int i = 0; i < sMask.Length; i++)
        {
            mask |= LayerMask.GetMask(sMask[i]);
        }

        Physics.Raycast(pos, dir, out RaycastHit hit, distance, mask);

        if (hit.collider != null)
            return new CollisionHit(hit.distance - distance, hit.normal, hit.point, hit.collider.gameObject.layer);
        else
            return null;
    }
}
