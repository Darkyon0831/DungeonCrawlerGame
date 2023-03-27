using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WallCollision : MonoBehaviour
{
    private float sizeY = 0.0f;
    private float sizeX = 0.0f;
    private BoxCollider2D boxCollider = null;
    private Vector3 needToBeMoves = new Vector2();

    private void Start()
    {
        sizeY = transform.localScale.y / 2.0f;
        sizeX = transform.localScale.x / 2.0f;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void checkHit(Vector3 pos, Vector3 dir, float distance)
    {
        LayerMask mask = LayerMask.GetMask("Wall");
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, distance, mask);

        if (hit.collider != null)
        {
            ColliderDistance2D distance2D = hit.collider.Distance(boxCollider);
            needToBeMoves += dir * distance2D.distance;
        }
    }

    private void Update()
    {
        LayerMask mask = LayerMask.GetMask("Wall");

        needToBeMoves = Vector2.zero;

        // Left and right rays
        checkHit(transform.position + Vector3.up * sizeY, Vector3.left, sizeX);
        checkHit(transform.position + Vector3.down * sizeY, Vector3.left, sizeX);
        checkHit(transform.position + Vector3.up * sizeY, Vector3.right, sizeX);
        checkHit(transform.position + Vector3.down * sizeY, Vector3.right, sizeX);

        // Up and down rays
        checkHit(transform.position + Vector3.left * sizeX, Vector3.up, sizeY);
        checkHit(transform.position + Vector3.right * sizeX, Vector3.up, sizeY);
        checkHit(transform.position + Vector3.left * sizeX, Vector3.down, sizeY);
        checkHit(transform.position + Vector3.right * sizeX, Vector3.down, sizeY);

        transform.position += needToBeMoves;
    }
}
