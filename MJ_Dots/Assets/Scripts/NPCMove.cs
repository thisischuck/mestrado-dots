using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMove : MonoBehaviour
{
    public Transform target;
    NavMeshAgent agent;

    float timer = 0;
    float coldown = 1;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        if (agent == null)
        {

        }else{
            SetDestination();
        }
    }

    void SetDestination(){
        if (target.position != Vector3.zero)
        {
            Vector3 targetVector = target.position;
            agent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer +=Time.deltaTime;
        if (timer >= coldown)
        {
            timer = 0;
            SetDestination();
        }
    }
}
