using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ProjectileHandler : MonoBehaviour
{
    List<ProjectileInterface> projectile_list_ = new List<ProjectileInterface>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButton(0))
        //{
        //    SpawnProjectile("Grenade", Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //}
    }

    void SpawnProjectile(string type, Vector2 position)
    {
        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Attacks/Projectiles/" + type + ".prefab", typeof(GameObject));
        GameObject temp = (GameObject)Instantiate(prefab, GameObject.Find("MockPlayer").transform.position, Quaternion.identity);
        // Get projectile interface
        ProjectileInterface temp_interface = temp.GetComponent<ProjectileInterface>();
        temp_interface.InitializeProjectile(position);
    }
}
