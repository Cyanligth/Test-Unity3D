using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private Vector3 moveDir;
    Rigidbody rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float rotateSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        rb.AddForce(moveDir * moveSpeed);
        // transform.position += moveDir * Time.deltaTime;  // ���� ��ġ�� ���� �����
        // transform.Translate(moveDir * moveSpeed * Time.deltaTime);   // ����� ������, �ٶ󺸰� �ִ� ���� ����
        // transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);  // ���� ����
        // transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);   // �� ����
    }

    public void Rotate()
    {
        transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime);
        // transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime, Space.Self);  // �� y�� ����
        // transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime, Space.World); // ������ y�� ����
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;
    }
    private void OnJump(InputValue value)
    {
        Jump();
        Debug.Log("jump");
    }
}
