using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZacsFindPath : MonoBehaviour
{
    Tilemap tilemap_reference_ = null;
    ZacsPathfinding pathfinding_reference_ = null;
    List<Node> current_path_ = new List<Node>();
    [SerializeField]
    float slack_ = 1.0f;
    int current_node_num_ = 0;
    Node current_node_ = null;
    [SerializeField]
    Vector2 random_speed_ = new Vector2(0.0f, 0.0f);
    float pathfinding_strength = 0.0f;
    
    [SerializeField]
    Vector2 path_update_delay = new Vector2(1.0f, 2.0f); // temporary fix
    float path_update__delay_counter_ = 0.0f;
    float og_delay = 0.0f;
    bool move_ = true;
    // Start is called before the first frame update
    void Start()
    {
        tilemap_reference_ = GameObject.Find("WallTilemap").GetComponent<Tilemap>();
        pathfinding_reference_ = GameObject.Find("Pathfinder").GetComponent<ZacsPathfinding>();
        og_delay = Random.Range(path_update_delay.x, path_update_delay.y + 1);
        path_update__delay_counter_ = og_delay;
        pathfinding_strength = Random.Range(random_speed_.x, random_speed_.y);

        if (tilemap_reference_ == null || pathfinding_reference_ == null)
        {
            Debug.Log("Pathfinding components not found in scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path_update__delay_counter_ > 0.0f)
        {
            path_update__delay_counter_ -= Time.deltaTime;
        }
        else
        {
            GetPathToPlayer();
            path_update__delay_counter_ = og_delay;
        }
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    GetPathToPlayer();
        //}
        if (move_)
        {
            ExecutePath();
        }
        //if (Random.Range(0.0f, 100.0f - awareness_) == 0)
        //{
        //    GetPathToPlayer();
        //}
        //GetPathToPlayer();
        //ExecutePath();
    }

    void GetPathToPlayer()
    {
        Vector3Int player_pos = GameObject.Find("WallTilemap").GetComponent<Grid>().WorldToCell(GameObject.Find("Player").transform.position);
        Vector3Int current_pos = GameObject.Find("WallTilemap").GetComponent<Grid>().WorldToCell(transform.position);
        //current_pos.x -= pathfinding_reference_.grid_offset_.x;
        //current_pos.y -= pathfinding_reference_.grid_offset_.y;
        //player_pos.x -= pathfinding_reference_.grid_offset_.x;
        //player_pos.y -= pathfinding_reference_.grid_offset_.y;
        if (pathfinding_reference_.FindPath((Vector2Int)current_pos, (Vector2Int)player_pos, ref current_path_))
        {
            current_node_num_ = 0;
        }
    }

    void ExecutePath()
    {
        if (current_node_num_ < current_path_.Count)
        {
            if ((NodeToWorld(current_path_[current_node_num_].gridX_, current_path_[current_node_num_].gridY_) + new Vector2(0.5f,0.5f) - (Vector2)transform.position).magnitude < slack_)
            {
                current_node_num_++; 
            }
            else
            {
                Vector2 pathfinding_direction = (NodeToWorld(current_path_[current_node_num_].gridX_, current_path_[current_node_num_].gridY_) + new Vector2(0.5f, 0.5f) - (Vector2)transform.position).normalized;

                //transform.position = (Vector2)transform.position + (NodeToWorld(current_path_[current_node_num_].gridX_, current_path_[current_node_num_].gridY_) - (Vector2)transform.position).normalized * 5.0f * Time.deltaTime;
                gameObject.GetComponent<Rigidbody2D>().AddForce(pathfinding_direction * pathfinding_strength, ForceMode2D.Force);
            }
        }
    }

    Vector2 NodeToWorld(int x, int y)
    {
        Vector2Int offset = pathfinding_reference_.grid_offset_;
        return (Vector2)(GameObject.Find("WallTilemap").GetComponent<Grid>().CellToWorld(new Vector3Int(x + offset.x, y + offset.y, 0)));
    }

    public void SetPathfindingStrength(float strength)
    {
        pathfinding_strength = strength;
    }

    public void SetMove(bool b)
    {
        move_ = b;
    }
}
