using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class ZacsPathfinding : MonoBehaviour
{
    public PathfindingGrid grid_ = new PathfindingGrid();
    List<Node> nodes_to_reset_ = new List<Node>();
    [SerializeField]
    Tilemap tilemap_ = null;
    [HideInInspector]
    public Vector2Int grid_offset_ = new Vector2Int(0, 0);

    private void Start()
    {
        ProcessTiles();
    }

    public bool FindPath(Vector2Int gridstart, Vector2Int gridend, ref List<Node> pathref)
    {
        // create open set
        List<Node> open_set = new List<Node>();

        Node start_node = grid_.GetNode(gridstart.x - grid_offset_.x, gridstart.y - grid_offset_.y);
        Node end_node = grid_.GetNode(gridend.x - grid_offset_.x, gridend.y - grid_offset_.y);

        if ((start_node.gridX_ < 0 || start_node.gridX_ > grid_.grid_size_x) || (start_node.gridY_ < 0 || start_node.gridY_ > grid_.grid_size_y) ||
            (end_node.gridX_ < 0 || end_node.gridX_ > grid_.grid_size_x) || (end_node.gridY_ < 0 || end_node.gridY_ > grid_.grid_size_y))
        {
            // out of bounds
            Debug.Log("Queried coordinates were out of bounds of mapped grid.");
            return false;
        }

        open_set.Add(start_node);
        start_node.inOpen_ = true;
        nodes_to_reset_.Add(start_node);

        while (open_set.Count > 0)
        {
            Node current_node = open_set[GetLowestFNode(open_set)];

            // erase current node from open set
            open_set.Remove(current_node);
            // remove current node from open set
            current_node.inOpen_ = false;
            // add current node to close set by flag
            current_node.inClosed_ = true;

            // if current node equals to end node, we have reached destination
            if (current_node == end_node)
            {
                pathref = RetracePath(start_node, end_node);
                return true;
            }

            // else continue search
            foreach (Node n in grid_.GetNeighbours(current_node))
            {
                // if not traversable or in closed set, skip
                if (!n.isWalkable_ || n.inClosed_)
                {
                    continue;
                }
                // if new g cost < g cost (need updating), or if not in open set, update/calculate f cost, add to open set
                // calculate new g cost of neighbour relative to current node
                int new_g_cost = current_node.gCost_ + GetDistanceBetweenNodes(current_node, n);
                if (new_g_cost < n.gCost_ || !n.inOpen_)
                {
                    n.gCost_ = new_g_cost;
                    n.hCost_ = GetDistanceBetweenNodes(n, end_node);
                    n.parent_ = current_node;
                    if (!n.inOpen_)
                    {
                        open_set.Add(n);
                        n.inOpen_ = true;
                    }
                    nodes_to_reset_.Add(n);
                }
            }
        }
        return false;
    }

    public int GetDistanceBetweenNodes(Node node1, Node node2)
    {
        int distance_x = Math.Abs(node1.gridX_ - node2.gridX_);
        int distance_y = Math.Abs(node1.gridY_ - node2.gridY_);

        if (distance_x > distance_y)
        {
            return distance_y * 14 + (distance_x - distance_y) * 10;
        }
        return distance_x * 14 + (distance_y - distance_x) * 10;
    }

    public int GetLowestFNode(List<Node> list)
    {
        int size = list.Count;
        int lowest = 0;
        for (int i = 0; i < size; i++)
        {
            if (list[lowest].GetFCost() > list[i].GetFCost())
            {
                lowest = i;
            }
        }
        return lowest;
    }

    public List<Node> RetracePath(Node startnode, Node endnode)
    {
        List<Node> path = new List<Node>();
        Node current_node = endnode;

        // trace parent back to find path
        while (startnode != current_node)
        {
            path.Add(current_node);
            current_node = current_node.parent_;
        }

        // reverse path from back to front
        path.Reverse();
        // reset all tempered nodes
        foreach (Node n in nodes_to_reset_)
        {
            n.Reset();
        }
        nodes_to_reset_.Clear();
        return path;
    }

    public void ProcessTiles()
    {
        if (tilemap_ == null)
        {
            Debug.Log("No tilemap attached to this pathfinding object.");
            return;
        }
        // get base tilemap bounds
        tilemap_.CompressBounds();
        BoundsInt bounds = tilemap_.cellBounds;
        int [,] collision_flags_ = new int[bounds.size.x, bounds.size.y];
        grid_offset_ = new Vector2Int(bounds.xMin, bounds.yMin);
        // loop through and init all tiles within bounds
        for (int y = 0, ry = bounds.yMin; ry < bounds.yMax - 1; y++, ry++)
        {
            for (int x = 0, rx = bounds.xMin; rx < bounds.xMax - 1; x++, rx++)
            {
                Vector3Int grid_position = new Vector3Int(rx, ry, 0);

                if (tilemap_.GetTile<Tile>(grid_position) != null)
                {
                    collision_flags_[x, y] = 1;
                }
            }
        }
        grid_.InitGrid(collision_flags_);
    }
}



