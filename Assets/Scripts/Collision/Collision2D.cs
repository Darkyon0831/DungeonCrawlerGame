using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Collision2D : BaseCollision
{
    private class CRaycast
    {
        public CRaycast(Vector2 _localPos, Vector2 _direction, float _distance) { localPos = _localPos; direction = _direction; distance = _distance; }

        public Vector2 localPos;
        public Vector2 direction;
        public float distance;
    }

    private Dictionary<string, CollisionSaveData2D> collisonSaveData = new Dictionary<string, CollisionSaveData2D>();
    private CRaycast[] raycasts = new CRaycast[8];

    private void Start()
    {
        sizeY = transform.localScale.y / 2.0f;
        sizeX = transform.localScale.x / 2.0f;

        // Up
        raycasts[0] = new CRaycast(Vector3.left * (sizeX - RayOffset), Vector3.up, sizeY + UpOffset);
        raycasts[1] = new CRaycast(Vector3.right * (sizeX - RayOffset), Vector3.up, sizeY + UpOffset);

        // Down
        raycasts[2] = new CRaycast(Vector3.left * (sizeX - RayOffset), Vector3.down, sizeY + DownOffset);
        raycasts[3] = new CRaycast(Vector3.right * (sizeX - RayOffset), Vector3.down, sizeY + DownOffset);

        // Left
        raycasts[4] = new CRaycast(Vector3.up * (sizeY - RayOffset), Vector3.left, sizeX + LeftOffset);
        raycasts[5] = new CRaycast(Vector3.down * (sizeY - RayOffset), Vector3.left, sizeX + LeftOffset);

        // Right
        raycasts[6] = new CRaycast(Vector3.up * (sizeY - RayOffset), Vector3.right, sizeX + RightOffset);
        raycasts[7] = new CRaycast(Vector3.down * (sizeY - RayOffset), Vector3.right, sizeX + RightOffset);
    }

    private void CheckHit(Vector2 pos, Vector2 dir, float distance, LayerMask mask)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, distance, mask);

        Debug.DrawRay(pos, dir * distance, Color.green);

        if (hit.collider != null)
        {
            Vector3 point = Vector3.zero;

            if (collisonSaveData.ContainsKey(hit.collider.name))
            {
                if (collisonSaveData[hit.collider.name].isHit == false)
                {
                    GameEvents.OnCollision2D(new Collision2DHit(hit.distance - distance - 0.01f, hit.normal, hit.point, pos, gameObject.tag, hit.collider.gameObject, gameObject, hit.collider.gameObject.layer));

                    collisonSaveData[hit.collider.name].isHit = true;
                }
            }
            else
            {
                GameEvents.OnCollision2DEnter(new Collision2DHit(hit.normal, gameObject.tag, hit.collider.gameObject.layer, hit.collider.gameObject, gameObject));
                GameEvents.OnCollision2D(new Collision2DHit(hit.distance - distance - 0.01f, hit.normal, hit.point, pos, gameObject.tag, hit.collider.gameObject, gameObject, hit.collider.gameObject.layer));
                collisonSaveData.Add(hit.collider.name, new CollisionSaveData2D(gameObject.tag, hit.collider.gameObject, hit.normal, hit.collider.gameObject.layer));
            }
        }
    }

    private bool IsCheckHit(Vector3 pos, Vector3 dir, float distance, LayerMask mask)
    {
        Physics.Raycast(pos, dir, out RaycastHit hit, distance, mask);

        if (hit.collider != null)
            return true;

        return false;
    }

    public bool IsCollisionInDir(Vector2 direction, LayerMask mask)
    {
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (raycasts[i].direction == direction)
            {
                if (IsCheckHit(transform.position + (Vector3)raycasts[i].localPos, raycasts[i].direction, raycasts[i].distance, mask))
                    return true;
            }
        }

        return false;
    }

    public bool IsCollision(LayerMask mask)
    {
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (IsCheckHit(transform.position + (Vector3)raycasts[i].localPos, raycasts[i].direction, raycasts[i].distance, mask)) ;
            return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        foreach (var data in collisonSaveData)
        {
            data.Value.isHit = false;
        }

        for (int i = 0; i < raycasts.Length; i++)
        {
            CheckHit(transform.position + (Vector3)raycasts[i].localPos, raycasts[i].direction, raycasts[i].distance, CheckMask);
        }

        foreach (var data in collisonSaveData.ToList())
        {
            if (data.Value.isHit == false)
            {
                GameEvents.OnCollisionLeave(new CollisionHit(data.Value.normal, data.Value.senderTag, data.Value.layer, data.Value.sender, gameObject));
                collisonSaveData.Remove(data.Key);
            }
        }
    }
}
