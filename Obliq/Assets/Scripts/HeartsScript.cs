using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsScript : MonoBehaviour
{
    [SerializeField]
    Texture2D texture_ = null;
    // Start is called before the first frame update
    void Start()
    {
        GUI.DrawTexture(new Rect(25, 25, 75, 75), texture_);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
