using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;
public class Boss1State
{
    // nothing here, or is there
}

public class Boss1TeleportingState : State
{
    float state_change_timer = 0.3f;
    Rigidbody2D rb2d_;

    public override void Enter(GameObject owner)
    {
        rb2d_ = owner.GetComponent<Rigidbody2D>();
        rb2d_.velocity = Vector2.zero;
        rb2d_.angularVelocity = 0;
        Debug.Log("Teleporting");
        int random_location_selection = Random.Range(0, 4);
        owner.transform.position = owner.GetComponent<Boss1AI>().spawn_location_list_[random_location_selection].position;
        owner.transform.rotation = Quaternion.Euler(0, 0, random_location_selection * 90);
    }

    public override void Execute(GameObject owner)
    {
        state_change_timer -= Time.deltaTime;
        //Debug.Log(state_change_timer);
        if(state_change_timer<=0.0f)
        {
            int random_move_selection = Random.Range(1, 2);
            switch(random_move_selection)
            {
                case 1:
                    owner.GetComponent<Entity>().statemachine_.ChangeState(new Boss1ChargingState());
                    break;

                case 2:
                    owner.GetComponent<Entity>().statemachine_.ChangeState(new Boss1WaveState());
                    break;

                case 3:
                    owner.GetComponent<Entity>().statemachine_.ChangeState(new Boss1BlastState());
                    break;

                default:
                    owner.GetComponent<Entity>().statemachine_.ChangeState(new Boss1TeleportingState());
                    break;
            }
            
        }
    }

    public override void Exit(GameObject owner)
    {

    }
}

public class Boss1ChargingState : State
{
    float charging_aim_timer = 3.0f;
    Transform player_;
    Rigidbody2D rb2d_;
    Boss1AI boss1ai_;
    float thrust = 1000000;
    public override void Enter(GameObject owner)
    {
        Debug.Log("Charging!");
        player_ = owner.GetComponent<Boss1AI>().player_;
        rb2d_ = owner.GetComponent<Rigidbody2D>();
        boss1ai_ = owner.GetComponent<Boss1AI>();
    }

    public override void Execute(GameObject owner)
    {
        charging_aim_timer -= Time.deltaTime;
        if(charging_aim_timer>0)
        {
            owner.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan2(owner.transform.position.y - player_.position.y, owner.transform.position.x - player_.position.x) * Mathf.Rad2Deg)+90);
        }
        else
        {
            rb2d_.AddForce(owner.transform.up * thrust, ForceMode2D.Impulse);
            if(boss1ai_.is_collided_)
            {
                owner.GetComponent<Entity>().statemachine_.ChangeState(new Boss1TeleportingState());
            }
        }
    }

    public override void Exit(GameObject owner)
    {
        rb2d_.velocity = Vector2.zero;
        rb2d_.angularVelocity = 0;
        boss1ai_.is_collided_ = false;
    }
}

public class Boss1WaveState : State
{
    float charging_wave_timer = 3.0f;

    Transform player_;
    public override void Enter(GameObject owner)
    {
        Debug.Log("Waving");
        player_ = owner.GetComponent<Boss1AI>().player_;
    }

    public override void Execute(GameObject owner)
    {
       
    }

    public override void Exit(GameObject owner)
    {

    }
}

public class Boss1BlastState : State
{
    public override void Enter(GameObject owner)
    {
        Debug.Log("Blasting");
    }

    public override void Execute(GameObject owner)
    {
       
    }

    public override void Exit(GameObject owner)
    {

    }
}


