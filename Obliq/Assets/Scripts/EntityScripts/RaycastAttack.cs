﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;

public class RaycastAttack : MonoBehaviour
{
    // Start is called before the first frame update
    LayerMask layerMask;
    LineRenderer line;
    RaycastHit hit;
    [Tooltip("How often line thickens")]
    [SerializeField] public float line_thicken_rate_; // 
    
    public float next_line_thicken_time;// counter
    [Tooltip("how much the line thickens each time. Example line_thicken_rate = 0.1, line_thicken_increment = 0.2, " +
    "line will thicken by 0.2 every 0.1 sec")]
    [SerializeField] public float line_thicken_increment_;

    [Tooltip("How often link contracts")]
    [SerializeField] public float line_contract_rate_;

    public float next_line_contract_time;// counter

    [Tooltip("How much the line contracts by")]
    [SerializeField] public float line_contract_increment_; 

    [Tooltip("Minimum width line will shrink to (should match width set in lineRenderer)")]
    [SerializeField]public float initial_width_; // min width to shrink to 
    
    void Start()
    {
        layerMask = LayerMask.GetMask("Obstacle");
       line = gameObject.GetComponent<LineRenderer>();
      
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RemoveLine(GameObject owner)
    {
        line.SetPosition(0, owner.transform.position);
        line.SetPosition(1, owner.transform.position);
        
    }
    public void Attack(GameObject owner, GameObject target)
    {
        RaycastHit2D isHit = Physics2D.Raycast(owner.transform.position, ((Vector2)target.transform.position - (Vector2)owner.transform.position).normalized,
            ((Vector2)owner.transform.position - (Vector2)target.transform.position).magnitude, layerMask);
        if (isHit.collider != null)
        {
            line.SetPosition(0, owner.transform.position);
            line.SetPosition(1, isHit.point);
            Debug.Log("Obstacle");
        }
        else
        {           
            line.SetPosition(0, owner.transform.position);
            line.SetPosition(1, target.transform.position);
            Debug.Log("tracking");
        }
    }
    public void LineExpand(LineRenderer line, float expand_increment)
    {
        line.startWidth = line.startWidth + expand_increment;
        line.endWidth = line.endWidth + expand_increment;
    }
    public void LineContract(LineRenderer line, float expand_increment)
    {

        line.startWidth = line.startWidth - expand_increment;
        line.endWidth = line.endWidth - expand_increment;
    }
}