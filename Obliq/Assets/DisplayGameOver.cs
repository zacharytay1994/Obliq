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
    [SerializeField]
    ScrollingTextSystem scroll_;

    [SerializeField] GameObject to_menu_button_;

    SceneTransitionLoader stl_;

    // Start is called before the first frame update
    void Start()
    {
        stl_ = FindObjectOfType<SceneTransitionLoader>();
        //text_ = GetComponent<TextMeshProUGUI>();
        text_.enabled = false;
        scroll_.enabled = false;
        to_menu_button_.SetActive(false);
        hp_ = GameObject.Find("Player").GetComponent<HealthComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("value is" + hp_.getCurrentHp());
        if (hp_.getCurrentHp() <= 0)
        {
            to_menu_button_.SetActive(true);
            text_.enabled = true;
            scroll_.enabled = true;
        }
    }

    public void GoToMenu()
    {
        stl_.load_scene_Asynch("MenuScene");
    }
}
