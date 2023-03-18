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

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            Input.GetAxis("KeyHorizontal") * MovementSpeed,
            Input.GetAxis("KeyVertical") * MovementSpeed,
            0.0f
            );
    }
}
