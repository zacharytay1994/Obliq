using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDictionary
{}

public enum ProjectileForceType
{
    Constant,
    Impulse
}

public enum ProjectileMovement
{
    Straight,
    Longitude,
    Lengitude,
    Spiral,
    RandomStraight,
    RandomSmooth,
    Flock,
}

public enum ProjectileSpawnLocation
{
    Mouse,
    RelativeSelf
}

public enum ProjectileTarget
{
    MouseFollow,
    MousePoint,
    MouseDirection,
    SpecifyDirection,
    //NearestEnemy
}

public enum OnCollideBasic
{
    None,
    DealDamage
}

public enum OnCollideEffect
{
    None,
    Slow
}

public enum OnCollideSpawn
{
    None,
    SpawnProjectile
}

public enum CollideTrigger
{
    Collide,
    Time
}

public enum ProjectileSpawnPattern
{
    None,
    Cone,
    Circle
}

public enum ProjectileSpawnStyle
{
    Stream,
    Burst
}

public enum CollideResult
{
    Despawn,
    Continue
}

public enum LocalMovement
{
    Fixed,
    Spin
}