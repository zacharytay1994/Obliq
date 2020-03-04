using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingTextSystem : MonoBehaviour
{
    [SerializeField] public bool UI_;
    TextMeshPro current_text_mesh_;
    public TextMeshProUGUI current_text_mesh_UI_;
    string current_text_;
    [SerializeField] public float scroll_delay_;
    float scroll_delay_timer_;
    AudioManager am_;
    [Space(10)]
    List<string> typing_sound_ = new List<string> { "alienpop" };
    bool flicker_audio_ = true;
    [SerializeField] bool chain_scrolling_text_;
    [SerializeField] ScrollingTextSystem prev_text_chain_;
    [HideInInspector] public bool scroll_complete_ = false;

    // Start is called before the first frame update
    void Start()
    {
        am_ = FindObjectOfType<AudioManager>();
        if (UI_)
        {
            current_text_mesh_UI_ = GetComponent<TextMeshProUGUI>();
            current_text_ = current_text_mesh_UI_.text;
            current_text_mesh_UI_.text = "";
        }

        else
        {
            current_text_mesh_ = GetComponent<TextMeshPro>();
            current_text_ = current_text_mesh_.text;
            current_text_mesh_.text = "";
        }



        scroll_delay_timer_ = scroll_delay_;
    }

    // Update is called once per frame
    void Update()
    {
        if (chain_scrolling_text_)
        {
            if (prev_text_chain_.scroll_complete_)
            {
                ScrollingText(UI_);
            }
        }
        else
        {
            ScrollingText(UI_);
        }

        if (current_text_.Length <= 0)
        {
            scroll_complete_ = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (UI_)
            {
                current_text_mesh_UI_.text += current_text_;
                current_text_ = "";
            }
            else
            {
                current_text_mesh_.text += current_text_;
                current_text_ = "";
            }
        }
    }

    void ScrollingText(bool isUI)
    {
        scroll_delay_timer_ -= Time.deltaTime;
        if (scroll_delay_timer_ <= 0 && current_text_.Length > 0)
        {
            int random_sound = Random.Range(0, typing_sound_.Count);

            //Debug.Log(typing_sound_[random_sound]);


            char c = current_text_[0];
            if (c != '>' && c != '.' && c != '<')
            {
                if (flicker_audio_)
                {
                    am_.PlaySound(typing_sound_[random_sound]);
                }

            }
            current_text_ = current_text_.Remove(0, 1);
            if (isUI)
            {
                current_text_mesh_UI_.text += c;
            }
            else
            {
                current_text_mesh_.text += c;
            }
            scroll_delay_timer_ = scroll_delay_;

            flicker_audio_ = !flicker_audio_;
        }
    }

    public static ScrollingTextSystem CreateComponent(GameObject baseGameObject, bool isUI, float scrollDelay, TextMeshProUGUI current_text_mesh_UI_)
    {
        ScrollingTextSystem stc = baseGameObject.AddComponent<ScrollingTextSystem>();
        stc.UI_ = isUI;
        stc.scroll_delay_ = scrollDelay;
        stc.current_text_mesh_UI_ = current_text_mesh_UI_;
        return stc;
    }
}