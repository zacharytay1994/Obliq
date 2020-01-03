using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClick : MonoBehaviour
{
    [SerializeField]
    string scene_name_ = "";
    SpriteRenderer sr_ = null;
    // Start is called before the first frame update
    void Start()
    {
        sr_ = GetComponent<SpriteRenderer>();
        Color temp = sr_.color;
        temp.a = 0.3f;
        sr_.color = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (scene_name_ != "")
        {
            SceneManager.LoadScene("Assets/Scenes/" + scene_name_ + ".unity");
        }
    }

    private void OnMouseEnter()
    {
        Color temp = sr_.color;
        temp.a = 1.0f;
        sr_.color = temp;
    }

    private void OnMouseExit()
    {
        Color temp = sr_.color;
        temp.a = 0.3f;
        sr_.color = temp;
    }
}
