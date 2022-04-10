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
    private float timeLeft;
    public Text timerText;
    public static bool timerRunning = false;
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
    // ��� �� ��������� ��������� ������������ ����� �����������
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            timeLeft = timeStart;
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
        }
    }

    void Update()
    {
        // ���������, ������� �� ������
        if (timerRunning == true)
        {
            // ���� ����� �� �������, �� ��������� ���������� �����
            if (timeLeft > 0)
            {
                // �������� ���� ������� � ������� � ������� � �����
                timeLeft -= Time.deltaTime;
                timerText.text = Mathf.Round(timeLeft).ToString();
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
