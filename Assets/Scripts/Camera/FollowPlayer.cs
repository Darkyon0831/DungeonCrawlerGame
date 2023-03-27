using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [field: SerializeField]
    public GameObject Player { get; set; }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Player.transform.position.x - transform.position.x, Player.transform.position.y - transform.position.y, 0.0f);

        int i = 0;
    }
}
