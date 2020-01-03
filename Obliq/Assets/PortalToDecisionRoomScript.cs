using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToDecisionRoomScript : MonoBehaviour
{
    SceneTransitionLoader scene_transition_ = null;
    [SerializeField] string decision_room_name_ = "Decision Room";
    // Start is called before the first frame update
    void Start()
    {
        if(FindObjectOfType<SceneTransitionLoader>())
        {
            scene_transition_ = FindObjectOfType<SceneTransitionLoader>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("MainPlayer")&&scene_transition_!=null)
        {
            scene_transition_.load_scene_Asynch(decision_room_name_);
        }

    }
}
