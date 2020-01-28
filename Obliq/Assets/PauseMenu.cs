using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool is_Paused_ = false;
    SceneTransitionLoader stl_;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    List<GameObject> getPauseObjects()
    {
        List<GameObject> pauseObjects = new List<GameObject>();
        GameObject parent = GameObject.Find("PauseCanvas");
        if (parent != null)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                pauseObjects.Add(parent.transform.GetChild(i).gameObject);
                parent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        return pauseObjects;
    }
    public void pauseFunction()
    {
        List<GameObject> pause_obj = getPauseObjects();
        if (is_Paused_ == true)
        {
            Time.timeScale = 1;
            is_Paused_ = false;
            foreach (GameObject g in pause_obj)
            {
                g.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 0;
            is_Paused_ = true;
            foreach (GameObject g in pause_obj)
            {
                g.SetActive(true);
            }
        }
    }
    public void restartFunction()
    {
        stl_.load_scene_Asynch(SceneManager.GetActiveScene().name);
        pauseFunction();
    }
}
