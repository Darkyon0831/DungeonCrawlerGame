using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockableDoor : MonoBehaviour
{
    [field: SerializeField]
    public Unlocker[] Unlockers { get; set; }
}
