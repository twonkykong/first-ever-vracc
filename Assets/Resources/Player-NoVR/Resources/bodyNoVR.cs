using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyNoVR : MonoBehaviour
{
    public GameObject joystick, joystickparent;
    bool grounded;

    private void Update()
    {
        //movement
        float x = -(joystickparent.transform.position.y - joystick.transform.position.y) / 100;
        float z = -(joystickparent.transform.position.x - joystick.transform.position.x) / 100;
        Vector3 move = (transform.forward * x + transform.right * z).normalized;

        if (x >= 0.4f || x <= -0.4f || z >= 0.4f || z <= -0.4f) GetComponent<Rigidbody>().AddForce(move, ForceMode.VelocityChange);
        if (z < 0.0001f && z > -0.0001f && x < 0.0001f && x > -0.0001f) GetComponent<Rigidbody>().velocity = new Vector3(0f, GetComponent<Rigidbody>().velocity.y, 0f);

        if (GetComponent<Rigidbody>().velocity.x > 5f) GetComponent<Rigidbody>().velocity = new Vector3(5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        if (GetComponent<Rigidbody>().velocity.z > 5f) GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, 5f);
        if (GetComponent<Rigidbody>().velocity.x < -5f) GetComponent<Rigidbody>().velocity = new Vector3(-5f, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.z);
        if (GetComponent<Rigidbody>().velocity.z < -5f) GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y, -5f);

        //grounded
        if (Physics.Raycast(transform.position, -transform.up, 1.5f)) grounded = true;
        else grounded = false;
    }

    public void jump()
    {
        if (grounded) GetComponent<Rigidbody>().AddForce(transform.up * 6f, ForceMode.Impulse);
    }
}
