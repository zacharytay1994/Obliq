using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerColorChange : MonoBehaviour
{
    SpriteRenderer sr_;
    [SerializeField] Color spawner_color_1;
    [SerializeField] Color spawner_color_2;
    // Start is called before the first frame update
    void Start()
    {
        sr_ = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr_.color = Color.Lerp(spawner_color_1, spawner_color_2, Mathf.PingPong(Time.time, 1));
    }
}

