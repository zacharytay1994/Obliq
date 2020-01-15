using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;
public class ChargerStates
{
   // nothing here, or is there
}

public class ChargerMoveState : State
{

    Vector2 closest_good_guy_position;
    Vector2 compare_vec;
   
    float charge_timer;
    public override void Enter(GameObject owner)
    {

        charge_timer = Time.time;
        owner.GetComponent<Charger>().target_reference_ = GameObject.Find("World").GetComponent<WorldHandler>().GetRandomGoodGuy();
        // if target was not found 
        if (owner.GetComponent<Charger>().target_reference_ == null)
        {
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());
            return;
        }
        // find position behind target
        Vector2 to_add = (owner.GetComponent<Charger>().target_reference_.transform.position - owner.transform.position).normalized;
        closest_good_guy_position =
            (Vector2)owner.GetComponent<Charger>().target_reference_.transform.position + (to_add * 1.5f); // temp magic number (how far behind target)
        // move charger to position
        owner.GetComponent<Rigidbody2D>().AddForce(to_add * 4000 * owner.GetComponent<Rigidbody2D>().mass); //* 40);
       /* owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
       
        owner.GetComponent<LineRenderer>().SetPosition(1, closest_good_guy_position);*/
        compare_vec = (Vector2)owner.transform.position - closest_good_guy_position;



    }
    public override void Execute(GameObject owner)
    {

        Debug.Log("Charger Move");
        owner.GetComponent<SpriteRenderer>().material.color =
           Color.Lerp(owner.GetComponent<SpriteRenderer>().material.color, Color.white, Mathf.PingPong(Time.time, 1 * Time.deltaTime));
        float angle = AngleBetween(owner.transform.position, closest_good_guy_position);       
        owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Mathf.PingPong(Time.time,
            1 * Time.deltaTime));
        
        // if overshoot the target
        
        //owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
        
        if (GC<HealthComponent>(owner).currentHp_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new ChargerDeadState());
        }
        if (Vector2.Dot(compare_vec, (Vector2)owner.transform.position - (Vector2)closest_good_guy_position) < 0)
        {

            //owner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // owner.GetComponent<Rigidbody2D>().angularVelocity = 0;
            GC<Charger>(owner).SpawnLesserChargers();

            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());
        }
        if (GC<Rigidbody2D>(owner).velocity == Vector2.zero || Time.time - charge_timer > 3.0f)
        {
            GC<Charger>(owner).SpawnLesserChargers();
            owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState()); 
        }
       /* 
        owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerIdleState());*/
    }
    public override void Exit(GameObject owner) {

    }
    float AngleBetween(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 90;
    }
}


public class ChargerAttackState : State
{
    public override void Enter(GameObject owner) { }
    public override void Execute(GameObject owner) { }
    public override void Exit(GameObject owner) { }
}

public class ChargerIdleState : State
{
    AudioManager am;
    CameraManager cm;
    float charge_start = Time.time;
    public override void Enter(GameObject owner)
    {
        am = Object.FindObjectOfType<AudioManager>();
        cm = Object.FindObjectOfType<CameraManager>();

    }
    public override void Execute(GameObject owner)
    {
        LayerMask layerMask = LayerMask.GetMask("Walls");
        Debug.Log("Charger Idle");
        // Debug.Log(Time.time - charge_start);
        if(GameObject.Find("World").GetComponent<WorldHandler>().GetRandomGoodGuy() != null)
        {         
            owner.GetComponent<Charger>().target_reference_ = GameObject.Find("World").GetComponent<WorldHandler>().GetRandomGoodGuy();
            Vector2 to_add = (owner.GetComponent<Charger>().target_reference_.transform.position - owner.transform.position).normalized;

            RaycastHit2D isHit = Physics2D.Raycast(owner.transform.position,
                ((Vector2)owner.GetComponent<Charger>().target_reference_.transform.position - (Vector2)owner.transform.position).normalized,
             ((Vector2)owner.transform.position - 
             (Vector2)owner.GetComponent<Charger>().target_reference_.transform.position).magnitude, layerMask);

            // if there is no obstruction between player and charger
            if (isHit.collider == null)
            {              
                Vector2 closest_good_guy_position =
                    (Vector2)owner.GetComponent<Charger>().target_reference_.transform.position + (to_add * 1.5f); // temp magic number (how far behind target)
              /*  owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
                owner.GetComponent<LineRenderer>().SetPosition(1, Vector2.Lerp(owner.GetComponent<LineRenderer>().GetPosition(1), closest_good_guy_position,
                    1 * Time.deltaTime));*/

                owner.GetComponent<SpriteRenderer>().material.color = Color.Lerp(owner.GetComponent<SpriteRenderer>().material.color, Color.red, Mathf.PingPong(Time.time, 3 * Time.deltaTime));
                // turn on particle charge
                owner.GetComponent<Charger>().GetComponent<GolemSucking>().StartSucking();

                float angle = AngleBetween(owner.transform.position, closest_good_guy_position);
                owner.transform.rotation = Quaternion.Lerp(owner.transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Mathf.PingPong(Time.time,
                    3 * Time.deltaTime));

                if (Time.time - charge_start >= 3.0f)//magic no
                {                                                                                                         // move charger to position
                    owner.GetComponent<Entity>().statemachine_.ChangeState(new ChargerMoveState());
                }
                // if there is no obstruction between player and charger, turn find path off
                owner.GetComponent<Charger>().find_path_ = false;

            }
            else 
            {
                owner.GetComponent<SpriteRenderer>().material.color = Color.Lerp(owner.GetComponent<SpriteRenderer>().material.color, Color.white, Mathf.PingPong(Time.time, 3 * Time.deltaTime));

                charge_start = Time.time;

                // if there is obstruction between player and charger, find path
                owner.GetComponent<Charger>().find_path_ = true;

               // owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
             //   owner.GetComponent<LineRenderer>().SetPosition(1, (Vector2)owner.transform.position);
            }
          
        }
        else
        {
            //owner.GetComponent<LineRenderer>().SetPosition(0, (Vector2)owner.transform.position);
            //owner.GetComponent<LineRenderer>().SetPosition(1, (Vector2)owner.transform.position);
        }
        if (GC<HealthComponent>(owner).currentHp_ <= 0 || Input.GetKeyDown(KeyCode.B)) // for testing
        {
            GC<Entity>(owner).statemachine_.ChangeState(new ChargerDeadState());
        }


    }
    public override void Exit(GameObject owner)
    {
        am.PlaySound("Charge");
        cm.Shake(0.5f, 15);
    }
    float AngleBetween(Vector2 a, Vector2 b)
    {
;
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg + 90;
    }
}
public class ChargerDeadState : State
{
    public override void Enter(GameObject owner)
    {
        GC<Rigidbody2D>(owner).velocity = Vector2.zero;
        GC<Rigidbody2D>(owner).angularVelocity = 0;
    }
    public override void Execute(GameObject owner)
    {
        //insert death anim
        //destroy obj
        Debug.Log("Charger dead");
            Object.Destroy(owner, 0);
    }
    public override void Exit(GameObject owner)
    {

    }
}
