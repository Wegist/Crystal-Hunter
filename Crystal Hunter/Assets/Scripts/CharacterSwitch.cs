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
        // Отображение секунд таймера в виде текста
        timerText.text = timeStart.ToString();
    }

    void OnMouseDown()
    {
        // Тап по кристаллу запускает переключение между персонажами
        Debug.Log("Подняли кристалл");
        // Считываем угол поворота монстра
        monsterRotationX = monster.transform.rotation.eulerAngles.x; 
        monsterRotationY = monster.transform.rotation.eulerAngles.y;
        // Здесь мы выключаем скрипт управления персонажем у человека и включаем его у монстра. Также выключаем ИИ монстра и его навигацию, затем вешаем камеру на монстра
        human.GetComponent<CustomCharacterController>().enabled = false;
        monster.GetComponent<CustomCharacterController>().enabled = true;
        monster.GetComponent<EnemyAI>().enabled = false;
        monster.GetComponent<NavMeshAgent>().isStopped = true;
        CVC.Follow = monster.transform;
        // Передаем поворот монстра камере, чтобы он смотрел туда же, куда и до переключения
        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Value = monsterRotationX;
        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value = monsterRotationY;
        // Запускаем таймер
        timerRunning = true;
        timerText.enabled = true;
        // Скрываем кристалл
        this.GetComponent<Renderer>().enabled = false;
    }

    void Update()
    {
        // Проверяем, запущен ли таймер
        if (timerRunning == true)
        {
            // Если время не истекло, то вычисляем оставшееся время
            if (timeStart > 0)
            {
                // Отнимаем одну единицу в секунду и выводим в текст
                timeStart -= Time.deltaTime;
                timerText.text = Mathf.Round(timeStart).ToString();
            }
            else
            {
                // Если время истекло, то останавливаем таймер
                timerRunning = false;
                // Получаем угол поворота человека
                humanRotationY = human.transform.rotation.eulerAngles.y;
                // Возвращаем все компоненты и камеру на место
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
