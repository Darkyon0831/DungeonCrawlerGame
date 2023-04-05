using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHit : BaseHit
{
    public CollisionHit(float _distance, Vector3 _normal, Vector3 _point, Vector3 _origin, string _senderTag, GameObject _sender, GameObject _gameObject, int _layer) { distance = _distance; normal = _normal; point = _point; origin = _origin; senderTag = _senderTag; sender = _sender; gameObject = _gameObject; layer = _layer; }
    public CollisionHit(Vector3 _normal, string _senderTag, int _layer, GameObject _sender, GameObject _gameObject) { normal = _normal; senderTag = _senderTag; layer = _layer; sender = _sender; gameObject = _gameObject; }

    public Vector3 normal = Vector3.zero;
    public Vector3 point = Vector3.zero;
    public Vector3 origin = Vector3.zero;
}
