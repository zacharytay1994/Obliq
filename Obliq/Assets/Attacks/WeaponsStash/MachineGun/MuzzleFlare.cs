using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlare : MonoBehaviour
{
    [SerializeField] GameObject effect_ = null;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject tempEffect = Instantiate(effect_);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
