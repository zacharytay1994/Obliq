using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    int round_ = 1;
    bool enemy_turn_ = false;

    // list of entities
    List<GameObject> enemies_ = new List<GameObject>();
    List<GameObject> enemies_done_ = new List<GameObject>();
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
        GameObject.Find("Round").GetComponent<Text>().text = "Round " + round_.ToString();
        GameObject.Find("Turn").GetComponent<Text>().text = "Player Turn";
    }

    // Update is called once per frame
    void Update()
    {
        // process entity updates
        ExecuteTurn(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // temp debugging - shows entity at mouse click and its health
        GameObject g = GetEntityAtMouse();
        if (g != null)
        {
            GameObject.Find("Selected").GetComponent<Text>().text = "Selected : " + g.name;
            GameObject.Find("Health").GetComponent<Text>().text = "Health : " + g.GetComponent<Entity>().health_;
        }
    }

    void ExecuteGoodTurn(Vector2 mouseposition)
    {
        int number_of_players_to_move = 0;
        foreach (GameObject g in goodguys_)
        {
            if (!g.GetComponent<Entity>().has_moved_)
            {
                number_of_players_to_move++;
            }
        }
        // if no good guys left to move this turn, switch to enemies turn
        if (number_of_players_to_move == 0)
        {
            // switch to enemy turn
            enemy_turn_ = true;
            GameObject.Find("Turn").GetComponent<Text>().text = "Enemy Turn";
            ResetGoodGuyTextBoxes();
            return;
        }

        // loop through all selectable good guys to make them act
        foreach (GameObject g in goodguys_)
        {
            // if that good guy still has ap left (selectable)
            if (!g.GetComponent<Entity>().has_moved_)
            {
                if (g.GetComponent<Entity>().OnSelect(mouseposition) && Input.GetMouseButton(0))
                {
                    // if no good guy yet selected, or if selected good guy is in idle state
                    if (currently_selected_goodguy_ == null || currently_selected_goodguy_.GetComponent<GoodGuy>().is_idle_)
                    {
                        currently_selected_goodguy_ = g;
                        // debug ui
                        GameObject.Find("GOODGUY").GetComponent<Text>().text = "Selected GoodGuy : " + currently_selected_goodguy_.name;
                        GameObject.Find("GoodGuyAttack").GetComponent<Text>().text = "Attack : " + currently_selected_goodguy_.GetComponent<Entity>().attack_damage_;
                        GameObject.Find("GoodGuyMove").GetComponent<Text>().text = "Distance/AP : " + currently_selected_goodguy_.GetComponent<GoodGuy>().distance_per_point_;
                        GameObject.Find("GoodGuyAP").GetComponent<Text>().text = "AP : " + currently_selected_goodguy_.GetComponent<GoodGuy>().action_points_.ToString();
                    }
                };
            }
        }

        // if there is a good guy selected
        if (currently_selected_goodguy_ != null)
        {
            currently_selected_goodguy_.GetComponent<GoodGuy>().ExecuteTurn();
        }
    }

    void ExecuteEnemyTurn()
    {
        // if no more enemies for processing, switch to player turn, and increment turn count
        if (enemies_.Count < 0)
        {
            enemy_turn_ = false;
            enemies_ = enemies_done_;
            return;
        }
        foreach(GameObject enemy in enemies_)
        {
            if (ProcessEnemy(enemy))
            {
                enemies_.Remove(enemy);
                enemies_done_.Add(enemy);
            }
        }
       
    }

    bool ProcessEnemy(GameObject g)
    {
        // if enemy turn has not ended
        if (g.GetComponent<Entity>().has_moved_)
        {
            return true;
        }
        // update enemy state machine
        Debug.Log(g);
        g.GetComponent<Entity>().statemachine_.Update();
        return false;
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

    // misc functions
    // Gets Enemy of GoodGuy at mouse position on click, return null if none
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

    // Gets the nearest good guy to world position, return null if none
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

    // Gets a random GoodGuy in the world, return null if none
    public GameObject GetRandomGoodGuy()
    {
        if (goodguys_.Count > 0)
        {
            return goodguys_[Random.Range(0, goodguys_.Count - 1)];
        }
        return null;
    }

    void ResetGoodGuyTextBoxes()
    {
        GameObject.Find("GOODGUY").GetComponent<Text>().text = "Selected GoodGuy : --";
        GameObject.Find("GoodGuyAttack").GetComponent<Text>().text = "Attack : --";
        GameObject.Find("GoodGuyMove").GetComponent<Text>().text = "Distance/AP : --";
        GameObject.Find("GoodGuyAP").GetComponent<Text>().text = "AP : --";
    }
}
