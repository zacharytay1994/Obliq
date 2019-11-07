using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliqBullet : ObliqProjectile
{
    public ObliqBullet(GameObject owner, Vector2 target) : base(owner, target, ProjectionType.Bullet)
    {
        SetVariables(0.4f, 4.0f);
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
