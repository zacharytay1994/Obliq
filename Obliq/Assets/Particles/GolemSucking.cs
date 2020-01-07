using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSucking : MonoBehaviour
{
    [SerializeField]
    GameObject particle_ = null;
    [SerializeField]
    float sucking_duration_ = 0.0f;
    float sucking_counter_ = 0.0f;
    bool sucking_ = false;
    [SerializeField]
    float radius_ = 5.0f;
    int particle_amt_ = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (sucking_)
        {
            Suck();
        }
    }

    public void StartSucking()
    {
        if (!sucking_)
        {
            sucking_ = true;
            sucking_counter_ = 0.0f;
        }
    }

    void Suck()
    {
        if (sucking_counter_ < sucking_duration_)
        {
            sucking_counter_+= Time.deltaTime;

            // randomly spawn particles in a circular radius
            for (int i = 0; i < particle_amt_; i++)
            {
                // get random coordinate
                Vector2 random = GF.RotateVector(new Vector2(1.0f, 0.0f), Random.RandomRange(0.0f, 360.0f));
                if (particle_ != null)
                {
                    GameObject temp = Instantiate(particle_);
                    temp.GetComponent<GolemParticle>().Init(transform);
                    temp.transform.position = (Vector2)gameObject.transform.position + random * radius_;
                }
            }
        }
        else
        {
            sucking_ = false;
        }
    }
}
