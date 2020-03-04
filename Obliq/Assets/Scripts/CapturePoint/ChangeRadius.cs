using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRadius : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = new Color (1, 1, 1, 0);

        transform.localScale = new Vector2(GetComponentInParent<CapturePoint>().capture_radius_ / 2,
            GetComponentInParent<CapturePoint>().capture_radius_ / 2);
    }

    // Update is called once per frame
    void Update()
    {
        Color temp = GetComponent<SpriteRenderer>().color;
        if (GetComponentInParent<CapturePoint>().capturing_)
        {
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, Mathf.Lerp(temp.a, 0.9f, 0.1f));
        }
        else if (!GetComponentInParent<CapturePoint>().captured_)
        {
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, Mathf.Lerp(temp.a, 0, 0.1f));
        }

        if (GetComponentInParent<CapturePoint>().captured_)
        {
            transform.localScale = new Vector2(Mathf.Lerp(transform.localScale.x, 0, 0.08f), Mathf.Lerp(transform.localScale.y, 0, 0.08f));
            GetComponent<SpriteRenderer>().color = new Color(temp.r, temp.g, temp.b, Mathf.Lerp(temp.a, 0, 0.08f));
        }
    }
}