using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorDimensionDoor : MonoBehaviour
{
    private bool isCollision = false;
    private bool isInput = false;

    [field: SerializeField]
    public KeyCode EnterButton { get; set; }

    private EnterState enterState = EnterState.Normal;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.MirrorCollisionEnterEvent += OnMirrorCollisionEnter;
        GameEvents.MirrorCollisionLeaveEvent += OnMirrorCollisionLeave;
    }

    void OnMirrorCollisionEnter()
    {
        isCollision = true;
    }

    void OnMirrorCollisionLeave()
    {
        isCollision = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(EnterButton)) isInput = true;
        if (Input.GetKeyUp(EnterButton)) isInput = false;

        if (isCollision && isInput)
        {
            if (enterState == EnterState.Normal) enterState = EnterState.Mirror;
            else if (enterState == EnterState.Mirror) enterState = EnterState.Normal;

            GameEvents.OnMirrorDimensionEnter(enterState);

            isInput = false;
        }
    }
}
