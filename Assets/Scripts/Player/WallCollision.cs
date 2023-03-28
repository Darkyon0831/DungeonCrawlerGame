using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    [field: SerializeField]
    public float RayOffset { get; set; }

    private float sizeY = 0.0f;
    private float sizeX = 0.0f;

    private void Start()
    {
        sizeY = transform.localScale.y / 2.0f;
        sizeX = transform.localScale.x / 2.0f;
    }

    private void CheckCollison(Vector3 pos, Vector3 dir, float distance)
    {
        CollisionHit hit = Collision.CheckHit(pos, dir, distance, "Wall", "Mirror");

        Debug.DrawRay(pos, dir * 10.0f);

        if (hit != null)
            transform.position += dir * (hit.distance - distance);
    }

    private void FixedUpdate()
    {
        // Up and down rays
        CheckCollison(transform.position + Vector3.left * (sizeX - RayOffset), Vector3.up, sizeY);
        CheckCollison(transform.position + Vector3.right * (sizeX - RayOffset), Vector3.up, sizeY);
        CheckCollison(transform.position + Vector3.left * (sizeX - RayOffset), Vector3.down, sizeY);
        CheckCollison(transform.position + Vector3.right * (sizeX - RayOffset), Vector3.down, sizeY);

        // Left and right rays
        CheckCollison(transform.position + Vector3.up * (sizeY - RayOffset), Vector3.left, sizeX);
        CheckCollison(transform.position + Vector3.down * (sizeY - RayOffset), Vector3.left, sizeX);
        CheckCollison(transform.position + Vector3.up * (sizeY - RayOffset), Vector3.right, sizeX);
        CheckCollison(transform.position + Vector3.down * (sizeY - RayOffset), Vector3.right, sizeX);
    }
}
