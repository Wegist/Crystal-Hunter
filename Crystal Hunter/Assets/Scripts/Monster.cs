using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public CustomCharacterController cc;
    public NavMeshAgent agent;
    public EnemyAI ai;

    public void SwitchTo()
    {
        cc.enabled = true;
        ai.enabled = false;
        agent.isStopped = true;
    }
}
