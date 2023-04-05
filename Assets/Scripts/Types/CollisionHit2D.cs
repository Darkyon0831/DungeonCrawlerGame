using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision2DHit : BaseHit
{
    public Collision2DHit(float _distance, Vector2 _normal, Vector2 _point, Vector2 _origin, string _senderTag, GameObject _sender, GameObject _gameObject, int _layer) { distance = _distance; normal = _normal; point = _point; origin = _origin; senderTag = _senderTag; sender = _sender; gameObject = _gameObject; layer = _layer; }
    public Collision2DHit(Vector3 _normal, string _senderTag, int _layer, GameObject _sender, GameObject _gameObject) { normal = _normal; senderTag = _senderTag; layer = _layer; sender = _sender; gameObject = _gameObject; }

    public Vector2 normal = Vector3.zero;
    public Vector2 point = Vector3.zero;
    public Vector2 origin = Vector3.zero;
}
