using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [field: SerializeField]
    public float MovementSpeed { get; set; }

    [field: SerializeField]
    public KeyCode UpKey { get; set; }

    [field: SerializeField]
    public KeyCode DownKey { get; set; }

    [field: SerializeField]
    public KeyCode LeftKey { get; set; }

    [field: SerializeField]
    public KeyCode RightKey { get; set; }

    private Vector2 NormalizedVector;

    class KeyboardInput
    {
        float ud = 0;
        float lr = 0;

        public float UD { get { return ud; } set { if (value != ud) { Debug.Log("Input value UD changed, from " + ud + " to " + value); } ud = value; } }
        public float LR { get { return lr; } set { if (value != lr) { Debug.Log("Input value LR changed, from " + lr + " to " + value); } lr = value; } }
    }

    KeyboardInput keyboardInput = new KeyboardInput();

    void UpdateInput()
    {
        float udCopy = keyboardInput.UD;
        float lrCopy = keyboardInput.LR;

        if (Input.GetKeyUp(UpKey)) udCopy -= 1.0f;
        if (Input.GetKeyUp(DownKey)) udCopy += 1.0f;
        if (Input.GetKeyUp(LeftKey)) lrCopy += 1.0f;
        if (Input.GetKeyUp(RightKey)) lrCopy -= 1.0f;


        if (Input.GetKeyDown(UpKey)) udCopy += 1.0f;
        if (Input.GetKeyDown(DownKey)) udCopy -= 1.0f;
        if (Input.GetKeyDown(LeftKey)) lrCopy -= 1.0f;
        if (Input.GetKeyDown(RightKey)) lrCopy += 1.0f;

        keyboardInput.UD = udCopy;
        keyboardInput.LR = lrCopy;
    }

    private void Start()
    {
        NormalizedVector = new Vector2(0.0f, 0.0f);
    }

    private void Update()
    {
        UpdateInput();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        NormalizedVector.Set(keyboardInput.LR, keyboardInput.UD);
        NormalizedVector.Normalize();

        transform.Translate(NormalizedVector.x * MovementSpeed * Time.deltaTime, NormalizedVector.y * MovementSpeed * Time.deltaTime, 0.0f);
    }
}
