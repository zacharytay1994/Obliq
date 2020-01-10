using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GimmeTooltip : MonoBehaviour
{
    [SerializeField]
    string text_ = "";
    [SerializeField]
    float hover_speed_ = 2.0f;
    [SerializeField]
    float y_offset_ = 2.0f;
    [SerializeField]
    float hover_scale = 0.2f;
    [SerializeField]
    Color color_ = new Color(1.0f, 1.0f, 1.0f);
    [SerializeField]
    GameObject tool_tip_ = null;
    // Start is called before the first frame update
    void Start()
    {
        //GameObject g = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/UI/ToopTip.prefab", typeof(GameObject));
        if (tool_tip_ != null)
        {
            GameObject temp = Instantiate(tool_tip_);
            temp.GetComponent<ToopTipControl>().Init(gameObject, text_, y_offset_, hover_speed_, hover_scale, color_);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
