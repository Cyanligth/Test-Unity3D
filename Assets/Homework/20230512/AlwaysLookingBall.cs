using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysLookingBall : MonoBehaviour
{
    Rigidbody rb;
    GameObject look;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        look = GameObject.Find("Sphere");
    }

    void Update()
    {
        transform.LookAt(look.transform.position);
    }
}
