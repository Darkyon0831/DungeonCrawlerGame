using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    public float DragWeight = 1.0f;
    public float PushWeight = 1.0f;

    [field: SerializeField]
    public float DerivedDragWeight { get; private set; } = 1.0f;

    [field: SerializeField]
    public float DerivedPushWeight { get; private set; } = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent<Collision>(out Collision col))
        {
            GameEvents.CollisionEnterEvent += OnCollisionEnter;
            GameEvents.CollisionLeaveEvent += OnCollisionLeave;
        }
        else if (TryGetComponent<Collision2D>(out Collision2D col2D))
        {
            GameEvents.Collision2DEnterEvent += OnCollision2DEnter;
            GameEvents.Collision2DLeaveEvent += OnCollision2DLeave;
        }
    }

    private void Derive(GameObject sender)
    {
        if (sender.TryGetComponent<Body>(out Body senderBody))
        {
            DerivedDragWeight = senderBody.DragWeight;
            DerivedPushWeight = senderBody.PushWeight;

            Debug.Log("Derived Drag Weight: " + DerivedDragWeight);
            Debug.Log("Derived Push Weight: " + DerivedPushWeight);
        }
    }

    private void Underive()
    {
        DerivedDragWeight = 1.0f;
        DerivedPushWeight = 1.0f;
    }

    void OnCollisionEnter(CollisionHit hit)
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
