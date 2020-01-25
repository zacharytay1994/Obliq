using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartScript : MonoBehaviour
{
    [SerializeField]
    float expansion_rate_ = 1.0f;
    [SerializeField]
    float fade_up_rate_ = 5.0f;
    [SerializeField]
    float fade_off_rate_ = 1.0f;
    SpriteRenderer sr_ = null;
    bool maxed_ = false;
    // Start is called before the first frame update
    void Start()
    {
        sr_ = GetComponent<SpriteRenderer>();
        // set local scale to 0
        transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        // set alpha to 0
        Color c = sr_.color;
        c.a = 0.0f;
        sr_.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        // expand circle
        Vector3 temp_scale = transform.localScale;
        temp_scale.x += expansion_rate_ * Time.deltaTime;
        temp_scale.y += expansion_rate_ * Time.deltaTime;
        transform.localScale = temp_scale;

        // fade up circle
        if (sr_.color.a < 1.0f && !maxed_)
        {
            Color temp_c = sr_.color;
            temp_c.a += fade_up_rate_ * Time.deltaTime;
            sr_.color = temp_c;
        }
        else
        {
            maxed_ = true;
        }

        // fade down circle 
        if (sr_.color.a > 0.0f && maxed_)
        {
            Color temp_c = sr_.color;
            temp_c.a -= fade_off_rate_ * Time.deltaTime;
            sr_.color = temp_c;
        }

        if (sr_.color.a < 0.0f)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
