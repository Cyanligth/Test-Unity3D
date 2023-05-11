using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBounce : MonoBehaviour
{
    public Rigidbody rb;
    public int jumpPower;
    // Start is called before the first frame update
    public void Start()
    {
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

}
