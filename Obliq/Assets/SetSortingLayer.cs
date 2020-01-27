using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSortingLayer : MonoBehaviour
{
    Renderer renderer_;
    [SerializeField] string sorting_;
    // Start is called before the first frame update
    void Start()
    {
        renderer_ = GetComponent<Renderer>();
        renderer_.sortingLayerName = sorting_;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
