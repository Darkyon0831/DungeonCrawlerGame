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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnWallCollisionStay(CollisionHit hit)
    {
        if (Collision.IsLayer(hit.layer, "Moveable"))
            Debug.Log("Moveable!");
    }
}
