using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MirrorCollision : MonoBehaviour
{
    private Vector3 oldPosition = Vector3.zero;
    private Vector3 dir = Vector3.zero;

    [field: SerializeField]
    public float RayOffset { get; set; }

    [field: SerializeField]
    public float HitDistance { get; set; } = 0;

    private float sizeY = 0.0f;
    private float sizeX = 0.0f;

    [field: SerializeField]
    public bool IsMirrorHit { get; set; }

    private bool _isMirrorHit = false;
    

    // Start is called before the first frame update
    void Start()
    {
        oldPosition = transform.position;
        sizeY = transform.localScale.y / 2.0f;
        sizeX = transform.localScale.x / 2.0f;
    }

    private void CheckCollison(Vector3 pos, Vector3 dir, float distance)
    {
        if (_isMirrorHit) return;

        Collision2DHit hit = Collision2D.CheckHit(pos, dir, distance, "Mirror");

        Debug.DrawRay(pos, dir * distance, Color.blue);

        if (hit != null)
            _isMirrorHit = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _isMirrorHit = false;

        // Up and down rays
        CheckCollison(transform.position + Vector3.left * (sizeX - RayOffset), Vector3.up, sizeY + HitDistance);
        CheckCollison(transform.position + Vector3.right * (sizeX - RayOffset), Vector3.up, sizeY + HitDistance);
        CheckCollison(transform.position + Vector3.left * (sizeX - RayOffset), Vector3.down, sizeY + HitDistance);
        CheckCollison(transform.position + Vector3.right * (sizeX - RayOffset), Vector3.down, sizeY + HitDistance);

        // Left and right rays
        CheckCollison(transform.position + Vector3.up * (sizeY - RayOffset), Vector3.left, sizeX + HitDistance);
        CheckCollison(transform.position + Vector3.down * (sizeY - RayOffset), Vector3.left, sizeX + HitDistance);
        CheckCollison(transform.position + Vector3.up * (sizeY - RayOffset), Vector3.right, sizeX + HitDistance);
        CheckCollison(transform.position + Vector3.down * (sizeY - RayOffset), Vector3.right, sizeX + HitDistance);

        if (IsMirrorHit == false && _isMirrorHit == true)
            GameEvents.OnMirrorCollisionEnter();
        
        if (IsMirrorHit == true && _isMirrorHit == false)
            GameEvents.OnMirrorCollisionLeave();

       IsMirrorHit = _isMirrorHit;

        oldPosition = transform.position;
    }
}
