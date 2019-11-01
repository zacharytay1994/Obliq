using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Animator buttonAnim;
    SceneTransitionLoader STM_;

    [SerializeField] string sceneToLoad;
    bool isMouseOver;

    // Start is called before the first frame update
    void Start()
    {
        STM_ = FindObjectOfType<SceneTransitionLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isMouseOver)
            {
                anim.SetBool("IsClicked", true);
                buttonAnim.SetTrigger("SceneEnd");
                if (STM_)
                {
                    if (sceneToLoad != "Exit")
                    {
                        buttonAnim.SetTrigger("SceneEnd");
                        STM_.load_scene_Asynch(sceneToLoad);
                    }
                    else
                    {
                        Application.Quit();
                    }
                }
                else
                {
                    Debug.Log("No scenemanager in scene. Launch game from splashscreen");
                }
            }
        }
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        isMouseOver = true;
        anim.SetBool("IsHover", isMouseOver);
        
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        isMouseOver = false;
        anim.SetBool("IsHover", isMouseOver);
    }
}
