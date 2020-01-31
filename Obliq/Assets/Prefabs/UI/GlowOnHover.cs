using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowOnHover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        gameObject.GetComponent<Text>().fontStyle = FontStyle.Bold;
        Debug.Log("Gay");
    }
    void OnMouseExit()
    {
        gameObject.GetComponent<Text>().fontStyle = FontStyle.Normal;
        Debug.Log("No U");
    }
}
