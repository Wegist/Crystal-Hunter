using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControllerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool pressed = false;
    public bool isMobile = true;
    public float sensitivity;
    public CinemachineVirtualCamera CVC;
    private int fingerId;

    private void Start()
    {
        if (isMobile)
        // ¬ мобильной версии мы не хотим, чтобы мышь могла управл€ть камерой, поэтому делаем так, чтобы Mouse Y и Mouse X не учитывались
        {
            CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sensitivity;
            CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sensitivity;
            CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
            CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
        }
        // Ќа св€кий случай есть возможность переключитьс€ на управление мышью
        else
        {
            CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
            CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
     // ѕровер€ем, нажали ли мы на панель
        if (eventData.pointerCurrentRaycast.gameObject == gameObject)
        {
            pressed = true;
            fingerId = eventData.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
     // огда мы отпускаем палец, делаем так, чтобы камера не двигалась
        pressed = false;
        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = 0;
        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = 0;
    }

    void Update()
    {
        if (pressed)
        {
            // ≈сли панель нажата, то проходимс€ по всем нажати€м на экран
            foreach (Touch touch in Input.touches)
            {
                // ѕровер€ем, насколько помен€лась позици€ пальца по оси "X" и "Y"
                if (touch.fingerId == fingerId)
                {
                    // ≈сли позици€ пальца изменилась, то переносим ее в Input Axis Value нашей камеры
                    if (touch.phase == TouchPhase.Moved)
                    {
                        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = touch.deltaPosition.y;
                        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = touch.deltaPosition.x;
                    }
                    // ≈сли палец не сдвинулс€, то делаем так, чтобы камера сто€ла на месте
                    if (touch.phase == TouchPhase.Stationary)
                    {
                        CVC.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = 0;
                        CVC.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = 0;
                    }
                }
            }
        }
    }
}
