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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = new Vector3(0,0,0);
        Instantiate(explosion, gameObject.transform);
        Destroy(gameObject, 0.5f);
    }
}
