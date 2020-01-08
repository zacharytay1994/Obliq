using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntBulletFly : MonoBehaviour
{
    [SerializeField]
    float life_span_ = 1.0f;
    [SerializeField]
    GameObject ring_;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (life_span_ > 0.0f)
        {
            life_span_ -= Time.deltaTime;
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainPlayer")
        {
            GameObject temp = Instantiate(ring_);
            temp.transform.position = transform.position;
            GameObject.Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Wall")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
