using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCharacterController : MonoBehaviour
{
    public Rigidbody rig;
    public Transform mainCamera;
    public float walkingSpeed = 2f;
    public float currentSpeed;
    public FixedJoystick fixedJoystick;
    public float horizontal;
    public float vertical;
    public float lerpMultiplier = 7f;
    public Slider staminaSlider;
    public float staminaValue;
    public float staminaMinValue;
    public float staminaMaxValue;
    public float staminaReturn;
    private float speed;
    public float runningSpeed;
    private bool runButtonClicked = false;
    public Button runButton;
    public static bool timerRunning;

    void Start()
    {
        timerRunning = CharacterSwitch.timerRunning;
        staminaSlider.gameObject.SetActive(false);
    }

    void Walk()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
        staminaValue += staminaReturn * Time.deltaTime;
    }

    void Run()
    {
        if (staminaValue > staminaMinValue)
        {
            staminaSlider.gameObject.SetActive(true);
            runButton.enabled = false;
            runButton.image.color = new Color(255f, 255f, 255f, .5f);
            currentSpeed = runningSpeed;
            staminaValue -= staminaReturn * Time.deltaTime * 2;
        }
        else
        {
            runButtonClicked = false;
        }
    }

    public void RunButtonClicked()
    {
        if (staminaValue == staminaMaxValue) runButtonClicked = !runButtonClicked;
    }

    void Stamina()
    {
        if (staminaValue > staminaMaxValue)
        {
            staminaValue = staminaMaxValue;
            staminaSlider.gameObject.SetActive(false);
            runButton.enabled = true;
            runButton.image.color = new Color(255f, 255f, 255f);
        }
        staminaSlider.value = staminaValue;
    }

    private void Update()
    {
        horizontal = Mathf.Lerp(horizontal, fixedJoystick.Horizontal, Time.deltaTime * lerpMultiplier);
        vertical = Mathf.Lerp(vertical, fixedJoystick.Vertical, Time.deltaTime * lerpMultiplier);
        // Устанавливаем поворот персонажа когда камера поворачивается 
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mainCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        if (runButtonClicked)
        {
            Run();
        }
        else
        {
            Walk();
        }

        Stamina();
    }

    void FixedUpdate()
    {
        // Здесь мы задаем движение персонажа в зависимости от направления, в которое смотрит камера
        // Сохраняем направление вперед и вправо от камеры 
        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;
        // Делаем так, чтобы направления вперед и вправо не зависили от того, смотрит ли камера вверх или вниз, иначе, когда мы смотрим вперед, персонаж будет идти быстрее, чем когда смотрим вверх или вниз
        camF.y = 0;
        camR.y = 0;
        Vector3 movingVector;
        // Тут мы умножаем смещение джойстика по вертикали на направление камеры вперед и прибавляем к смещению по горизонтали, затем умножаем на направление камеры вправо
        movingVector = Vector3.ClampMagnitude(camF.normalized * vertical * currentSpeed + camR.normalized * horizontal * currentSpeed, currentSpeed);
        // Здесь мы двигаем персонажа! Устанавливаем движение только по x & z
        rig.velocity = new Vector3(movingVector.x, rig.velocity.y, movingVector.z);
        // Чтобы персонаж не крутился на месте
        rig.angularVelocity = Vector3.zero;
    }
}