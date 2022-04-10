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
        // Отображение секунд таймера в виде текста
        timerText.text = timeStart.ToString();
    }
    // Тап по кристаллу запускает переключение между персонажами
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crystal"))
        {
            timeLeft = timeStart;
            // Считываем угол поворота монстра
            monsterRotationX = monster.transform.rotation.eulerAngles.x;
            monsterRotationY = monster.transform.rotation.eulerAngles.y;
            // Здесь мы выключаем скрипт управления персонажем у человека. Далее вызываем функцию, которая выключает ии и навигацию у монстра, и влючает скрипт управления персонажем. Затем вешаем камеру на монстра
            human.enabled = false;
            monster.SwitchTo();
            CVC.Follow = monster.transform;
            // Передаем поворот монстра камере, чтобы он смотрел туда же, куда и до переключения
            CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = monsterRotationX;
            CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = monsterRotationY;
            // Запускаем таймер
            timerRunning = true;
            timerText.enabled = true;
        }
    }

    void Update()
    {
        // Проверяем, запущен ли таймер
        if (timerRunning == true)
        {
            // Если время не истекло, то вычисляем оставшееся время
            if (timeLeft > 0)
            {
                // Отнимаем одну единицу в секунду и выводим в текст
                timeLeft -= Time.deltaTime;
                timerText.text = Mathf.Round(timeLeft).ToString();
            }
            else
            {
                // Если время истекло, то останавливаем таймер
                timerRunning = false;
                // Получаем угол поворота человека
                humanRotationY = human.transform.rotation.eulerAngles.y;
                // Возвращаем все компоненты и камеру на место
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
