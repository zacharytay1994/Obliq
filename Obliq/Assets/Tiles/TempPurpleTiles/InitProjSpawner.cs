using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitProjSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject proj_prefab = null;

    [SerializeField]
    float time_delay_ = 0.0f;

    GameObject weapon_instance_ = null;

    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = Instantiate(proj_prefab, transform.position, Quaternion.identity);
        temp.GetComponent<ImAProjectile>().spawn_delay_ = time_delay_;
        temp.GetComponent<ImAProjectile>().InitProj();
        temp.transform.SetParent(transform);
        weapon_instance_ = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetWeapon()
    {
        return weapon_instance_;
    }
}
