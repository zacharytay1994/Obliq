using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GF;

public class RaycastAttack : MonoBehaviour
{
    // Start is called before the first frame update
    int layerMask;
    LineRenderer line;
    RaycastHit hit;

    [SerializeField] public float line_thicken_rate_; // how often link thicklens
    public float next_line_thicken_time;// counter
    [SerializeField] public float line_thicken_increment_; //how much line thickens

    [SerializeField] public float line_contract_rate_;
    public float next_line_contract_time;// counter
    [SerializeField] public float line_contract_increment_; //how much line thickens

    [SerializeField]public float initial_width_; // min width to shrink to 
    void Start()
    {
       layerMask = 1 << 8;
       line = gameObject.GetComponent<LineRenderer>();
      
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack(GameObject owner, GameObject target)
    {
        if(Physics.Raycast(owner.transform.position, (owner.transform.position - target.transform.position).normalized, out hit, Mathf.Infinity, layerMask))
        {
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
