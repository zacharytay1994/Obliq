using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenRect : MonoBehaviour
{
    [SerializeField]
    float top_ = 25.0f;
    [SerializeField]
    float bottom_ = -25.0f;
    [SerializeField]
    float speed_ = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > bottom_ && transform.position.y < top_)
        {
            transform.position = new Vector3(0.0f, transform.position.y + speed_ * Time.deltaTime, 0.0f);
        }
        else
        {
            speed_ *= -1;
            if (transform.position.y < bottom_)
            {
                transform.position = new Vector3(0.0f, bottom_ + 0.1f, 0.0f);
            }
            if (transform.position.y > top_)
            {
                transform.position = new Vector3(0.0f, top_ - 0.1f, 0.0f);
            }
        }
    }
}
