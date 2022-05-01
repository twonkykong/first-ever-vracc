using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyPC : MonoBehaviour
{
    public float joystickVert, joystickHor;
    bool grounded;
    Vector3 move;

    private void Update()
    {
        //movement
        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal");
        Vector3 move = (transform.forward * x + transform.right * z).normalized;

        GetComponent<Rigidbody>().AddForce(move, ForceMode.VelocityChange);
        if (z < 0.0001f && z > -0.0001f && x < 0.0001f && x > -0.0001f) GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f);

        if (GetComponent<Rigidbody>().velocity.x > 5f) GetComponent<Rigidbody>().velocity = new Vector3(5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        if (GetComponent<Rigidbody>().velocity.z > 5f) GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 5f);
        if (GetComponent<Rigidbody>().velocity.x < -5f) GetComponent<Rigidbody>().velocity = new Vector3(-5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        if (GetComponent<Rigidbody>().velocity.z < -5f) GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, -5f);

        //grounded
        if (Physics.Raycast(transform.position, -transform.up, 1.5f)) grounded = true;
        else grounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));
        if (other.tag == "kill")
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            transform.position = pos;
        }
    }
}
