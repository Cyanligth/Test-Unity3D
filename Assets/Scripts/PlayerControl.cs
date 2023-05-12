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
        // transform.position += moveDir * Time.deltaTime;  // 너의 위치는 이제 여기야
        // transform.Translate(moveDir * moveSpeed * Time.deltaTime);   // 여기로 움직여, 바라보고 있는 방향 기준
        // transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.World);  // 세계 기준
        // transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);   // 나 기준
    }

    public void Rotate()
    {
        transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime);
        // transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime, Space.Self);  // 내 y축 기준
        // transform.Rotate(Vector3.up, moveDir.x * rotateSpeed * Time.deltaTime, Space.World); // 월드의 y축 기준
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
