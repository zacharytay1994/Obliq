
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject camera_;
    [SerializeField] float camera_height_;
    // Start is called before the first frame update
    void Start()
    {
        camera_ = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //camera_.transform.rotation = Quaternion.Euler(Vector3.zero);
        camera_.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z-camera_height_);
    }
}
