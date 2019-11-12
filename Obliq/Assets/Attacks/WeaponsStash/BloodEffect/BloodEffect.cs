using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    [SerializeField]
    GameObject blood_ = null;
    [SerializeField]
    int intensity_ = 0;
    [SerializeField]
    Vector2 splatter_upper_lower = new Vector2(0.0f, 0.0f);
    [SerializeField]
    float angle_range_ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        angle_range_ = angle_range_ * (3.142f / 180.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawBlood(Vector2 direction)
    {
        for (int i = 0; i < intensity_; i ++)
        {
            // get random angle
            Vector2 new_vector = Random.Range(0,2) == 0 ? GF.RotateVector(direction, Random.Range(0.0f, angle_range_)).normalized :
                GF.RotateVector(direction, Random.Range(0.0f, -angle_range_)).normalized;
            if (blood_ != null)
            {
                GameObject temp = Instantiate(blood_, transform.position, Quaternion.identity);
                temp.GetComponent<Rigidbody2D>().AddForce(new_vector * Random.Range(splatter_upper_lower.x, splatter_upper_lower.y), ForceMode2D.Impulse);
            }
        }
    }
}
