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
        NormalCamera.enabled = !NormalCamera.enabled;
        MirrorCamera.enabled = !MirrorCamera.enabled;
        Counterpart.GetComponent<FollowPlayer>().enabled = !Counterpart.GetComponent<FollowPlayer>().enabled;
        Counterpart.GetComponent<Movement>().enabled = !Counterpart.GetComponent<Movement>().enabled;
        Counterpart.GetComponent<WallCollision>().enabled = !Counterpart.GetComponent<WallCollision>().enabled;
        Counterpart.GetComponent<MirrorCollision>().enabled = !Counterpart.GetComponent<MirrorCollision>().enabled;
        Player.GetComponent<Movement>().enabled = !Player.GetComponent<Movement>().enabled;
        Player.GetComponent<WallCollision>().enabled = !Player.GetComponent<WallCollision>().enabled;
        Player.GetComponent<MirrorCollision>().enabled = !Player.GetComponent<MirrorCollision>().enabled;
        Player.GetComponent<FollowPlayer>().enabled = !Player.GetComponent<FollowPlayer>().enabled;
    }
}