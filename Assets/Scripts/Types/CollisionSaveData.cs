using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSaveData : BaseCollisionSaveData
{
    public CollisionSaveData(string _senderTag, GameObject _sender, Vector3 _normal, int _layer) { senderTag = _senderTag; sender = _sender; normal = _normal; layer = _layer; }

    public Vector3 normal;
}
