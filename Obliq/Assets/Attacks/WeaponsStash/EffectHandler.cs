using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    enum Effects {
        None,
        Fire
    }
    Effects current_effect_ = Effects.None;
    Effects last_effect_ = Effects.None;
    // Tolerance
    [SerializeField]
    GameObject fire_effect_ = null;
    [SerializeField]
    [Range(0,20)]
    int fire_tolerance_ = 0;
    bool on_fire_ = false;
    bool lit_ = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // follow parent game object, temporary thing for testing
        if (!lit_)
        {
            if (on_fire_)
            {
                GameObject temp = Instantiate(fire_effect_, transform.position, Quaternion.identity);
                temp.transform.SetParent(transform);
                temp.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
                lit_ = true;
            }
        }
    }

    bool Tolerated(int tolerance)
    {
        return !(Random.Range(0, 20) >= fire_tolerance_);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FireParticle")
        {
            if (!Tolerated(fire_tolerance_))
            {
                on_fire_ = true;
            }
        }
    }
}
