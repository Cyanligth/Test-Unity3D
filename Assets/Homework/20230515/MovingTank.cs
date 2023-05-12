using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingTank : MonoBehaviour
{
    private Vector3 moveDir;
    private Vector3 rotate;
    private Vector3 turretRot;
    Rigidbody rb;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotateSpeed;
    float h;
    float v;
    [SerializeField] private GameObject turret;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletColldown;
    [SerializeField] private GameObject explosion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bulletCount = 0;
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
        if(turret.transform.rotation.y <= 10 && turret.transform.rotation.y >= -35)
            turret.transform.Rotate(Vector3.left, turretRot.y * rotateSpeed * Time.deltaTime);
    }
    private void TurretRotate()
    {
        turret.transform.Rotate(Vector3.up, turretRot.x * rotateSpeed * Time.deltaTime);
    }


    private int bulletCount;
    private void OnFire(InputValue input)
    {
        if (bulletCount == 0)
        {
            Fire();
            StartCoroutine(BulletCount());
        }
    }
    IEnumerator BulletCount()
    {
        yield return new WaitForSeconds(bulletColldown);
        bulletCount--;
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
        // Instantiate(explosion, bulletPoint.position, bulletPoint.rotation);
        bulletCount++;
    }

    private Coroutine cooldown;
    
    IEnumerator BullteCooldown()
    {
        while(true)
        {
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            yield return new WaitForSeconds(bulletColldown);
        }
    }

    private void OnRapidFire(InputValue input)
    {
        if(input.isPressed)
        {
            cooldown = StartCoroutine(BullteCooldown());
        }
        else
        {
            StopCoroutine(cooldown);
        }
    }
}
