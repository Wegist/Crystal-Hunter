using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour
{
    private GameObject human;
    private GameObject monster;
    public CinemachineVirtualCamera CVC;
    public float timeStart = 15;
    public Text timerText;
    private bool timerRunning = false;

    void Start()
    {
        human = GameObject.FindGameObjectWithTag("Human");
        monster = GameObject.FindGameObjectWithTag("Monster");
        // ����������� ������ ������� � ���� ������
        timerText.text = timeStart.ToString();
    }

    void OnMouseDown()
    {
        // ��� �� ��������� ��������� ������������ ����� �����������
        Debug.Log("������� ��������");
        // ����� �� ��������� ������ ���������� ���������� � �������� � �������� ��� � �������. ����� ��������� �� ������� � ��� ���������, ����� ������ ������ �� �������
        human.GetComponent<CustomCharacterController>().enabled = false;
        monster.GetComponent<CustomCharacterController>().enabled = true;
        monster.GetComponent<EnemyAI>().enabled = false;
        monster.GetComponent<NavMeshAgent>().isStopped = true;
        CVC.Follow = monster.transform;
        // ��������� ������
        timerRunning = true;
        timerText.enabled = true;
        // �������� ��������
        this.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        // ���������, ������� �� ������
        if (timerRunning == true)
        {
            // ���� ����� �� �������, �� ��������� ���������� �����
            if (timeStart > 0)
            {
                // �������� ���� ������� � ������� � ������� � �����
                timeStart -= Time.deltaTime;
                timerText.text = Mathf.Round(timeStart).ToString();
            }
            else
            {
                // ���� ����� �������, �� ������������� ������
                timerRunning = false;
                // ���������� ��� ������� � ������ �� �����
                human.GetComponent<CustomCharacterController>().enabled = true;
                monster.GetComponent<CustomCharacterController>().enabled = false;
                monster.GetComponent<EnemyAI>().enabled = true;
                monster.GetComponent<NavMeshAgent>().isStopped = false;
                CVC.Follow = human.transform;
 
                timerText.enabled = false;
            }
        }
    }
}
