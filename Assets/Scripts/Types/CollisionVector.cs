using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVector<T>
{
    CollisionVector(T _value) { Value = _value; }

    public T Value { get; set; }
}
