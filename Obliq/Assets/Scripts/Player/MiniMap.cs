using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]
    float minimap_dimensions_x_ = 50.0f;
    [SerializeField]
    float minimap_dimensions_y_ = 50.0f;
    float scale_x_ = 0.0f;
    float scale_y_ = 0.0f;
    float half_scale_x_ = 0.0f;
    float half_scale_y_ = 0.0f;
    float world_to_map_scale_x_ = 0.0f;
    float world_to_map_scale_y_ = 0.0f;
    List<MapObject> map_objects_ = new List<MapObject>();
    List<MapObject> removal_ = new List<MapObject>();
    Vector2 offset_ = Vector2.zero;
    GameObject player_ = null;
    [SerializeField]
    GameObject map_object_prefab_ = null;
    //Vector2 minimap_offset_ = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        scale_x_ = GetComponent<RectTransform>().localScale.x/2.0f;
        scale_y_ = GetComponent<RectTransform>().localScale.y/2.0f;
        half_scale_x_ = scale_x_ / 2.0f;
        half_scale_y_ = scale_y_ / 2.0f;
        world_to_map_scale_x_ = scale_x_ / minimap_dimensions_x_;
        world_to_map_scale_y_ = scale_y_ / minimap_dimensions_y_;
        player_ = GameObject.Find("Player");
        //minimap_offset_ = GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        // update offset with player
        offset_ = new Vector2(player_.transform.position.x * world_to_map_scale_x_, player_.transform.position.y * world_to_map_scale_y_);
        MapObjects();
    }

    void MapObjects()
    {
        foreach (MapObject m in map_objects_)
        {
            // remove
            if (m.world_object_ == null)
            {
                m.sr_.enabled = false;
                removal_.Add(m);
                continue;
            }
            // get worldspace of map object to minimap space
            Vector2 minimapspace = new Vector2(m.world_object_.transform.position.x * world_to_map_scale_x_,
                m.world_object_.transform.position.y * world_to_map_scale_y_) - offset_;
            // check if within minimap bounds
            if (minimapspace.x < (-half_scale_x_) ||
                minimapspace.x > (half_scale_x_) ||
                minimapspace.y < (-half_scale_y_) ||
                minimapspace.y > (half_scale_y_))
            {
                m.sr_.enabled = false;
            }
            else
            {
                m.sr_.enabled = true;
            }
            m.GetComponent<RectTransform>().localPosition = new Vector3(minimapspace.x, minimapspace.y, 0.0f);
        }
        // remove 
        foreach (MapObject m in removal_)
        {
            map_objects_.Remove(m);
        }
        removal_.Clear();
    }

    public void AddMapObject(GameObject g, Sprite sprite, float scale, Color color)
    {
        GameObject temp;
        if (map_objects_ != null)
        {
            temp = Instantiate(map_object_prefab_, transform);
            temp.GetComponent<MapObject>().Init(g);
            temp.transform.localScale = new Vector3(scale, scale, scale);
            temp.GetComponent<SpriteRenderer>().color = color;
            if (sprite != null)
            {
                temp.GetComponent<SpriteRenderer>().sprite = sprite;
            }
            map_objects_.Add(temp.GetComponent<MapObject>());
        }
    }
}
