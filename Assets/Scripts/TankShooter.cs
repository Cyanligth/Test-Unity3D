using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TankShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletColldown;
    [SerializeField] public int maxBullet;
    [SerializeField] public int curBullet;
    [SerializeField] private int reloadCool;
    private bool reloading;
    public UnityEvent OnFired;
    public UnityEvent OnReloaded;
    private void Awake()
    {
        bulletCount = 0;
        curBullet = maxBullet;
        reloading = false;
    }
    IEnumerator Reload()
    {
        reloading = true;
        OnReloaded?.Invoke();
        yield return new WaitForSeconds(reloadCool);
        curBullet = maxBullet;
        reloading = false;
    }
    private void OnReload(InputValue input)
    {
        if (!reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private int bulletCount;
    private void OnFire(InputValue input)
    {
        if (curBullet <= 0 && !reloading)
        {
            StartCoroutine(Reload());
            return;
        }
        else
        {
            Fire();
        }
    }
    IEnumerator BulletCount()
    {
        yield return new WaitForSeconds(bulletColldown);
        bulletCount--;
    }

    public void Fire()
    {
        if (bulletCount == 0 && curBullet > 0 && !reloading)
        {
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            OnFired?.Invoke();
            GameManager.Data.AddShootCount(1);
            bulletCount++;
            curBullet--;
            StartCoroutine(BulletCount());
        }
    }


    private Coroutine cooldown;

    IEnumerator RapidBullet()
    {
        while (true)
        {
            if (curBullet <= 0 && !reloading)
            {
                StartCoroutine(Reload());
                break;
            }
            else if (curBullet <= 0 && reloading)
            {
                break;
            }
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            OnFired?.Invoke();
            GameManager.Data.AddShootCount(1);
            curBullet--;
            yield return new WaitForSeconds(bulletColldown);
        }
    }

    private void OnRapidFire(InputValue input)
    {
        if (input.isPressed)
        {
            cooldown = StartCoroutine(RapidBullet());
        }
        else if (curBullet > 0)
        {
            StopCoroutine(cooldown);
        }
    }
}
