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
    [SerializeField] private int maxBullet;
    [SerializeField] private int curBullet;
    [SerializeField] private int reloadCool;
    private bool reloading;

    [SerializeField] private AudioSource engineDrive;
    [SerializeField] private AudioSource engineIdle;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource reloadSound;
    [SerializeField] private AudioSource bgm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bulletCount = 0;
        curBullet = maxBullet;
        reloading = false;
        engineIdle.enabled = true;
        engineDrive.enabled = false;
        fireSound.enabled = false;
        reloadSound.enabled = false;
        bgm.enabled = true;
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
        if(moveDir.z != 0)
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
        if(turret.transform.rotation.y <= 10 && turret.transform.rotation.y >= -35)
            turret.transform.Rotate(Vector3.left, turretRot.y * rotateSpeed * Time.deltaTime);
    }
    private void TurretRotate()
    {
        turret.transform.Rotate(Vector3.up, turretRot.x * rotateSpeed * Time.deltaTime);
    }

    IEnumerator Reload()
    {
        reloading = true;
        Instantiate(reloadSound);
        yield return new WaitForSeconds(reloadCool);
        curBullet = maxBullet;
        Destroy(reloadSound);
        reloading = false;
    }
    private void OnReload(InputValue input)
    {
        if(!reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private int bulletCount;
    private void OnFire(InputValue input)
    {
        if(curBullet <= 0 && !reloading)
        {
            StartCoroutine(Reload());
            return;
        }
        if (bulletCount == 0 && curBullet > 0 && !reloading)
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
        Instantiate(fireSound);
        // Instantiate(explosion, bulletPoint.position, bulletPoint.rotation);
        bulletCount++;
        curBullet--;
    }

    private Coroutine cooldown;
    
    IEnumerator RapidBullet()
    {
        while(true)
        {
            if (curBullet <= 0 && !reloading)
            {
                StartCoroutine(Reload());
                break;
            }
            else if(curBullet <= 0 && reloading)
            {
                break;
            }
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            Instantiate(fireSound);
            curBullet--;
            yield return new WaitForSeconds(bulletColldown);
        }
    }

    private void OnRapidFire(InputValue input)
    {
        if(input.isPressed)
        {
            cooldown = StartCoroutine(RapidBullet());
        }
        else if(curBullet > 0)
        {
            StopCoroutine(cooldown);
        }
    }
}
