using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private bool CheckIfLocked(Vector3 normal, GameObject moveableCube)
    {
        MoveableCollision moveCollison = moveableCube.GetComponent<MoveableCollision>();

        if (normal == Vector3.right) return moveCollison.lockPosX;
        if (normal == Vector3.left) return moveCollison.lockNegX;
        if (normal == Vector3.up) return moveCollison.lockPosY;
        if (normal == Vector3.down) return moveCollison.lockNegY;

        return false;
    }

    private void Start()
    {
        GameEvents.CollisionEvent += OnCollision;
    }

    private void OnCollision(CollisionHit hit)
    {
        if ((Game.IsLayer(hit.layer, "Wall")) && hit.senderTag == gameObject.tag)
            transform.position += -hit.normal * hit.distance;

        if (hit.senderTag == gameObject.tag && Game.IsLayer(hit.layer, "Moveable"))
        {
            if (CheckIfLocked(hit.normal, hit.sender))
            {
                transform.position += -hit.normal * hit.distance;
            }
        }
    }
}
