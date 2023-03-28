using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionSwitch : MonoBehaviour
{
    [field: SerializeField]
    public GameObject Player { get; set; }

    [field: SerializeField]
    public GameObject Counterpart { get; set; }

    [field: SerializeField]
    public Camera NormalCamera { get; set; } 

    [field: SerializeField]
    public Camera MirrorCamera { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.MirrorDimensionEnterEvent += OnMirrorDoor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMirrorDoor(EnterState state)
    {
        if (state == EnterState.Mirror)
        {
            NormalCamera.enabled = false;
            MirrorCamera.enabled = true;
            Counterpart.GetComponent<FollowPlayer>().enabled = false;
            Counterpart.GetComponent<Movement>().enabled = true;
            Counterpart.GetComponent<WallCollision>().enabled = true;
            Counterpart.GetComponent<MirrorCollision>().enabled = true;
            Player.GetComponent<Movement>().enabled = false;
            Player.GetComponent<WallCollision>().enabled = false;
            Player.GetComponent<MirrorCollision>().enabled = false;
            Player.GetComponent<FollowPlayer>().enabled = true;
        }
        else if (state == EnterState.Normal)
        {
            NormalCamera.enabled = true;
            MirrorCamera.enabled = false;
            Counterpart.GetComponent<FollowPlayer>().enabled = true;
            Counterpart.GetComponent<Movement>().enabled = false;
            Counterpart.GetComponent<WallCollision>().enabled = false;
            Counterpart.GetComponent<MirrorCollision>().enabled = false;
            Player.GetComponent<Movement>().enabled = true;
            Player.GetComponent<WallCollision>().enabled = true;
            Player.GetComponent<MirrorCollision>().enabled = true;
            Player.GetComponent<FollowPlayer>().enabled = false;
        }
    }
}