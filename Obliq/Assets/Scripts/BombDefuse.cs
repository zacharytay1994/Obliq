using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDefuse : MonoBehaviour
{
    bool bomb_defused_ = false;
    [SerializeField]
    float bomb_defuse_range_ = 0.0f;
    [SerializeField]
    KeyCode key_code_ = KeyCode.O;
    [SerializeField]
    GameObject player_ = null;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       if (PlayerIsWithinRange())
        {
            if (Input.GetKeyDown(key_code_))
            {
                bomb_defused_ = true;
                Destroy(gameObject);
            }
        } 
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
