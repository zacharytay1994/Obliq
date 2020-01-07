using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldHandler : MonoBehaviour
{
   // GameObject radius_one_;
   // GameObject radius_two_;
   // GameObject radius_three;
    public  List<GameObject> enemies_ = new List<GameObject>();
    public  List<GameObject> goodguys_ = new List<GameObject>();
    bool has_enemies_ = true;
    // Start is called before the first frame update
    void Start()
    {
        // radius_one_ = GameObject.Find("RadiusOne");
        // radius_two_ = GameObject.Find("RadiusTwo");
        // radius_three = GameObject.Find("RadiusThree");
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            foreach (Transform t in GameObject.Find("Enemy").transform)
            {
                Debug.Log(t.gameObject);
                enemies_.Add(t.gameObject);
            }
        }
        else has_enemies_ = false; 

        //foreach (Transform t in GameObject.Find("Player").transform)
        //{
        //    Debug.Log(t.gameObject);
        //    goodguys_.Add(t.gameObject);
        //}
        goodguys_.Add(GameObject.Find("Player"));
       // SetMoveRadiusActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(has_enemies_)
        {
            foreach (GameObject gameobject in enemies_)
            {
                if (gameobject != null)
                {
                    Debug.Log(gameobject);
                    gameobject.GetComponent<Entity>().statemachine_.Update();

                }

            }
        }

    }

    public void SetMoveRadiusActive(bool active)
    {
       // radius_one_.SetActive(active);
      //  radius_two_.SetActive(active);
       // radius_three.SetActive(active);
    }
    public GameObject GetNearestGoodGuy(Vector2 position)
    {
        GameObject game_object = null;
        float nearest = float.PositiveInfinity;
        foreach (GameObject g in goodguys_)
        {
            float compare = (position - (Vector2)g.transform.position).magnitude;
            if (compare < nearest)
            {
                nearest = compare;
                game_object = g;
            }
        }
        return game_object;
    }
    public GameObject GetRandomGoodGuy()
    {
        //if (goodguys_.Count > 0)
        //{
        //    return goodguys_[Random.Range(0, goodguys_.Count - 1)];
        //}
        //return null;
        return goodguys_[0];
    }
}
