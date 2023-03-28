using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    [field: SerializeField]
    public GameObject Player { get; set; }

    private Vector3 position = new Vector3(0, 0, 0);

    // Update is called once per frame
    void Update()
    {
        position.Set(Player.transform.position.x, Player.transform.position.y, transform.position.z);
        transform.position = position;
    }
}
