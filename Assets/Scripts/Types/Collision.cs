using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class CollisionHit
{
    public CollisionHit(float _distance, Vector3 _normal, Vector3 _point, Vector3 _origin, string _senderTag, GameObject _sender, GameObject _gameObject, int _layer) { distance = _distance; normal = _normal; point = _point; origin = _origin; senderTag = _senderTag; sender = _sender; gameObject = _gameObject; layer = _layer; }
    public CollisionHit(string _senderTag, int _layer, GameObject _sender, GameObject _gameObject) { senderTag = _senderTag; layer = _layer; sender = _sender; gameObject = _gameObject; }

    public float distance = 0.0f;
    public Vector3 normal = Vector3.zero;
    public Vector3 point = Vector3.zero;
    public Vector3 origin = Vector3.zero;
    public string senderTag = "";
    public int layer = 0;
    public GameObject sender;
    public GameObject gameObject;
}

public class CollisionSaveData
{
    public CollisionSaveData(string _senderTag, GameObject _sender, int _layer) { senderTag = _senderTag; sender = _sender; layer = _layer; }

    public bool isHit = true;
    public string senderTag = "";
    public GameObject sender = null;
    public int layer = 0;
}

public class Collision : MonoBehaviour
{
    private class CRaycast
    {
        public CRaycast(Vector3 _localPos, Vector3 _direction, float _distance) { localPos = _localPos; direction = _direction; distance = _distance; }

        public Vector3 localPos;
        public Vector3 direction;
        public float distance;
    }

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

    private float sizeY = 0.0f;
    private float sizeX = 0.0f;
    private Dictionary<string, CollisionSaveData> collisonSaveData = new Dictionary<string, CollisionSaveData>();
    private CRaycast[] raycasts = new CRaycast[8];

    private void Start()
    {
        sizeY = transform.localScale.y / 2.0f;
        sizeX = transform.localScale.x / 2.0f;

        // Up
        raycasts[0] = new CRaycast(Vector3.left * (sizeX - RayOffset), Vector3.up, sizeY + UpOffset);
        raycasts[1] = new CRaycast(Vector3.right * (sizeX - RayOffset), Vector3.up, sizeY + UpOffset);

        // Down
        raycasts[2] = new CRaycast(Vector3.left * (sizeX - RayOffset), Vector3.down, sizeY + DownOffset);
        raycasts[3] = new CRaycast(Vector3.right * (sizeX - RayOffset), Vector3.down, sizeY + DownOffset);

        // Left
        raycasts[4] = new CRaycast(Vector3.up * (sizeY - RayOffset), Vector3.left, sizeX + LeftOffset);
        raycasts[5] = new CRaycast(Vector3.down * (sizeY - RayOffset), Vector3.left, sizeX + LeftOffset);

        // Right
        raycasts[6] = new CRaycast(Vector3.up * (sizeY - RayOffset), Vector3.right, sizeX + RightOffset);
        raycasts[7] = new CRaycast(Vector3.down * (sizeY - RayOffset), Vector3.right, sizeX + RightOffset);
    }

    public static bool IsLayer(int mask, string layer)
    {
        return ((1 << mask) & LayerMask.GetMask(layer)) != 0;
    }

    private void CheckHit(Vector3 pos, Vector3 dir, float distance, LayerMask mask)
    {
        Physics.Raycast(pos, dir, out RaycastHit hit, distance, mask);

        Debug.DrawRay(pos, dir * distance, Color.green);

        if (hit.collider != null)
        {
            if (collisonSaveData.ContainsKey(hit.collider.name))
            {
                if (collisonSaveData[hit.collider.name].isHit == false)
                {
                    GameEvents.OnCollision(new CollisionHit(hit.distance - distance, hit.normal, hit.point, pos, gameObject.tag, hit.collider.gameObject, gameObject, hit.collider.gameObject.layer));
                    collisonSaveData[hit.collider.name].isHit = true;
                }
            }
            else
            {
                GameEvents.OnCollisionEnter(new CollisionHit(gameObject.tag, hit.collider.gameObject.layer, hit.collider.gameObject, gameObject));
                GameEvents.OnCollision(new CollisionHit(hit.distance - distance, hit.normal, hit.point, pos, gameObject.tag, hit.collider.gameObject, gameObject, hit.collider.gameObject.layer));
                collisonSaveData.Add(hit.collider.name, new CollisionSaveData(gameObject.tag, hit.collider.gameObject, hit.collider.gameObject.layer));
            }
        }
    }

    private bool IsCheckHit(Vector3 pos, Vector3 dir, float distance, LayerMask mask)
    {
        Physics.Raycast(pos, dir, out RaycastHit hit, distance, mask);

        if (hit.collider != null)
            return true;

        return false;
    }

    public bool IsCollision(LayerMask mask)
    {
        for (int i = 0; i < raycasts.Length; i++)
        {
            if (IsCheckHit(transform.position + raycasts[i].localPos, raycasts[i].direction, raycasts[i].distance, mask));
                return true;
        }

        return false;
    }

    private void FixedUpdate()
    {
        // Reset all isHit
        foreach (var data in collisonSaveData)
        {
            data.Value.isHit = false;
        }
        
        for (int i = 0; i < raycasts.Length; i++)
        {
            CheckHit(transform.position + raycasts[i].localPos, raycasts[i].direction, raycasts[i].distance, CheckMask);
        }

        // Remove all collision save datas that is not a hit
        foreach (var data in collisonSaveData.ToList())
        {
            if (data.Value.isHit == false)
            {
                GameEvents.OnCollisionLeave(new CollisionHit(data.Value.senderTag, data.Value.layer, data.Value.sender, gameObject));
                collisonSaveData.Remove(data.Key);
            }
        }
    }
}
