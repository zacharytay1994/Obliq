using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToopTipControl : MonoBehaviour
{
    [SerializeField]
    string text_ = "";
    TMPro.TextMeshPro tmp_ = null;
    [SerializeField]
    GameObject tracking_ = null;
    [SerializeField]
    float y_offset_ = 2.0f;
    [SerializeField]
    float hover_speed_ = 1.0f;
    float hover_offset_ = 0.0f;
    float hover_counter_ = 0.0f;
    [SerializeField]
    float hover_scale_ = 2.0f;
    [SerializeField]
    bool persistent_ = true;
    // Start is called before the first frame update
    void Start()
    {
        tmp_ = GetComponent<TMPro.TextMeshPro>();
        tmp_.text = text_;
    }

    // Update is called once per frame
    void Update()
    {
        // hover offset calculation
        hover_counter_ += hover_speed_ * Time.deltaTime;
        hover_offset_ = Mathf.Cos(hover_counter_);
        // move position of
        if (tracking_ != null)
        {
            Vector2 temp = (Vector2)tracking_.transform.position;
            temp.y += y_offset_ + hover_offset_ * hover_scale_;
            transform.position = new Vector3(temp.x, temp.y, transform.position.z);
        }
        // if attached gameobject is destroyed, destroy itself
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    public void Init(GameObject g, string s, float yoffset, float hoverspeed_, float hoverscale, Color c)
    {
        tracking_ = g;
        text_ = s;
        GetComponent<TMPro.TextMeshPro>().text = text_;
        y_offset_ = yoffset;
        hover_speed_ = hoverspeed_;
        hover_scale_ = hoverscale;
        GetComponent<TMPro.TextMeshPro>().color = c;
    }
}
