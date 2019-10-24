using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ObliqPathfinding : MonoBehaviour
{
    public Vector2 target_;

    public float speed_ = 200.0f;
    public float next_waypoint_distance_ = 1.0f;
    Quaternion rotation_;

    // Pathfinding variables
    Path path_;
    int current_waypoint_ = 0;
    //bool reached_end_path_ = false;

    Seeker seeker_;
    Rigidbody2D rb2D_;
    // Start is called before the first frame update
    void Start()
    {
        seeker_ = GetComponent<Seeker>();
        rb2D_ = GetComponent<Rigidbody2D>();
        target_ = gameObject.transform.position;

        InvokeRepeating("UpdatePath", 0.0f, 1.0f);
    }

    void UpdatePath()
    {
        if (seeker_.IsDone())
        {
            seeker_.StartPath(rb2D_.position, target_, OnPathComplete);
        }
    }

    void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            path_ = path;
            current_waypoint_ = 0;
            //print("completed");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path_ == null)
        {
            return;
        }
        // if reached end of path
        if (current_waypoint_ >= path_.vectorPath.Count)
        {
            //reached_end_path_ = true;
            return;
        }
        else
        {
            //reached_end_path_ = false;
        }

        Vector2 direction_ = ((Vector2)path_.vectorPath[current_waypoint_] - rb2D_.position).normalized;
        Vector2 force_ = direction_ * speed_ * Time.deltaTime;
        rb2D_.AddForce(force_);

        //LookAt(path_.vectorPath[current_waypoint_]);

        float distance_ = Vector2.Distance(rb2D_.position, path_.vectorPath[current_waypoint_]);

        if (distance_ < next_waypoint_distance_)
        {
            current_waypoint_++;
        }
    }
}
