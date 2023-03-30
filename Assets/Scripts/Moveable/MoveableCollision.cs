using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.WallCollisionStayEvent += OnWallCollisionStay;
    }

    private void OnWallCollisionStay(CollisionHit hit)
    {
        if (Collision.IsLayer(hit.layer, "Moveable"))
            transform.position += hit.normal * hit.distance;
    }
}
