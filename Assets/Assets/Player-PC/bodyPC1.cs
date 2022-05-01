using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyPC1 : MonoBehaviour
{
    public float x, z, speed;
    private void Update()
    {
        x = Input.GetAxis("Vertical");
        z = Input.GetAxis("Horizontal");

        Vector3 move = transform.position + ((transform.forward * x * speed) + (transform.right * z * speed)).normalized;
        GetComponent<Rigidbody>().MovePosition(move);
    }
}
