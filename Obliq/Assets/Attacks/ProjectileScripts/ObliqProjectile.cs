using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObliqProjectile
{
    // Types of movement the projectile has to follow
    [System.Serializable]
    public enum ProjectionType
    {
        Bullet,
        Grenade,
        BouncingGrenade,
    }

    // Projectile variables
    protected GameObject owner_; // the game object representing the projectile
    Vector2 projectile_position_;
    ProjectionType type_;
    Vector2 target_position_;

    // Movement variables
    float speed_;
    float max_speed_;
    float acceleration_;
    float drag_;
    float decceleration_;
    Vector2 heading_;

    // Misc variables
    public float life_span_ = 10.0f;

    public ObliqProjectile(GameObject owner, Vector2 target, ProjectionType type)
    {
        owner_ = owner;
        projectile_position_ = owner.transform.position;
        // set type
        type_ = type;
        target_position_ = target;
        // set heading vector
        heading_ = (target - projectile_position_).normalized;
    }

    public void Update()
    {
        // update base on type
        switch (type_)
        {
            case ProjectionType.Bullet:
                BulletMovement();
                break;
            case ProjectionType.Grenade:
                GrenadeMovement();
                break;
            case ProjectionType.BouncingGrenade:
                break;
        }
    }

    public void ParentDestroy()
    {
        MonoBehaviour.Destroy(owner_, life_span_);
    }
    public abstract void Destroy();
    public abstract void IfHit();
    public abstract void IfGetHit();

    // Projectile movement functions
    public void BulletMovement()
    {
        if (speed_ < max_speed_)
        {
            speed_ += acceleration_ * Time.deltaTime;
        }
        projectile_position_ += heading_ * speed_;
        owner_.transform.position = projectile_position_;
    }

    public void GrenadeMovement()
    {
        // if reached target
        Vector2 projectile_to_target = target_position_ - projectile_position_;
        if (Vector2.Dot(projectile_to_target, heading_) < 0.0f)
        {
            // start rolling
            speed_ = 0.01f;
        }
        else
        {
            if (speed_ < max_speed_)
            {
                speed_ += acceleration_ * Time.deltaTime;
            }
        }
        projectile_position_ += heading_ * speed_;
        owner_.transform.position = projectile_position_;
    }

    public void SetVariables(float maxspeed, float acceleration)
    {
        max_speed_ = maxspeed;
        acceleration_ = acceleration;
    }
}
