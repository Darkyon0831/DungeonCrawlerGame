using System;
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
    public static void OnMirrorCollisionEnter() { try { MirrorCollisionEnterEvent(); } catch (NullReferenceException) { } }

    // Mirror dimension collision leave event
    public delegate void MirrorCollisionLeaveAction();
    public static event MirrorCollisionLeaveAction MirrorCollisionLeaveEvent;
    public static void OnMirrorCollisionLeave() { try { MirrorCollisionLeaveEvent(); } catch (NullReferenceException) { } }

    // Mirror dimension enter event
    public delegate void MirrorDimensionEnterAction(EnterState state);
    public static event MirrorDimensionEnterAction MirrorDimensionEnterEvent;
    public static void OnMirrorDimensionEnter(EnterState state) { try { MirrorDimensionEnterEvent(state); } catch (NullReferenceException) { } }

    // Wall collision stay event
    public delegate void WallCollisionStayAction(CollisionHit hit);
    public static event WallCollisionStayAction WallCollisionStayEvent;
    public static void OnWallCollisionStay(CollisionHit hit) { try { WallCollisionStayEvent(hit); } catch (NullReferenceException) { } }

    // Collision event
    public delegate void CollisionAction(CollisionHit hit);
    public static event CollisionAction CollisionEvent;
    public static void OnCollision(CollisionHit hit) { try { CollisionEvent(hit); } catch (NullReferenceException) { } }

    // Collision enter event
    public delegate void CollisionEnterAction(CollisionHit hit);
    public static event CollisionEnterAction CollisionEnterEvent;
    public static void OnCollisionEnter(CollisionHit hit) { try { CollisionEnterEvent(hit); } catch (NullReferenceException) { } }

    // Collision leave event
    public delegate void CollisionLeaveAction(CollisionHit hit);
    public static event CollisionLeaveAction CollisionLeaveEvent;
    public static void OnCollisionLeave(CollisionHit hit) { try { CollisionLeaveEvent(hit); } catch (NullReferenceException) { } }

    // Collision 2D event
    public delegate void Collision2DAction(Collision2DHit hit);
    public static event Collision2DAction Collision2DEvent;
    public static void OnCollision2D(Collision2DHit hit) { try { Collision2DEvent(hit); } catch (NullReferenceException) { } }

    // Collision 2D enter event
    public delegate void Collision2DEnterAction(Collision2DHit hit);
    public static event Collision2DEnterAction Collision2DEnterEvent;
    public static void OnCollision2DEnter(Collision2DHit hit) { try { Collision2DEnterEvent(hit); } catch (NullReferenceException) { } }

    // Collision 2D leave event
    public delegate void Collsion2DLeaveAction(Collision2DHit hit);
    public static event Collsion2DLeaveAction Collision2DLeaveEvent;
    public static void OnCollision2DLeave(Collision2DHit hit) { try { Collision2DLeaveEvent(hit); } catch (NullReferenceException) { } }
}
