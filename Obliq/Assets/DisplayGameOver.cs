using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayGameOver : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text_;
    [SerializeField]
    HealthComponent hp_;

    // Start is called before the first frame update
    void Start()
    {
        //text_ = GetComponent<TextMeshProUGUI>();
        text_.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("value is" + hp_.getCurrentHp());
        if (hp_.getCurrentHp() <= 0)
        {
            
            text_.enabled = true;
        }
    }
}
