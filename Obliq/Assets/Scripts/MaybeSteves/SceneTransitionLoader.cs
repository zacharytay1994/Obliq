﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransitionLoader : MonoBehaviour
{
    public static GameObject instance;
    [Space(5)]
    public Slider loading_bar;
    public float load_progress;
    public float displayed_load_progress;

    [Range(0.0f,1.0f)]
    public float load_bar_speed;
    public TextMeshProUGUI tmp_text, nextlevel_text;

    public bool scene_loading;
    public bool scene_loaded;

    public Animator Animator;

    AnimationClip white_to_black;
    [SerializeField] float white_to_black_length;

    AnimationClip black_to_white;
    [SerializeField] float black_to_white_length;

    private void Awake()    //Make this object persistent
    {
        if (!instance)
        {
            instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateAnimClipTimes();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void load_scene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void load_scene_Asynch(string SceneName)
    {
        if (!scene_loading)
        {
            StartCoroutine(LoadSceneAsync(SceneName));
        }
    }

    IEnumerator LoadSceneAsync(string SceneName)
    {
        load_progress = 0;
        loading_bar.value = load_progress;
        displayed_load_progress = load_progress;
        nextlevel_text.text = "";
        UpdateProgressText(SceneName);
        
        scene_loading = true;
        scene_loaded = false;
        
        FadeToBlack();
        
        yield return new WaitForSeconds(white_to_black_length);

        nextlevel_text.text = "Initializing" + '\n' + SceneName;
        ScrollingTextSystem stc = ScrollingTextSystem.CreateComponent(GameObject.Find("NextLevelText (TMP)"), true, 0.03f, GameObject.Find("NextLevelText (TMP)").GetComponent<TextMeshProUGUI>());

        AsyncOperation async = SceneManager.LoadSceneAsync(SceneName);

        while (!async.isDone || displayed_load_progress<1)
        {
            load_progress = (async.progress / 0.9f);

            if ((load_progress - displayed_load_progress) > load_bar_speed)
            {
                displayed_load_progress = displayed_load_progress + (load_progress - displayed_load_progress) * load_bar_speed ;
            }
            else
            {
                displayed_load_progress = load_progress;
            }

            loading_bar.value = displayed_load_progress;

            UpdateProgressText(SceneName);
            //Debug.Log("Load progress: " + load_progress * 100 + "%");
            yield return null;
        }
        
        yield return new WaitForSeconds(0.4f);

        scene_loading = false;
        scene_loaded = true;
        FadeFromBlack();
        Destroy(stc);
    }

    public void FadeToBlack()
    {
        Animator.SetBool("scene_loading", true);
        Animator.SetBool("scene_loaded", false);
    }

    public void FadeFromBlack()
    {
        Animator.SetBool("scene_loading", false);
        Animator.SetBool("scene_loaded", true);
    }

    public void UpdateAnimClipTimes()
    {
        AnimationClip[] clips = Animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "BlackToWhite":
                    black_to_white_length = clip.length;
                    break;
                case "WhiteToBlack":
                    white_to_black_length = clip.length;
                    break;
            }
        }
    }

    public void UpdateProgressText(string SceneName)
    {
        tmp_text.text = Mathf.Floor(displayed_load_progress * 100) + "%";
    }
}
