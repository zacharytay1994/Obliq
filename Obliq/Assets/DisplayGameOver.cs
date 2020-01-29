using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DisplayGameOver : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text_;
    [SerializeField]
    HealthComponent hp_;
    [SerializeField]
    ScrollingTextSystem scroll_;

    [SerializeField] List<GameObject> objects_to_activate_;

    SceneTransitionLoader stl_;

    GameObject player_;

    // Start is called before the first frame update
    void Start()
    {
        stl_ = FindObjectOfType<SceneTransitionLoader>();
        //text_ = GetComponent<TextMeshProUGUI>();
        foreach (GameObject g in objects_to_activate_)
        {
            g.SetActive(false);
        }
        text_.enabled = false;
        scroll_.enabled = false;
        hp_ = GameObject.Find("Player").GetComponent<HealthComponent>();
        player_ = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (hp_.getCurrentHp() <= 0)
        {
            //to_menu_button_.SetActive(true);
            //restart_level_button.SetActive(true);

            foreach(GameObject g in objects_to_activate_)
            {
                g.SetActive(true);
            }
            text_.enabled = true;
            scroll_.enabled = true;

            player_.GetComponent<PlayerController>().SetAcceleration(0.0f);
        }
    }

    public void GoToMenu()
    {
        stl_.load_scene_Asynch("MenuScene");
    }
    public void restartFunction()
    {
        stl_.load_scene_Asynch(SceneManager.GetActiveScene().name);
       // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
