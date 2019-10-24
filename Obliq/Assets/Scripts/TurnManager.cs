using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    bool enemy_turn_ = false;
    bool turn_changed_ = false;
    List<GameObject> enemies_ = new List<GameObject>();
    List<GameObject> goodguys_ = new List<GameObject>();

    // game turn variables
    GameObject currently_selected_goodguy_;
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform t in GameObject.Find("Enemies").transform)
        {
            enemies_.Add(t.gameObject);
        }
        foreach (Transform t in GameObject.Find("GoodGuys").transform)
        {
            goodguys_.Add(t.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // for end player turn
        if (Input.GetKeyDown(KeyCode.P) && !turn_changed_)
        {
            turn_changed_ = true;
            enemy_turn_ = false;
        }

        // enemy turn update
        if (enemy_turn_)
        {
        }
        else if (!enemy_turn_)
        {
            ExecuteGoodTurn(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Enemy").GetComponent<ObliqPathfinding>().target_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GameObject.Find("GoodGuy").GetComponent<ObliqPathfinding>().target_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void ExecuteGoodTurn(Vector2 mouseposition)
    {
        int number_of_players_to_move = goodguys_.Count;

        // if no good guys left to move this turn
        if (number_of_players_to_move <= 0)
        {
            // switch to enemy turn
            enemy_turn_ = true;
        }

        // loop through all selectable good guys to make them act
        foreach (GameObject g in goodguys_)
        {
            if (g.GetComponent<Entity>().OnSelect(mouseposition) && Input.GetMouseButton(0) && !g.GetComponent<Entity>().GetHasMoved())
            {
                currently_selected_goodguy_ = g;
            };
        }

        // if there is a good guy selected
        if (currently_selected_goodguy_ != null)
        {
            currently_selected_goodguy_.GetComponent<GoodGuy>().ExecuteTurn();
        }
    }

    void ExecuteEnemyTurn()
    {

    }

    void ExecuteTurn(Vector2 mouseposition)
    {
        if (enemy_turn_)
        {
            ExecuteEnemyTurn();
        }
        else
        {
            ExecuteGoodTurn(mouseposition);
        }
    }

    public GameObject GetEntityAtMouse()
    {
        if (Input.GetMouseButton(0))
        {
            foreach (GameObject g in enemies_)
            {
                if (g.GetComponent<Entity>().OnSelect(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    return g;
                }
            }

            foreach (GameObject g in goodguys_)
            {
                if (g.GetComponent<Entity>().OnSelect(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    return g;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }
}
