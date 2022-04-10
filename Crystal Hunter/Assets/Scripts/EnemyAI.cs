using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    private Transform human;
    private UnityEngine.AI.NavMeshAgent NMA;
    public float speed;

    void Start()
    {
        human = GM.Instance.human.transform;
        NMA = GetComponent<NavMeshAgent>();
        NMA.speed = speed;
    }

    void Update()
    {
        NMA.SetDestination(human.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (CharacterSwitch.timerRunning == false)
        {
            if (other.CompareTag("Human"))
            {
                Debug.Log("Game over");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
