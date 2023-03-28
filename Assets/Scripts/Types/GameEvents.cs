using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EnterState
{
    Normal,
    Mirror
}

public class GameEvents
{
    // Mirror dimension collision enter event
    public delegate void MirrorCollisionEnterAction();
    public static event MirrorCollisionEnterAction MirrorCollisionEnterEvent;
    public static void OnMirrorCollisionEnter() { MirrorCollisionEnterEvent(); }

    // Mirror dimension collision leave event
    public delegate void MirrorCollisionLeaveAction();
    public static event MirrorCollisionLeaveAction MirrorCollisionLeaveEvent;
    public static void OnMirrorCollisionLeave() { MirrorCollisionLeaveEvent(); }

    // Mirror dimension enter event
    public delegate void MirrorDimensionEnterAction(EnterState state);
    public static event MirrorDimensionEnterAction MirrorDimensionEnterEvent;
    public static void OnMirrorDimensionEnter(EnterState state) { MirrorDimensionEnterEvent(state); }
}
