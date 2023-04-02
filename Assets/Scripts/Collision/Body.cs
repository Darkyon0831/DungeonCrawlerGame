using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public float DragWeight = 1.0f;

    public float DerivedDragWeight { get; private set; } = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out Collision col))
        {
            GameEvents.CollisionEnterEvent += OnCollisionOwnEnter;
            GameEvents.CollisionLeaveEvent += OnCollisionLeave;

        }
        else if (TryGetComponent(out Collision2D col2D))
        {
            GameEvents.Collision2DEnterEvent += OnCollision2DEnter;
            GameEvents.Collision2DLeaveEvent += OnCollision2DLeave;
        }
    }

    public void Derive(GameObject sender)
    {
        if (sender.TryGetComponent(out Body senderBody))
        {
            DerivedDragWeight = senderBody.DragWeight;
        }
    }

    public void Derive(Body body)
    {
        DerivedDragWeight = body.DragWeight;
    }

    public void Underive()
    {
        DerivedDragWeight = 1.0f;
    }

    void OnCollisionOwnEnter(CollisionHit hit)
    {
        Derive(hit.sender);
    }

    void OnCollisionLeave(CollisionHit hit)
    {
        Underive();
    }

    void OnCollision2DEnter(Collision2DHit hit)
    {

    }

    void OnCollision2DLeave(Collision2DHit hit)
    {

    }
}
