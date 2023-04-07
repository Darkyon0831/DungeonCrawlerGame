using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : MonoBehaviour
{
    [field: SerializeField]
    public Unlocker[] Unlockers { get; set; }

    bool oldIsAllSolved = false;

    private void Update()
    {
        bool isAllSolved = true;

        for (int i = 0; i < Unlockers.Length; i++)
        {
            if (Unlockers[i].IsSolved == false) 
            { 
                isAllSolved = false; 
                break; 
            }
        }

        if (isAllSolved == true)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
        else if (oldIsAllSolved == true)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
        }

        oldIsAllSolved = isAllSolved;
    }
}
