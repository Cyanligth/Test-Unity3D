using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TankMover : MonoBehaviour
{
    private Vector3 moveDir;
    private Vector3 rotate;
    private Vector3 turretRot;
    Rigidbody rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    float h;
    float v;
    [SerializeField] private GameObject turret;
    [SerializeField] private AudioSource engineDrive;
    [SerializeField] private AudioSource engineIdle;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        engineIdle.enabled = true;
        engineDrive.enabled = false;
    }
    private void Update()
    {
        Move();
        Rotate();
        TurretUpDown();
        TurretRotate();
    }

    private void Move()
    {
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
        if (moveDir.z != 0 || rotate.x != 0)
        {
            engineIdle.enabled = false;
            engineDrive.enabled = true;
        }
        else
        {
            engineIdle.enabled = true;
            engineDrive.enabled = false;
        }
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
        Dash();
    }

    private void Dash()
    {
        Vector3 dir = transform.TransformDirection(moveDir);
        rb.AddForce(dir * moveSpeed * 3, ForceMode.Impulse);
    }

    private void OnTurretMove(InputValue value)
    {
        turretRot.y = value.Get<Vector2>().y;
        turretRot.x = value.Get<Vector2>().x;
    }
    private void TurretUpDown()
    {
        if (turret.transform.rotation.y <= 10 && turret.transform.rotation.y >= -35)
            turret.transform.Rotate(Vector3.left, turretRot.y * rotateSpeed * Time.deltaTime);
    }
    private void TurretRotate()
    {
        turret.transform.Rotate(Vector3.up, turretRot.x * rotateSpeed * Time.deltaTime);
    }
}
