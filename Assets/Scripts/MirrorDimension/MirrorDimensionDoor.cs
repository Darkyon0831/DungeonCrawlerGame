using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MirrorDimensionDoor : MonoBehaviour
{
    private bool isCollision = false;
    private bool isInput = false;

    [field: SerializeField]
    public KeyCode EnterButton { get; set; }

    private EnterState enterState = EnterState.Normal;

    private void Start()
    {
        GameEvents.Collision2DEnterEvent += OnCollision2DEnter;
        GameEvents.Collision2DLeaveEvent += OnCollision2DLeave;
    }

    private void OnCollision2DEnter(Collision2DHit hit)
    {
        if (hit.senderTag == "Mirror" && Game.IsLayer(hit.layer, "Player"))
        {
            Debug.Log("Here");
            isCollision = true;
        }
    }

    private void OnCollision2DLeave(Collision2DHit hit)
    {
        if (hit.senderTag == "Mirror" && Game.IsLayer(hit.layer, "Player"))
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
