using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;
using UnityEngine.UI;

public class CharacterSwitch : MonoBehaviour
{
    private CustomCharacterController human;
    private Monster monster;
    public CinemachineVirtualCamera CVC;
    public float timeStart = 15;
    public Text timerText;
    private bool timerRunning = false;
    private float monsterRotationX;
    private float monsterRotationY;
    private float humanRotationY;

    void Start()
    {
        human = GM.Instance.human;
        monster = GM.Instance.monster;
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
        // ����� �� ��������� ������ ���������� ���������� � ��������. ����� �������� �������, ������� ��������� �� � ��������� � �������, � ������� ������ ���������� ����������. ����� ������ ������ �� �������
        human.enabled = false;
        monster.SwitchTo();
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
                human.enabled = true;
                monster.cc.enabled = false;
                monster.ai.enabled = true;
                monster.agent.isStopped = false;
                CVC.Follow = human.transform;

                CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = 0;
                CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = humanRotationY;

                timerText.enabled = false;
            }
        }
    }
}
