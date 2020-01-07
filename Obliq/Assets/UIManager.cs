using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject player;
    bool is_Paused_ = false;
    
    public bool is_Quitting_ = false;

    // Start is called before the first frame update
    void Start()
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
            }
        }
        return pauseObjects;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnApplicationQuit()
    {
        is_Quitting_ = true;
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
        is_Quitting_ = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseFunction();
    }
    public void quitFunction()
    {
        Application.Quit();// doesnt work in editor
    }
}
