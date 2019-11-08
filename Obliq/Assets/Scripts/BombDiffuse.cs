using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDiffuse : MonoBehaviour
{
    bool bomb_diffused_ = false;
    [SerializeField]
    float bomb_diffuse_range_ = 0.0f;
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
                bomb_diffused_ = true;
                Debug.Log("Bomb diffused");
                Destroy(gameObject);
            }
        } 
    }

    public bool BombDiffused()
    {
        return bomb_diffused_;
    }

    bool PlayerIsWithinRange()
    {
        if ((gameObject.transform.position - player_.transform.position).magnitude < bomb_diffuse_range_)
        {
            return true;
        }
        return false;
    }
}
