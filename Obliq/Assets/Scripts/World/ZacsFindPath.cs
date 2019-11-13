using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZacsFindPath : MonoBehaviour
{
    Tilemap tilemap_reference_ = null;
    ZacsPathfinding pathfinding_reference_ = null;
    List<Node> current_path_ = new List<Node>();
    float slack_ = 0.5f;
    int current_node_num_ = 0;
    Node current_node_ = null;
    // Start is called before the first frame update
    void Start()
    {
        tilemap_reference_ = GameObject.Find("TestTilemap").GetComponent<Tilemap>();
        pathfinding_reference_ = GameObject.Find("TestPathfinding").GetComponent<ZacsPathfinding>();

        if (tilemap_reference_ == null || pathfinding_reference_ == null)
        {
            Debug.Log("Pathfinding components not found in scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetPathToPlayer();
        }
        ExecutePath();
    }

    void GetPathToPlayer()
    {
        Vector3Int player_pos = GameObject.Find("TestTilemap").GetComponent<Grid>().WorldToCell(GameObject.Find("Player").transform.position);
        Vector3Int current_pos = GameObject.Find("TestTilemap").GetComponent<Grid>().WorldToCell(transform.position);
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
            if ((NodeToWorld(current_path_[current_node_num_].gridX_, current_path_[current_node_num_].gridY_) - (Vector2)transform.position).magnitude < slack_)
            {
                current_node_num_++; 
            }
            else
            {
                transform.position = (Vector2)transform.position + (NodeToWorld(current_path_[current_node_num_].gridX_, current_path_[current_node_num_].gridY_) - (Vector2)transform.position).normalized * 5.0f * Time.deltaTime;
            }
        }
    }

    Vector2 NodeToWorld(int x, int y)
    {
        Vector2Int offset = pathfinding_reference_.grid_offset_;
        return (Vector2)(GameObject.Find("TestTilemap").GetComponent<Grid>().CellToWorld(new Vector3Int(x + offset.x, y + offset.y, 0)));
    }
}
