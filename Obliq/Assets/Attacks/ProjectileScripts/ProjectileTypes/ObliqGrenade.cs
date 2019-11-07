using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliqGrenade : ObliqProjectile
{
    public ObliqGrenade(GameObject owner, Vector2 target) : base(owner, target, ProjectionType.Grenade)
    {
        SetVariables(0.1f, 2.0f);
    }
    public override void Destroy()
    {
        ParentDestroy();
    }

    public override void IfHit()
    {
    }

    public override void IfGetHit()
    {
    }
}
