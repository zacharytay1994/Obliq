using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraSetter : MonoBehaviour
{
    [SerializeField] Camera current_camera;
    [SerializeField] string cameraTag;
    Canvas current_canvas;
    // Start is called before the first frame update
    void Start()
    {
        current_canvas = GetComponent<Canvas>();
        current_camera = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!current_camera)
        {
            if (Camera.main != null)
            {
                current_camera = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>();
                current_canvas.worldCamera = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>();
                Debug.Log("Loading Scene Camera Set!");
            }
            else
            {
                Debug.Log("No camera found!");
            }
        }
    }
}
