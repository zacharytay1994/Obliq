using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingHandler : MonoBehaviour
{
    // temporary inefficient check
    public List<ObliqFlock> flockers_ = new List<ObliqFlock>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNewBoid(ObliqFlock boid)
    {
        flockers_.Add(boid);
    }

    public void RemoveBoid(ObliqFlock boid)
    {
        flockers_.Remove(boid);
    }
}
