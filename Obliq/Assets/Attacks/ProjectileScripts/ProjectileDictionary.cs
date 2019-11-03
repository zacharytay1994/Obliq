using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDictionary
{}

public enum ProjectileMovement
{
    Straight,
    Longitude,
    Lengitude,
    Spiral,
    Random,
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
    MouseDirectionLimit,
    DirectionShort,
    DirectionLong,
    NearestEnemy
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
    Single,
    Cone,
    Circle
}

public enum ProjectileSpawnStyle
{
    None,
    Burst,
    Stream
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