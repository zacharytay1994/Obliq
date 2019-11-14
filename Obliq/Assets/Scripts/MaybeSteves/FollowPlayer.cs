using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] float x_offset_;
    [SerializeField] float y_offset_;
    [SerializeField] float height_;
    [SerializeField] Transform player_;

    Vector3 x_y_offset_;
    // Start is called before the first frame update
    void Start()
    {
        x_y_offset_ = new Vector3(x_offset_, y_offset_, height_);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player_.position + x_y_offset_;
    }
}
