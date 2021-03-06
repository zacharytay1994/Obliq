﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkedByGrunt : MonoBehaviour
{
    [SerializeField]
    GameObject mark_ = null;
    [SerializeField]
    float y_offset_ = 1.0f;
    GameObject mark_inst_ = null;
    bool marked_ = false;

    float mark_time_ = 0.5f;
    float mark_counter_ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        mark_inst_ = GameObject.Instantiate(mark_, transform);
        mark_inst_.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (marked_)
        {
            mark_inst_.gameObject.transform.rotation = Quaternion.identity;
            mark_inst_.gameObject.transform.position = mark_inst_.gameObject.transform.parent.position + new Vector3(0.0f, y_offset_, 0.0f);
            if (mark_counter_ > mark_time_)
            {
                mark_inst_.GetComponent<SpriteRenderer>().enabled = false;
                marked_ = false;
                mark_counter_ = 0.0f;
            }
            else
            {
                mark_counter_ += Time.deltaTime;
            }
        }
    }

    public void SetMark(bool b)
    {
        if (b)
        {
            mark_inst_.GetComponent<SpriteRenderer>().enabled = true;
            mark_counter_ = 0.0f;
        }
        else
        {
            mark_inst_.GetComponent<SpriteRenderer>().enabled = false;
        }
        marked_ = b;
    }

    public bool Marked()
    {
        return marked_;
    }
}
