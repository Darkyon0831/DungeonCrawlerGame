using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Grabable : MonoBehaviour
{
    private GameObject attachedObject = null;
    private bool isGrab = false;
    private Vector3 grabNormal = Vector3.zero;

    [field: SerializeField]
    public KeyCode GrabKey { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.CollisionEnterEvent += OnCollisionEnterOwn;
        GameEvents.CollisionLeaveEvent += OnCollisionLeaveOwn;
    }

    void Update()
    {
        if (Input.GetKeyDown(GrabKey) && attachedObject != null)
        {
            isGrab = true;
            //attachedObject.GetComponent<Movement>().ResetInput();
            attachedObject.GetComponent<Movement>().LockToNormal(grabNormal);
        }
        else if (Input.GetKeyUp(GrabKey) && attachedObject != null)
        {
            if (attachedObject.TryGetComponent(out Body b1))
                b1.Underive();

            attachedObject.GetComponent<Movement>().UnlockInput();
            isGrab = false;
            attachedObject = null;
            grabNormal = Vector3.zero;
        };

        if (attachedObject != null && isGrab)
        {
            if (attachedObject.TryGetComponent(out Body b1) && transform.TryGetComponent(out Body b2))
            {
                b1.Derive(b2);
            }

            float sizeDistanceA = Mathf.Abs(grabNormal.x * transform.localScale.x + grabNormal.y * transform.localScale.y) / 2;
            float sizeDistanceB = Mathf.Abs(grabNormal.x * attachedObject.transform.localScale.x + grabNormal.y * transform.localScale.y) / 2;

            Vector3 pos = new(attachedObject.transform.position.x, attachedObject.transform.position.y, attachedObject.transform.position.z);
            pos += -grabNormal * (sizeDistanceA + sizeDistanceB);

            if (grabNormal.x == 0)
                pos.x = transform.position.x;
            else if (grabNormal.y == 0)
                pos.y = transform.position.y;

            transform.position = pos;
        }
    }

    void OnCollisionEnterOwn(CollisionHit hit)
    {
        if (hit.gameObject.CompareTag("Player") && Collision.IsLayer(hit.layer, "Moveable") && hit.sender == gameObject)
        {
            attachedObject = hit.gameObject;
            grabNormal = hit.normal;
        }
    }

    void OnCollisionLeaveOwn(CollisionHit hit)
    {
        if (hit.gameObject.CompareTag("Player") && Collision.IsLayer(hit.layer, "Moveable") && hit.sender == gameObject && isGrab == false)
        {
            if (attachedObject.TryGetComponent(out Body b1))
                b1.Underive();

            attachedObject.GetComponent<Movement>().UnlockInput();
            attachedObject = null;
            grabNormal = Vector3.zero;
        }
    }
}
