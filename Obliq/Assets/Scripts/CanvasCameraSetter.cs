using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraSetter : MonoBehaviour
{
    [SerializeField] Camera current_camera;
    Canvas current_canvas;
    // Start is called before the first frame update
    void Start()
    {
        current_canvas = GetComponent<Canvas>();
        current_camera = GameObject.FindGameObjectWithTag("GlowCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!current_camera)
        {
            if (Camera.main != null)
            {
                current_camera = Camera.main;
                current_canvas.worldCamera = GameObject.FindGameObjectWithTag("GlowCamera").GetComponent<Camera>();
                Debug.Log("Loading Scene Camera Set!");
            }
            else
            {
                Debug.Log("No camera found!");
            }
        }
    }
}
