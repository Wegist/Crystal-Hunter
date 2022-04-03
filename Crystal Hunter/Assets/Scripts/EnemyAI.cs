using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private Transform Player;
    private UnityEngine.AI.NavMeshAgent NMA;
    public float speed;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        NMA = (UnityEngine.AI.NavMeshAgent)this.GetComponent("NavMeshAgent");
        NMA.speed = speed;
    }

    void Update()
    {
        NMA.SetDestination(Player.position);
    }

    void OnTriggerEnter(Collider Player)
    {
        Debug.Log("Game over");
    }
}
