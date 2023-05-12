using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingSphere : MonoBehaviour
{
    private Vector3 moveDir;
    Rigidbody rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpPower;

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
