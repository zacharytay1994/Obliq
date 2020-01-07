using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;

public class Sectopod : MonoBehaviour
{
    public GameObject target_reference_;
    // entity reference
    Entity entity_reference_;
    [SerializeField]
    public GameObject explosion_;
    // Start is called before the first frame update
    void Start()
    {
        entity_reference_ = gameObject.GetComponent<Entity>();
        entity_reference_.statemachine_.SetState(new SectopodIdleState());
        //GetComponent<ImAProjectile>().InitProj();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<ImAProjectile>() != null)
        {
            Debug.Log("Enemy hit");
            GC<Entity>(gameObject).TakeDamage(20); //temp magic no
            
        }
    }
        // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnExplosion()
    {
        Instantiate(explosion_, (Vector2)target_reference_.transform.position, Quaternion.identity);
    }
}
