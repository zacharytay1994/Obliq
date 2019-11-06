using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInterface : MonoBehaviour
{
    public ObliqProjectile.ProjectionType type_;
    ObliqProjectile instance_;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (instance_ != null)
        {
            instance_.Update();
        }
    }
    
    public void InitializeProjectile(Vector2 target)
    {
        switch (type_)
        {
            case ObliqProjectile.ProjectionType.Bullet:
                instance_ = new ObliqBullet(this.gameObject, target);
                break;
            case ObliqProjectile.ProjectionType.Grenade:
                instance_ = new ObliqGrenade(this.gameObject, target);
                break;
        }
        instance_.Destroy();
    }
}
