using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Unlocker
{
    [field: SerializeField]
    public PressurePlateNode[] Nodes { get; set; } = new PressurePlateNode[4];

    private void Update()
    {
        for (int i = 0; i < Nodes.Length; i++)
        {
            if (Nodes[i].IsHit)
                isSolved = true;
            else
                isSolved = false;

            if (isSolved == false)
                break;
        }

        if (isSolved == true)
            transform.Find("MiddleMiddle").GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        else
            transform.Find("MiddleMiddle").GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }
}
