using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    private void checkHit(Vector3 pos, Vector3 dir, float distance)
    {
        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, distance, mask);

        Debug.DrawRay(pos, dir * 10.0f);

        if (hit.collider != null)
            transform.position += dir * (hit.distance - distance);
    }

    private void Update()
    {


        LayerMask mask = LayerMask.GetMask("Wall");

        // Up and down rays
        checkHit(transform.position + Vector3.left * (sizeX - RayOffset), Vector3.up, sizeY);
        checkHit(transform.position + Vector3.right * (sizeX - RayOffset), Vector3.up, sizeY);
        checkHit(transform.position + Vector3.left * (sizeX - RayOffset), Vector3.down, sizeY);
        checkHit(transform.position + Vector3.right * (sizeX - RayOffset), Vector3.down, sizeY);

        // Left and right rays
        checkHit(transform.position + Vector3.up * (sizeY - RayOffset), Vector3.left, sizeX);
        checkHit(transform.position + Vector3.down * (sizeY - RayOffset), Vector3.left, sizeX);
        checkHit(transform.position + Vector3.up * (sizeY - RayOffset), Vector3.right, sizeX);
        checkHit(transform.position + Vector3.down * (sizeY - RayOffset), Vector3.right, sizeX);
    }
}
