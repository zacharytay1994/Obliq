using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool isPaused = false;
    List<GameObject> pauseObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        GameObject parent = GameObject.Find("PauseCanvas");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            pauseObjects.Add(parent.transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
     
    }
    public void pauseFunction()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
            foreach(GameObject g in pauseObjects)
            {
                g.SetActive(false);
            }
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            foreach (GameObject g in pauseObjects)
            {
                g.SetActive(true);
            }
        }
    }
    public void restartFunction()
    {       
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseFunction();
    }
}