using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateNode : Unlocker
{
    private bool isHit = false;
    public bool IsHit { get { return isHit; } }

    private void Start()
    {
        GameEvents.CollisionEnterEvent += OnCollisionEnterOwn;
        GameEvents.CollisionLeaveEvent += OnCollisionLeaveOwn;
    }

    private void OnCollisionEnterOwn(CollisionHit hit)
    {
        if (hit.senderTag == "Plate" && Game.IsLayer(hit.layer, "Moveable") && hit.gameObject.GetInstanceID() == gameObject.GetInstanceID())
            isHit = true;
    }

    private void OnCollisionLeaveOwn(CollisionHit hit)
    {
        if (hit.senderTag == "Plate" && Game.IsLayer(hit.layer, "Moveable") && hit.gameObject.GetInstanceID() == gameObject.GetInstanceID())
            isHit = false;
    }
}
