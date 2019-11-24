using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObliqFlock : MonoBehaviour
{
    [SerializeField]
    public string group_ = "";
    FlockingHandler flock_handler_ = null;
    bool initialized_ = false;
    Vector2 flocking_force_ = new Vector2(0.0f, 0.0f);
    // flock parameters
    [SerializeField]
    float seperation_threshold_ = 0.0f;
    [SerializeField]
    float seperation_strength_ = 0.0f;
    [SerializeField]
    float cohesion_threshold_ = 0.0f;
    [SerializeField]
    float cohesion_strength_ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        flock_handler_ = GameObject.Find("FlockHandler").GetComponent<FlockingHandler>();
        if (flock_handler_ != null)
        {
            if (group_ != "")
            {
                flock_handler_.AddNewBoid(this);
                initialized_ = true;
            }
            else
            {
                Debug.Log("No group has been assigned to this flocker, it is lonely");
            }
        } 
        else
        {
            Debug.Log("no flock handler found in scene, can't process flockers");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized_)
        {
            flocking_force_ = CalculateFlockingForce();
            gameObject.GetComponent<Rigidbody2D>().AddForce(flocking_force_, ForceMode2D.Force);
        }
    }

    public Vector2 GetFlockingForce()
    {
        return flocking_force_;
    }
    Vector2 CalculateFlockingForce()
    {
        return ProcessSeperation() + ProcessCohesion() + ProcessAlignment();
    }

    Vector2 ProcessSeperation()
    {
        Vector2 accumulated_direction = new Vector2(0.0f, 0.0f);
        foreach (ObliqFlock o in flock_handler_.flockers_)
        {
            if (o.group_ == group_ && o != null)
            {
                Vector2 direction = ((Vector2)transform.position - (Vector2)o.transform.position);
                // check if other is near enough to influence seperation force
                if ((direction.x * direction.x + direction.y * direction.y) < (seperation_threshold_ * seperation_threshold_))
                {
                    accumulated_direction += direction;
                }
            }
        }
        // return seperation force
        return accumulated_direction.normalized * seperation_strength_;
    }

    Vector2 ProcessCohesion()
    {
        Vector2 accumulated_direction = new Vector2(0.0f, 0.0f);
        foreach (ObliqFlock o in flock_handler_.flockers_)
        {
            if (o.group_ == group_ && o != null)
            {
                Vector2 direction = ((Vector2)o.transform.position - (Vector2)transform.position);
                //accumulated_direction += direction;
                // check if other is near enough to influence seperation force
                if ((direction.x * direction.x + direction.y * direction.y) < (cohesion_threshold_ * cohesion_threshold_))
                {
                    // add seperation direction to accumulated direction
                    accumulated_direction += direction;
                }
            }
        }
        // return seperation force
        return accumulated_direction.normalized * cohesion_strength_;
    }

    Vector2 ProcessAlignment()
    {
        return new Vector2(0.0f, 0.0f);
    }

    private void OnDestroy()
    {
        flock_handler_.RemoveBoid(this);
    }
}
