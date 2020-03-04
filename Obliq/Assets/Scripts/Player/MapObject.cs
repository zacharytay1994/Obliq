using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer sr_;
    [HideInInspector]
    public GameObject world_object_;
    // Start is called before the first frame update
    void Start()
    {
        sr_ = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(GameObject g)
    {
        world_object_ = g;
    }
}
