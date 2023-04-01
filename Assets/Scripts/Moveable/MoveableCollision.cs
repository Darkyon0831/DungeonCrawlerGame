using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCollision : MonoBehaviour
{
    public bool lockPosX = false;
    public bool lockNegX = false;
    public bool lockPosY = false;
    public bool lockNegY = false;

    public float dragWeight = 0.0f;
    public float pushWeight = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.CollisionEvent += OnCollision;
    }

    private void UnlockAxis(Vector3 normal)
    {
        if (normal == Vector3.right)
            lockPosX = false;
        else if (normal == Vector3.left)
            lockNegX = false;
        else if (normal == Vector3.up)
            lockPosY = false;
        else if (normal == Vector3.down)
            lockNegY = false;
    }

    private void LockAxis(Vector3 normal)
    {
        if (normal == Vector3.right)
            lockPosX = true;
        else if (normal == Vector3.left)
            lockNegX = true;
        else if (normal == Vector3.up)
            lockPosY = true;
        else if (normal == Vector3.down)
            lockNegY = true;
    }

    private bool CheckIfLocked(Vector3 normal)
    {
        if (normal == Vector3.right) return lockPosX;
        if (normal == Vector3.left) return lockNegX;
        if (normal == Vector3.up) return lockPosY;
        if (normal == Vector3.down) return lockNegY;

        return false;
    }

    private void OnCollision(CollisionHit hit)
    {
        if ((hit.senderTag == "Player") && Collision.IsLayer(hit.layer, "Moveable") && hit.sender.name == gameObject.name)
        {
            if (CheckIfLocked(hit.normal) == false) {
                transform.position += hit.normal * hit.distance;
            }
            else
            {
                Vector3 oldPos = transform.position;
                transform.position += hit.normal * hit.distance;

                if (GetComponent<Collision>().IsCollisionInDir(-hit.normal, LayerMask.GetMask("Wall")) == false)
                    UnlockAxis(hit.normal);

                transform.position = oldPos;
            }
        }

        if (hit.senderTag == "Moveable" && (Collision.IsLayer(hit.layer, "Wall")) && hit.gameObject.name == gameObject.name)
        {
            transform.position += -hit.normal * hit.distance;
            LockAxis(hit.normal);
        }
    }
}
