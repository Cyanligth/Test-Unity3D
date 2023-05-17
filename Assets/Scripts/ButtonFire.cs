using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonFire : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletPoint;
    [SerializeField] private float bulletColldown;
    [SerializeField] private int maxBullet;
    [SerializeField] private int curBullet;
    [SerializeField] private int reloadCool;
    [SerializeField] private GameObject tank;
    [SerializeField] private GameObject turret;
    private bool reloading;

    [SerializeField] private AudioSource fireSound;
    [SerializeField] private AudioSource reloadSound;

    private void Awake()
    {
        bulletCount = 0;
        curBullet = maxBullet;
        reloading = false;
        fireSound.enabled = false;
        reloadSound.enabled = false;
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
        if (!reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private int bulletCount;

    IEnumerator BulletCount()
    {
        yield return new WaitForSeconds(bulletColldown);
        bulletCount--;
        fireSound.enabled = false;
    }

    public void Fire()
    {
        if (curBullet <= 0 && !reloading)
        {
            StartCoroutine(Reload());
            return;
        }
        if (bulletCount == 0 && curBullet > 0 && !reloading)
        {
            Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
            fireSound.enabled = true;
            bulletCount++;
            curBullet--;
            StartCoroutine(BulletCount());
        }
    }

}