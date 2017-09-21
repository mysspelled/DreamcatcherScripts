using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationTest : MonoBehaviour {

    public List<Transform> target;
    private float dist;
    public int i = 0;

    NavMeshAgent agent;
    Transform self;

    

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        self = GetComponent<Transform>();
        agent.SetDestination(target[i].position);
    }
	
	// Update is called once per frame
	void Update () {
        
        

        dist = Vector3.Distance(target[i].position, transform.position);

        if(dist < 2)
        {
            if (i == 6)
            {
                i = -1;
            }
           
            i++;
            agent.SetDestination(target[i].position);
            
        }
        


    }

    
}
