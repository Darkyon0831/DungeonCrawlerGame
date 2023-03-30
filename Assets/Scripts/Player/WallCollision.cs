using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void Start()
    {
        GameEvents.CollisionEvent += OnCollision;
    }

    private void OnCollision(CollisionHit hit)
    {
        if ((Collision.IsLayer(hit.layer, "Wall") || Collision.IsLayer(hit.layer, "Mirror")) && hit.senderTag == gameObject.tag)
            transform.position += -hit.normal * hit.distance;
    }
}
