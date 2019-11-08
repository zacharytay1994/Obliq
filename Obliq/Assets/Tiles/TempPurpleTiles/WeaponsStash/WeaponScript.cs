using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [SerializeField]
    GameObject weapon_ = null;
    GameObject weapon_instance_ = null;
    [SerializeField]
    KeyCode trigger_ = KeyCode.Space;
    bool is_down_ = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // input trigger
        if (Input.GetKeyDown(trigger_))
        {
            is_down_ = true;
            weapon_instance_ = Instantiate(weapon_, transform.position, Quaternion.identity);
            weapon_instance_.GetComponent<ImAProjectile>().InitProj();
        }
        if (Input.GetKeyUp(trigger_))
        {
            is_down_ = false;
            Destroy(weapon_instance_);
        }
        // make weapon follow player
        if (weapon_instance_ != null)
        {
            weapon_instance_.transform.position = gameObject.transform.position;
        }
    }
}
