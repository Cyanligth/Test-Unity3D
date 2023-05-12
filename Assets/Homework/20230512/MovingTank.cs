using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingTank : MonoBehaviour
{
    private Vector3 moveDir;
    private Vector3 rotate;
    Rigidbody rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    float h;
    float v;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
    }

    private void OnMove(InputValue value)
    {
        rotate.x = value.Get<Vector2>().x;
        moveDir.z = value.Get<Vector2>().y;
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up, rotate.x * rotateSpeed * Time.deltaTime);
    }

    private void OnJump(InputValue input)
    {
        Jump();
    }

    private void Jump()
    {
        Vector3 dir = transform.TransformDirection(moveDir);
        rb.AddForce(dir * moveSpeed * 3, ForceMode.Impulse);
    }
}
