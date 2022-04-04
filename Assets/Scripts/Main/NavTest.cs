using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavTest : MonoBehaviour {

    private NavMeshAgent agent;
    public Transform target;
	void Start () {
        agent = this.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (agent!=null)
        {
            agent.SetDestination(target.position);
        }
	}
}
