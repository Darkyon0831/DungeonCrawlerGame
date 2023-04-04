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

    [field: SerializeField]
    public bool XInverse { get; set; } = false;

    [field: SerializeField]
    public bool YInverse { get; set; } = false;

    private Vector2 NormalizedVector;

    private Vector3 lockedVector = Vector3.zero;

    class KeyboardInput
    {
        float ud = 0;
        float lr = 0;

        public float UD { get { return ud; } set { ud = value; } }
        public float LR { get { return lr; } set { lr = value; } }
    }

    KeyboardInput keyboardInput = new KeyboardInput();

    void UpdateInput()
    {
        float udCopy = keyboardInput.UD;
        float lrCopy = keyboardInput.LR;

        float xInverseValue = XInverse ? -1 : 1;
        float yInverseValue = YInverse ? -1 : 1;

        if (Input.GetKeyUp(UpKey) && (lockedVector == Vector3.up * yInverseValue || lockedVector == Vector3.zero)) udCopy -= 1.0f * yInverseValue;
        if (Input.GetKeyUp(DownKey) && (lockedVector == Vector3.down * yInverseValue || lockedVector == Vector3.zero)) udCopy += 1.0f * yInverseValue;
        if (Input.GetKeyUp(LeftKey) && (lockedVector == Vector3.left * xInverseValue || lockedVector == Vector3.zero)) lrCopy += 1.0f * xInverseValue;
        if (Input.GetKeyUp(RightKey) && (lockedVector == Vector3.right * xInverseValue || lockedVector == Vector3.zero)) lrCopy -= 1.0f * xInverseValue;


        if (Input.GetKeyDown(UpKey) && (lockedVector == Vector3.up * yInverseValue || lockedVector == Vector3.zero)) udCopy += 1.0f * yInverseValue;
        if (Input.GetKeyDown(DownKey) && (lockedVector == Vector3.down * yInverseValue || lockedVector == Vector3.zero)) udCopy -= 1.0f * yInverseValue;
        if (Input.GetKeyDown(LeftKey) && (lockedVector == Vector3.left * xInverseValue || lockedVector == Vector3.zero)) lrCopy -= 1.0f * xInverseValue;
        if (Input.GetKeyDown(RightKey) && (lockedVector == Vector3.right * xInverseValue || lockedVector == Vector3.zero)) lrCopy += 1.0f * xInverseValue;

        keyboardInput.UD = udCopy;
        keyboardInput.LR = lrCopy;
    }

    public void LockToNormal(Vector3 normal)
    {
        lockedVector = normal;
    }

    public void UnlockInput()
    {
        lockedVector = Vector3.zero;
    }

    public void ResetInput()
    {
        keyboardInput.UD = 0;
        keyboardInput.LR = 0;
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
        Body body = GetComponent<Body>();

        NormalizedVector.Set(keyboardInput.LR, keyboardInput.UD);
        NormalizedVector.Normalize();

        transform.Translate((NormalizedVector.x * MovementSpeed * body.DerivedDragWeight) * Time.deltaTime, (NormalizedVector.y * MovementSpeed * body.DerivedDragWeight) * Time.deltaTime, 0.0f);
    }
}
