using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTextControl : MonoBehaviour
{
    [SerializeField]
    string text_ = "";
    [SerializeField]
    float life_span_ = 3.0f;
    [SerializeField]
    float fade_speed_ = 1.0f;
    bool to_destroy_ = false;
    TMPro.TextMeshProUGUI tmp_ = null;
    // Start is called before the first frame update
    void Start()
    {
        tmp_ = GetComponent<TMPro.TextMeshProUGUI>();
        tmp_.text = text_;
    }

    // Update is called once per frame
    void Update()
    {
        // if to destroy, slowly fade and destroy after
        if (to_destroy_)
        {
            Color temp = tmp_.color;
            if (temp.a > 0.0f)
            {
                temp.a -= fade_speed_ * Time.deltaTime;
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
            tmp_.color = temp;
        }

        // if life span up, mark to destroy
        if (life_span_ > 0.0f && !to_destroy_)
        {
            life_span_ -= Time.deltaTime;
        }
        else
        {
            to_destroy_ = true;
        }
    }
}
