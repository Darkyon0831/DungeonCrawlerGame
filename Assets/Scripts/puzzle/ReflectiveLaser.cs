using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectiveLaser : MonoBehaviour
{
    private LineRenderer lineRenderer = null;

    [field: SerializeField]
    public Color LaserColor { get; set; } = Color.white;

    [field: SerializeField]
    public float Width { get; set; }

    [field: SerializeField]
    public float MaxDistance { get; set; }

    [field: SerializeField]
    public Material Mat { get; set; }

    [field: SerializeField]
    public Vector3 Dir { get; set;  }

    [field: SerializeField]
    public LayerMask CheckMask { get; set; }

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = Width;
        lineRenderer.endWidth = Width;
        lineRenderer.material = Mat;
        lineRenderer.startColor = LaserColor;
        lineRenderer.endColor = LaserColor;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, GetComponentInChildren<Transform>().position);
        lineRenderer.SetPosition(1, GetComponentInChildren<Transform>().position + Dir * MaxDistance);
        lineRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
    }

    private void Update()
    {
        
    }
}
