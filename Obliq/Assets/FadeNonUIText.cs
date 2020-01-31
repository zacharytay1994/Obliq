using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeNonUIText : MonoBehaviour
{
    ScrollingTextSystem sts_;
    TextMeshPro text_;
    [SerializeField] float fade_speed_;
    Color text_color_;
    // Start is called before the first frame update
    void Start()
    {
        sts_ = GetComponent<ScrollingTextSystem>();
        text_ = GetComponent<TextMeshPro>();
        text_color_ = text_.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(sts_.scroll_complete_)
        {
            text_color_.a -= fade_speed_;
            text_.color = text_color_;
            if(text_color_.a<=0)
            {
                Destroy(gameObject);
            }
        }
    }
}
