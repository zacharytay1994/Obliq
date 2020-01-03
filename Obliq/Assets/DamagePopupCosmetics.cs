using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopupCosmetics : MonoBehaviour
{
    

    [SerializeField] int text_lifetime_;
    float text_create_time_;
    public Vector2 origin;
    public bool is_crit;
    
    // Start is called before the first frame update
    void Start()
    {
        text_create_time_ = Time.time;  
    }

    // Update is called once per frame
    void Update()
    {
        if (is_crit)
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.cyan;
            transform.position += (Vector3)(origin * 4) * Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<TextMeshPro>().color = Color.white;
            transform.position += (Vector3)(origin * 2) * Time.deltaTime;
        }
        

        Destroy(gameObject, text_lifetime_);
        
    }
}
