using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMiniMap : MonoBehaviour
{
    [SerializeField]
    Sprite sprite_ = null;
    [SerializeField]
    Color color_ = Color.white;
    [SerializeField]
    float scale_ = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("MiniMap").GetComponent<MiniMap>().AddMapObject(gameObject, sprite_, scale_, color_);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
