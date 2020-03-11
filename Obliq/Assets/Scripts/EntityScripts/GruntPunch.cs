using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntPunch : MonoBehaviour
{
    AudioManager am_;
    [SerializeField]
    GameObject grunt_punch_;
    Animator animator_ = null;
    GameObject player_ = null;
    [SerializeField]
    float punching_range_ = 5.0f;
    [SerializeField]
    float firing_timer_ = 0.5f;
    float counter_ = 0.0f;
    [SerializeField]
    GameObject pewpew_ = null;
    [SerializeField]
    float speed_ = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        am_ = FindObjectOfType<AudioManager>();
        // instantatiate grunt_punch
        if (grunt_punch_ != null)
        {
            GameObject temp = Instantiate(grunt_punch_, transform);
            animator_ = temp.GetComponent<Animator>();
            animator_.speed = 0;
        }
        player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player_ != null)
        {
            Vector2 punching_distance_ = ((Vector2)player_.transform.position - (Vector2)transform.position);
            if (punching_distance_.magnitude < punching_range_)
            {
                // stop the grunt
                GetComponent<ZacsFindPath>().SetMove(false);
                // activate punching animation
                animator_.speed = 0.5f;
                // force face player
                GetComponent<FaceVelocity>().SetActive(false);
                float angle = GF.AngleBetween(transform.position, player_.transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0.0f, 0.0f, angle), Mathf.PingPong(Time.time,
                    3 * Time.deltaTime));
                // fire some particle at it
                if (counter_ < firing_timer_)
                {
                    counter_ += Time.deltaTime;
                }
                else
                {
                    // fire a particle
                    if (pewpew_ != null)
                    {
                        am_.PlaySound("gruntPunch");
                        GameObject temp = Instantiate(pewpew_);
                        temp.transform.position = transform.position;
                        temp.GetComponent<Rigidbody2D>().velocity = punching_distance_.normalized * speed_ * Time.deltaTime;
                        Physics2D.IgnoreCollision(temp.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
                    }
                    counter_ = 0.0f;
                }
            }
            else
            {
                GetComponent<ZacsFindPath>().SetMove(true);
                GetComponent<FaceVelocity>().SetActive(true);
                animator_.speed = 0;
            }
        }
    }
}
