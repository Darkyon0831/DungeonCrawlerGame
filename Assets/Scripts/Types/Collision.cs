using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionHit
{
    public CollisionHit(float _distance, Vector3 _normal, Vector3 _point, Vector3 _origin, string _senderTag, int _layer) { distance = _distance; normal = _normal; point = _point; origin = _origin; senderTag = _senderTag; layer = _layer; }
    public CollisionHit(string _senderTag, int _layer) { senderTag = _senderTag; layer = _layer; }

    public float distance = 0.0f;
    public Vector3 normal = Vector3.zero;
    public Vector3 point = Vector3.zero;
    public Vector3 origin = Vector3.zero;
    public string senderTag = "";
    public int layer = 0;
}

public class CollisionSaveData
{
    public CollisionSaveData(string _senderTag, int _layer) { senderTag = _senderTag; layer = _layer; }

    public bool isHit = true;
    public string senderTag = "";
    public int layer = 0;
}

public class Collision : MonoBehaviour
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

    private float sizeY = 0.0f;
    private float sizeX = 0.0f;

    private Dictionary<string, CollisionSaveData> collisonSaveData = new Dictionary<string, CollisionSaveData>();



    private void Start()
    {
        sizeY = transform.localScale.y / 2.0f;
        sizeX = transform.localScale.x / 2.0f;
    }

    public static bool IsLayer(int mask, string layer)
    {
        return ((1 << mask) & LayerMask.GetMask(layer)) != 0;
    }

    public void CheckHit(Vector3 pos, Vector3 dir, float distance)
    {
        Physics.Raycast(pos, dir, out RaycastHit hit, distance, Physics.AllLayers);

        Debug.DrawRay(pos, dir * distance, Color.green);

        if (hit.collider != null)
        {
            if (collisonSaveData.ContainsKey(hit.collider.name))
                collisonSaveData[hit.collider.name].isHit = true;
            else
            {
                GameEvents.OnCollisionEnter(new CollisionHit(gameObject.tag, hit.collider.gameObject.layer));
                collisonSaveData.Add(hit.collider.name, new CollisionSaveData(gameObject.tag, hit.collider.gameObject.layer));
            }

            GameEvents.OnCollision(new CollisionHit(hit.distance - distance, hit.normal, hit.point, pos, gameObject.tag, hit.collider.gameObject.layer));

        }
    }

    private void FixedUpdate()
    {
        // Reset all isHit
        foreach (var data in collisonSaveData)
        {
            data.Value.isHit = false;
        }

        // Up and down rays
        CheckHit(transform.position + Vector3.left * (sizeX - RayOffset), Vector3.up, sizeY + UpOffset);
        CheckHit(transform.position + Vector3.right * (sizeX - RayOffset), Vector3.up, sizeY + UpOffset);
        CheckHit(transform.position + Vector3.left * (sizeX - RayOffset), Vector3.down, sizeY + DownOffset);
        CheckHit(transform.position + Vector3.right * (sizeX - RayOffset), Vector3.down, sizeY + DownOffset);

        // Left and right rays
        CheckHit(transform.position + Vector3.up * (sizeY - RayOffset), Vector3.left, sizeX + LeftOffset);
        CheckHit(transform.position + Vector3.down * (sizeY - RayOffset), Vector3.left, sizeX + LeftOffset);
        CheckHit(transform.position + Vector3.up * (sizeY - RayOffset), Vector3.right, sizeX + RightOffset);
        CheckHit(transform.position + Vector3.down * (sizeY - RayOffset), Vector3.right, sizeX + RightOffset);

        // Remove all collision save datas that is not a hit
        foreach (var data in collisonSaveData.ToList())
        {
            if (data.Value.isHit == false)
            {
                GameEvents.OnCollisionLeave(new CollisionHit(data.Value.senderTag, data.Value.layer));
                collisonSaveData.Remove(data.Key);
            }
        }
    }
}
