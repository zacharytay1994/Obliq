using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayObjectiveAtStart : MonoBehaviour
{
    public bool first_display_done_ = false;
    [SerializeField]
    string objective_text_ = "";

    TextMeshProUGUI text_;
    [Space(10)]
    [SerializeField]
    float objective_diplay_duration_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        text_ = GetComponent<TextMeshProUGUI>();
        text_.text = objective_text_;
        text_.enabled = false;
        objective_display_timer_ = objective_diplay_duration_;
    }

    // Update is called once per frame
    void Update()
    {
        if (objective_display_timer_ > 0 && !first_display_done_)
        {
            StartCoroutine(ActivateTriggerDisplayOnce());
        }
    }

    float objective_display_timer_ = 0;
    
    IEnumerator ActivateTriggerDisplayOnce()
    {
        text_.enabled = true;
        yield return new WaitForSeconds(objective_display_timer_);
        objective_display_timer_ = 0;
        first_display_done_ = true;
        text_.enabled = false;
    }
}