public class Node
{
    public int gCost_ = 0;
    public int hCost_ = 0;
    public readonly int gridX_;
    public readonly int gridY_;

    public bool isWalkable_;
    public Node parent_;
    public bool inClosed_ = false;
    public bool inOpen_ = false;

    public Node(bool iswalkable, int gridx, int gridy)
    {
        isWalkable_ = iswalkable;
        gridX_ = gridx;
        gridY_ = gridy;
    }

    public int GetFCost()
    {
        return gCost_ + hCost_;
    }

    public void Reset()
    {
        gCost_ = 0;
        hCost_ = 0;
        inClosed_ = false;
        inOpen_ = false;
    }
}

public class PathfindingGrid
{
    Node[,] node_grid_;
    public int grid_size_x;
    public int grid_size_y;

    public PathfindingGrid()
    { }

    public void InitGrid(int[,] flagarray)
    {
        int grid_end_x = flagarray.GetLength(0);
        int grid_end_y = flagarray.GetLength(1);
        node_grid_ = new Node[grid_end_x, grid_end_y];
        grid_size_x = grid_end_x;
        grid_size_y = grid_end_y;

        for (int y = 0; y < grid_end_y; y++)
        {
            for (int x = 0; x < grid_end_x; x++)
            {
                if (flagarray[x, y] == 1)
                {
                    node_grid_[x, y] = new Node(false, x, y);
                }
                else
                {
                    node_grid_[x, y] = new Node(true, x, y);
                }
            }
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        // 3 by 3 grid search
        int node_grid_x;
        int node_grid_y;
        // nw neighbour
        node_grid_x = node.gridX_ + 1;
        node_grid_y = node.gridY_;
        if (node_grid_x >= 0 && node_grid_x < grid_size_x &&
            node_grid_y >= 0 && node_grid_y < grid_size_y)
        {
            neighbours.Add(node_grid_[node_grid_x, node_grid_y]);
        }
        // se neighbour
        node_grid_x = node.gridX_ - 1;
        node_grid_y = node.gridY_;
        if (node_grid_x >= 0 && node_grid_x < grid_size_x &&
            node_grid_y >= 0 && node_grid_y < grid_size_y)
        {
            neighbours.Add(node_grid_[node_grid_x, node_grid_y]);
        }
        // ne neighbour
        node_grid_x = node.gridX_;
        node_grid_y = node.gridY_ + 1;
        if (node_grid_x >= 0 && node_grid_x < grid_size_x &&
            node_grid_y >= 0 && node_grid_y < grid_size_y)
        {
            neighbours.Add(node_grid_[node_grid_x, node_grid_y]);
        }
        // sw neighbour
        node_grid_x = node.gridX_;
        node_grid_y = node.gridY_ - 1;
        if (node_grid_x >= 0 && node_grid_x < grid_size_x &&
            node_grid_y >= 0 && node_grid_y < grid_size_y)
        {
            neighbours.Add(node_grid_[node_grid_x, node_grid_y]);
        }
        return neighbours;
    }

    public Node GetNode(int x, int y)
    {
        Debug.Log(x + "," + y);
        return node_grid_[x, y];
    }
}
