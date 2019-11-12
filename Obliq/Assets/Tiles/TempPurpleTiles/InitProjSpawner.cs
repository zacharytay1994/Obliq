using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitProjSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject proj_prefab = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject temp = Instantiate(proj_prefab, transform.position, Quaternion.identity);
        temp.GetComponent<ImAProjectile>().InitProj();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
