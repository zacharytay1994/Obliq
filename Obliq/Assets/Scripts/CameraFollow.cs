
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player_;
    [SerializeField] float camera_height_;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //camera_.transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = new Vector3(player_.transform.position.x, player_.transform.position.y, player_.transform.position.z-camera_height_);
    }
}
