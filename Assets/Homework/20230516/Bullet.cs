using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

// [RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource explosionSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        explosionSound.enabled = true;
    }

    private void Start()
    {
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(rb);
        explosionSound.Play();
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
