using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollision : MonoBehaviour
{
    public enum RayDirection
    {
        All,
        Top,
        Bottom,
        Right,
        Left
    }

    [field: SerializeField]
    public Vector2 Size { get; set; } = Vector2.zero;

    [field: SerializeField, Header("Offsets"), Space(20)]
    public float RayOffset { get; set; }

    [field: SerializeField]
    public float LeftOffset { get; set; }

    [field: SerializeField]
    public float RightOffset { get; set; }

    [field: SerializeField]
    public float UpOffset { get; set; }

    [field: SerializeField]
    public float DownOffset { get; set; }

    [field: SerializeField, Space(20)]
    public Color DebugColor { get; set; } = Color.white;

    [field: SerializeField]
    public LayerMask CheckMask { get; set; } = Physics.AllLayers;

    [field: SerializeField]
    public RayDirection RayDir { get; set; } = RayDirection.All;
}
