using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Move : MonoBehaviour
{
    float h;
    float v;
    public float moveSpeed;
    public float flyPower;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v); // new Vector3(h, 0, v)�� ���� ���̰� �Ǿ����Ƿ� dir�̶�� ������ �ְ� ���� ���ϰ� ����� �� �ְ� ��
        dir = transform.TransformDirection(dir);
        Rigidbody rb = GetComponent<Rigidbody>();
        if (Input.GetKey(KeyCode.Space))
            rb.AddForce(Vector3.up * flyPower);

        rb.AddForce(dir *moveSpeed);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(new Vector3(0, -1 * rotateSpeed * Time.deltaTime, 0));

    }
}
