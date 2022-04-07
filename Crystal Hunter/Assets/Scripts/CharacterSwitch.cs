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
        // Отображение секунд таймера в виде текста
        timerText.text = timeStart.ToString();
    }

    void OnMouseDown()
    {
        // Тап по кристаллу запускает переключение между персонажами
        Debug.Log("Подняли кристалл");
        // Здесь мы выключаем скрипт управления персонажем у человека и включаем его у монстра. Также выключаем ИИ монстра и его навигацию, затем вешаем камеру на монстра
        human.GetComponent<CustomCharacterController>().enabled = false;
        monster.GetComponent<CustomCharacterController>().enabled = true;
        monster.GetComponent<EnemyAI>().enabled = false;
        monster.GetComponent<NavMeshAgent>().isStopped = true;
        CVC.Follow = monster.transform;
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
                // Возвращаем все скрипты и камеру на место
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
