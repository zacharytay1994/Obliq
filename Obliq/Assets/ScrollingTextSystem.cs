using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollingTextSystem : MonoBehaviour
{
    TextMeshProUGUI current_text_mesh_;
    string current_text_;
    [SerializeField] float scroll_delay_;
    float scroll_delay_timer_;
    AudioManager am_;
    [Space(10)]
    [SerializeField] string typing_sound_;

    // Start is called before the first frame update
    void Start()
    {
        am_ = FindObjectOfType<AudioManager>();
        current_text_mesh_ = GetComponent<TextMeshProUGUI>();
        current_text_ = current_text_mesh_.text;
        current_text_mesh_.text = "";
        scroll_delay_timer_ = scroll_delay_;
    }

    // Update is called once per frame
    void Update()
    {
        scroll_delay_timer_ -= Time.deltaTime;
        if(scroll_delay_timer_<=0 && current_text_.Length>0)
        {
            float random_pitch = Random.Range(0.7f, 1.3f);
            am_.PlaySound(typing_sound_,1, random_pitch);
            char c = current_text_[0];
            current_text_ = current_text_.Remove(0, 1);
            current_text_mesh_.text += c;
            scroll_delay_timer_ = scroll_delay_;
        }
    }
}
