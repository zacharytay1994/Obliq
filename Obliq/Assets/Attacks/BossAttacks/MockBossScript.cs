using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockBossScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ZBossAttacks.PulseWave(1.0f, 5, 0.2f, 1.0f, GameObject.Find("MockPlayer").GetComponent<CircleCollider2D>(), gameObject.transform);
        }
    }
}
