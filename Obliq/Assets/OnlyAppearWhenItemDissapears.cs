using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyAppearWhenItemDissapears : MonoBehaviour
{
    [SerializeField]
    GameObject disappear_item_;
    bool exists_at_start_;

    [SerializeField]
    List<GameObject> activate_items_;
    
    // Start is called before the first frame update
    void Start()
    {
        if(disappear_item_!=null)
        {
            exists_at_start_ = true;
        }
        else
        {
            exists_at_start_ = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(disappear_item_ == null)
        {
            foreach(GameObject g in activate_items_)
            {
                g.SetActive(true);
            }
        }
    }

}
