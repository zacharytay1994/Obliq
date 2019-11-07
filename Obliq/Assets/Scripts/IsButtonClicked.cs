using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsButtonClicked : MonoBehaviour
{
    public bool isButtonClicked = false;

    public Animator animator;

    public bool turnon = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GameObject.FindGameObjectWithTag("AugmentManagerObject").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("White"))
        {
            turnon = true;
        }
        else
        {
            turnon = false;
        }
    }
}
