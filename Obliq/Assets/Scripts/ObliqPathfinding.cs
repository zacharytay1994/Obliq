using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ObliqPathfinding : MonoBehaviour
{
    public Vector2 target_;

    public float speed_ = 400.0f;
    float next_waypoint_distance_ = 0.3f;
    Quaternion rotation_;

    // Pathfinding variables
    Path path_;
    int current_waypoint_ = 0;
    public bool reached_end_path_ = false;

    Seeker seeker_;
    Rigidbody2D rb2D_;

    // Start is called before the first frame update
    void Start()
    {
        seeker_ = GetComponent<Seeker>();
        rb2D_ = GetComponent<Rigidbody2D>();
        target_ = gameObject.transform.position;
    }

    public void StartPath(Vector2 position)
    {
        target_ = position;
        reached_end_path_ = false;
        UpdatePath();
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
            reached_end_path_ = false;
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
        if (current_waypoint_ > path_.vectorPath.Count - 1)
        {
            rb2D_.velocity = Vector2.zero;
            rb2D_.angularVelocity = 0;
            reached_end_path_ = true;
            return;
        }

        if (!reached_end_path_)
        {
            Vector2 direction_ = ((Vector2)path_.vectorPath[current_waypoint_] - rb2D_.position).normalized;
            Vector2 force_ = direction_ * speed_ * Time.deltaTime;
            rb2D_.AddForce(force_);

            float distance_ = Vector2.Distance(rb2D_.position, path_.vectorPath[current_waypoint_]);

            if (distance_ < next_waypoint_distance_)
            {
                current_waypoint_++;
            }
        }
    }
}
