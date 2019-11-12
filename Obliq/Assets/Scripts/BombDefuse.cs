using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BombDefuse : MonoBehaviour
{
    bool bomb_defused_ = false;
    [SerializeField]
    float bomb_defuse_range_ = 0.0f;
    [SerializeField]
    KeyCode key_code_ = KeyCode.O;
    [SerializeField]
    GameObject player_ = null;

    // Bomb defuse timer
    public GameObject BombDefusePrefab;
    [SerializeField]
    public float bomb_defuse_time_ = 5.0f;
    Image defuse_timer_bar_;
    float defuse_time_left_;

    // Start is called before the first frame update
    void Start()
    {
        // Defuse timer initialise
        Instantiate(BombDefusePrefab, GameObject.FindGameObjectWithTag("Objective").
            transform.position + (new Vector3(0, 3, 0)), Quaternion.identity);
        // for future scaling
        /*foreach (GameObject o in GameObject.FindGameObjectsWithTag("Objective"))
        {
            Instantiate(BombDefusePrefab, o.transform.position + (new Vector3(0, 3, 0)), Quaternion.identity);
        }*/

        // Bomb defuse timer bar
        defuse_timer_bar_ = GameObject.FindGameObjectWithTag("ObjectiveProgress").GetComponent<Image>();
        defuse_time_left_ = bomb_defuse_time_;
        // for future scaling
        /*foreach (GameObject p in GameObject.FindGameObjectsWithTag("ObjectiveProgress"))
        {
            defuse_timer_bar_ = p.GetComponent<Image>();
            defuse_time_left_ = bomb_defuse_time_;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsWithinRange())
        {
            // If bomb is not defused, decrease defuse timer bar size.
            if (Input.GetKeyDown(key_code_) || defuse_time_left_ <= 0)
            {
                bomb_defused_ = true;
                Destroy(gameObject);
                Destroy(GameObject.FindGameObjectWithTag("ObjectiveProgress"));
            }
            else
            {
                // Countdown
                defuse_time_left_ -= Time.deltaTime;
            }
        }
        else
        {
            defuse_time_left_ = bomb_defuse_time_;
        }
        
        // Defuse timer bar scales to time left
        defuse_timer_bar_.fillAmount = defuse_time_left_ / bomb_defuse_time_;
    }

    public bool BombDefused()
    {
        return bomb_defused_;
    }

    bool PlayerIsWithinRange()
    {
        if ((gameObject.transform.position - player_.transform.position).magnitude < bomb_defuse_range_)
        {
            return true;
        }
        return false;
    }
}
