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
    private float monsterRotationX;
    private float monsterRotationY;
    private float humanRotationY;

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
        // ��������� ���� �������� �������
        monsterRotationX = monster.transform.rotation.eulerAngles.x; 
        monsterRotationY = monster.transform.rotation.eulerAngles.y;
        // ����� �� ��������� ������ ���������� ���������� � �������� � �������� ��� � �������. ����� ��������� �� ������� � ��� ���������, ����� ������ ������ �� �������
        human.GetComponent<CustomCharacterController>().enabled = false;
        monster.GetComponent<CustomCharacterController>().enabled = true;
        monster.GetComponent<EnemyAI>().enabled = false;
        monster.GetComponent<NavMeshAgent>().isStopped = true;
        CVC.Follow = monster.transform;
        // �������� ������� ������� ������, ����� �� ������� ���� ��, ���� � �� ������������
        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = monsterRotationX;
        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = monsterRotationY;
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
                // �������� ���� �������� ��������
                humanRotationY = human.transform.rotation.eulerAngles.y;
                // ���������� ��� ���������� � ������ �� �����
                human.GetComponent<CustomCharacterController>().enabled = true;
                monster.GetComponent<CustomCharacterController>().enabled = false;
                monster.GetComponent<EnemyAI>().enabled = true;
                monster.GetComponent<NavMeshAgent>().isStopped = false;
                CVC.Follow = human.transform;

                CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0;
                CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = humanRotationY;

                timerText.enabled = false;
            }
        }
    }
}
