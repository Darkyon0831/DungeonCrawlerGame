using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
public class CollisionSaveData2D : BaseCollisionSaveData
{
    public CollisionSaveData2D(string _senderTag, GameObject _sender, Vector2 _normal, int _layer) { senderTag = _senderTag; sender = _sender; normal = _normal; layer = _layer; }

    public Vector2 normal;
}
