using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;

public class Boss1AI : MonoBehaviour
{
    [SerializeField]
    Transform boss_head_one_;

    Entity entity_reference_;
    HealthComponent health_;

    [SerializeField]
    public List<Transform> spawn_location_list_;

    [SerializeField]
    public Transform player_;

    public bool is_collided_ = false;
    // Start is called before the first frame update
    void Start()
    {
        entity_reference_ = gameObject.GetComponent<Entity>();
        health_ = gameObject.GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health_.getCurrentHp() <= 0)
        {
            Destroy(gameObject);
        }
        entity_reference_.statemachine_.Update();
    }

    private void OnEnable()
    {
        transform.position = boss_head_one_.position;
        transform.rotation = boss_head_one_.rotation;
        if(entity_reference_!=null)
        {
            entity_reference_.statemachine_.SetState(new Boss1TeleportingState());
        }
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        is_collided_ = true;
    }
}
