using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGunStates : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        string w = "0000";
        TextIO.WriteFile(w, "Assets/TextFiles/GunStates.txt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
