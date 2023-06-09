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

    void NeutralizeLocked(ref float UD, ref float LF)
    {
        float xInverseValue = XInverse ? -1 : 1;
        float yInverseValue = YInverse ? -1 : 1;

        if (lockedVector != Vector3.zero)
        {
            if (UD == 1 && lockedVector != Vector3.up) UD = 0;
            if (UD == -1 && lockedVector != Vector3.down) UD = 0;

            if (LF == -1 && lockedVector != Vector3.left) LF = 0;
            if (LF == 1 && lockedVector != Vector3.right) LF = 0;
        }
    }

    void UpdateInput()
    {
        float udCopy = keyboardInput.UD;
        float lrCopy = keyboardInput.LR;

        float xInverseValue = XInverse ? -1 : 1;
        float yInverseValue = YInverse ? -1 : 1;

        if (Input.GetKeyUp(UpKey)) udCopy -= 1.0f * yInverseValue;
        if (Input.GetKeyUp(DownKey)) udCopy += 1.0f * yInverseValue;
        if (Input.GetKeyUp(LeftKey)) lrCopy += 1.0f * xInverseValue;
        if (Input.GetKeyUp(RightKey)) lrCopy -= 1.0f * xInverseValue;


        if (Input.GetKeyDown(UpKey)) udCopy += 1.0f * yInverseValue;
        if (Input.GetKeyDown(DownKey)) udCopy -= 1.0f * yInverseValue;
        if (Input.GetKeyDown(LeftKey)) lrCopy -= 1.0f * xInverseValue;
        if (Input.GetKeyDown(RightKey)) lrCopy += 1.0f * xInverseValue;

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

        float udCopy = keyboardInput.UD;
        float lrCopy = keyboardInput.LR;

        NeutralizeLocked(ref udCopy, ref lrCopy);

        NormalizedVector.Set(lrCopy, udCopy);
        NormalizedVector.Normalize();

        transform.Translate((NormalizedVector.x * MovementSpeed * body.DerivedDragWeight) * Time.deltaTime, (NormalizedVector.y * MovementSpeed * body.DerivedDragWeight) * Time.deltaTime, 0.0f);
    }
}
