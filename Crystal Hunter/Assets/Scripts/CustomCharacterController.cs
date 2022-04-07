using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {

    }

    void Walk()
    {
        currentSpeed = Mathf.Lerp(currentSpeed, walkingSpeed, Time.deltaTime * 3);
    }

    private void Update()
    {
        horizontal = Mathf.Lerp(horizontal, fixedJoystick.Horizontal, Time.deltaTime * lerpMultiplier);
        vertical = Mathf.Lerp(vertical, fixedJoystick.Vertical, Time.deltaTime * lerpMultiplier);
        // ������������� ������� ��������� ����� ������ �������������� 
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, mainCamera.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        Walk();
    }

    void FixedUpdate()
    {
        // ����� �� ������ �������� ��������� � ����������� �� �����������, � ������� ������� ������
        // ��������� ����������� ������ � ������ �� ������ 
        Vector3 camF = mainCamera.forward;
        Vector3 camR = mainCamera.right;
        // ������ ���, ����� ����������� ������ � ������ �� �������� �� ����, ������� �� ������ ����� ��� ����, �����, ����� �� ������� ������, �������� ����� ���� �������, ��� ����� ������� ����� ��� ����
        camF.y = 0;
        camR.y = 0;
        Vector3 movingVector;
        // ��� �� �������� �������� ��������� �� ��������� �� ����������� ������ ������ � ���������� � �������� �� �����������, ����� �������� �� ����������� ������ ������
        movingVector = Vector3.ClampMagnitude(camF.normalized * vertical * currentSpeed + camR.normalized * horizontal * currentSpeed, currentSpeed);
        // ����� �� ������� ���������! ������������� �������� ������ �� x & z
        rig.velocity = new Vector3(movingVector.x, rig.velocity.y, movingVector.z);
        // ����� �������� �� �������� �� �����
        rig.angularVelocity = Vector3.zero;
    }
}