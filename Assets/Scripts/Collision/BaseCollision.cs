using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollision : MonoBehaviour
{
    [field: SerializeField]
    public float RayOffset { get; set; }

    [field: SerializeField]
    public float LeftOffset { get; set; }

    [field: SerializeField]
    public float RightOffset { get; set; }

    [field: SerializeField]
    public float UpOffset { get; set; }

    [field: SerializeField]
    public float DownOffset { get; set; }

    [field: SerializeField]
    public Color DebugColor { get; set; } = Color.white;

    [field: SerializeField]
    public LayerMask CheckMask { get; set; } = Physics.AllLayers;

    protected float sizeY = 0.0f;
    protected float sizeX = 0.0f;
}
