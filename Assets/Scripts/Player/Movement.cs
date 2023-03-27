using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [field: SerializeField]
    public float MovementSpeed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    int KeyToValue(string key)
    {
        if (Input.GetButtonDown(key)) 
            return 1;
        else
            return 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            (KeyToValue("w") + -KeyToValue("s")) * MovementSpeed,
            (KeyToValue("d") + -KeyToValue("a")) * MovementSpeed,
            0.0f
            );
    }
}
