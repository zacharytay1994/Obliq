using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] GameObject spawnParticle;
    AudioManager _AM;

    [Space(10)]
    [SerializeField] Animator anim;
    [SerializeField] Animator buttonAnim;

    [Space(10)]
    [SerializeField] float pauseSecondsBeforeLoad;
    float internaltimer;
    SceneTransitionLoader STM_;
    IsButtonClicked isclicked;

    [Space(10)]
    [SerializeField] string sceneToLoad;
    bool isMouseOver;

    [Space(10)]
    [SerializeField] bool OptionButton;


    // Start is called before the first frame update
    void Start()
    {
        _AM = FindObjectOfType<AudioManager>();
        STM_ = FindObjectOfType<SceneTransitionLoader>();
        isclicked = FindObjectOfType<IsButtonClicked>();
        internaltimer = pauseSecondsBeforeLoad;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isclicked.isButtonClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (isMouseOver)
                {
                    if (!OptionButton) //Non Option Buttons Code
                    {
                        anim.SetBool("IsClicked", true);
                        buttonAnim.SetTrigger("SceneEnd");


                        if (STM_)
                        {
                            Instantiate(spawnParticle, transform.position, new Quaternion(0, 0, 0, 0));
                            if (sceneToLoad != "Exit")
                            {
                                _AM.PlaySound("WAHH");
                                buttonAnim.SetTrigger("SceneEnd");

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

                        isclicked.isButtonClicked = true;
                    }
                    else //Option Button Code
                    {
                        if (STM_)
                        {
                            Instantiate(spawnParticle, transform.position, new Quaternion(0, 0, 0, 0));
                            _AM.PlaySound("WAHH");
                        }
                    }
                }
            }
        }
        else
        {
            if (anim.GetBool("IsClicked"))
            {
                if (pauseSecondsBeforeLoad <= 0)
                {
                    STM_.load_scene_Asynch(sceneToLoad);
                }
                else
                {
                    pauseSecondsBeforeLoad -= Time.deltaTime;
                }
            }
        }
    }

    void OnMouseEnter()
    {
        if (isclicked.turnon && !isclicked.isButtonClicked)
        {
            //If your mouse hovers over the GameObject with the script attached, output this message
            _AM.PlaySound("Wahh");
            isMouseOver = true;
            anim.SetBool("IsHover", isMouseOver);
            
        }
        
    }

    void OnMouseExit()
    {
        if (isclicked.turnon && !isclicked.isButtonClicked)
        {
            //The mouse is no longer hovering over the GameObject so output this message each frame
            isMouseOver = false;
            anim.SetBool("IsHover", isMouseOver);
            
        }
    }

    
}
