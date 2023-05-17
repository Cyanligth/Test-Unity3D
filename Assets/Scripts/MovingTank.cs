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
    [SerializeField] private int maxBullet;
    [SerializeField] private int curBullet;
    [SerializeField] private int reloadCool;
    private bool reloading;

    [SerializeField] private AudioSource engineDrive;
    [SerializeField] private AudioSource engineIdle;
    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource reloadSound;

    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        bulletCount = 0;
        curBullet = maxBullet;
        reloading = false;
        engineIdle.enabled = true;
        engineDrive.enabled = false;
        fireSound.enabled = false;
        reloadSound.enabled = false;
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
        if(moveDir.z != 0 || rotate.x != 0)
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
        reloadSound.enabled = true;
        yield return new WaitForSeconds(reloadCool);
        curBullet = maxBullet;
        reloadSound.enabled = false;
        reloading = false;
    }
    private void OnReload(InputValue input)
    {
        if(!reloading)
        {
            StartCoroutine(Reload());
        }
    }
    public void Shot()
    {
        Fire();
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
        }
    }
    IEnumerator BulletCount()
    {
        yield return new WaitForSeconds(bulletColldown);
        bulletCount--;
        fireSound.enabled = false;
    }

    public void Fire()
    {
        if (bulletCount == 0 && curBullet > 0 && !reloading)
        {
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            fireSound.enabled = true;
            animator.SetTrigger("Fire");
            bulletCount++;
            curBullet--;
            StartCoroutine(BulletCount());
        }
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
            fireSound.enabled = true;
            animator.SetTrigger("Fire");
            curBullet--;
            yield return new WaitForSeconds(bulletColldown);
            fireSound.enabled = false;
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
